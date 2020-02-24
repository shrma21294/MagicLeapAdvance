using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.MagicLeap;

public class TrackGestures : MonoBehaviour
{
    private MLHandKeyPose[] spawnIndicatorPose = new MLHandKeyPose[10];

    private void Start()
    {
        MLHands.Start();

        spawnIndicatorPose[0] = MLHandKeyPose.Fist;
        spawnIndicatorPose[1] = MLHandKeyPose.Pinch;
        spawnIndicatorPose[2] = MLHandKeyPose.Ok;
        spawnIndicatorPose[3] = MLHandKeyPose.OpenHand;
        spawnIndicatorPose[4] = MLHandKeyPose.Finger;
        spawnIndicatorPose[5] = MLHandKeyPose.Thumb;
        spawnIndicatorPose[6] = MLHandKeyPose.L;
        spawnIndicatorPose[7] = MLHandKeyPose.C;
        spawnIndicatorPose[8] = MLHandKeyPose.NoHand;
        spawnIndicatorPose[9] = MLHandKeyPose.NoPose;

        MLHands.KeyPoseManager.EnableKeyPoses(spawnIndicatorPose, true, false);

    }

    public static bool GetGesture(MLHand _hand, MLHandKeyPose _type)
    {
        if (_hand != null && _hand.KeyPose == _type)
            return true;

        return false;
    }

    private void OnDisable()
    {
        MLHands.Stop();
    }
}
