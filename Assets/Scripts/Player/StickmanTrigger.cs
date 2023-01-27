using UnityEngine;

namespace Player
{
  public class StickmanTrigger : MonoBehaviour
  {
    [SerializeField] private PlayerAnimator _playerAnimator;
    private void OnTriggerEnter(Collider other)
    {
      if (other.CompareTag("Obstacle"))
      {
        _playerAnimator.PlayDeath();
        Debug.Log("GameOver");
      }
    }
  }
}