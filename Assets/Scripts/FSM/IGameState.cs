public interface IGameState
{
    public void Enter();
    public void Exit();
    public IGameState GetNewState();
}