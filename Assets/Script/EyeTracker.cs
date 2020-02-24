using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.MagicLeap;

public class EyeTracker : MonoBehaviour
{
    //add a variable refrencing a prefab we will instantiate

    [SerializeField] private GameObject targetPrefab;
   // [SerializeField] private Transform spawnPoint;

    [SerializeField] private Transform tracker;
    [SerializeField] private Color leftEyeBlinkColor;
    [SerializeField] private Color rightEyeBlinkColor;
    private Renderer trackerRenderer;

    // This is where we get a reference to the renderer of the object we're tracking. This will allows us to change the material for the object. 
    private void Awake()
    {
        trackerRenderer = tracker.GetComponent<MeshRenderer>();
    }

    //In Start we launch the MLEyes and activates it so we can use it.
    private void Start()
    {
        MLEyes.Start();
    }

    private void Update()
    {
        // We check if MLEyes has started or not. If MLEyes didn't start we return and don't do anything.
        if (MLEyes.IsStarted == false)
            return;

        // The tracker's position is set based on the FixationPoint which is basically where the eyes are lookign at, at the moment.
        //tracker.position = MLEyes.FixationPoint;

        //instantiate the prefab
        Instantiate(targetPrefab, MLEyes.FixationPoint, Quaternion.identity);

        ChangeColorOnBlink();
    }

    // Here we check which eyes are blinking, depedning on which eye is blinking we change the color of the object.
    private void ChangeColorOnBlink()
    {
        if (MLEyes.LeftEye.IsBlinking)
            trackerRenderer.material.color = leftEyeBlinkColor;
        else if (MLEyes.RightEye.IsBlinking)
            trackerRenderer.material.color = rightEyeBlinkColor;
    }

    private void OnDisable()
    {
        MLEyes.Stop();
    }
}
