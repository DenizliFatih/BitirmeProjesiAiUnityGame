using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CagataySc;
using TMPro;
using Cinemachine;
using UnityEngine.UI;

public class CombatEndListener : MonoBehaviour
{
    public GameObject winLosePanel, parentObj;
    public TextMeshProUGUI combatTxt;
    private void OnEnable()
    {
        EventsHandler.OnCombatEnd.Event += CombatEndHandler;
    }

    private void OnDisable()
    {
        EventsHandler.OnCombatEnd.Event -= CombatEndHandler;
    }

    private void CombatEndHandler(bool value)
    {
        winLosePanel.SetActive(true);
        parentObj.SetActive(false);
        if (value)
        {
            combatTxt.text = "KAZANDIN!";
            winLosePanel.GetComponent<Image>().color = new Color(0.0f, 1.0f, 0.0f, 0.15f);
        }
        else
        {
            combatTxt.text = "KAYBETTÝN!";
            winLosePanel.GetComponent<Image>().color = new Color(1.0f, 0.0f, 0.0f, 0.15f);
        }
        //combatTxt.text = value ? "VÝCTORY!" : "LOSE!";
    }
}
