using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class StoryEvents : MonoBehaviour
{
    [SerializeField] string eventName = "";
    [SerializeField] string initialEventValue = "";
    private void Start()
    {
        EventManager.instance.AddStoryEvents(eventName, initialEventValue);
    }
    private void OnTriggerEnter(Collider other)
    {
        EventManager.instance.UpdateStoryEvents(eventName, "passed");
    }
    private void OnTriggerExit(Collider other)
    {
        EventManager.instance.UpdateStoryEvents(eventName, "notpassed");
    }
}
