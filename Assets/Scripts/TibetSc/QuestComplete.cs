using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameCreator.Runtime.VisualScripting;


public class QuestComplete : MonoBehaviour
{
    public static QuestComplete instance;
    public GameObject canvas;
    public GameObject completeObject;
    public Actions myAct;

    private void Awake()
    {
        instance = this;
    }

    public void ButtonAct()
    {
        _ = myAct.Run();
        canvas.gameObject.SetActive(false);
        completeObject.SetActive(false);
    }


}
