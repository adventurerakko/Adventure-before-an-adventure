using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class Talkable : MonoBehaviour
{
    enum States
    {
        NotTalking, CurrentlyTalking
    }

    private class DialogueData
    {
        public string[] dialoguelines;
    }
    [SerializeField] TextAsset dialogueFile;
    DialogueData dialogueData;
    string[] dialoguelines;
    int currentLineIndex = 0;
    int maxLineIndex = 0;
    void Start()
    {
        dialogueData = JsonUtility.FromJson<DialogueData>(dialogueFile.text);
        maxLineIndex = dialogueData.dialoguelines.Length;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject == ReferenceManager.instance.Player)
        {
            DialogueManager.instance.AddTalkTarget(this.gameObject);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if(other.gameObject == ReferenceManager.instance.Player)
        {
            DialogueManager.instance.RemoveTalkTarget(this.gameObject);
        }
    }
    public void Talk()
    {
        print(dialogueData.dialoguelines[currentLineIndex]);
        currentLineIndex++;
        if(currentLineIndex >= maxLineIndex)
        {
            currentLineIndex = 0;
        }
    }
}