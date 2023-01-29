using Services.WindowService;
using UI.Windows;
using UnityEngine;

namespace Player
{
  public class PlayerMove : MonoBehaviour
  {
    [SerializeField] private float _moveSpeed;
    [SerializeField] private GameObject _warpFx;
    private IWindowService _windowService;
    private Vector3 _targetPosition;
    private Vector3 _startMousePosition;
    private Vector2 _startTouchPosition;
    private Vector3 _delta, _deltaPosition;
    private bool _isStart;
    private bool _isMoving;
    private bool _isGameOver;

    public void Construct(IWindowService windowService) =>
      _windowService = windowService;

    private void Awake() =>
      _targetPosition = transform.position;

    private void Update()
    {
      if (!_isGameOver)
      {
        if (Application.isEditor)
          EditorInput();
        else
          MobileInput();

        AutoMove();
        IsGameStarted();
      }
    }

    public void ChangePositionY(float value) =>
      _targetPosition = new Vector3(_targetPosition.x, transform.position.y + value, _targetPosition.z);

    public void StopMoving()
    {
      _isGameOver = true;
      _warpFx.SetActive(false);
    }

    private void EditorInput()
    {
      if (Input.GetMouseButtonDown(0))
      {
        _startMousePosition = Input.mousePosition;
        _isStart = true;
        _isMoving = true;
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

    private void MobileInput()
    {
      if (Input.touchCount == 1)
      {
        Touch touch = Input.GetTouch(0);
        _isStart = true;
        _isMoving = true;
        if (touch.phase == TouchPhase.Began)
        {
          _startTouchPosition = touch.position;
        }
        else if (touch.phase == TouchPhase.Moved)
        {
          _delta = -(_startTouchPosition - touch.position).normalized;
          _deltaPosition = Vector3.Lerp(_deltaPosition, _delta, 5f * Time.deltaTime);
          transform.position += new Vector3(_deltaPosition.x * 0.3f, 0, 0);
          _startTouchPosition = touch.position;
          _targetPosition.x = Mathf.Clamp(transform.position.x, -2f, 2f);
          transform.position = _targetPosition;
        }
      }
    }

    private void AutoMove()
    {
      if (_isMoving)
      {
        _targetPosition += Vector3.forward * _moveSpeed * Time.deltaTime;
        transform.position = _targetPosition;
      }
    }

    private void IsGameStarted()
    {
      if (_isStart)
      {
        _warpFx.SetActive(true);
        _windowService.CloseWindow(WindowId.StartWindow);
        _isStart = false;
      }
    }
  }
}