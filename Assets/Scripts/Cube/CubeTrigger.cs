using Const;
using UnityEngine;

namespace Cube
{
  public class CubeTrigger : MonoBehaviour
  {
    private CubeHolder _cubeHolder;

    public void Construct(CubeHolder cubeHolder) => 
      _cubeHolder = cubeHolder;

    public void RemoveCube(CubeTrigger cube) => 
      _cubeHolder.RemoveCube(cube);

    private void OnCollisionEnter(Collision collision)
    {
      if (collision.gameObject.CompareTag(Tags.CubePickup))
      {
        Destroy(collision.gameObject);
        _cubeHolder.AddCube();
      }
    }
  }
}