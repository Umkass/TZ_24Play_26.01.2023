using Data;
using Services.GameStateMachine;
using Services.ResourcesLoader;
using Services.TrackFactory;
using Services.WindowService;

namespace States
{
  public class BootstrapState : IState
  {
    private readonly GameStateMachine _stateMachine;
    private readonly SceneLoader _sceneLoader;

    public BootstrapState(GameStateMachine stateMachine, SceneLoader sceneLoader)
    {
      _stateMachine = stateMachine;
      _sceneLoader = sceneLoader;

      RegisterServices();
    }

    public void Enter() =>
      _sceneLoader.Load(SceneNames.BootstrapScene, onLoaded: EnterLoadLevel);

    public void Exit()
    {
    }

    private void EnterLoadLevel() =>
      _stateMachine.Enter<LoadLevelState, string>(SceneNames.GameScene);

    private void RegisterServices()
    {
      IResourcesLoader resourcesLoader = new ResourcesLoader();
      resourcesLoader.LoadAll();
      ITrackFactory trackFactory = new TrackFactory(resourcesLoader);
      IWindowService windowService = new WindowService(resourcesLoader, _stateMachine);
      _stateMachine.InitServices(trackFactory, windowService);
    }
  }
}