using CameraLogic;
using Cube;
using UnityEngine;

namespace Obstacle
{
  public class ObstacleTrigger : MonoBehaviour
  {
    private int _enteredNumber;
    private int _leavingNumber;
    private bool _isExit;
    private bool _isEnter;
    
    private void OnTriggerEnter(Collider other)
    {
      if (!_isEnter && other.TryGetComponent(out CubeTrigger cubeTrigger))
      {
        _isEnter = true;
        StartCoroutine(Camera.main.GetComponent<CameraFollow>().Shake(0.15f, 0.4f));
        Handheld.Vibrate();
        CubeHolder cubeHolder;
        cubeTrigger.transform.parent.TryGetComponent(out cubeHolder);
        _enteredNumber = cubeHolder.GetCubesCount();
        cubeHolder.isTrail = false;
      }
    }

    private void OnTriggerExit(Collider other)
    {
      if (!_isExit && other.TryGetComponent(out CubeTrigger cubeTrigger))
      {
        _isExit = true;
        CubeHolder cubeHolder;
        cubeTrigger.transform.parent.TryGetComponent(out cubeHolder);
        _leavingNumber = cubeHolder.GetCubesCount();
        int value = _enteredNumber - _leavingNumber;
        cubeHolder.RecalculateCubesPositions();
        cubeHolder.ChangePlayerPositionY(-value);
        cubeHolder.isTrail = true;
      }
    }
  }
}