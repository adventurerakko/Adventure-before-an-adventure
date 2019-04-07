using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Collider))]
public class Throwable : MonoBehaviour
{
    Rigidbody rb;
    Collider col;
    [SerializeField] int throwDamage = 2;
    [SerializeField] int health = 2;
    int Health
    {
        get
        {
            return health;
        }
        set
        {
            health = value;
            if(health <= 0)
            {
                Destroy(this.gameObject);
            }
        }
    }
    enum States
    {
        IsEquipped, IsRooted, IsThrown
    }
    [SerializeField] States currentState = States.IsRooted;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        col = GetComponent<Collider>();
    }
    public void PickUpThrowable(Transform parentTransform)
    {
        currentState = States.IsEquipped;
        this.transform.parent = parentTransform;
        rb.isKinematic = true;
    }
    public void ThrowThrowable(float force, Vector3 throwDirection)
    {
        StartCoroutine(ThrowBuffer());
        rb.isKinematic = false; // must make non-kinematic before applying force
        rb.AddForce((throwDirection + new Vector3(0, 1, 0)) * force, ForceMode.Impulse);        
        this.transform.parent = null;
        this.transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
    }
    IEnumerator ThrowBuffer()
    {
        yield return new WaitForSeconds(0.2f);
        currentState = States.IsThrown;
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == ReferenceManager.instance.referenceDictionary["CONST_LAYERPLAYER"] || collision.gameObject.tag == ReferenceManager.instance.referenceDictionary["CONST_LAYERENEMY"] || collision.gameObject.tag == ReferenceManager.instance.referenceDictionary["CONST_LAYEROBJECT"])
        {
            DamageTarget(collision.gameObject);
            DamageThis();
            Destroy(this.gameObject);
            return;
        }
        
        if (currentState == States.IsThrown && Physics.Raycast(transform.position, Vector3.down, col.bounds.extents.y + 0.1f))
        {
            rb.isKinematic = true;
            currentState = States.IsRooted;
            return;
        }
    }
    void DamageThis()
    {
        Health -= 1;
    }
    void DamageTarget(GameObject target)
    {
    }
}
