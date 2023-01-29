using System;
using System.Collections.Generic;
using Services.AssetManagement;
using Services.TrackFactory;
using Services.WindowService;
using States;

namespace Services.GameStateMachine
{
  public class GameStateMachine : IGameStateMachine
  {
    private Dictionary<Type, IExitableState> _states;
    private IExitableState _activeState;

    public ITrackFactory TrackFactory;
    public IWindowService WindowService;
    public IAssetProvider AssetProvider;

    public GameStateMachine(SceneLoader sceneLoader)
    {
      _states = new Dictionary<Type, IExitableState>
      {
        [typeof(BootstrapState)] = new BootstrapState(this, sceneLoader),
        [typeof(LoadLevelState)] = new LoadLevelState(this, sceneLoader),
        [typeof(GameLoopState)] = new GameLoopState(this),
      };
    }

    public void InitServices(ITrackFactory trackFactory, IWindowService windowService, IAssetProvider assetProvider)
    {
      TrackFactory = trackFactory;
      WindowService = windowService;
      AssetProvider = assetProvider;
    }

    public void Enter<TState>() where TState : class, IState
    {
      IState state = ChangeState<TState>();
      state.Enter();
    }

    public void Enter<TState, TPayload>(TPayload payload) where TState : class, IPayloadedState<TPayload>
    {
      TState state = ChangeState<TState>();
      state.Enter(payload);
    }

    public void ReEnter<TState, TPayload>() where TState : class, IPayloadedState<TPayload>
    {
      TState state = ChangeState<TState>();
      state.ReEnter();
    }

    private TState ChangeState<TState>() where TState : class, IExitableState
    {
      _activeState?.Exit();

      TState state = GetState<TState>();
      _activeState = state;

      return state;
    }

    private TState GetState<TState>() where TState : class, IExitableState =>
      _states[typeof(TState)] as TState;
  }
}