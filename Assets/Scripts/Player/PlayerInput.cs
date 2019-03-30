using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class PlayerInput : MonoBehaviour
{
    float rotationAxis = 0;
    bool dodgeButton = false;
    Animator animator;
    private void Start()
    {
        animator = GetComponent<Animator>();
    }
    private void Update()
    {
        CheckMove();
        CheckDodge();
    }
    void CheckDodge()
    {
        animator.SetBool("IsDodging", Input.GetButtonDown("Dodge"));
    }
    void CheckMove()
    {
        float horizontalMovement = 0;
        float verticalMovement = 0;
        animator.SetBool("IsMoving", Input.GetButton("Up") || Input.GetButton("Down") || Input.GetButton("Left") || Input.GetButton("Right"));
        horizontalMovement += Input.GetButton("Up") ? 1 : 0;
        horizontalMovement += Input.GetButton("Down") ? -1 : 0;
        verticalMovement += Input.GetButton("Left") ? -1 : 0;
        verticalMovement += Input.GetButton("Right") ? 1 : 0;
        animator.SetFloat("Vertical", horizontalMovement);
        animator.SetFloat("Horizontal", verticalMovement);
    }
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
}
