using UnityEngine;

public class AnimCtrl : MonoBehaviour
{
    Animator an;
    static readonly int WAIT = Animator.StringToHash("wait");
    static readonly int WAIT2 = Animator.StringToHash("wait2");
    static readonly int RUNF = Animator.StringToHash("runF");    
    static readonly int RUNB = Animator.StringToHash("runB");
    static readonly int RUN2 = Animator.StringToHash("run2");
    static readonly int SHOOT = Animator.StringToHash("shoot");
    static readonly int SHOOT2 = Animator.StringToHash("shoot2");
    static readonly int GATHER = Animator.StringToHash("gather");
    static readonly int HITED = Animator.StringToHash("hited");
    static readonly int DEATH = Animator.StringToHash("death");
    static readonly int WIN = Animator.StringToHash("win");


    int currLowState = 0, currUpState = 0;
    float lowLock, upLock;
    public static bool bPistol = true;

    public enum plState { wait, runF, runB, shoot};
    public static plState lowState = 0, upState = 0;   //inject from PlayerCtrl or rightInput
    public static bool hited = false, gather = false, win = false, death = false;


    private void Awake()
    {
        an = GetComponentInChildren<Animator>();

        lowState = 0; upState = 0;
        hited = false; gather = false; win = false; death = false;
    }



    void Update()
    {
        int lowSt = GetLowState();
        if (lowSt != currLowState)
        {
            an.CrossFade(lowSt, .2f, 0);
            currLowState = lowSt;           
        }

        int upSt = GetUpState();
        if (upSt != currUpState)
        {
            an.CrossFade(upSt, .2f, 1);
            currUpState = upSt;
        }

        if (win || death)  //ending camera
        {
            if (win) { 
                GetComponent<PlayerCtrl>().gunHolder.gameObject.SetActive(false);
                transform.rotation = Quaternion.LookRotation( Vector3.back );
            }

            Camera.main.transform.LookAt(transform);

            if ( Vector3.Distance(Camera.main.transform.position, transform.position) > 2f )
            {
                Camera.main.transform.position = Vector3.MoveTowards(Camera.main.transform.position, transform.position, 5f * Time.deltaTime);
            }            
        }
        else { Camera.main.transform.position = transform.position + new Vector3(0f, 22f, -15f); }
    }


    int GetLowState()
    {
        if (Time.time < lowLock) { return currLowState; }

        //priority and delays     
        if (win == true) { return LockState(WIN, 10f); }
        if (death == true) { return LockState(DEATH, 10f); }
        if (hited == true) { return LockState(HITED, .4f); }  
        

        if (leftTouch.inpDir != Vector3.zero)
        {
            if (lowState == plState.runB) { return RUNB; }
            else { return RUNF; }
        }
        else { return WAIT; }


        int LockState(int st, float del)
        {
            lowLock = Time.time + del;

            return st;
        }
    }


    int GetUpState()
    {
        if (Time.time < upLock) { return currUpState; }

        //priority and delays
        if (win == true) { return LockState(WIN, 10f); }
        if (death == true) { return LockState(DEATH, 10f); }
        if (gather == true){ return LockState(GATHER, .5f); }
        if (upState == plState.shoot) { return  bPistol?  LockState(SHOOT2, .3f) : LockState(SHOOT2, .3f); }  //no animation
        if (hited == true) { return LockState(HITED, .4f); }


        if (leftTouch.inpDir != Vector3.zero) 
        {
            if (upState == plState.runB) { return bPistol? RUNB: RUN2; }
            else { return bPistol ? RUNF: RUN2; }
        }
        else { return bPistol ? WAIT: WAIT2; }


        int LockState(int st, float del)
        {
            upLock = Time.time + del;

            return st;
        }
       
    }

    
   

    //void ChangeAnimation( string lowSt, string upSt)
    //{
    //    //if(an.GetNextAnimatorClipInfoCount(0) !=0) { return; }

    //    if (an.GetNextAnimatorClipInfoCount(0)==0 && !an.GetCurrentAnimatorStateInfo(0).IsName(lowSt))
    //    {
    //        an.CrossFade("lower."+lowSt, .2f, 0);
    //    }       

    //    if (an.GetNextAnimatorClipInfoCount(1)==0 && !an.GetCurrentAnimatorStateInfo(1).IsName(upSt))
    //    {
    //        an.CrossFade("upper."+upSt, .2f, 1);
    //    }    

    //}
}
