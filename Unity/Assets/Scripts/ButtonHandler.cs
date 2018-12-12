using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Assertions;
using UnityEngine.EventSystems;

public class ButtonHandler : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{

    public GameObject prefab;

    public bool isPressed;

    // Use this for initialization
    void Start ()
    {
        Debug.Log("ButtonHandler init");
        Debug.Log(gameObject.name);
        if (gameObject.name == "CubeImageButton")
        {
            gameObject.GetComponent<Button>().onClick.AddListener(OnClickButton);
        }

        isPressed = false;
    }

    private void OnClickButton()
    {
        Debug.Log("I was clicked");
        GameObject newCube = instantiate(prefab);
    }

    private GameObject instantiate(GameObject prefab)
    {
        return Instantiate(prefab, new Vector3(0, 0, 0), Quaternion.identity);
    }

    public void OnPointerDown(PointerEventData e)
    {
        Debug.Log("pointer down");
        isPressed = true;
    }
    public void OnPointerExit()
    {
        Debug.Log("pointer exit");
        isPressed = false;
    }
    public void OnPointerUp(PointerEventData e)
    {
        Debug.Log("pointer up");
        isPressed = false;
    }
}
