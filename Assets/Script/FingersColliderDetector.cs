using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.MagicLeap;

public class FingersColliderDetector : MonoBehaviour
{
    [SerializeField] private GameEvent moveObjectWithPinchEvent;
    [SerializeField] private GameEvent dropObjectWithEvent;
    private int colldersCount = 0;

    private void OnTriggerEnter(Collider other)
    {
        FingerTracker fingerTracker = other.GetComponent<FingerTracker>();

        if (fingerTracker != null)
            colldersCount++;

        if (colldersCount == 2)
            moveObjectWithPinchEvent.RaiseEvent();
    }

    private void OnTriggerExit(Collider other)
    {
        FingerTracker fingerTracker = other.GetComponent<FingerTracker>();

        if (fingerTracker != null)
            colldersCount--;

        if (colldersCount < 0)
            colldersCount = 0;

        if (colldersCount < 2)
            dropObjectWithEvent.RaiseEvent();
    }
}
