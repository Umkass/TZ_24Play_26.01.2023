using Const;
using UnityEngine;

namespace Player
{
  public class StickmanTrigger : MonoBehaviour
  {
    [SerializeField] private PlayerDeath _playerDeath;
    private bool _isEntered;

    private void OnTriggerEnter(Collider other)
    {
      if ((other.CompareTag(Tags.Obstacle) || other.CompareTag(Tags.Track)) && !_isEntered)
      {
        _isEntered = true;
        _playerDeath.Death();
      }
    }
  }
}