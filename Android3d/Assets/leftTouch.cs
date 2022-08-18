using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class leftTouch : MonoBehaviour
{
    public static Vector3 inpDir = new Vector3();

    [SerializeField] Camera UIcam;


    private void Awake()
    {
        
    }

    void Start()
    {
        
    }

    
    void Update()
    {
        //WASD inputs
        if ( Input.GetAxis("Horizontal") != 0f || Input.GetAxis("Vertical") != 0f)
        {
            inpDir = new Vector3(Input.GetAxisRaw("Horizontal"), 0f, Input.GetAxisRaw("Vertical"));

        }
        if ( Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.S) || Input.GetKeyUp(KeyCode.D))
        {
            inpDir = Vector3.zero;
        }

    }

    private void OnMouseDrag()
    {       
        //max drag?

        inpDir = ( UIcam.ScreenToWorldPoint(Input.mousePosition) - transform.position ).normalized;
        inpDir = new Vector3(inpDir.x, 0, inpDir.y);       //print(inpDir);

    }
    private void OnMouseUp()
    {
        inpDir = Vector3.zero;
    }
}
