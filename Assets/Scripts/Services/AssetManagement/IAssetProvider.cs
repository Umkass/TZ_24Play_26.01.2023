using UnityEngine;

namespace Services.AssetManagement
{
  public interface IAssetProvider : IService
  {
    public GameObject Instantiate(string path, Vector3 at);
    public GameObject Instantiate(GameObject prefab);
    public GameObject Instantiate(GameObject prefab, Transform parent);
    public GameObject Instantiate(GameObject prefab, Vector3 at, Quaternion rotation, Transform parent);
  }
}