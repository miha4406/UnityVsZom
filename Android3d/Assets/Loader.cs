using UnityEngine.SceneManagement;
using System.Collections.Generic;
using UnityEngine;


public class Loader : MonoBehaviour
{
    public static Loader singl;

    [SerializeField] public GameObject player;    
    [SerializeField] GameObject[] zomPrefs; 
    public List<GameObject> enemyList = new List<GameObject>();
    public GameObject grave, grave2, grave3;
    public rightInput rInput;
   
    int num = 0, zomNum = 0;
    bool isVictory = false;


    private void Awake()
    {
        singl = this;

        rInput = GetComponent<rightInput>();
    }


    private void Start()
    {
        InvokeRepeating( "ZomGenerator", 1f, 3f);
        InvokeRepeating( "ZomGenerator2", 25f, 6f);
                
    }


    private void Update()
    {      
        //camera control moved to AnimCtrl.cs
        if(zomNum >= 15 && !isVictory)
        {
            CancelInvoke();           

            if (enemyList.Count == 0)
            {
                player.GetComponent<PlayerCtrl>().enabled = false;
                AnimCtrl.win = true;

                isVictory = true;
            }
                        
        }
    }


    void ZomGenerator()
    {
        if (SceneManager.GetActiveScene().buildIndex == 1)  //SCENE1
        {
            num++;

            Instantiate(zomPrefs[0], new Vector3(Random.Range(-4f, 4f), 1f, Random.Range(-2f, 2f)), Quaternion.identity); //zom1

            if (num == 5)
            {
                Instantiate(zomPrefs[1], grave.transform.position, Quaternion.identity);  //zom2

                num = 0; zomNum++;
            }
        }
        else if (SceneManager.GetActiveScene().buildIndex == 2)  //SCENE2
        {
            num++;

            Instantiate(zomPrefs[0], grave.transform.position, Quaternion.identity);  //zom1

            if (num == 5)
            {
                Instantiate(zomPrefs[2], grave2.transform.position, Quaternion.identity);  //zom3

                num = 0; zomNum++;
            }
        }
        else if (SceneManager.GetActiveScene().buildIndex == 3)  //SCENE3
        {
            num++;

            Instantiate(zomPrefs[0], grave.transform.position, Quaternion.identity);  //zom1
            //Instantiate(zomPrefs[0], grave2.transform.position, Quaternion.identity);  //zom1

            if (num == 5)
            {
                Instantiate(zomPrefs[3], grave3.transform.position, Quaternion.identity);  //zom4

                num = 0; zomNum++;
            }
        }
    }

    void ZomGenerator2()
    {
        if (SceneManager.GetActiveScene().buildIndex == 3)  //SCENE3
        { 
            Instantiate(zomPrefs[0], grave2.transform.position, Quaternion.identity);  //zom1
        }
    }

}
