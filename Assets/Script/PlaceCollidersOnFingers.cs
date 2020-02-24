using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.MagicLeap;

public class PlaceCollidersOnFingers : MonoBehaviour
{
    [SerializeField] private Transform trackerPrefab;
    private Transform[] trackers;

    // Start is called before the first frame update
    void Start()
    {
        trackers = new Transform[4];

        for (int i=0; i<4; i++)
        {
            trackers[i] = Instantiate(trackerPrefab);
            trackers[i].gameObject.SetActive(false);
        }
    }

    public void SetCollidersPositions(Vector3 _rightIndexFinger, Vector3 _rightThumbFinger, Vector3 _leftIndexFinger, Vector3 _leftThumbFinger)
    {
        if(trackers[0].gameObject.activeSelf == false)
        {
            for(int i=0; i<trackers.Length; i++)
            {
                trackers[i].gameObject.SetActive(true);
            }
        }

        trackers[0].position = _rightIndexFinger;
        trackers[1].position = _rightThumbFinger;
        trackers[2].position = _leftIndexFinger;
        trackers[3].position = _leftThumbFinger;
    }
}
