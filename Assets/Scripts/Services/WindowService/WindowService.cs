using System.Collections.Generic;
using Data;
using Services.GameStateMachine;
using Services.ResourcesLoader;
using UI.Windows;
using UnityEngine;

namespace Services.WindowService
{
  public class WindowService : IWindowService
  {
    private readonly IResourcesLoader _resourcesLoader;
    private readonly IGameStateMachine _stateMachine;
    private Dictionary<WindowId, GameObject> _createdWindows = new Dictionary<WindowId, GameObject>();
    private Transform _uiRoot;

    public WindowService(IResourcesLoader resourcesLoader, IGameStateMachine stateMachine)
    {
      _resourcesLoader = resourcesLoader;
      _stateMachine = stateMachine;
    }

    public void Open(WindowId windowId)
    {
      if (windowId == WindowId.None)
        return;

      if (_uiRoot == null)
        CreateUIRoot();
      
      GameObject window = Object.Instantiate(_resourcesLoader.GetWindow(windowId), _uiRoot);

      switch (windowId)
      {
        case WindowId.StartWindow:
          window.GetComponent<WindowBase>().Construct(this);
          break;
        case WindowId.GameOverWindow:
          window.GetComponent<GameOverWindow>().Construct(this,_stateMachine);
          break;
      }
      
      _createdWindows.Add(windowId, window);
    }

    public void CloseWindow(WindowId windowId)
    {
      if (_createdWindows.TryGetValue(windowId, out GameObject window))
      {
        Object.Destroy(window.gameObject);
        _createdWindows.Remove(windowId);
      }
    }

    private void CreateUIRoot()
    {
      var prefab = Resources.Load<GameObject>(AssetPath.UIRootPath);
      GameObject root = Object.Instantiate(prefab);
      _uiRoot = root.transform;
    }
  }
}