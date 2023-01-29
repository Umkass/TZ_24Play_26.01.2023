using UI.Windows;
using UnityEngine;

namespace Services.ResourcesLoader
{
  public interface IResourcesLoader : IService
  {
    public void LoadAll();
    public GameObject GetRandomTrack();
    public GameObject GetWindow(WindowId windowId);
  }
}