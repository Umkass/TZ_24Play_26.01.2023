using UnityEngine;

namespace CameraLogic
{
  public class CameraFollow : MonoBehaviour
  {
    [Header("Follow Target")] 
    [SerializeField] private Transform _target;

    [Header("Position Offset")] 
    [SerializeField] private float _positionOffsetX;
    [SerializeField] private float _positionOffsetY;
    [SerializeField] private float _positionOffsetZ;

    [Header("Rotation Offset")] 
    [SerializeField] private float _RotationOffsetX;
    [SerializeField] private float _RotationOffsetY;

    private void LateUpdate()
    {
      if (_target == null)
        return;

      Vector3 positionWithOffset = new Vector3
      (
        _positionOffsetX,
        _positionOffsetY,
        _target.position.z + _positionOffsetZ
      );

      Vector3 rotationWithOffset = new Vector3
      (
        _RotationOffsetX,
        _RotationOffsetY
      );

      transform.rotation = Quaternion.Euler(rotationWithOffset);
      transform.position = positionWithOffset;
    }
  }
}