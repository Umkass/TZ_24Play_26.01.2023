using System.Collections.Generic;
using System.Linq;
using UI.Windows;
using UnityEngine;

namespace Services.ResourcesLoader
{
  public class ResourcesLoader : IResourcesLoader
  {
    private const string TracksPath = "Prefabs/Tracks";
    private const string WindowsPath = "Prefabs/UI/Windows";
    private List<GameObject> _tracks = new();
    private Dictionary<WindowId, GameObject> _windows = new();

    public void LoadAll()
    {
      LoadTracks();
      LoadWindows();
    }

    public GameObject GetRandomTrack() =>
      _tracks[Random.Range(0, _tracks.Count)];

    public GameObject GetWindow(WindowId windowId) =>
      _windows.TryGetValue(windowId, out GameObject windowGO)
        ? windowGO
        : null;

    private void LoadTracks()
    {
      _tracks = Resources
        .LoadAll<GameObject>(TracksPath)
        .ToList();
    }

    private void LoadWindows()
    {
      _windows = Resources
        .LoadAll<WindowBase>(WindowsPath)
        .ToDictionary(x => x.Id, x => x.gameObject);
    }
  }
}