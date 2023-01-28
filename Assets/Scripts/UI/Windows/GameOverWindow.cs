using Services.GameStateMachine;
using Services.WindowService;
using UnityEngine;

namespace UI.Windows
{
  public class GameOverWindow : WindowBase
  {
    [SerializeField] private TryAgainButton _btn;

    public void Construct(IWindowService windowService, IGameStateMachine stateMachine)
    {
      base.Construct(windowService);
      _btn.Construct(windowService, stateMachine);
    }
  }
}