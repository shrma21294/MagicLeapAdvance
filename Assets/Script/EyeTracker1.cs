using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.MagicLeap;

public class EyeTracker1 : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        MLEyes.Start();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public bool IsBlinking()
    {
        if (MLEyes.IsStarted)
        {
            if (MLEyes.LeftEye.IsBlinking || MLEyes.RightEye.IsBlinking)
            {
                return true;
            }
        }
        return false;
    }

    private void OnDisable()
    {
        MLEyes.Stop();
    }
}
