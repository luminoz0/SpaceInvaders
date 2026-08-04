using UnityEngine;
using System.Collections;
using UnityEngine.Events;
public class Enemy : MonoBehaviour
{
   private Health health;
   protected Animator animator;
   private Collider objectCollider;
   protected bool IsDead => health.CurrentHealth <= 0;
   private UnityEvent<Transform> onDeath = new UnityEvent<Transform>();
   public UnityEvent<Transform> OnDeath => onDeath;
   [SerializeField]
   protected Transform target;
   [SerializeField]
   protected string destroyAnimationName = "Destroy";
   public Transform Target { set { target = value; } }
   protected enum State { Active, Dead }
   protected State currentState;
   private void Awake()
   {
       health = GetComponent<Health>();
       animator = GetComponent<Animator>();
       objectCollider = GetComponent<Collider>();
   }
   public virtual void OnEnable()
   {
       health.InitializeHealth();
       currentState = State.Active;
   }
   public virtual void Destroy()
   {
       StopAllCoroutines();
       StartCoroutine(DestroyCoroutine());
   }
   private IEnumerator DestroyCoroutine()
   {
       onDeath?.Invoke(transform);
       objectCollider.enabled = false;
       animator.Play(destroyAnimationName, 0, 0f);
       yield return animator.WaitForCurrentAnimation();
       gameObject.SetActive(false);
   }
}