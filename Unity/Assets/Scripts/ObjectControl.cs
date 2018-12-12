using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectControl : MonoBehaviour {

    private ButtonHandler moveButtonHandler;
    private ButtonHandler rotateButtonHandler;
    private ButtonHandler scaleButtonHandler;
    private CustomHandModelManager handModelManager;

    private GameObject focused;

    // Use this for initialization
    void Start () {
        handModelManager = GameObject.Find("HandModels").GetComponent<CustomHandModelManager>();
        moveButtonHandler = GameObject.Find("MoveImageButton").GetComponent<ButtonHandler>();
        rotateButtonHandler = GameObject.Find("RotateImageButton").GetComponent<ButtonHandler>();
        scaleButtonHandler = GameObject.Find("ScaleImageButton").GetComponent<ButtonHandler>();
        focused = null;
    }
	
	// Update is called once per frame
	void Update () {
		if(focused != null)
        {
            if (moveButtonHandler.isPressed)
            {

            }
        }
	}

    public void Focus(GameObject target)
    {
        focused = target;
    }
}
