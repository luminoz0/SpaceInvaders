using UnityEngine;
using System.Collections;
public static class AnimatorExtensions
{
   public static IEnumerator WaitForCurrentAnimation(this Animator animator, int layer = 0)
   {
       yield return null;
       yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(layer).length);
   }
}