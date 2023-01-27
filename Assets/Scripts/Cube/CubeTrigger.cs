using UnityEngine;

namespace Cube
{
  public class CubeTrigger : MonoBehaviour
  {
    private CubeHolder _cubeHolder;

    public void Construct(CubeHolder cubeHolder) => 
      _cubeHolder = cubeHolder;

    public void RemoveCube(CubeTrigger gameObject) => 
      _cubeHolder.RemoveCube(gameObject);

    private void OnCollisionEnter(Collision collision)
    {
      if (collision.gameObject.CompareTag("CubePickup"))
      {
        Destroy(collision.gameObject);
        _cubeHolder.AddCube();
      }
    }
  }
}