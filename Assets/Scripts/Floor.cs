using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum FloorType
{
    Static,
    HorizontalMovement,
    VerticalMovement,
    Circular
}
public class Floor : MonoBehaviour
{
    private Camera _mainCamera;
    public Floor(float platformLength, FloorType floorType)
    {
        _floorType = floorType;
        transform.localScale.Set(platformLength,transform.localScale.y,transform.localScale.z);
    }

    [Range(0f, 20f)]
    public float _maxDistance;
    [Range(0.1f,5f)]
    public float _movementSpeed;
    

    [SerializeField]
    private FloorType _floorType;
    private Vector3 _initalPosition;
    private Vector3 _offset;
    
    private void Awake()
    {
        _initalPosition = transform.position;
        _mainCamera = Camera.main;
    }
    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.Lerp(_initalPosition,_initalPosition + _offset,0.95f);
        if(_floorType == FloorType.HorizontalMovement)
        {
            _offset = new Vector3(((Mathf.Cos(Time.realtimeSinceStartup*_movementSpeed)) * _maxDistance) / 10, 0, 0);
        }
        if (_floorType == FloorType.VerticalMovement)
        {
            _offset = new Vector3(0, ((Mathf.Cos(Time.realtimeSinceStartup * _movementSpeed)) * _maxDistance) / 10, 0);
        }
        if (_floorType == FloorType.Circular)
        {
            _offset = new Vector3(((Mathf.Cos(Time.realtimeSinceStartup * _movementSpeed)) * _maxDistance) / 10, ((Mathf.Sin(Time.realtimeSinceStartup * _movementSpeed)) * _maxDistance) / 10, 0);
        }

        if(!inBoundaries())
        {
            Destroy( gameObject );
        }
        

    }

    private bool inBoundaries()
    {
        float leftBoundary = _mainCamera.transform.position.x - (_mainCamera.orthographicSize / 2);
        float rightBoundary = _mainCamera.transform.position.x + (_mainCamera.orthographicSize / 2);
        bool xCheck = leftBoundary < transform.position.x && transform.position.x < rightBoundary;
        return xCheck;
    }
}
