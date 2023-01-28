using Services.WindowService;
using UI.Windows;
using UnityEngine;

namespace Player
{
  public class PlayerDeath : MonoBehaviour
  {
    [SerializeField] private PlayerAnimator _playerAnimator;
    [SerializeField] private PlayerMove _playerMove;
    private IWindowService _windowService;

    public void Construct(IWindowService windowService) =>
      _windowService = windowService;

    public void Death()
    {
      _playerAnimator.PlayDeath();
      _playerMove.StopMoving();
      _windowService.Open(WindowId.GameOverWindow);
    }
  }
}