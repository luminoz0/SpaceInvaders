using UnityEngine;
public class Asteroid : Enemy
{
   [SerializeField]
   private Rotate rotateScript;
   [SerializeField]
   private float distanceToTarget = 10f;
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
           transform.LookAt(target);
           Vector3 direction = (target.position - transform.position).normalized;
           transform.position += direction * speed * Time.deltaTime;
       }
   }
    public override void Destroy()
   {
           currentState = State.Dead;
           rotateScript.enabled = false;
           base.Destroy();
   }
    private void OnTriggerEnter(Collider other)
   {
     if (currentState == State.Active && other.CompareTag("Player"))
        {
           Health playerHealth = other.GetComponent<Health>();
           playerHealth.TakeDamage(damage);
           Destroy();
        }
   }
   public override void PositionEnemy()
   {
           Vector3 direction = Random.onUnitSphere;
           float distance = Random.Range(distanceToTarget, distanceToTarget + 5f);
           transform.position = target.position + direction * distance;
           gameObject.SetActive(true);
   }
}