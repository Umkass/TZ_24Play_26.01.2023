using System.Collections.Generic;
using System.Linq;
using Player;
using UnityEngine;

namespace Cube
{
  public class CubeHolder : MonoBehaviour
  {
    [SerializeField] private PlayerAnimator _animator;
    [SerializeField] private PlayerMove _player;
    private List<CubeTrigger> cubes = new List<CubeTrigger>();

    private void Start() =>
      CreateCube(transform.position);

    public int GetCubesCount() =>
      cubes.Count;

    public void AddCube()
    {
      _animator.PlayJump();

      Vector3 createPosition = new Vector3
      (
        _player.transform.position.x,
        cubes.Last().transform.position.y - 1f,
        _player.transform.position.z
      );

      ChangePlayerPositionY(1f);

      CreateCube(createPosition);

      _animator.PlayIdle();
    }

    public void RemoveCube(CubeTrigger cube)
    {
      cubes.Remove(cube);
      if (cubes.Count == 0)
        _animator.PlayDeath();
    }

    public void RecalculateCubesPositions()
    {
      if (cubes.Count == 1)
      {
        cubes[0].transform.position = new Vector3(_player.transform.position.x, transform.position.y, _player.transform.position.z);
        return;
      }
      for (int i = 0; i < cubes.Count - 1; i++)
      {
        if (cubes[i].transform.position.y - 1f != cubes[i + 1].transform.position.y)
          cubes[i + 1].transform.position = new Vector3(_player.transform.position.x, cubes[i].transform.position.y - 1f, _player.transform.position.z);
      }
    }

    public void ChangePlayerPositionY(float value)
    {
      _player.ChangePositionY(value);
    }

    private void CreateCube(Vector3 createPosition)
    {
      GameObject cubeGO = Resources.Load(AssetPath.CubePath) as GameObject;
      GameObject createdCube = Instantiate(cubeGO, createPosition, Quaternion.identity, transform);
      CubeTrigger cubeTrigger = createdCube.GetComponent<CubeTrigger>();
      cubes.Add(cubeTrigger);
      cubeTrigger.Construct(this);
    }
  }
}