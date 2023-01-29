using System.Collections.Generic;
using System.Linq;
using Const;
using Player;
using Services.AssetManagement;
using UnityEngine;

namespace Cube
{
  public class CubeHolder : MonoBehaviour
  {
    [SerializeField] private PlayerAnimator _animator;
    [SerializeField] private PlayerMove _playerMove;
    [SerializeField] private PlayerDeath _playerDeath;
    private List<CubeTrigger> _cubes = new();

    private void Start() =>
      CreateCube(transform.position);

    public int GetCubesCount() =>
      _cubes.Count;

    public void AddCube()
    {
      _animator.PlayJump();

      Vector3 createPosition = new Vector3
      (
        _playerMove.transform.position.x,
        _cubes.Last().transform.position.y - 1f,
        _playerMove.transform.position.z
      );

      ChangePlayerPositionY(1f);

      CreateCube(createPosition);

      _animator.PlayIdle();
    }

    public void RemoveCube(CubeTrigger cube)
    {
      _cubes.Remove(cube);
      if (_cubes.Count == 0)
        _playerDeath.Death();
    }

    public void RecalculateCubesPositions()
    {
      if (_cubes.Count == 1)
      {
        _cubes[0].transform.position = new Vector3(_playerMove.transform.position.x, transform.position.y, _playerMove.transform.position.z);
        return;
      }

      for (int i = 0; i < _cubes.Count - 1; i++)
      {
        if (_cubes[i].transform.position.y - 1f != _cubes[i + 1].transform.position.y)
          _cubes[i + 1].transform.position = new Vector3(_playerMove.transform.position.x, _cubes[i].transform.position.y - 1f, _playerMove.transform.position.z);
      }
    }

    public void ChangePlayerPositionY(float value) =>
      _playerMove.ChangePositionY(value);

    private void CreateCube(Vector3 createPosition)
    {
      GameObject cubeGo = Resources.Load(AssetPath.CubePath) as GameObject;
      GameObject createdCube = Instantiate(cubeGo, createPosition, Quaternion.identity, transform);
      CubeTrigger cubeTrigger = createdCube.GetComponent<CubeTrigger>();
      _cubes.Add(cubeTrigger);
      cubeTrigger.Construct(this);
    }
  }
}