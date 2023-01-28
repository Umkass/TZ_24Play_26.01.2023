using CameraLogic;
using Player;
using Services.GameStateMachine;
using UI.Windows;
using UnityEngine;

namespace States
{
  public class LoadLevelState : IPayloadedState<string>
  {
    private const string PlayerPath = "Prefabs/Player";
    private readonly Vector3 _startPlayerPosition = new Vector3(0f, 0f, 6f);
    private readonly GameStateMachine _stateMachine;
    private readonly SceneLoader _sceneLoader;
    public LoadLevelState(GameStateMachine stateMachine, SceneLoader sceneLoader)
    {
      _stateMachine = stateMachine;
      _sceneLoader = sceneLoader;
    }

    public void Enter(string sceneName) =>
      _sceneLoader.Load(sceneName, onLoaded: OnLoaded);

    public void ReEnter() => 
      _sceneLoader.ReLoad(OnLoaded);

    public void Exit()
    {
    }

    private void OnLoaded()
    {
      _stateMachine.TrackFactory.CreateTracks(3,false);
      GameObject player = CreatePlayer();
      _stateMachine.WindowService.Open(WindowId.StartWindow);
      CameraFollow(player.transform);
      _stateMachine.Enter<GameLoopState>();
    }

    private GameObject CreatePlayer()
    {
      GameObject player = Instantiate(PlayerPath, _startPlayerPosition);
      player.GetComponent<PlayerDeath>().Construct(_stateMachine.WindowService);
      player.GetComponent<PlayerMove>().Construct(_stateMachine.WindowService);
      return player;
    }

    private void CameraFollow(Transform player) => 
      Camera.main.GetComponent<CameraFollow>().Follow(player);

    public GameObject Instantiate(string path, Vector3 at)
    {
      var prefab = Resources.Load<GameObject>(path);
      return Object.Instantiate(prefab, at, Quaternion.identity);
    }

    public GameObject Instantiate(string path)
    {
      var prefab = Resources.Load<GameObject>(path);
      return Object.Instantiate(prefab);
    }
  }
}