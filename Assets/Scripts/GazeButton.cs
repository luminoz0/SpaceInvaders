using UnityEngine;
using UnityEngine.Events;

public class GazeButton : MonoBehaviour
{
   [SerializeField]
   private UnityEvent onPointerEnter;
   [SerializeField]
   private UnityEvent onPointerExit;
   [SerializeField]
   private UnityEvent onPointerClick;
   [SerializeField]
   private string onPointerEnterAnimationName;
   [SerializeField]
   private string onPointerExitAnimationName;
   [SerializeField]
   private string onPointerClickAnimationName;
   [SerializeField]
   private Animator animator;
   public void OnPointerEnter()
   {
       animator.Play(onPointerEnterAnimationName, 0, 0f);
       onPointerEnter?.Invoke();
   }
   public void OnPointerExit()
   {
       animator.Play(onPointerExitAnimationName, 0, 0f);
       onPointerExit?.Invoke();
   }
   public void OnPointerClick()
   {
       animator.Play(onPointerClickAnimationName, 0, 0f);
       onPointerClick?.Invoke();
   }
}