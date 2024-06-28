using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestText : MonoBehaviour
{
    private void OnEnable()
    {
        if (QuestMain.instance.activeQuest)
        {
            transform.GetChild(2).gameObject.SetActive(true);
        }
        else
        {
            transform.GetChild(2).gameObject.SetActive(false);
        }
    }
}
