using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorManager : MonoBehaviour
{
    Texture2D _original;
    Texture2D _leftoriginal;
    private bool _mouseDownCheck = false;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Confined;
        _original = Resources.Load<Texture2D>("Cursor/Basic/Basic");
        _leftoriginal = Resources.Load<Texture2D>("Cursor/Basic/LeftBasic");
    }

    private void Update()
    {

        if (Input.GetMouseButtonDown(0))
        {
            LeftBasicMouse();
            _mouseDownCheck = true;

        }


        if (Input.GetMouseButtonUp(0) && _mouseDownCheck)
        {
            BasicMouse();
            _mouseDownCheck = false;
        }




    }
    public void LeftBasicMouse()
    {

        Cursor.SetCursor(_leftoriginal, new Vector2(0, 0), CursorMode.Auto);

    }

    public void BasicMouse()
    {
        Cursor.SetCursor(_original, new Vector2(0, 0), CursorMode.Auto);
    }
}
