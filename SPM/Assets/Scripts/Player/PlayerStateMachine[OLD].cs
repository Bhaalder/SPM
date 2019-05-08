using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace StateHandler
{
    public class PlayerStateMachine<T>
    {

        public PlayerState<T> currentState { get; private set; }
        public T owner;

        public PlayerStateMachine(T owner)
        {
            this.owner = owner;
            currentState = null;
        }

        public void ChangeState(PlayerState<T> newState)
        {
            if(currentState != null)
                currentState.ExitState(owner);

            currentState = newState;
            currentState.EnterState(owner);
        }

        public void Update()
        {
            if (currentState != null)
                currentState.UpdateState(owner);
        }
    }

    public abstract class PlayerState<T>
    {
        public abstract void EnterState(T owner);
        public abstract void ExitState(T owner);
        public abstract void UpdateState(T owner);
    }
}
