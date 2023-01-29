using Services.AssetManagement;
using Services.ResourcesLoader;
using Track;
using UnityEngine;

namespace Services.TrackFactory
{
  public class TrackFactory : ITrackFactory
  {
    private readonly IResourcesLoader _resourcesLoader;
    private readonly IAssetProvider _assetProvider;
    private Vector3 _nextSpawnTrackPosition;
    private Transform _tracksParent;

    public TrackFactory(IResourcesLoader resourcesLoader, IAssetProvider assetProvider)
    {
      _resourcesLoader = resourcesLoader;
      _assetProvider = assetProvider;
    }

    public void CreateTracks(int numberOfTracks, bool withAnim)
    {
      if (_tracksParent == null)
      {
        _tracksParent = GameObject.Find("Tracks").transform;
        _nextSpawnTrackPosition = Vector3.zero;
      }
      for (int i = 0; i < numberOfTracks; i++)
      {
        GameObject trackGO = _assetProvider.Instantiate
        (
          _resourcesLoader.GetRandomTrack(),
          _nextSpawnTrackPosition,
          Quaternion.identity,
          _tracksParent
        );
        
        if (withAnim)
          trackGO.GetComponentInChildren<Animation>().enabled = true;
        
        TrackTrigger trackTrigger = trackGO.GetComponent<TrackTrigger>();
        trackTrigger.Construct(this);
        
        _nextSpawnTrackPosition.z += 30;
      }
    }
  }
}