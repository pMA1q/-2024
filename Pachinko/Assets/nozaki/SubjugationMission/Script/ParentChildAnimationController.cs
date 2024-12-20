using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParentChildAnimationController : MonoBehaviour
{
    public Animator parentAnimator;
    public Animator childAnimator;

    public void PlayParentAnimation(string ParentAnimation)
    {
        parentAnimator.Play(ParentAnimation);
    }

    public void PlayChildAnimation(string ChildAnimation)
    {
        childAnimator.Play(ChildAnimation);
    }
}
