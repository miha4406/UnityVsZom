using UnityEngine;

public class bulGrenade : MonoBehaviour
{
    Vector3 pos, dir;
    int bulSpeed = 15;
    int dmg = 150;
    float hitRange = 3f;

    [SerializeField]GameObject blastVFX;


    void Start()
    {        
        //pos = Loader.singl.player.transform.position;  //shoot pos

        dir = (PlayerCtrl.currTarget.transform.position - Loader.singl.player.transform.position - Loader.singl.player.transform.forward*2).normalized; 
    }


    void Update()
    {
        if (gameObject.transform.position.y >= 0.1f)   
        {
            gameObject.transform.Translate(dir * bulSpeed * Time.deltaTime);
        }
        else { Destroy(gameObject, .5f); }

        DrawCircle( (int) hitRange);
    }
        

    private void OnDestroy()
    {
        foreach(Collider z in Physics.OverlapSphere(transform.position, hitRange, 3))  
        {
            if (z.transform.parent != null)
            {
                if (z.transform.parent.name.Contains("zom"))
                {
                    z.GetComponentInParent<EnemyCtrl>().HP -= dmg;

                    z.transform.position += (z.transform.position - transform.position).normalized;
                }
            }
        }

        if ( Vector3.Distance(transform.position, Loader.singl.player.transform.position) < hitRange) { Loader.singl.player.SendMessage("GetDmg", 10f); }

        Instantiate(blastVFX, transform.position, Quaternion.identity);
    }


    void DrawCircle(int rad)
    {
        int n = 50;
        GetComponent<LineRenderer>().positionCount = n;

        for (int step = 0; step < n; step++)
        {
            float cirPos = (float)step / n;
            float curRadian = cirPos * 2 * Mathf.PI;
            float x = rad * Mathf.Cos(curRadian);
            float y = rad * Mathf.Sin(curRadian);
            Vector3 curPos = new Vector3(x, 0, y);

            GetComponent<LineRenderer>().SetPosition(step, transform.position + curPos);
        }
    }

}
