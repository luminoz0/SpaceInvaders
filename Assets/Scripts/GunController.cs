using UnityEngine;
public class GunController : MonoBehaviour
{
   [SerializeField]
   private Gun[] guns;
   [SerializeField]
   private Camera playerCamera;
   private int currentGunIndex = 0;
   private void Start()
   {
       foreach (Gun gun in guns)
       {
           gun.CameraUsed = playerCamera;
       }
   }
   public void Update()
   {
       if (Input.GetMouseButtonDown(0))
       {
           guns[currentGunIndex].Shoot();
           currentGunIndex++;
           if (currentGunIndex >= guns.Length)
           {
               currentGunIndex = 0;
           }
       }
   }
}