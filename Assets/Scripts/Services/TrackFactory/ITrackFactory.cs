namespace Services.TrackFactory
{
  public interface ITrackFactory : IService
  {
    public void CreateTracks(int numberOfTracks, bool withAnim);
  }
}