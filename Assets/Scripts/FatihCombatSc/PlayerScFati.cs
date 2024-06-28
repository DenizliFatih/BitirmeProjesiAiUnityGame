using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using GameCreator.Runtime.VisualScripting;
using GameCreator.Editor.VisualScripting;
using GameCreator.Runtime.Variables;
using CagataySc;
using System;
using UnityEngine.Events;

public class PlayerScFati : MonoBehaviour
{
    public static PlayerScFati instance;


    public Button startCombatButton;
    public GameObject battleCam;
    public List<Transform> enemyAreas;
    public Actions myAct;
    public Trigger trg;
    public NewPlayer newPlayer;
    public NewAI newAi;
    public GlobalNameVariables enemyVariable;

    private void Awake()
    {
        instance = this;
    }
    void Start()
    {

        if (startCombatButton != null)
        {
            startCombatButton.gameObject.SetActive(false);
        }
    }



    private void OnButtonClick()
    {
        _ = trg.Execute();

    }
    private void OnTriggerEnter(Collider other)
    {
        
        if (other.gameObject.CompareTag("Enemy"))
        {
            enemyAreas.Add(other.transform);
            newAi = other.gameObject.GetComponent<NewAI>();
            Debug.Log("Deðdi");
            // Enemy ile temas gerçekleþtiðinde StartCombatButton'u görünür yap

            if (startCombatButton != null)
            {
                startCombatButton.onClick.AddListener(OnButtonClick);
                startCombatButton.gameObject.SetActive(true);
            }
            //InstructionCharacterNavigationMoveTo instMove = startCombatButton.GetComponent<Trigger>().GetComponent<InstructionCharacterNavigationMoveTo>();
            //instMove.
            enemyVariable.Set("enemy1", newAi.gameObject);

        }
    }
    private void OnTriggerExit(Collider other)
    {
        
        if (other.gameObject.CompareTag("Enemy"))
        {
            enemyAreas.Remove(other.transform);

            Debug.Log("Çýktý");
            // Enemy ile temas sona erdiðinde StartCombatButton'u gizle
            if (startCombatButton != null)
            {
                startCombatButton.onClick.RemoveListener(OnButtonClick);
                startCombatButton.gameObject.SetActive(false);
            }
        }
    }

    public void StartBattle( )
    {

        // Start Battle düðmesini devre dýþý býrak
        startCombatButton.gameObject.SetActive(false);

        foreach (Transform enemy in enemyAreas)
        {
            Vector3 newPosition = enemy.position;
            newPosition.y = transform.position.y;  // Karakterin yüksekliðini koru
            newPosition.z -= 5f;  // Z ekseni üzerinde -5 birim hareket et
            transform.position = newPosition;
        }
    }

}
