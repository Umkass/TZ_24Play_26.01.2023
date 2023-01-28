using Data;
using Services.TrackFactory;
using UnityEngine;

namespace Track
{
  public class TrackTrigger : MonoBehaviour
  {
    private ITrackFactory _trackFactory;
    
    public void Construct(ITrackFactory trackFactory) => 
      _trackFactory = trackFactory;

    private void OnTriggerEnter(Collider other)
    {
      if (other.CompareTag(Tags.Player)) 
        _trackFactory.CreateTracks(1,true);
    }
  }
}