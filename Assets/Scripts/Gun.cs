using UnityEngine;
public class Gun : MonoBehaviour
{
   private Camera cameraUsed;
   public Camera CameraUsed
   {
       set { cameraUsed = value; }
   }
   [SerializeField]
   private Animator animator;
   [SerializeField]
   private Transform bulletPivot;
   [SerializeField]
   private GameObject bulletPrefab;
   [SerializeField]
   private float gunDamage = 20f;
   [SerializeField]
   private string soundName;
   private float rayDistance = 100f;
   public void Shoot()
   {
       Ray ray = cameraUsed.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));
       RaycastHit hit;
       Vector3 targetPoint;
       if (Physics.Raycast(ray, out hit, rayDistance))
       {
           targetPoint = hit.point;
           CheckEnemy(hit.collider);
       }
       else
       {
           targetPoint = ray.origin + ray.direction * rayDistance;
       }
       Vector3 direction = (targetPoint - transform.position).normalized;
       bulletPivot.forward = direction;
       GameObject bullet = PoolManager.Instance.GetObject(bulletPrefab, bulletPivot.position, false);
       bullet.transform.LookAt(targetPoint);
       bullet.SetActive(true);
       animator.Play("Shoot", 0, 0f);
       SoundManager.instance.Play(soundName);
   }
   private void CheckEnemy(Collider collider)
   {
       if (collider.CompareTag("Enemy"))
       {
           Health enemyHealth = collider.GetComponent<Health>();
           enemyHealth?.TakeDamage(gunDamage);
       }
   }
}