using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader
{
  private readonly ICoroutineRunner _coroutineRunner;

  public SceneLoader(ICoroutineRunner coroutineRunner) => _coroutineRunner = coroutineRunner;

  public void Load(string name, Action onLoaded = null) =>
    _coroutineRunner.StartCoroutine(LoadScene(name,onLoaded));
  
  public void ReLoad(Action onLoaded = null) =>
    _coroutineRunner.StartCoroutine(ReLoadScene(onLoaded));

  private IEnumerator LoadScene(string nextScene, Action onLoaded = null)
  {
    if (SceneManager.GetActiveScene().name == nextScene)
    {
      onLoaded?.Invoke();
      yield break;
    }
      
    AsyncOperation waitNextScene = SceneManager.LoadSceneAsync(nextScene);
    while (!waitNextScene.isDone)
      yield return null;
      
    onLoaded?.Invoke();
  }
  
  private IEnumerator ReLoadScene(Action onLoaded = null)
  {
    AsyncOperation waitNextScene = SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().name);
    while (!waitNextScene.isDone)
      yield return null;
      
    onLoaded?.Invoke();
  }
}