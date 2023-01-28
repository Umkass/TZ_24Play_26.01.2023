namespace States
{
  public interface IPayloadedState<TPayload> : IExitableState
  {
    void Enter(TPayload payload);

    public void ReEnter();
  }
}