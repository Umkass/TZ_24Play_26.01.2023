using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [SerializeField] private Rigidbody[] _ragdolls;
    private readonly int _idleHash = Animator.StringToHash("Idle");
    private readonly int _jumpHash = Animator.StringToHash("Jump");

    private void Start()
    {
        for (int i = 0; i < _ragdolls.Length; i++) 
            _ragdolls[i].isKinematic = true;
    }

    public void PlayIdle() => 
        _animator.SetTrigger(_idleHash);

    public void PlayJump() => 
        _animator.SetTrigger(_jumpHash);

    public void PlayDeath()
    {
        for (int i = 0; i < _ragdolls.Length; i++)
        {
            _animator.transform.parent = null;
            _animator.enabled = false;
            _ragdolls[i].isKinematic = false;
        }
    }
}
