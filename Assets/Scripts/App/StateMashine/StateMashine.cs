using UnityEngine;

namespace winterStage
{
    public class StateMashine : MonoBehaviour
    {
        public State CurrentState { get; private set; }

        public void Init(State state)
        {
            CurrentState = state;
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
