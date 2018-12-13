using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectControl : MonoBehaviour {

    private ButtonHandler moveButtonHandler;
    private ButtonHandler rotateButtonHandler;
    private ButtonHandler scaleButtonHandler;
    private CustomHandModelManager handModelManager;

    public Material defaultMaterial;
    public Material focusedMaterial;

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
                focused.transform.rotation =  handModelManager.deltaRotation() * focused.transform.rotation;
            }
            if (scaleButtonHandler.isPressed)
            {
                focused.transform.localScale = focused.transform.localScale + focused.transform.rotation * handModelManager.deltaFinger() * Time.deltaTime * 200 ;
            }
        }
	}

    public void Focus(GameObject target)
    {
        if(focused != null)
            focused.GetComponent<MeshRenderer>().material = defaultMaterial;
        focused = target;
        focused.GetComponent<MeshRenderer>().material = focusedMaterial;
    }

    public GameObject getFocusedObject(){
        return focused;
    }
}
