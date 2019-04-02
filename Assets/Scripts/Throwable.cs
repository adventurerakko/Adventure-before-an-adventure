using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
public class Throwable : MonoBehaviour
{
    Collider collider;
    bool isEquipped = false;
    // Start is called before the first frame update
    void Start()
    {
        collider = GetComponent<Collider>();
    }
    public void PickUpThrowable()
    {
        isEquipped = true;
    }
    public void ThrowThrowable()
    {
        isEquipped = false;
        this.transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
    }
}
