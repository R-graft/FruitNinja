namespace winterStage
{
    public class StateMashine
    {
        public State CurrentState { get; private set; }

        public void Init(State StartState)
        {
            CurrentState = StartState;
            CurrentState.Enter();
        }

        public void SetState(State newState)
        {
            CurrentState.Exit();
            CurrentState = newState;
            CurrentState.Enter();
        }
    }
}
