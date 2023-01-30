using System.Collections;
using UnityEngine;

public class DestroyOverTime : MonoBehaviour
{
  [SerializeField] private float _timeToDestroy;

  private void Start() => 
    StartCoroutine("DestroyCoroutine");

  IEnumerator DestroyCoroutine()
  {
    yield return new WaitForSeconds(_timeToDestroy);
    Destroy(gameObject);
  }
}