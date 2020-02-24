using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "General/Game Event")]
public class GameEvent : ScriptableObject
{
    private UnityEvent currentEvent;

    private List<UnityAction> actions = new List<UnityAction>();
    // Start is called before the first frame update
    
    public void AddEvent(UnityAction _action)
    {
        if (actions.Contains(_action))
            return;

        actions.Add(_action);
        currentEvent.AddListener(_action);
    }

    public void RaiseEvent()
    {
        currentEvent.Invoke();
    }

    public void RemoveEvent(UnityAction _action)
    {
        if (actions.Contains(_action) == false)
            return;

        actions.Remove(_action);
        currentEvent.RemoveListener(_action);
    }

    private void OnDisable()
    {
        currentEvent.RemoveAllListeners();
    }
}
