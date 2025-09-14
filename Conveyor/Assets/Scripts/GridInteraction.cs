﻿using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// Обрабатывает взаимодействие игрока с сеткой, например, размещение объектов.
/// Взаимодействует с GridGenerator для получения информации о ячейках.
/// Логика обновления конвейеров была переработана для использования DirectionFlags и цикла,
/// что сделало код чище и надежнее.
/// </summary>
public class GridInteraction : MonoBehaviour
{
    [Header("Настройки размещения")]
    [Tooltip("Префаб объекта, который будет размещаться на ячейке")]
    public GameObject conveyorPrefab;
    [Tooltip("Смещение по оси Z для размещаемых объектов, чтобы они были видны над сеткой.")]
    [SerializeField] private float placedObjectZOffset = -0.1f;

    [Header("Настройки логики")]
    [Tooltip("Если включено, запрещает изменять соседние конвейеры, если они уже были изменены ранее другим соседом.")]
    public bool lockModifiedNeighbors;

    [Header("Ссылки")]
    [Tooltip("Ссылка на основную камеру. Если не указать, будет найдена автоматически")]
    public Camera mainCamera;
    private GridGenerator _gridGenerator;

    private void Start()
    {
        if (!mainCamera)
        {
            mainCamera = Camera.main;
            if (!mainCamera)
            {
                Debug.LogError("Камера не найдена! Убедитесь, что у вас есть камера с тегом 'MainCamera' или назначьте ее вручную в инспекторе.");
                enabled = false;
                return;
            }
        }

        if (!conveyorPrefab)
        {
            Debug.LogError("Префаб для размещения (conveyorPrefab) не назначен в инспекторе!");
            enabled = false;
            return;
        }

        if (conveyorPrefab.GetComponent<Conveyor>() == null)
        {
            Debug.LogError("На префабе 'conveyorPrefab' отсутствует компонент 'Conveyor'! Добавьте его.");
            enabled = false;
            return;
        }

        _gridGenerator = GridGenerator.Instance;
        if (_gridGenerator) return;
        Debug.LogError("Экземпляр GridGenerator не найден на сцене!");
        enabled = false;
    }

    private void Update()
    {
        if (Mouse.current != null && Mouse.current.leftButton.wasPressedThisFrame)
        {
            HandleMouseClick();
        }
    }

    /// <summary>
    /// Обрабатывает клик мыши, определяет ячейку и размещает на ней объект, если она свободна.
    /// </summary>
    private void HandleMouseClick()
    {
        var mouseScreenPos = Mouse.current.position.ReadValue();
        var mouseWorldPos = mainCamera.ScreenToWorldPoint(mouseScreenPos);
        var gridPosition = _gridGenerator.WorldToGridPosition(mouseWorldPos);
        var targetCell = _gridGenerator.GetCell(gridPosition);
        if (targetCell == null)
        {
            return;
        }

        if (targetCell.IsOccupied)
        {
            Debug.Log($"Ячейка {targetCell.GridPosition} уже занята.");
            return;
        }

        PlaceObjectInCell(targetCell);
    }

    /// <summary>
    /// Создает, размещает объект в ячейке и обновляет ориентацию
    /// нового конвейера и его соседей для автоматического соединения.
    /// </summary>
    /// <param name="cell">Целевая ячейка для размещения.</param>
    private void PlaceObjectInCell(Cell cell)
    {
        var spawnPosition = cell.WorldPosition;
        spawnPosition.z = placedObjectZOffset;
        var placedObject = Instantiate(
            conveyorPrefab,
            spawnPosition,
            Quaternion.identity,
            _gridGenerator.placedObjectsContainer
        );
        cell.SetPlacedObject(placedObject);
        Debug.Log($"Размещен конвейер на {cell.GridPosition}. Запускается обновление соседей.");
        UpdateSurroundingConveyors(cell);
    }

    /// <summary>
    /// Обновляет ориентацию конвейера в указанной ячейке и всех ее соседних конвейеров.
    /// Это гарантирует, что все конвейеры в области правильно соединены.
    /// Включает новую логику блокировки изменений для соседей.
    /// </summary>
    /// <param name="centerCell">Ячейка, вокруг которой нужно произвести обновление.</param>
    private void UpdateSurroundingConveyors(Cell centerCell)
    {
        UpdateSingleConveyorOrientation(centerCell, true);

        foreach (var neighborCell in _gridGenerator.GetNeighbors(centerCell).Where(nc => nc.IsOccupied))
        {
            var neighborConveyor = neighborCell.GetConveyor();
            if (!neighborConveyor) continue;

            if (lockModifiedNeighbors)
            {
                if (neighborConveyor.WasModifiedByNeighbor)
                {
                    Debug.Log($"Обновление конвейера в {neighborCell.GridPosition} пропущено, так как он заблокирован.");
                    continue;
                }

                UpdateSingleConveyorOrientation(neighborCell, false);
                neighborConveyor.MarkAsModifiedByNeighbor();
                Debug.Log($"Конвейер в {neighborCell.GridPosition} обновлен и теперь помечен как измененный соседом.");
            }
            else
            {
                UpdateSingleConveyorOrientation(neighborCell, false);
            }
        }
    }

    /// <summary>
    /// Вычисляет битовую маску соединений для конвейера в указанной ячейке
    /// и передает ее в компонент Conveyor для обновления его внешнего вида.
    /// </summary>
    /// <param name="cell">Ячейка с конвейером для обновления.</param>
    /// <param name="isNewlyPlaced">Указывает, является ли этот конвейер только что размещенным.</param>
    private void UpdateSingleConveyorOrientation(Cell cell, bool isNewlyPlaced)
    {
        var conveyor = cell.GetConveyor();
        if (!conveyor) return;
        var gridPos = cell.GridPosition;
        var connectionMask = DirectionFlags.None;
        foreach (var (flag, offset) in ConveyorDirections.DirectionVectors)
        {
            var neighborCell = _gridGenerator.GetCell(gridPos + offset);
            if (neighborCell?.IsOccupied != true) continue;
            var neighborConveyor = neighborCell.GetConveyor();
            if (!neighborConveyor) continue;
            if (lockModifiedNeighbors && neighborConveyor.WasModifiedByNeighbor)
            {
                if (isNewlyPlaced)
                {
                    continue;
                }

                var oppositeDirection = ConveyorDirections.GetOppositeDirection(flag);
                if ((neighborConveyor.ConnectionMask & (int)oppositeDirection) == 0)
                {
                    continue;
                }
            }

            connectionMask |= flag;
        }

        conveyor.UpdateState((int)connectionMask);
    }
}