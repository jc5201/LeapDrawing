using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectHandler : MonoBehaviour {

    private ObjectControl script;

    // Use this for initialization
    void Start () {
        script = GameObject.Find("Canvas").GetComponent<ObjectControl>();
	}

    void OnMouseDown()
    {
        Debug.Log("object focused");
        script.Focus(gameObject);
    }

}
