using UnityEngine;

namespace Services.AssetManagement
{
  public class AssetProvider : IAssetProvider
  {
    public GameObject Instantiate(string path, Vector3 at)
    {
      var prefab = Resources.Load<GameObject>(path);
      return Object.Instantiate(prefab, at, Quaternion.identity);
    }

    public GameObject Instantiate(GameObject prefab) =>
      Object.Instantiate(prefab);

    public GameObject Instantiate(GameObject prefab, Transform parent) =>
      Object.Instantiate(prefab, parent);

    public GameObject Instantiate(GameObject prefab, Vector3 at, Quaternion rotation, Transform parent) =>
      Object.Instantiate(prefab, at, rotation, parent);
  }
}