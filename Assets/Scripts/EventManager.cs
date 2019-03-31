using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    public static EventManager instance = null;
    private Dictionary<string, string> storyEvents = new Dictionary<string, string>();

    public delegate void StoryEventDelegate();
    public StoryEventDelegate storyEventDelegate;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }
    public void UpdateStoryEvents(string key, string value)
    {
        storyEvents[key] = value;
        storyEventDelegate();
    }
    public string GetStoryEvents(string key)
    {
        return storyEvents[key];
    }
    public void AddStoryEvents(string key, string value)
    {
        storyEvents.Add(key, value);
    }
    
}