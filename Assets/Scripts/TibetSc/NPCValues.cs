using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using GameCreator.Runtime.Variables;

public class NPCValues : MonoBehaviour
{
    public GlobalNameVariables AIvariables;
    [TextArea(1, 20)]
    public string NPC_role;
    public string NPC_nick;
    public string NPC_start_text;
    public Sprite NPC_sprite;
    private void OnEnable()
    {
        AIvariables.Set("LastAITrigger", transform.parent.GetChild(0).gameObject);
        AIvariables.Set("AIRole", NPC_role);
        AIvariables.Set("AIStartRole", NPC_start_text);
        AIvariables.Set("AIPNG", NPC_sprite);
        OpenAIController.instance.StartChat(NPC_role, NPC_start_text,NPC_sprite, NPC_nick);
        gameObject.SetActive(false);
    }
}
