using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class PlayerInput : MonoBehaviour
{
    public bool CheckPickThrowInput()
    {
        return Input.GetButtonDown("PickThrow");
    }
    public bool CheckAttackInput()
    {
        return Input.GetButtonDown("Attack");
    }
    public bool CheckDodgeInput()
    {
        return Input.GetButtonDown("Dodge");
    }
    public Vector3 CheckMoveInput()
    {
        float horizontalMovement = 0;
        float verticalMovement = 0;
        verticalMovement += Input.GetButton("Up") ? 1 : 0;
        verticalMovement += Input.GetButton("Down") ? -1 : 0;
        horizontalMovement += Input.GetButton("Left") ? -1 : 0;
        horizontalMovement += Input.GetButton("Right") ? 1 : 0;
        Vector3 movementVector = new Vector3(horizontalMovement, 0, verticalMovement);
        return movementVector;
    }
    public bool CheckCameraLockInput()
    {
        return Input.GetButton("CameraLock");
    }
    public float CheckCameraRotateInput()
    {
        float rotationAxis = 0;
        if (Input.GetButton("RotateCameraLeft") || Input.GetButton("RotateCameraRight"))
        {
            if (Input.GetButton("RotateCameraLeft"))
                rotationAxis -= 1;
            if (Input.GetButton("RotateCameraRight"))
                rotationAxis += 1;
        }
        return rotationAxis;
    }
}