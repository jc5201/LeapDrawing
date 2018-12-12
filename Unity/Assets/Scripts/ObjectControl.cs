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
                focused.transform.position = focused.transform.position + handModelManager.deltaMovement() * Time.deltaTime * 100;
            }
            if (rotateButtonHandler.isPressed)
            {
                focused.transform.rotation = focused.transform.rotation * handModelManager.deltaRotation();
            }
            if (scaleButtonHandler.isPressed)
            {
                focused.transform.localScale = focused.transform.localScale - handModelManager.deltaFinger() * Time.deltaTime * 100;
            }
        }
	}

    public void Focus(GameObject target)
    {
        focused = target;
    }
}
