using UnityEngine;
using System.Linq;


public class Box : MonoBehaviour
{
    [SerializeField] int[] avWeapon;


    private void OnTriggerEnter(Collider other)
    {
        if(other.name == "PLAYER")
        {
            var ran = new System.Random();
            Loader.singl.GetComponentInParent<rightInput>().PickGun( avWeapon.OrderBy(x => ran.Next() ).First() ) ;
                        
            
            Destroy(gameObject);
        }
    }
}
