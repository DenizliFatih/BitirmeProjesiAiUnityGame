using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Skills : MonoBehaviour
{

    public List<Button> SkillsButtonList = new List<Button>();
    public List<int> SkillsRefillTurnList = new List<int>();
    public List<int> SkillsWaitTurnCounterList = new List<int>();


    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        //if (Input.GetKeyDown(KeyCode.Q))
        //{
        //    //StartCoroutine(CombatScript.instance.Player1stAttack(20, SkillsButtonList[0]));
        //}
        //if (Input.GetKeyDown(KeyCode.W))
        //{
        //    StartCoroutine(CombatScript.instance.Player2ndAttack(20, SkillsButtonList[1]));
        //}
        //if (Input.GetKeyDown(KeyCode.E))
        //{
        //    StartCoroutine(CombatScript.instance.Player3rdAttack(20, SkillsButtonList[2]));
        //}
        //if (Input.GetKeyDown(KeyCode.R))
        //{
        //    StartCoroutine(CombatScript.instance.Player4thAttack(20, SkillsButtonList[3]));
        //}

    }

    public void Skill1ClickingEvent()
    {
        //StartCoroutine(CombatScript.instance.Player1stAttack(20, SkillsButtonList[0]));
    }

    public void Skill2ClickingEvent()
    {
        StartCoroutine(CombatScript.instance.Player2ndAttack(20, SkillsButtonList[1]));
    }

    public void Skill3ClickingEvent()
    {
        StartCoroutine(CombatScript.instance.Player3rdAttack(20, SkillsButtonList[2]));
    }

    public void Skill4ClickingEvent()
    {
        StartCoroutine(CombatScript.instance.Player4thAttack(20, SkillsButtonList[3]));
    }
}
