using UnityEngine;

public class bulPistol : MonoBehaviour
{
    Vector3 pos, dir;
    int bulSpeed = 25;
    float dmg = 50;

    void Start()
    {
        // Destroy(gameObject, 3f);

        pos = Loader.singl.player.transform.position;  //shoot pos

        dir = (PlayerCtrl.currTarget.transform.position + Vector3.up - Loader.singl.player.transform.position).normalized;
    }

    
    void Update()
    {
        gameObject.transform.Translate( dir *bulSpeed *Time.deltaTime );

        if( Vector3.Distance(transform.position, pos) > 4f) { Destroy(gameObject); }

    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.name != "PLAYER" && !other.gameObject.CompareTag("zBul"))
        {
            if (other.transform.parent.name.Contains("zom"))
            {
                var otherCtrl = other.transform.parent.GetComponent<EnemyCtrl>();

                otherCtrl.StopAllCoroutines();
                otherCtrl.StartCoroutine("Knockback", dmg);

                float hp = otherCtrl.HP;
                otherCtrl.HP -= dmg;
                dmg -= hp;
                
                if (dmg <= 0)
                {
                    Destroy(gameObject);
                }
            }

        }

    }

    //private void OnTriggerExit(Collider other)
    //{
    //    if (other.gameObject.name != "PLAYER")
    //    {
    //        if (other.transform.parent.name.Contains("zom"))
    //        {
    //            other.transform.parent.transform.position += dir;
    //            //other.transform.parent.GetComponent<Rigidbody>().isKinematic = false;
    //            //other.transform.parent.GetComponent<Rigidbody>().AddForce(dir * dmg*10);
    //            //other.transform.parent.GetComponent<Rigidbody>().isKinematic = true;
    //        }

    //    }
    //}
}
