using System.Collections.Generic;
using Const;
using Services.AssetManagement;
using Services.GameStateMachine;
using Services.ResourcesLoader;
using UI.Windows;
using UnityEngine;

namespace Services.WindowService
{
  public class WindowService : IWindowService
  {
    private readonly IResourcesLoader _resourcesLoader;
    private readonly IAssetProvider _assetProvider;
    private readonly IGameStateMachine _stateMachine;
    private Dictionary<WindowId, GameObject> _createdWindows = new();
    private Transform _uiRoot;

    public WindowService(IResourcesLoader resourcesLoader, IGameStateMachine stateMachine, IAssetProvider assetProvider)
    {
      _resourcesLoader = resourcesLoader;
      _stateMachine = stateMachine;
      _assetProvider = assetProvider;
    }

    public void Open(WindowId windowId)
    {
      if (windowId == WindowId.None || _createdWindows.ContainsKey(windowId))
        return;

      if (_uiRoot == null)
        CreateUIRoot();

      GameObject window = _assetProvider.Instantiate(_resourcesLoader.GetWindow(windowId), _uiRoot);

      switch (windowId)
      {
        case WindowId.StartWindow:
          window.GetComponent<WindowBase>().Construct(this);
          break;
        case WindowId.GameOverWindow:
          window.GetComponent<GameOverWindow>().Construct(this, _stateMachine);
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
      GameObject root = _assetProvider.Instantiate(prefab);
      _uiRoot = root.transform;
    }
  }
}