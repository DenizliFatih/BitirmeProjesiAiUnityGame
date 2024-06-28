using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CompleteCall : MonoBehaviour
{
    private void OnEnable()
    {
        QuestMain.instance.activeQuest = false;
    }
}
