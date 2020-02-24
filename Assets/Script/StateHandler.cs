using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "General/States/StateHandler")]
public class StateHandler : ScriptableObject
{
    public State currentState { get; private set; }
    [SerializeField] private State[] allStatesOrdered;

    [SerializeField] private GameEvent collidersInLocation;
    [SerializeField] private GameEvent collidersNotInLocation;
    [SerializeField] private State moveObjectState;
    [SerializeField] private State dropObjectState;

    private int currentStateIndex;

    private void OnEnable()
    {
        currentState = allStatesOrdered[0];
        currentStateIndex = 0;

        collidersInLocation.AddEvent(SetStateToMoveObject);
        collidersNotInLocation.AddEvent(SetStateToDropObject);
    }

    void SetStateToMoveObject()
    {
        SetState(moveObjectState);
    }

    void SetStateToDropObject()
    {
        SetState(dropObjectState);
    }

    public void SetState(State _state)
    {
        if(currentState == _state)
        {
            return;
        }
        else
        {
            currentState = _state;
        }
    }

    public void SwitchToNextState()
    {
        if(currentStateIndex < allStatesOrdered.Length)
        {
            currentStateIndex++;
            currentState = allStatesOrdered[currentStateIndex];
        }
    }
}
