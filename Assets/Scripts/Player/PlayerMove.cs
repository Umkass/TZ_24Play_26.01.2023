using UnityEngine;

namespace Player
{
  public class PlayerMove : MonoBehaviour
  {
    private const float QuarterScreen = 0.25f; //1/4 of screen in normalized viewport space
    [SerializeField] private float _moveSpeed;
    [SerializeField] private Vector3 _targetPosition;
    [SerializeField] private float _stepValue;
    private Vector3 _startTouchPosition;

    void Update()
    {
      if (Application.isEditor)
      {
        if (Input.GetMouseButtonDown(0))
          _startTouchPosition = Camera.main.ScreenToViewportPoint(Input.mousePosition);
        if (Input.GetMouseButton(0))
        {
          _targetPosition += Vector3.forward * _moveSpeed * Time.deltaTime;
          if ((_startTouchPosition - Camera.main.ScreenToViewportPoint(Input.mousePosition)).x < -QuarterScreen && _targetPosition.x != _stepValue)
          {
            _targetPosition.x += _stepValue;
          }
          else if ((_startTouchPosition - Camera.main.ScreenToViewportPoint(Input.mousePosition)).x > QuarterScreen && _targetPosition.x != -_stepValue)
          {
            _targetPosition.x -= _stepValue;
          }
          else if ((_startTouchPosition - Camera.main.ScreenToViewportPoint(Input.mousePosition)).x >= -QuarterScreen
                   && (_startTouchPosition - Camera.main.ScreenToViewportPoint(Input.mousePosition)).x <= QuarterScreen)
          {
            _targetPosition.x = 0;
          }

          transform.position = _targetPosition;
        }
      }
      else
      {
        if (Input.touchCount == 1)
        {
          Touch touch = Input.GetTouch(0);
          _targetPosition += Vector3.forward * _moveSpeed * Time.deltaTime;
          if (touch.phase == TouchPhase.Began)
          {
            _startTouchPosition = Camera.main.ScreenToViewportPoint(touch.position);
          }
          else if (touch.phase == TouchPhase.Moved)
          {
            Debug.Log(_startTouchPosition);
            Debug.Log(touch.position);
            Debug.Log(Camera.main.ScreenToViewportPoint(touch.position));
            Debug.Log((_startTouchPosition - Camera.main.ScreenToViewportPoint(touch.position)).x);
            if ((_startTouchPosition - Camera.main.ScreenToViewportPoint(touch.position)).x < -QuarterScreen && _targetPosition.x != _stepValue)
            {
              _targetPosition.x += _stepValue;
            }
            else if ((_startTouchPosition - Camera.main.ScreenToViewportPoint(touch.position)).x > QuarterScreen && _targetPosition.x != -_stepValue)
            {
              _targetPosition.x -= _stepValue;
            }
            else if ((_startTouchPosition - Camera.main.ScreenToViewportPoint(touch.position)).x >= -QuarterScreen
                     && (_startTouchPosition - Camera.main.ScreenToViewportPoint(touch.position)).x <= QuarterScreen)
            {
              _targetPosition.x = 0;
            }
          }

          transform.position = _targetPosition;
        }
      }
    }
  }
}