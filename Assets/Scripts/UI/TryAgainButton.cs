using Data;
using Services.GameStateMachine;
using Services.WindowService;
using States;
using UI.Windows;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace UI
{
  public class TryAgainButton : MonoBehaviour
  {
    [SerializeField] private Button _btnTryAgain;
    private IGameStateMachine _stateMachine;
    private IWindowService _windowService;

    public void Construct(IWindowService windowService, IGameStateMachine stateMachine)
    {
      _windowService = windowService;
      _stateMachine = stateMachine;
    }

    private void Awake() =>
      _btnTryAgain.onClick.AddListener(Restart);

    private void Restart()
    {
      _windowService.CloseWindow(WindowId.GameOverWindow);
      ReloadLevel();
    }

    private void ReloadLevel()
    {
      //SceneManager.LoadScene(SceneManager.GetActiveScene().name);
      
      //_stateMachine.Enter<LoadLevelState, string>(SceneNames.GameScene);
      _stateMachine.ReEnter<LoadLevelState, string>();
    }
  }
}