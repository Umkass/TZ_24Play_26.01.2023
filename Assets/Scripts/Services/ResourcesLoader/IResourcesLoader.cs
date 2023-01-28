using UI.Windows;
using UnityEngine;

namespace Services.ResourcesLoader
{
  public interface IResourcesLoader : IService
  {
    void LoadAll();
    GameObject GetRandomTrack();
    GameObject GetWindow(WindowId windowId);
  }
}