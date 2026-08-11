using UnityEngine;

public class EnemyShip : Enemy
{
   private Transform startPoint;
   private Transform endPoint;
   [SerializeField]
   private float distanceFromTarget = 300f;
   [SerializeField]
   private float pathLength = 25f;
   [SerializeField]
   private GameObject bulletPrefab;
   [SerializeField]
   private Transform bulletPivot;
   [SerializeField]
   private float randomDistanceMultiplier = 0.3f;
   [SerializeField]
   private string shootSoundName;
   private float distanceToShoot;
   private Vector3 movementDirection;
   private bool hasShot = false;
   public override void PositionEnemy()
   {
       if (target == null) return;
       if (startPoint == null || endPoint == null)
       {
           CreatePathPoints();
       }
       Vector3 direction = target.forward;
       direction.y = 0f;
       direction.Normalize();
       Vector3 side = target.right;
       side.y = 0f;
       side.Normalize();
       float slotDirection = Random.value > 0.5f ? 1f : -1f;
       float minDistance = distanceFromTarget * (1f - randomDistanceMultiplier);
       float maxDistance = distanceFromTarget * (1f + randomDistanceMultiplier);
       float randomDistance = Random.Range(minDistance, maxDistance);
       Vector3 pathCenter = target.position + side * slotDirection * randomDistance;
       startPoint.position = pathCenter - direction * (pathLength * 0.5f);
       endPoint.position = pathCenter + direction * (pathLength * 0.5f);
       transform.position = startPoint.position;
       movementDirection = (endPoint.position - startPoint.position).normalized;
       transform.LookAt(endPoint.position);
       hasShot = false;
       distanceToShoot = distanceFromTarget * 2f;
       gameObject.SetActive(true);
       SoundManager.instance.Play(appearSoundName);
   }
   private void CreatePathPoints()
   {
       GameObject startPointObj = new GameObject("StartPoint");
       startPoint = startPointObj.transform;
       GameObject endPointObj = new GameObject("EndPoint");
       endPoint = endPointObj.transform;
   }
   private void Update()
   {
       if (currentState != State.Active || target == null) return;
       transform.position += movementDirection * speed * Time.deltaTime;
       CheckShootTarget();
       if (Vector3.Distance(transform.position, endPoint.position) < 0.1f)
       {
           PositionEnemy();
       }
   }
   private void CheckShootTarget()
   {
       if (hasShot || target == null) return;
       if (Vector3.Distance(transform.position, target.position) <= distanceToShoot)
       {
           hasShot = true;
           Shoot();
       }
   }
   private void Shoot()
   {
       GameObject bullet = PoolManager.Instance.GetObject(bulletPrefab,bulletPivot.position,true);
       bullet.transform.LookAt(target);
       bullet.SetActive(true);
       Health playerHealth = target.GetComponent<Health>();
       playerHealth?.TakeDamage(damage);
   }

}