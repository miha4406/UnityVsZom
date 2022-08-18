using UnityEngine;


[RequireComponent((typeof(Rigidbody)))]
public class zomBullet : MonoBehaviour
{    
    Vector3 dir, pos;
    float mag;
    [SerializeField]AnimationCurve curveY;
    float time = 0f;

    bool bHit = false;
    int dmg = 1;


    private void Start()
    {
        dir = (Loader.singl.player.transform.position - transform.position).normalized;
        pos = transform.position;        

        mag = (Loader.singl.player.transform.position - transform.position).magnitude / 20f;
                
    }


    private void Update()
    {
        if (transform.position.y >= 0.1f)
        {
            time += Time.deltaTime;
         
            GetComponent<Rigidbody>().                
                MovePosition(transform.position + new Vector3(dir.x * mag , -dir.y * curveY.Evaluate(time), dir.z * mag ));
        }
        else { if(!bHit) GroundHit(); }

        if (bHit) { transform.localScale += new Vector3(.5f, .5f, .5f) * Time.deltaTime; }
    }


    private void GroundHit()
    {
        bHit = true;

        Destroy(gameObject, 2f);

        GetComponent<AudioSource>().PlayOneShot(GetComponent<AudioSource>().clip);
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.name == "PLAYER")
        {
            other.SendMessage( "GetDmg",  (dmg ) );
        }
    }
}
