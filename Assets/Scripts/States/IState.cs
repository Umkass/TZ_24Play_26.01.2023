namespace States
{
  public interface IState : IExitableState
  {
    public void Enter();
  }
}