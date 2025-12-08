using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MeetFriendCollider : MonoBehaviour
{
    public bool[] firstMeet = new bool[4];

    public bool happyEndFinal = false;

    private void Start()
    {
        for (int i=0; i<firstMeet.Length; i++)
        {
            firstMeet[i] = false;
        }
    }

    public void HappyOn()
    {
        happyEndFinal = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Rabbit"))
        {
           
            QuestSystem.Instance.AxcMission("rabbit");
        }

        if (other.CompareTag("Enemy"))
        {
            if (happyEndFinal)
            {
                QuestSystem.Instance.GoToHappyEnd();
            }
            else
            {
                QuestSystem.Instance.AxcMission("wolf");
            }

        
        }

        if (other.CompareTag("Frog"))
        {
            QuestSystem.Instance.AxcMission("frog");
        }

        if (other.CompareTag("Store"))
        {
            QuestSystem.Instance.AxcMission("store");
        }
    }

}
