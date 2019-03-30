using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class PlayerInput : MonoBehaviour
{
    Vector3 movementVector = Vector3.zero;
    float rotationAxis = 0;
    bool dodgeButton = false;
    public float CheckCameraRotate()
    {
        rotationAxis = 0;
        if (Input.GetButton("RotateCameraLeft") || Input.GetButton("RotateCameraRight"))
        {
            if (Input.GetButton("RotateCameraLeft"))
            {
                rotationAxis -= 1;
            }
            if (Input.GetButton("RotateCameraRight"))
            {
                rotationAxis += 1;
            }
            return rotationAxis;
        }
        else
        {
            return rotationAxis;
        }
    }
    public Vector3 CheckMove()
    {
        movementVector = Vector3.zero;
        if (Input.GetButton("Up"))
        {
            movementVector = movementVector + new Vector3(0, 0, 1);
        }
        if (Input.GetButton("Down"))
        {
            movementVector = movementVector + new Vector3(0, 0, -1);
        }
        if (Input.GetButton("Left"))
        {
            movementVector = movementVector + new Vector3(-1, 0, 0);
        }
        if (Input.GetButton("Right"))
        {
            movementVector = movementVector + new Vector3(1, 0, 0);
        }
        if (movementVector != Vector3.zero)
        {
            return movementVector = Vector3.ClampMagnitude(movementVector, 1);
        }
        return movementVector;
    }
    public bool CheckDodge()
    {
        if (Input.GetButton("Dodge"))
        {
            dodgeButton = true;
            return dodgeButton;
        }
        return dodgeButton;
    }
}
