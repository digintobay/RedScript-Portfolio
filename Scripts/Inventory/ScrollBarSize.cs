using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScrollBarSize : MonoBehaviour
{
    private void Start()
    {
        Size();
    }

    public void Size()
    {
        transform.GetComponent<Scrollbar>().size = 0.1f;
    }
}
