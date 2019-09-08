using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishAnim : StateMachineBehaviour
{
    override public void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

        //animator.transform.parent.position = animator.transform.position;
        animator.gameObject.transform.position = animator.transform.position;

    }
}
