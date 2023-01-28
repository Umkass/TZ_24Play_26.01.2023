using Services.GameStateMachine;
using States;
using UnityEngine;

public class GameBootstrapper : MonoBehaviour, ICoroutineRunner
{
  private GameStateMachine _stateMachine;

  private void Awake()
  {
    _stateMachine = new GameStateMachine(new SceneLoader(this));
    _stateMachine.Enter<BootstrapState>();
    DontDestroyOnLoad(this);
  }
}