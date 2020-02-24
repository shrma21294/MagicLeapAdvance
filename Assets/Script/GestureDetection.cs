using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.MagicLeap;

public class GestureDetection : MonoBehaviour
{
    // We define an array of MLHandKeyPoses; predefined by ML itself. In total there are 9 poses (given NoPose isn't a pose but we'll add it just in case).
    private MLHandKeyPose[] spawnIndicatorPose = new MLHandKeyPose[10];

    [SerializeField] private GameObject targetPrefab;
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private Transform spawnPoint2;

    private void Start()
    {
        //Here we call Start on the hands to initialize the ML Hand module.
        MLHands.Start();

        // We assign each of the predefined poses to the array elements.
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

        // We let the pose manager know what kind of poses we need and enable them. Normally we'd define only the ones we need, however due to a bug in tracking we have to define all of them
        MLHands.KeyPoseManager.EnableKeyPoses(spawnIndicatorPose, true, false);

        MLHands.Right.OnKeyPoseBegin += HandPoseReaction;
        MLHands.Left.OnKeyPoseBegin += HandPoseReaction;
    }

    // We subscribe to the event "OnKeyPoseBegin" which basically fires right when a gesture is being detected. 
    //private void OnEnable()
    //{
    //    MLHands.Right.OnKeyPoseBegin += HandPoseReaction;
    //    MLHands.Left.OnKeyPoseBegin += HandPoseReaction;
    //}

    // Normally you'd put here your reactive code; what would happen when a specific gesture is recognized. Here we simply just print the gesture's name on screen.
    private void HandPoseReaction(MLHandKeyPose _pose)
    {
        Debug.Log(_pose.ToString());
    }

    private void Update()
    {
        //if (GetGesture(MLHands.Right, MLHandKeyPose.Thumb) || GetGesture(MLHands.Left, MLHandKeyPose.Thumb))
        //    Debug.Log("Get Gesture recognized Thumb gesture.");

        if (GetGesture(MLHands.Right, MLHandKeyPose.L))
        {
            Debug.Log("Right hand L gesture.");
            Instantiate(targetPrefab, spawnPoint.position, Quaternion.identity);
        }
            

        if (GetGesture(MLHands.Left, MLHandKeyPose.Fist))
        {
            Debug.Log("Left hand Fist gesture.");
            Instantiate(targetPrefab, spawnPoint2.position, Quaternion.identity);
        }
            
            
    }

    // This checks whether the hand given has the pose we want. Originally this should use a confidence measure (how accurate the hand pose is) but due it not being accurately tracked it was
    // removed
    private bool GetGesture(MLHand _hand, MLHandKeyPose _type)
    {
        if (_hand != null && _hand.KeyPose == _type)
            return true;

        return false;
    }

    // We stop the MLHands module given we no longer need it at this point. But before that, we unsubscribe from the events given we are no longer active at this moment.
    private void OnDisable()
    {
        MLHands.Right.OnKeyPoseBegin -= HandPoseReaction;
        MLHands.Left.OnKeyPoseBegin -= HandPoseReaction;

        MLHands.Stop();
    }
}
