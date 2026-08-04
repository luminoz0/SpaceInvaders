using UnityEngine;
public class Asteroid : Enemy
{
   [SerializeField]
   private Rotate rotateScript;
   [SerializeField]
   private float speed = 20f;
   [SerializeField]
   private float damage = 20f;
   public override void OnEnable()
   {
       base.OnEnable();
       rotateScript.enabled = true;
       animator.Play("Idle", 0, 0f);
   }
   private void Update()
   {
       if (currentState == State.Active && target != null)
       {
           Vector3 direction = (target.position - transform.position).normalized;
           transform.position += direction * speed * Time.deltaTime;
       }
   }
   public void Die()
   {
       currentState = State.Dead;
       rotateScript.enabled = false;
       animator.Play("Destroy", 0, 0f);
   }
}