using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameCreator.Runtime.VisualScripting;


public class QuestMain : MonoBehaviour
{
    public static QuestMain instance;
    public bool activeQuest;
    public GameObject canvas;
    public GameObject questObject;
    public GameObject questEnemy;
    public Actions myAct;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        
        if (!questObject.activeSelf)
        {
            activeQuest = true;
        }
        else
        {
            activeQuest = false;
        }
    }

    private void OnEnable()
    {
    }

    public void AcceptButton()
    {
        activeQuest = true;
        _ = myAct.Run();
        canvas.gameObject.SetActive(false);
        questObject.SetActive(false);
    }

    public void RejectButton()
    {
        _ = myAct.Run();
        canvas.gameObject.SetActive(false);
        
    }

    public void AskForQuest()
    {
        if (activeQuest)
        {
            QuestComplete.instance.completeObject.SetActive(true);
            activeQuest = false;
        }
    }
}
