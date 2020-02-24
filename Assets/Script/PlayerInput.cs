using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.MagicLeap;

public class PlayerInput : MonoBehaviour
{
    //create reference to your state handler
    [SerializeField] private StateHandler stateHandler;

    //create reference to your 1st state
    [SerializeField] private State spawnIndicator;

    //create reference to your 2nd state 
    [SerializeField] private State moveIndicator;

    //create reference to your 3rd state
    [SerializeField] private State fireIndicator;

    //create reference to your 4th state 
    [SerializeField] private State jumpIndicator;

    TrackGestures trackGestures;

    [SerializeField] Transform indicatorPrefab;

    Transform currentIndicator = null;

    EyeTracker1 eyeTracker;

    Transform currentObject;

    [SerializeField] Transform objectToSpawn;

    State spawnObjectState;
    State moveObjectState;
    State dropObjectState;

    PlaceCollidersOnFingers placeColliders;

    [SerializeField] private float scaleSensitivity = 2;
    private float originalDistance = float.MinValue;

    private void Awake()
    {
        eyeTracker = GetComponent<EyeTracker1>();
    }


    // Update is called once per frame
    void Update()
    {
        SpawnIndicator();
        MoveIndicator();

        //add 2 custom methods here
        FireIndicator();
        JumpIndicator();

        MoveCollidersToFingers();
        ScaleSpawnedObject();
        
    }

    void SpawnIndicator()
    {
        //first check whether the current state is equal to the SpawnIndicator state
        //if not, return;

        //log a message to the console of the current state you are in
        //if you press a certain key, switch state to the next state

        //create an additional two states along with two matching methods

        if(stateHandler.currentState != spawnIndicator)
            return;

        Debug.Log("In spawn indicator");

        //if some gesture = specify hand and specify the thumb pose (right hand)
        //spawn the indicator prefab

        if (TrackGestures.GetGesture(MLHands.Right, MLHandKeyPose.Thumb) )
        {
            InstantiateObject(MLHands.Right.Center, indicatorPrefab);
        }

        //else if some gesture = specify hand and specify the thumb pose (left hand)
        //spawn the indicator prefab

        if (TrackGestures.GetGesture(MLHands.Left, MLHandKeyPose.Thumb))
        {
            InstantiateObject(MLHands.Left.Center, indicatorPrefab);
        }

        //we want to make sure that the current state is the SpawnIndicator state to execute the code inside this method.

        //if gesture is recogized, do the ocde below
        //write some code that will spaw the indicator
        //change the state to the next state
    }

    void MoveIndicator()
    {
        //first check whether the current state is equal to the MoveIndicator state
        //if not, return;

        //log a message to the console of the current state you are in
        //if you press a certain key, switch state to the next state

        //create an additional two states along with two matching methods

        if (stateHandler.currentState != moveIndicator)
        {
            return;
        }


        Debug.Log("In move indicator");
        //if (TrackGestures.GetGesture(MLHands.Right, MLHandKeyPose.Thumb))
        //{
        //    InstantiateObject(MLHands.Right.Center, indicatorPrefab);
        //}

        //else if some gesture = specify hand and specify the thumb pose (left hand)
        //spawn the indicator prefab

        //if (TrackGestures.GetGesture(MLHands.Left, MLHandKeyPose.Thumb))
        //{
        //    InstantiateObject(MLHands.Left.Center, indicatorPrefab);
        //}

        // check if the we're using the left hand and finger  gesture
        //Current indicator  position = the gesture position

        //do the same with the left hand and finger gesture
        if (TrackGestures.GetGesture(MLHands.Left, MLHandKeyPose.Finger))
        {
            currentIndicator.position = MoveObjectWithGesture.MoveObjectToPosition(MLHands.Left);

        }else if(TrackGestures.GetGesture(MLHands.Right, MLHandKeyPose.Finger))
        {
            currentIndicator.position = MoveObjectWithGesture.MoveObjectToPosition(MLHands.Right);
        }

        if(eyeTracker.IsBlinking() && TrackGestures.GetGesture(MLHands.Right, MLHandKeyPose.Thumb))
        {
            currentIndicator.gameObject.SetActive(false);
            currentObject = Instantiate(objectToSpawn, currentIndicator.position, Quaternion.identity);
            currentIndicator = null;
            stateHandler.SwitchToNextState();
        
        }
           

        // nothing should happen unless the state is the Move idicator state
        // we want to check is the current state is the Move Indicator state (if it is, continue on in this method)

        //if a gesture is recognized, do the code below
        // write some code that will allow

    }

    void FireIndicator()
    {

    }

    void JumpIndicator()
    {

    }

    void InstantiateObject(Vector3 _position, Transform _object)
    {
        currentIndicator = Instantiate(_object, _position, Quaternion.identity);
        stateHandler.SwitchToNextState();
    }

    void MoveCollidersToFingers()
    {
        Debug.Log("In move colliders fingers");
        if (stateHandler.currentState == spawnObjectState || stateHandler.currentState == moveObjectState || stateHandler.currentState == dropObjectState)
            placeColliders.SetCollidersPositions(MLHands.Right.Index.Tip.Position, MLHands.Right.Thumb.Tip.Position, MLHands.Left.Index.Tip.Position, MLHands.Left.Thumb.Tip.Position);
    }

    void ScaleSpawnedObject()
    {
        Debug.Log("In scale spawned object");
        if(stateHandler.currentState == spawnObjectState || stateHandler.currentState == moveObjectState || stateHandler.currentState == dropObjectState)
        {
            if(TrackGestures.GetGesture(MLHands.Right, MLHandKeyPose.Finger) && TrackGestures.GetGesture(MLHands.Left, MLHandKeyPose.Finger))
            {
                if (originalDistance == float.MinValue)
                    originalDistance = Vector3.Distance(MLHands.Right.Center, MLHands.Left.Center);

                float scaleFactor = Vector3.Distance(MLHands.Right.Center, MLHands.Left.Center) - originalDistance;

                scaleFactor = scaleFactor / scaleSensitivity;

                currentObject.localScale = new Vector3(currentObject.localScale.x + scaleFactor, currentObject.localScale.y + scaleFactor, currentObject.localScale.z + scaleFactor);
            }
            else
            {
                if (originalDistance != float.MinValue)
                    originalDistance = float.MinValue;
            }
        }
    }

    private void MoveSpawnedObject()
    {
        if((stateHandler.currentState == spawnObjectState || stateHandler.currentState == moveObjectState)){
            if(TrackGestures.GetGesture(MLHands.Right, MLHandKeyPose.Pinch))
            {
                currentObject.position = MoveObjectWithGesture.MoveObjectBetweenTwoFingers(MLHands.Right);
            }else if (TrackGestures.GetGesture(MLHands.Left, MLHandKeyPose.Pinch))
            {
                currentObject.position = MoveObjectWithGesture.MoveObjectBetweenTwoFingers(MLHands.Left);
            }
        }
    }
}
