using Cube;
using UnityEngine;

namespace Obstacle
{
  public class ObstacleCubeTrigger : MonoBehaviour
  {
    private void OnTriggerEnter(Collider other)
    {
      if (other.TryGetComponent(out CubeTrigger cubeTrigger))
      {
        other.gameObject.transform.SetParent(null);
        cubeTrigger.RemoveCube(cubeTrigger);
      }
    }
  }
}