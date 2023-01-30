using System.Collections;
using UnityEngine;

namespace CameraLogic
{
  public class CameraFollow : MonoBehaviour
  {
    [Header("Position Offset")] 
    [SerializeField] private float _positionOffsetX;
    [SerializeField] private float _positionOffsetY;
    [SerializeField] private float _positionOffsetZ;

    [Header("Rotation Offset")] 
    [SerializeField] private float _rotationOffsetX;
    [SerializeField] private float _rotationOffsetY;
    
    private Transform _target;
    private bool _isShaking;

    private void LateUpdate()
    {
      if (_target == null || _isShaking)
        return;

      Vector3 positionWithOffset = new Vector3
      (
        _positionOffsetX,
        _positionOffsetY,
        _target.position.z + _positionOffsetZ
      );

      Vector3 rotationWithOffset = new Vector3
      (
        _rotationOffsetX,
        _rotationOffsetY
      );

      transform.rotation = Quaternion.Euler(rotationWithOffset);
      transform.position = positionWithOffset;
    }
    
    public IEnumerator Shake(float duration, float magnitude)
    {
      _isShaking = true;
      Vector3 orignalPosition = transform.position;
      float elapsed = 0f;
        
      while (elapsed < duration)
      {
        float x = Random.Range(-0.2f, 0.2f) * magnitude;
        float y = Random.Range(-0.2f, 0.2f) * magnitude;

        transform.position = new Vector3(transform.position.x + x, transform.position.y + y, _target.position.z + _positionOffsetZ);
        elapsed += Time.deltaTime;
        yield return 0;
      }

      _isShaking = false;
      transform.position = orignalPosition;
    }

    public void Follow(Transform target) => 
      _target = target;
  }
}