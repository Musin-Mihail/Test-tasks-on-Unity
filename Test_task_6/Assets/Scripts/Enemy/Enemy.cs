using System.Collections;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private int _health;
    private int _maxHealth;
    private Transform _player;
    public RectTransform healthBar;
    public RectTransform convas;

    public Enemy()
    {
        _health = 20;
        _maxHealth = 20;
    }

    public void SetPlayer(Transform player)
    {
        _player = player;
    }

    void OnEnable()
    {
        StartCoroutine(Move());
    }

    private IEnumerator Hit(int damage)
    {
        _health -= damage;
        float width = (float)_health / _maxHealth * 1.5f;
        float startWidth = healthBar.sizeDelta.x;
        while (healthBar.sizeDelta.x > width)
        {
            if (healthBar.sizeDelta.x <= 0)
            {
                break;
            }

            yield return new WaitForSeconds(0.01f);
            startWidth -= 0.1f;
            if ( _health > 0 && startWidth <= 0.1f)
            {
                startWidth = 0.1f;
            }
            healthBar.sizeDelta = new Vector2(startWidth, 0.3f);
        }

        if (_health <= 0)
        {
            healthBar.sizeDelta = new Vector2(1.5f, 0.3f);
            gameObject.SetActive(false);
            EventManager.DeadEnemy?.Invoke(transform.position);
            _maxHealth += 5;
            _health = _maxHealth;
        }
    }

    private IEnumerator Move()
    {
        while (gameObject.activeSelf)
        {
            yield return new WaitForSeconds(0.01f);
            var playerPosition = _player.position;
            transform.position = Vector3.MoveTowards(transform.position, playerPosition, 0.01f);
            transform.LookAt(playerPosition);
            convas.transform.LookAt(Camera.main.transform.position);
        }
    }
    private IEnumerator MoveDamage()
    {
        while (gameObject.activeSelf)
        {
            yield return new WaitForSeconds(0.01f);
            var playerPosition = _player.position;
            transform.position = Vector3.MoveTowards(transform.position, playerPosition, 0.01f);
            transform.LookAt(playerPosition);
            convas.transform.LookAt(Camera.main.transform.position);
        }
    }


    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == 6)
        {
            other.gameObject.SetActive(false);
            var damage = EventManager.Hit?.Invoke(transform.position);
            if (damage != null) StartCoroutine(Hit(damage.Value));
        }
    }
}