using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Sausage : MonoBehaviour
{
    Rigidbody _rb;
    Vector3 _startPoint;
    Vector3 _endPoint;
    Vector3 _jumpDirection;
    public GameObject _arrow;
    public GameObject _arrowJoint;
    float _jumpPower = 0;
    int layerMask = 1 << 6;
    public Image _EnergyBar;
    RectTransform _RectTransformEnergyBar;
    public Image _EnergyBar2;
    RectTransform _RectTransformEnergyBar2;
    RaycastHit _hit;
    int _check;
    float _energy;
    public Text _fps;
    public static float fps;   
    void Start()
    {
        
        Application.targetFrameRate = 60;
        _energy = 100;
        _rb = GetComponent<Rigidbody>();
        _RectTransformEnergyBar = _EnergyBar.GetComponent<RectTransform>();
        _RectTransformEnergyBar2 = _EnergyBar2.GetComponent<RectTransform>();
    }
    void Update()
    {
        fps = 1.0f / Time.deltaTime;
        int i = (int) Mathf.Round(fps / 10) * 10;
        _fps.text = i.ToString();

        _RectTransformEnergyBar.sizeDelta = new Vector2(_energy*10.8f, 100);
        Camera.main.transform.position = new Vector3(transform.position.x - 15, transform.position.y, -40);
        _arrowJoint.transform.position = transform.position;
        if(_startPoint != Vector3.zero)
        {
            // Ray _ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            Ray _ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
            if (Physics.Raycast(_ray, out _hit, Mathf.Infinity, layerMask))
            {
                _endPoint = _hit.point;
            }                
            _arrowJoint.transform.rotation = Quaternion.LookRotation(Vector3.forward, _arrowJoint.transform.position - _endPoint);
            _jumpPower = Vector3.Distance(transform.position,_endPoint)*6;
            if(_jumpPower > 100)
            {
                _jumpPower = 100;
            }
            
            if(_energy > 0)
            {
                if(_energy < _jumpPower)
                {
                    _arrow.transform.localPosition = new Vector3(0, _energy/20, 0);
                    _arrow.transform.localScale = new Vector3(0.3f, _energy/10, 0.3f); 
                    _RectTransformEnergyBar2.sizeDelta = new Vector2(_energy*10.8f, 100);
                }
                else
                {
                    _arrow.transform.localPosition = new Vector3(0, _jumpPower/20, 0);
                    _arrow.transform.localScale = new Vector3(0.3f, _jumpPower/10, 0.3f);
                    _RectTransformEnergyBar2.sizeDelta = new Vector2(_jumpPower*10.8f, 100);
                }
            }
        }
        if (Input.GetMouseButtonDown(0))
        {
            // Ray _ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            Ray _ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
            if (Physics.Raycast(_ray, out _hit, Mathf.Infinity, layerMask))
            {
                _startPoint = _hit.point;
            }
        }
        if (Input.GetMouseButtonUp(0))
        {
            _jumpDirection = (transform.position - _endPoint).normalized;
            
            if(_energy < _jumpPower)
            {
                Jump(_jumpDirection*_energy);
            }
            else
            {
                Jump(_jumpDirection*_jumpPower);
            }
            _energy -= _jumpPower;
            if(_energy < 0)
            {
                _energy = 0;
            }
            _startPoint = Vector3.zero;
            _arrow.transform.localPosition = new Vector3(0, 0, 0);
            _arrow.transform.localScale = new Vector3(0.3f, 0.3f, 0.3f);
            _RectTransformEnergyBar2.sizeDelta = new Vector2(0, 100);
            Invoke("Wait", 0.1f);
        }
    }
    void Wait()
    {
        _check = 1;
    }
    void OnCollisionStay(Collision other) 
    {
        if(_check == 1)
        {
            if(other.gameObject.tag == "Road" )
            {
                _energy = 100;
                _check = 0;
            }
        } 
    }
    void Jump(Vector3 _jumpVector)
    {
        _rb.AddForce(_jumpVector, ForceMode.Impulse);
    }
}