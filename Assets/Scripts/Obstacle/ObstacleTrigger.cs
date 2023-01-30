﻿using CameraLogic;
using Cube;
using UnityEngine;

namespace Obstacle
{
  public class ObstacleTrigger : MonoBehaviour
  {
    private int _enteredNumber;
    private int _leavingNumber;
    
    private void OnTriggerEnter(Collider other)
    {
      if (other.TryGetComponent(out CubeTrigger cubeTrigger))
      {
        StartCoroutine(Camera.main.GetComponent<CameraFollow>().Shake(0.15f, 0.4f));
        Handheld.Vibrate();
        _enteredNumber = cubeTrigger.transform.parent.GetComponent<CubeHolder>().GetCubesCount();
      }
    }

    private void OnTriggerExit(Collider other)
    {
      if (other.TryGetComponent(out CubeTrigger cubeTrigger))
      {
        CubeHolder cubeHolder = cubeTrigger.transform.parent.GetComponent<CubeHolder>();
        _leavingNumber = cubeHolder.GetCubesCount();
        int value = _enteredNumber - _leavingNumber;
        cubeHolder.ChangePlayerPositionY(-value);
        cubeHolder.RecalculateCubesPositions();
      }
    }
  }
}