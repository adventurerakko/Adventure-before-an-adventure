using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReferenceManager : MonoBehaviour
{
    public static ReferenceManager instance = null;
    public Dictionary<string, string> referenceDictionary = new Dictionary<string, string>();
    public GameObject Player { get {
            return player;
        } }
    private GameObject player;
    
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
        referenceDictionary.Add("CONST_LAYERPLAYER", "Player");
        referenceDictionary.Add("CONST_LAYERENEMY", "Enemy");
        referenceDictionary.Add("CONST_LAYEROBJECT", "Object");
    }
}
