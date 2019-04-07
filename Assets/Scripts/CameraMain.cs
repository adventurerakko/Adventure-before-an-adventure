using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;
public class CameraMain : MonoBehaviour
{
    [SerializeField] GameObject target = null;
    [SerializeField] float cameraOffsetDistance = 40, smoothFollowSpeed = 0.1F, rotationSpeed = 120F, smoothCameraLockSpeed = 0.1f;
    Vector3 smoothFollowTarget = Vector3.zero;

    void Start()
    {
        Assert.IsNotNull(target);
    }

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
    public void LockCamera(float playerVerticalRotation)
    {
        this.transform.rotation = Quaternion.Lerp(this.transform.rotation, Quaternion.Euler(new Vector3(this.transform.rotation.eulerAngles.x, playerVerticalRotation, this.transform.rotation.eulerAngles.z)), smoothCameraLockSpeed);
    }
    public void LockCamera(float playerVerticalRotation, GameObject lockTarget)
    {
        Quaternion lookRot = Quaternion.LookRotation(lockTarget.transform.position - target.transform.position);
        this.transform.rotation = Quaternion.Lerp(this.transform.rotation, Quaternion.Euler(new Vector3(this.transform.rotation.eulerAngles.x, lookRot.eulerAngles.y, this.transform.rotation.eulerAngles.z)), smoothCameraLockSpeed);
    }
}