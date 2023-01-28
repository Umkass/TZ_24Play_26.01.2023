namespace States
{
  public interface IState: IExitableState
  {
    void Enter();
  }
}