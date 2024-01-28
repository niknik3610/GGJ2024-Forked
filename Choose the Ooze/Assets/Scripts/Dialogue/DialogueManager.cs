using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueManager : MonoBehaviour
{
    /*private static DialogueManager INSTANCE;

    public static DialogueManager GetInstance()
    {
        if(INSTANCE == null)
        {
            INSTANCE = new DialogueManager();
        }
        return INSTANCE;
    }*/
    public Dialogues dialogues;
    public string generateCustomerDialogue(ClientType type, ClientRequest request)
    {
        string result = "";
        for(int i = 0; i < dialogues.characterDialogues.Length; i++)
        {
            if(dialogues.characterDialogues[i].client == type)
            {
                int rand = Random.Range(0, dialogues.characterDialogues[i].introLines.Length);
                result += dialogues.characterDialogues[i].introLines[rand];
                break;
            }
        }
        for(int i = 0; i < request.Request_required_ingredients.Count; i++)
        {
            var(emotion, severity) = request.Request_required_ingredients[i];
            for(int j = 0; j < dialogues.ailmentDialogues.Length; j++)
            {
                if(dialogues.ailmentDialogues[j].emotion == emotion && dialogues.ailmentDialogues[j].severity == severity)
                {
                    int rand = Random.Range(0, dialogues.ailmentDialogues[j].dialogueLines.Length);
                    result += dialogues.ailmentDialogues[j].dialogueLines[rand];
                    if(i < request.Request_required_ingredients.Count - 1)
                    {
                        result += "Also, ";
                    }
                    break;
                }
            }
        }
        for(int i = 0; i < dialogues.characterDialogues.Length; i++)
        {
            if(dialogues.characterDialogues[i].client == type)
            {
                int rand = Random.Range(0, dialogues.characterDialogues[i].outroLines.Length);
                result += dialogues.characterDialogues[i].outroLines[rand];
                break;
            }
        }
        return result;
    }
}
