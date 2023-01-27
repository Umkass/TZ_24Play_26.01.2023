using UnityEngine;

namespace Player
{
  public class PlayerMove : MonoBehaviour
  {
    [SerializeField] private float _moveSpeed;
    private Vector3 _targetPosition;
    private Vector3 _startMousePosition;
    private Vector2 _startTouchPosition;
    private Vector3 _delta, _deltaPosition;
    private bool _isMove;

    private void Awake() => 
      _targetPosition = transform.position;

    private void Update()
    {
      if (Application.isEditor)
      {
        if (Input.GetMouseButtonDown(0))
        {
          _startMousePosition = Input.mousePosition;
          _isMove = true;
        }

        if (Input.GetMouseButton(0))
        {
          _delta = -(_startMousePosition - Input.mousePosition).normalized;
          _deltaPosition = Vector3.Lerp(_deltaPosition, _delta, 5f * Time.deltaTime);
          transform.position += new Vector3(_deltaPosition.x * 0.3f, 0, 0);
          _startMousePosition = Input.mousePosition;
          _targetPosition.x = Mathf.Clamp(transform.position.x, -2f, 2f);
          transform.position = _targetPosition;
        }
      }
      else
      {
        if (Input.touchCount == 1)
        {
          Touch touch = Input.GetTouch(0);
          _isMove = true;
          if (touch.phase == TouchPhase.Began)
            _startTouchPosition = touch.position;
          else if (touch.phase == TouchPhase.Moved)
          {
            _delta = -(_startTouchPosition - touch.position).normalized;
            _deltaPosition = Vector3.Lerp(_deltaPosition, _delta, 5f * Time.deltaTime);
            transform.position += new Vector3(_deltaPosition.x * 0.3f, 0, 0);
            _startTouchPosition = touch.position;
            _targetPosition.x = Mathf.Clamp(transform.position.x, -2f, 2f);
            transform.position = _targetPosition;
          }

          transform.position = _targetPosition;
        }
      }

      AutoMove();
    }

    public void ChangePositionY(float value) => 
      _targetPosition = new Vector3(_targetPosition.x, transform.position.y + value, _targetPosition.z);

    private void AutoMove()
    {
      if (_isMove)
      {
        _targetPosition += Vector3.forward * _moveSpeed * Time.deltaTime;
        transform.position = _targetPosition;
      }
    }
  }
}