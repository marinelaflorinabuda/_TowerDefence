using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    public float cameraSpeed = 20f;
    public float scrollSpeed = 20f;
    public float mouseBorderThickness = 10f;
    public Vector2 cameraLimits, cameraScrollLimits;

    private const string _mouseScrollWheelImputName = "Mouse ScrollWheel";
    private const string _upKeyName = "w";
    private const string _downKeyName = "s";
    private const string _leftKeyName = "a";
    private const string _rightKeyName = "d";

    private const int _scrollMultiplier = 100;

    private Vector3 _cameraPosition;
    private float _scrollingValue;

    void Update()
    {
        _cameraPosition = transform.position;

        if(Input.GetKey("w") || Input.mousePosition.y >= Screen.height - mouseBorderThickness)
        {
            _cameraPosition.z += cameraSpeed * Time.deltaTime;
        }
        if (Input.GetKey("s") || Input.mousePosition.y <= mouseBorderThickness)
        {
            _cameraPosition.z -= cameraSpeed * Time.deltaTime;
        }
        if (Input.GetKey("a") || Input.mousePosition.x <= mouseBorderThickness)
        {
            _cameraPosition.x -= cameraSpeed * Time.deltaTime;
        }
        if (Input.GetKey("d") || Input.mousePosition.x >= Screen.width - mouseBorderThickness)
        {
            _cameraPosition.x += cameraSpeed * Time.deltaTime;
        }

        _cameraPosition.x = Mathf.Clamp(_cameraPosition.x, -cameraLimits.x, cameraLimits.x);
        _cameraPosition.y = Mathf.Clamp(_cameraPosition.y, cameraScrollLimits.x, cameraScrollLimits.y);
        _cameraPosition.z = Mathf.Clamp(_cameraPosition.z, -cameraLimits.y, cameraLimits.y);

        _scrollingValue = Input.GetAxis("Mouse ScrollWheel");
        _cameraPosition.y -= _scrollingValue * scrollSpeed * _scrollMultiplier * Time.deltaTime;

        transform.position = _cameraPosition;
    }
    
}
