using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimatorExtensions : MonoBehaviour
{
    public void ClearAllBools(Animator animator)
    {
        foreach(AnimatorControllerParameter parameter in animator.parameters)
        {
            animator.SetBool(parameter.name, false);
        }
    }
    public void ClearAllBoolsExcept(Animator animator, string exceptParameter)
    {
        foreach(AnimatorControllerParameter parameter in animator.parameters)
        {
            if (parameter.name != exceptParameter)
            {
                animator.SetBool(parameter.name, false);
            }
        }
    }
}
