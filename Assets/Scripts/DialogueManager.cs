using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager instance = null;
    GameObject player;
    [SerializeField] List<GameObject> talkTargetsInRange = new List<GameObject>();
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
    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }
    public void AddTalkTarget(GameObject target)
    {
        talkTargetsInRange.Add(target);
    }
    public void RemoveTalkTarget(GameObject target)
    {
        talkTargetsInRange.Remove(target);
    }
    public void Talk()
    {
        GameObject nearestTalkTarget = null;
        float nearestTalkTargetDistance = Mathf.Infinity;
        foreach (GameObject target in talkTargetsInRange)
        {
            float targetDistance = Vector3.Distance(target.transform.position, player.transform.position);
            if (targetDistance < nearestTalkTargetDistance)
            {
                nearestTalkTargetDistance = targetDistance;
                nearestTalkTarget = target;
            }
        }
        nearestTalkTarget.GetComponent<Talkable>().Talk();
    }
}
