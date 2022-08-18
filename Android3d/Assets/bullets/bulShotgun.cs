using UnityEngine;

public class bulShotgun : MonoBehaviour
{
    Vector3 pos, dir;
    int bulSpeed = 25;
    float dmg = 35;

    void Start()
    {       
        pos = Loader.singl.player.transform.position;  //shoot pos

        dir = (PlayerCtrl.currTarget.transform.position + Vector3.up - Loader.singl.player.transform.position).normalized;
    }


    void Update()
    {
        gameObject.transform.Translate(dir * bulSpeed * Time.deltaTime);

        if (Vector3.Distance(transform.position, pos) > 4f) { Destroy(gameObject); }  //fireRange = 4

    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name != "PLAYER" && !other.gameObject.CompareTag("zBul"))
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
}
