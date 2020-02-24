using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.MagicLeap;

public class someScript : MonoBehaviour
{
    //create a variable for the object we want to manipulate the color of the object
    //create a variable for a first color 
    //create a variable for second color
    //create a variable to cache the MeshRenderer of the object we will manipulate


    // in the Update() method, call a custom method you've created

    // in the custom method, you create and write an if/else condition
    //if the first condition is true, the color should change to our first color
    //if the second condition is true, the color should change to our second color.

    [SerializeField]
    private GameObject targetObject;
    [SerializeField]
    private Color firstColor;
    [SerializeField]
    private Color secondColor;
    private MeshRenderer objectMaterial;


    // Start is called before the first frame update
    void Start()
    {
        objectMaterial = targetObject.GetComponent<MeshRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        changeColor();
    }

    void changeColor()
    {
        //blink right eye to change to first color
        if (Input.GetKeyDown("r"))
        {
            objectMaterial.material.color = firstColor;
        }

        //blink with left eye to change to second color
        if (Input.GetKeyDown("l"))
        {
            objectMaterial.material.color = secondColor;
        }
    }
}
