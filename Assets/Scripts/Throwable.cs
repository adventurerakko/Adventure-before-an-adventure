using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
public class Throwable : MonoBehaviour
{
    [SerializeField]Collider collider;
    Rigidbody rigidbody;
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
    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
        collider = GetComponent<Collider>();
    }
    public void PickUpThrowable(Transform parentTransform)
    {
        currentState = States.IsEquipped;
        this.transform.parent = parentTransform;
        rigidbody.isKinematic = true;
    }
    public void ThrowThrowable(float force, Vector3 throwDirection)
    {
        StartCoroutine(ThrowBuffer());
        rigidbody.isKinematic = false;
        rigidbody.AddForce((throwDirection + new Vector3(0, 1, 0)) * force, ForceMode.Impulse);        
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
        if (collision.gameObject.tag == "Player" || collision.gameObject.tag == "Enemy" || collision.gameObject.tag == "Object")
        {
            DamageTarget(collision.gameObject);
            DamageThis();
            Destroy(this.gameObject);
            return;
        }
        
        if (currentState == States.IsThrown && Physics.Raycast(transform.position, Vector3.down, collider.bounds.extents.y + 0.1f))
        {
            rigidbody.isKinematic = true;
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
