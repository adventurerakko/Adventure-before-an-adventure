using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class RegisterStoryEvents : MonoBehaviour
{
    [SerializeField] string eventName = "";
    [SerializeField] string initialEventValue = "";
    [SerializeField] string onTriggerEnterEventValue = "passed";
    [SerializeField] string onTriggerExitEventValue = "notpassed";
    private void Start()
    {
        EventManager.instance.AddStoryEvents(eventName, initialEventValue);
    }
    private void OnTriggerEnter(Collider other)
    {
        EventManager.instance.UpdateStoryEvents(eventName, onTriggerEnterEventValue);
    }
    private void OnTriggerExit(Collider other)
    {
        EventManager.instance.UpdateStoryEvents(eventName, onTriggerExitEventValue);
    }
}