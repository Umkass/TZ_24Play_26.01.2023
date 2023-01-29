namespace States
{
  public interface IPayloadedState<TPayload> : IExitableState
  {
    public void Enter(TPayload payload);

    public void ReEnter();
  }
}