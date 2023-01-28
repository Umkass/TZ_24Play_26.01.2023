using States;

namespace Services.GameStateMachine
{
  public interface IGameStateMachine : IService
  {
    public void Enter<TState>() where TState : class, IState;
    public void Enter<TState, TPayload>(TPayload payload) where TState : class, IPayloadedState<TPayload>;
    public void ReEnter<TState, TPayload>() where TState : class, IPayloadedState<TPayload>;
  }
}