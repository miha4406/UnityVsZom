using UnityEngine;
using System.Linq;
using System.Collections;


public class PlayerCtrl : MonoBehaviour
{
    CharacterController chCtrl;
    public float plSpeed = 5f;
    Quaternion corRot;

    public Transform gunHolder;
    public static GameObject currTarget;
    public int currWeapon = 0;
    public int fireRange = 4; //0.5 width
    public int fireRate = 10;  //1 sec
    bool isReloaded = true;
    [SerializeField] GameObject[] bulPrefs;

    int maxHp = 100; //test
    public float hp = 100;    
    

    private void Awake()
    {
        chCtrl = GetComponent<CharacterController>();       

        hp = maxHp;
    }

    
    void Update()
    {          
        //if(Input.touchCount > 0)
        {            
            chCtrl.Move( new Vector3(leftTouch.inpDir.x, -1f, leftTouch.inpDir.z) * plSpeed * Time.deltaTime);

            if (currTarget != null)
            {
                if (Vector3.Distance(transform.position, currTarget.transform.position) > fireRange+1f)  //run forward
                {
                    transform.rotation = Quaternion.LookRotation(Vector3.RotateTowards(transform.forward, leftTouch.inpDir, 5f*Time.deltaTime, 0f));

                    if (leftTouch.inpDir != Vector3.zero) { AnimCtrl.lowState = AnimCtrl.plState.runF; AnimCtrl.upState = AnimCtrl.plState.runF; }
                    //else { AnimCtrl.lowState = AnimCtrl.plState.wait; AnimCtrl.upState = AnimCtrl.plState.wait; }
                }
                else if (Vector3.Distance(transform.position, currTarget.transform.position) <= fireRange+1f)  //run back
                {
                    //transform.rotation = Quaternion.LookRotation( Vector3.RotateTowards(transform.forward, 
                    //    new Vector3((currTarget.transform.position-transform.position).normalized.x, 0f, (currTarget.transform.position-transform.position).normalized.z),
                    //        10f*Time.deltaTime, 0f));
                    corRot = Quaternion.AngleAxis(
                    Vector3.SignedAngle(Vector3.forward, (currTarget.transform.position - transform.position).normalized, Vector3.up) + 50f,  Vector3.up);                    
                    transform.rotation = Quaternion.RotateTowards(transform.rotation, corRot, 720f * Time.deltaTime);

                    if (leftTouch.inpDir != Vector3.zero) { AnimCtrl.lowState = AnimCtrl.plState.runB; AnimCtrl.upState = AnimCtrl.plState.runB; }
                    //else { AnimCtrl.lowState = AnimCtrl.plState.wait; AnimCtrl.upState = AnimCtrl.plState.wait; }
                }
            }
            else
            {
                transform.rotation = Quaternion.LookRotation(Vector3.RotateTowards(transform.forward, leftTouch.inpDir, 5f * Time.deltaTime, 0f));

                if (leftTouch.inpDir != Vector3.zero) { AnimCtrl.lowState = AnimCtrl.plState.runF; AnimCtrl.upState = AnimCtrl.plState.runF; }
            }
            
        }
                        
        DefineTarget();
        if (currTarget != null) { Shoot(); }
                
        if (currWeapon==8 && currTarget!=null && Vector3.Distance(transform.position, currTarget.transform.position) < fireRange)  //laser ray
        {
            gunHolder.GetComponent<LineRenderer>().enabled = true;
            gunHolder.GetComponent<LineRenderer>().SetPositions( new Vector3[] { gunHolder.GetChild(8).transform.GetChild(0).position, currTarget.transform.position + Vector3.up } );
        }
        else { gunHolder.GetComponent<LineRenderer>().enabled = false; }

        //Debug.DrawRay(transform.position, transform.forward*2, Color.white, 0f );        
        //TestKeys();
        DrawCircle(fireRange);
    }

    #region SHOOTING

    void DefineTarget()
    {
        if (Loader.singl.enemyList.Any(x => x != null) )
        {
            currTarget = Loader.singl.enemyList.First();
        }
        else return;  //no enemies        

        if (currTarget == null) { return; }

        foreach (GameObject zom in Loader.singl.enemyList)
        {
            if (Vector3.Distance(zom.transform.position, transform.position) < Vector3.Distance(currTarget.transform.position, transform.position))
            {
                currTarget = zom;
            }
        }
    }

    void Shoot()
    {
        foreach (GameObject zom in Loader.singl.enemyList)
        {
            if (!isReloaded) { return; }
            if (Vector3.Distance(currTarget.transform.position, transform.position) < fireRange)  //shot
            {      
                if (currWeapon == 0)  //pistol
                {
                    Instantiate(bulPrefs[0], gunHolder.position, Quaternion.identity);
                }
                if (currWeapon == 1)  //shotgun
                {
                    Instantiate(bulPrefs[1], gunHolder.position, Quaternion.identity);
                    Instantiate(bulPrefs[1], gunHolder.position, Quaternion.identity).transform.Rotate(0f, 10f, 0f);
                    Instantiate(bulPrefs[1], gunHolder.position, Quaternion.identity).transform.Rotate(0f, -10f, 0f); 
                }
                if (currWeapon == 2)  //SMG
                {
                    Instantiate(bulPrefs[2], gunHolder.position, Quaternion.identity).transform.Rotate(0f, Random.Range(-10f, 10f), 0f);
                }
                if (currWeapon == 3)  //burst
                {
                    StartCoroutine("BurstFire");
                }
                if (currWeapon == 4)  //rifle
                {
                    Instantiate(bulPrefs[4], gunHolder.position, Quaternion.identity);
                }
                if (currWeapon == 5)  //flame
                {
                    Instantiate(bulPrefs[5], gunHolder.position, Quaternion.identity);
                }
                if (currWeapon == 6)  //minigun
                {
                    Instantiate(bulPrefs[6], gunHolder.position, Quaternion.identity).transform.Rotate(0f, Random.Range(-10f, 10f), 0f);
                }
                if (currWeapon == 7)  //grenade
                {
                    Instantiate(bulPrefs[7], gunHolder.position, Quaternion.identity);
                }
                if (currWeapon == 8)  //laser
                {
                    currTarget.GetComponent<EnemyCtrl>().HP -= (fireRate/5 * Time.deltaTime);
                    
                    currTarget.GetComponent<EnemyCtrl>().StopAllCoroutines();
                    currTarget.GetComponent<EnemyCtrl>().StartCoroutine("Knockback", 5f);  //?
                }
               

                isReloaded = false;
                Invoke("Reload", 10f/fireRate);

                AnimCtrl.upState = AnimCtrl.plState.shoot;

                Effects.singl.GunVFX(currWeapon);
                Effects.singl.GunSFX(currWeapon);

                gunHolder.transform.LookAt(currTarget.transform.GetChild(0).transform);
            }
        }
    }
    void Reload()
    {
        Loader.singl.rInput.OnReload();
        if (currWeapon != 8 ) { gunHolder.transform.localRotation = Quaternion.Euler(0, -85f, 0); }

        isReloaded = true;
    }


    IEnumerator BurstFire()
    {      
        Vector3 burstDir = (currTarget.transform.position + Vector3.up - transform.position).normalized;

        Instantiate(bulPrefs[3], transform.position, Quaternion.identity).GetComponent<bulBurst>().dir = burstDir;   
        yield return new WaitForSeconds(.1f);
        Instantiate(bulPrefs[3], transform.position, Quaternion.identity).GetComponent<bulBurst>().dir = burstDir;  
        yield return new WaitForSeconds(.1f); 
        Instantiate(bulPrefs[3], transform.position, Quaternion.identity).GetComponent<bulBurst>().dir = burstDir;  
    }


    #endregion


    public void GetDmg(float dmg)
    {
        hp -= dmg;  print("GetDMG " + dmg);
       
        AnimCtrl.hited = true;
        Invoke("invHited", .4f);  //delay

        if (hp <= 0)  //death
        {
            AnimCtrl.death = true;

            this.enabled = false;             
        }
    }

    void invHited()
    {
        AnimCtrl.hited = false;
    }


    void DrawCircle(int rad)
    {
        int n = 50;
        GetComponent<LineRenderer>().positionCount = n;

        for(int step = 0; step < n; step++)
        {
            float cirPos =  (float)step/n;
            float curRadian = cirPos * 2 * Mathf.PI;
            float x = rad * Mathf.Cos(curRadian);
            float y = rad * Mathf.Sin(curRadian);
            Vector3 curPos = new Vector3(x, 0, y);

            GetComponent<LineRenderer>().SetPosition(step, transform.position + curPos);
        }
    }




#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        //UnityEditor.Handles.color = Color.white;
        //UnityEditor.Handles.DrawWireDisc(transform.position, new Vector3(0, 1, 0), fireRange);

        //if( currWeapon==8 && currTarget!=null && Vector3.Distance(transform.position, currTarget.transform.position)<fireRange)
        //{
        //    Handles.color = Color.red;
        //    Handles.DrawLine(gunHolder.GetChild(8).transform.GetChild(0).position, currTarget.transform.position+Vector3.up, 3f);  
        //}
        
    }
#endif

    void TestKeys()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))  //test
        {
            AnimCtrl.gather = true;
            //AnimCtrl.lowState = AnimCtrl.plState.gather;
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            AnimCtrl.hited = true;
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            AnimCtrl.win = true;
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            AnimCtrl.death = true;
        }
        if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            AnimCtrl.bPistol = !AnimCtrl.bPistol;
        }
    }

}



