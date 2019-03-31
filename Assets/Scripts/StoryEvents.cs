using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class StoryEvents : MonoBehaviour
{
    [SerializeField] string eventName = "";
    [SerializeField] string initialEventValue = "";
    [SerializeField] EventManager eventManager;
    private void Start()
    {
        Assert.IsNotNull(eventManager);
        eventManager.AddStoryEvents(eventName, initialEventValue);
    }
    private void OnTriggerEnter(Collider other)
    {
        eventManager.UpdateStoryEvents(eventName, "passed");
    }
    private void OnTriggerExit(Collider other)
    {
        eventManager.UpdateStoryEvents(eventName, "notpassed");
    }
}
