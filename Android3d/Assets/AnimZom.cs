using UnityEngine;
using UnityEngine.AI;


public class AnimZom : MonoBehaviour
{
    Animator an;
    static readonly int WAIT = Animator.StringToHash("wait");
    static readonly int RUN = Animator.StringToHash("run");
    static readonly int ATTACK = Animator.StringToHash("attack");
    static readonly int SHOOT = Animator.StringToHash("shoot");
    static readonly int HITED = Animator.StringToHash("hited");
    static readonly int DEATH = Animator.StringToHash("death");

    int currState = 0;
    float lockTill;

    NavMeshAgent agent;
    public bool attack = false, shoot = false, hited = false, death = false;



    void Start()
    {
        an = GetComponentInChildren<Animator>();
        agent = GetComponent<NavMeshAgent>();
    }



    void Update()
    {
        int state = GetState();
        if (state != currState)
        {
            an.CrossFade(state, .2f, 0);
            currState = state;
        }
                
    }

    int GetState()
    {
        if (Time.time < lockTill) { return currState; }

        //priority and delays             
        if (death == true) { return LockState(DEATH, 5f); }
        if (hited == true) { return LockState(HITED, .4f); }
        if (attack == true) { return LockState(ATTACK, .5f); }
        if (shoot == true) { return LockState(SHOOT, .4f); }


        if (agent.velocity.magnitude > .5f) { return RUN; }
        else { return WAIT; }


        int LockState(int st, float del)
        {
            lockTill = Time.time + del;

            return st;
        }
    }

        

}
