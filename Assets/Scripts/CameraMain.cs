using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
public class CameraMain : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] GameObject target = null;
    [SerializeField] float cameraOffsetDistance = 40, smoothFollowSpeed = 0.1F, rotationSpeed = 120F;
    Vector3 smoothFollowTarget = Vector3.zero;
    void Start()
    {
        Assert.IsNotNull(target);
    }

    // Update is called once per frame
    void Update()
    {
        smoothFollowTarget = Vector3.Lerp(smoothFollowTarget, target.transform.position, smoothFollowSpeed);
        this.transform.position = smoothFollowTarget + -transform.forward * cameraOffsetDistance;
    }
    public void RotateCamera(float rotationAxis)
    {
        this.transform.rotation = Quaternion.Euler(this.transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y + (rotationAxis * rotationSpeed * Time.deltaTime), this.transform.rotation.eulerAngles.z);
        this.transform.position = smoothFollowTarget + -transform.forward * cameraOffsetDistance;

    }
}