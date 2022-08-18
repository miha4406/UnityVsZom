using UnityEngine;

public class bulBurst : MonoBehaviour
{
    public Vector3 pos, dir;
    int bulSpeed = 25;
    float dmg = 45;

    void Start()
    {
        pos = Loader.singl.player.transform.position;  //shoot pos
    }


    void Update()
    {
        gameObject.transform.Translate(dir * bulSpeed * Time.deltaTime);

        if (Vector3.Distance(transform.position, pos) > 7f) { Destroy(gameObject); }  //fireRange = 7

    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name != "PLAYER" && !other.gameObject.CompareTag("zBul"))
        {
            if (other.transform.parent.name.Contains("zom"))
            {
                var otherCtrl = other.transform.parent.GetComponent<EnemyCtrl>();

                float hp = otherCtrl.HP;

                otherCtrl.HP -= dmg;
                dmg -= hp;

                otherCtrl.StopAllCoroutines();
                otherCtrl.StartCoroutine("Knockback", dmg);

                if (dmg <= 0)
                {
                    Destroy(gameObject);                    
                }
            }

        }

    }

    
}
