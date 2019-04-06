using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReferenceManager : MonoBehaviour
{
    public static ReferenceManager instance = null;
    public GameObject player;
    public Dictionary<string, string> referenceDictionary = new Dictionary<string, string>();
    private void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);

        player = GameObject.FindGameObjectWithTag("Player");
        referenceDictionary.Add("LAYERPLAYER", "Player");
        referenceDictionary.Add("LAYERENEMY", "Enemy");
        referenceDictionary.Add("LAYEROBJECT", "Object");
    }
    private void Start()
    {
        
    }
}
