using System.Collections;
using UnityEngine;
using UnityEngine.AI;


public class EnemyCtrl : MonoBehaviour
{
    public float HP = 1000;

    NavMeshAgent agent;
    AnimZom anZ;
    GameObject player;

    float hitDmg = 10f;

    [SerializeField] GameObject boxPref, bulPref;
    int bulletRange = 10;
    int bulletRate = 5;
    bool isReloaded = true, isDeath = false;


    void Start()
    {        
        agent = GetComponent<NavMeshAgent>();
        anZ = GetComponent<AnimZom>();
        player = Loader.singl.player;

        Loader.singl.enemyList.Add(gameObject);  //don't forget to delete

        if (gameObject.tag == "zom1" || gameObject.tag == "zom2")
        {
            HP = 100f;
        }
        if (gameObject.CompareTag("zom2"))  //dog
        {
            agent.speed = 7f;
            hitDmg = 15f;
        }
        if (gameObject.CompareTag("zom3"))  //fat
        {
            HP = 300f;
            hitDmg = 20f;
        }
        if (gameObject.CompareTag("zom4"))  //shooter
        {
            HP = 150f;
            agent.stoppingDistance = bulletRange;
        }

        Physics.IgnoreCollision(Loader.singl.grave.GetComponent<Collider>(), transform.GetChild(0).GetComponent<Collider>());
    }
    

    
    void Update()
    {
        if (isDeath) { return; }

        agent.SetDestination(player.transform.position);
        if (HP>0) { transform.rotation = Quaternion.LookRotation(Vector3.RotateTowards(transform.forward,
                           new Vector3((player.transform.position - transform.position).normalized.x, 0f, (player.transform.position - transform.position).normalized.z),
                               5f * Time.deltaTime, 0f)); 
        }

        if (!CompareTag("zom4") && Vector3.Distance(player.transform.position, transform.position) < 2f)  //hit range
        {
            if (isReloaded) { ZomAttack(); }
        }

        if (CompareTag("zom4") && Vector3.Distance(player.transform.position, transform.position) < bulletRange)
        {
            if (isReloaded) { ZomBullet(); }
        }
        

        if (HP<=0)   //death
        {
            isDeath = true;

            Destroy(gameObject, 3f); 
            Loader.singl.enemyList.Remove(gameObject);            
            agent.isStopped = true;

            anZ.death = true;
            GetComponent<AudioSource>().PlayOneShot(GetComponent<AudioSource>().clip); 
        }        
    }



    private void OnDestroy()
    {
        if (!this.gameObject.scene.isLoaded) return;  //to avoid "Objects was not cleared" exception

        if (gameObject.tag != "zom1")
        {
            Instantiate(boxPref, transform.position, Quaternion.identity);
        }
    }



    IEnumerator Knockback(float dmg)
    {
        //StopAllCoroutines();
        if (gameObject.tag == "zom3") { yield break; }

        anZ.hited = true;  Invoke("swOff", .5f);

        agent.speed -= 3f;
        yield return new WaitForSeconds(dmg/100f);
        agent.speed += 3f;
    }


    void ZomAttack()
    {        
        Loader.singl.player.GetComponent<PlayerCtrl>().GetDmg(hitDmg);

        //plCol = Physics.OverlapSphere(transform.forward, 1f).Where(c => c.name == "PLAYER").FirstOrDefault();
        isReloaded = false;
        Invoke("Reload", 2f);  //hit rate

        anZ.attack = true;  Invoke("swOff", .5f);

       
    }
    void swOff()
    {
        anZ.hited = false;
        anZ.attack = false;
        anZ.shoot = false;
    }


    void ZomBullet()
    {
        Instantiate(bulPref, transform.position + 2*Vector3.up, Quaternion.identity);

        isReloaded = false;
        Invoke("Reload", 10f/bulletRate);

        anZ.shoot = true; Invoke("swOff", .5f);
    }


    void Reload()
    {
        isReloaded = true;
    }
}
