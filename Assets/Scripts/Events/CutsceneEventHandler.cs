using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Timeline;

public class CutsceneEventHandler : MonoBehaviour
{
    [SerializeField]
    DialogueTriggerEvent dialogueTriggerEvent;
    [SerializeField]
    DialogueTriggerData dd;
    public void BeginCutscene()
    {
        
    }

    public void PlayDialogue()
    {
        dialogueTriggerEvent.Raise(dd);
    }

    public void EnablePlayer()
    {

    }

}
