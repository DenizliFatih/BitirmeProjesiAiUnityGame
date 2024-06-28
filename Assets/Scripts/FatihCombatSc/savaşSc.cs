using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CagataySc;
using Cinemachine;
using GameCreator.Runtime.VisualScripting;
using GameCreator.Runtime.Variables;

public class savaşSc : Singleton<savaşSc>
{

    public Actions myAct;
    public GameObject battleCam;
    public GameObject battleCamPos;
    public GameObject combatManagerParentObj;
    public GlobalNameVariables enemyVar;
    public GameObject completeObj;
    // Start is called before the first frame update
  
    private void OnEnable()
    {
        StartCoroutine(StartBattle());
    }


    public IEnumerator StartBattle()
    {
        MoveAndReplaceCamera1();
        battleCam.GetComponent<CinemachineVirtualCamera>().Priority = 3;
        yield return new WaitForSeconds(2f);
        combatManagerParentObj.SetActive(true);
        EventsHandler.OnCombatStart.Invoke(PlayerScFati.instance.newPlayer, PlayerScFati.instance.newAi);
    }
    public void MoveAndReplaceCamera1()
    {
        var oldParent = battleCam.transform.parent;
        battleCam.transform.SetParent(battleCamPos.transform,true);
        battleCam.transform.localRotation = Quaternion.Euler(Vector3.zero);
        battleCam.transform.localPosition = Vector3.zero;
        StartCoroutine(DelayCam(oldParent));
    }

    private IEnumerator DelayCam(Transform parent)
    {
        yield return new WaitForSeconds(1.5f) ;
        battleCam.transform.SetParent(parent, true);
    }

    public void MoveAndReplaceCamera2()
    {
        var oldParent = battleCam.transform.parent;
        battleCam.transform.parent = battleCamPos.transform;
        battleCam.transform.localPosition = Vector3.zero;
        battleCam.transform.localEulerAngles = Vector3.zero;
        battleCam.transform.parent = oldParent;
    }
    public void EnableCharacterController()
    {
        
        battleCam.GetComponent<CinemachineVirtualCamera>().Priority = 1;
        
        PlayerScFati.instance.enemyAreas[0].GetComponent<SphereCollider>().enabled = false;
        PlayerScFati.instance.enemyAreas.RemoveAt(0);
        completeObj.SetActive(true);
        
        _ = myAct.Run();
        this.gameObject.SetActive(false);

    }


}
