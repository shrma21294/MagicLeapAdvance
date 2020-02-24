using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.MagicLeap;

public static class MoveObjectWithGesture
{
    public static Vector3 MoveObjectToPosition(MLHand _hand)
    {
        return _hand.Index.Tip.Position;
    }

    public static Vector3 MoveObjectBetweenTwoFingers(MLHand _hand)
    {
        Vector3 position = (_hand.Index.Tip.Position + _hand.Thumb.Tip.Position) / 2;

        return position;
    }
}
