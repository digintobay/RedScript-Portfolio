using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Quest : MonoBehaviour
{
    public GameObject InGameFirstMeetWolfPanel;

    public TextMeshProUGUI RoadInfo;
    public TextMeshProUGUI NameInfo;

    public bool firstMeet = false;

    public string thisname;
    public string description;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            RoadInfo.text = thisname;
            NameInfo.text = description;
        }

     
    }
}
