using UnityEngine.UI;
using UnityEngine;

public class rightInput : MonoBehaviour
{   
    public GameObject[] gunButtons;
    public Sprite[] gunSprtites;
    public int activeSlot = 0;
    [SerializeField] GameObject player;
    [SerializeField] Slider hpSlider;

    public int[] gun;
    public int[] ammo;

    PlayerCtrl pl;
    Transform gunHolder;


    private void Awake()
    {
        gun = new int[4] { 0, 0, 0, 0 };
        ammo = new int[4] { 0, 0, 0, 0 };               
    }

    void Start()
    {
        //var pl = player.GetComponent<PlayerCtrl>();
        pl = Loader.singl.player.GetComponent<PlayerCtrl>();
        gunHolder = player.GetComponent<PlayerCtrl>().gunHolder;

        //button1                
        gunButtons[1].GetComponent<Button>().onClick.AddListener( () => 
        {           
            if (activeSlot != 1) { 
                activeSlot = 1; 
                gunButtons[1].GetComponent<Image>().color = Color.yellow;
                gunButtons[2].GetComponent<Image>().color = Color.white;
                gunButtons[3].GetComponent<Image>().color = Color.white;
                
                if (gun[1] == 1)  //shotgun
                {
                    pl.plSpeed = 5f;
                    pl.currWeapon = 1; pl.fireRange = 4; pl.fireRate = 7;
                }
                if (gun[1] == 2)  //SMG
                {
                    pl.plSpeed = 5f;
                    pl.currWeapon = 2; pl.fireRange = 4; pl.fireRate = 50;
                }
                if (gun[1] == 3)  //burst
                {
                    pl.plSpeed = 5f;
                    pl.currWeapon = 3; pl.fireRange = 7; pl.fireRate = 10;
                }
                if (gun[1] == 4)  //rifle
                {
                    pl.plSpeed = 5f;
                    pl.currWeapon = 4; pl.fireRange = 8; pl.fireRate = 5;
                }
                if (gun[1] == 5)  //flame
                {
                    pl.plSpeed = 5f;
                    pl.currWeapon = 5; pl.fireRange = 6; pl.fireRate = 20;
                }
                if (gun[1] == 6)  //minigun
                {
                    pl.plSpeed = 3f;
                    pl.currWeapon = 6; pl.fireRange = 7; pl.fireRate = 150;  //40
                }
                if (gun[1] == 7)  //grenade
                {
                    pl.plSpeed = 3f;
                    pl.currWeapon = 7; pl.fireRange = 6; pl.fireRate = 5;
                }
                if (gun[1] == 8)  //laser
                {
                    pl.plSpeed = 3f;
                    pl.currWeapon = 8; pl.fireRange = 4; pl.fireRate = 1000;
                }

                AnimCtrl.bPistol = false;
                for (int i = 0; i <= 8; i++)
                {
                    gunHolder.GetChild(i).gameObject.SetActive(false);
                }
                gunHolder.GetChild(gun[1]).gameObject.SetActive(true);
            }
            else { SetPistol(); }
        });


        //button2                
        gunButtons[2].GetComponent<Button>().onClick.AddListener( () => 
        {
            if (activeSlot != 2)
            {
                activeSlot = 2;
                gunButtons[2].GetComponent<Image>().color = Color.yellow;
                gunButtons[1].GetComponent<Image>().color = Color.white;
                gunButtons[3].GetComponent<Image>().color = Color.white;
                              
                if (gun[2] == 1)  //shotgun
                {
                    pl.plSpeed = 5f;
                    pl.currWeapon = 1; pl.fireRange = 4; pl.fireRate = 7;
                }
                if (gun[2] == 2)  //SMG
                {
                    pl.plSpeed = 5f;
                    pl.currWeapon = 2; pl.fireRange = 4; pl.fireRate = 50;
                }
                if (gun[2] == 3)  //burst
                {
                    pl.plSpeed = 5f;
                    pl.currWeapon = 3; pl.fireRange = 7; pl.fireRate = 10;
                }
                if (gun[2] == 4)  //rifle
                {
                    pl.plSpeed = 5f;
                    pl.currWeapon = 4; pl.fireRange = 8; pl.fireRate = 5;
                }
                if (gun[2] == 5)  //flame
                {
                    pl.plSpeed = 5f;
                    pl.currWeapon = 5; pl.fireRange = 6; pl.fireRate = 20;
                }
                if (gun[2] == 6)  //minigun
                {
                    pl.plSpeed = 3f;
                    pl.currWeapon = 6; pl.fireRange = 7; pl.fireRate = 150;
                }
                if (gun[2] == 7)  //grenade
                {
                    pl.plSpeed = 3f;
                    pl.currWeapon = 7; pl.fireRange = 6; pl.fireRate = 5;
                }
                if (gun[2] == 8)  //laser
                {
                    pl.plSpeed = 3f;
                    pl.currWeapon = 8; pl.fireRange = 4; pl.fireRate = 1000;
                }

                AnimCtrl.bPistol = false;
                for (int i = 0; i <= 8; i++)
                {
                    gunHolder.GetChild(i).gameObject.SetActive(false);
                }
                gunHolder.GetChild(gun[2]).gameObject.SetActive(true);
            }
            else{ SetPistol(); }
        });


        //button3                
        gunButtons[3].GetComponent<Button>().onClick.AddListener( () =>
        {
            if (activeSlot != 3)
            {
                activeSlot = 3;
                gunButtons[3].GetComponent<Image>().color = Color.yellow;
                gunButtons[1].GetComponent<Image>().color = Color.white;
                gunButtons[2].GetComponent<Image>().color = Color.white;

                if (gun[3] == 1)  //shotgun
                {
                    pl.plSpeed = 5f;
                    pl.currWeapon = 1; pl.fireRange = 4; pl.fireRate = 7;
                }
                if (gun[3] == 2)  //SMG
                {
                    pl.plSpeed = 5f;
                    pl.currWeapon = 2; pl.fireRange = 4; pl.fireRate = 50;
                }
                if (gun[3] == 3)  //burst
                {
                    pl.plSpeed = 5f;
                    pl.currWeapon = 3; pl.fireRange = 7; pl.fireRate = 10;
                }
                if (gun[3] == 4)  //rifle
                {
                    pl.plSpeed = 5f;
                    pl.currWeapon = 4; pl.fireRange = 8; pl.fireRate = 5;
                }
                if (gun[3] == 5)  //flame
                {
                    pl.plSpeed = 5f;
                    pl.currWeapon = 5; pl.fireRange = 6; pl.fireRate = 20;
                }
                if (gun[3] == 6)  //minigun
                {
                    pl.plSpeed = 3f;
                    pl.currWeapon = 6; pl.fireRange = 7; pl.fireRate = 150;
                }
                if (gun[3] == 7)  //grenade
                {
                    pl.plSpeed = 3f;
                    pl.currWeapon = 7; pl.fireRange = 6; pl.fireRate = 5;
                }
                if (gun[3] == 8)  //laser
                {
                    pl.plSpeed = 3f;
                    pl.currWeapon = 8; pl.fireRange = 4; pl.fireRate = 1000;
                }

                AnimCtrl.bPistol = false;
                for (int i = 0; i <= 8; i++)
                {
                    gunHolder.GetChild(i).gameObject.SetActive(false);
                }
                gunHolder.GetChild(gun[3]).gameObject.SetActive(true);
            }
            else { SetPistol(); }
        });

    }

   
    void Update()
    {
        hpSlider.value = pl.hp;
    }


    public void PickGun(int gunID)
    {
        // 0 pistol, 1 shotgun, 2 smg, 3 burst, 4 rifle, 5 flame, 6 minigun, 7 grenade, 8 laser 

        int ammoPlus = 0;
        if (gunID == 1) { ammoPlus = 8; }
        if (gunID == 2) { ammoPlus = 20; }
        if (gunID == 3) { ammoPlus = 10; }
        if (gunID == 4) { ammoPlus = 5; }
        if (gunID == 5) { ammoPlus = 15; }
        if (gunID == 6) { ammoPlus = 100; }
        if (gunID == 7) { ammoPlus = 5; }
        if (gunID == 8) { ammoPlus = 500; }

              
        if (gun[1] == gunID)
        {
            ammo[1] += ammoPlus;
            gunButtons[1].transform.GetChild(1).GetComponent<Text>().text = ammo[1].ToString();
            gunButtons[1].GetComponentInChildren<Button>().interactable = true;
        }
        else if (gun[2] == gunID)
        {
            ammo[2] += ammoPlus;
            gunButtons[2].transform.GetChild(1).GetComponent<Text>().text = ammo[2].ToString();
            gunButtons[2].GetComponentInChildren<Button>().interactable = true;
        }
        else if (gun[3] == gunID)
        {
            ammo[3] += ammoPlus;
            gunButtons[3].transform.GetChild(1).GetComponent<Text>().text = ammo[3].ToString();
            gunButtons[3].GetComponentInChildren<Button>().interactable = true;
        }
        else  //new weapon
        if (gun[1] == 0)
        {
            gun[1] = gunID;
            ammo[1] = ammoPlus;
            //gunButtons[1].GetComponentInChildren<Text>().text = gun[1].ToString();
            gunButtons[1].GetComponent<Image>().sprite = gunSprtites[gun[1]];
            gunButtons[1].transform.GetChild(1).GetComponent<Text>().text = ammo[1].ToString();
            gunButtons[1].GetComponentInChildren<Button>().interactable = true;
        }
        else if (gun[2] == 0)
        {
            gun[2] = gunID;
            ammo[2] = ammoPlus;
            //gunButtons[2].GetComponentInChildren<Text>().text = gun[2].ToString();
            gunButtons[2].GetComponent<Image>().sprite = gunSprtites[gun[2]];
            gunButtons[2].transform.GetChild(1).GetComponent<Text>().text = ammo[2].ToString();
            gunButtons[2].GetComponentInChildren<Button>().interactable = true;
        }
        else if (gun[3] == 0)
        {
            gun[3] = gunID;
            ammo[3] = ammoPlus;
            //gunButtons[3].GetComponentInChildren<Text>().text = gun[3].ToString();
            gunButtons[3].GetComponent<Image>().sprite = gunSprtites[gun[3]];
            gunButtons[3].transform.GetChild(1).GetComponent<Text>().text = ammo[3].ToString();
            gunButtons[3].GetComponentInChildren<Button>().interactable = true;
        }

        AnimCtrl.gather = true; Invoke("invGather", .5f);  //delay
    }

    void invGather()
    {
        AnimCtrl.gather = false;
    }


    public void OnReload()
    {
        if(activeSlot == 0) { return; }

        ammo[activeSlot] -= 1;
        gunButtons[activeSlot].transform.GetChild(1).GetComponent<Text>().text = ammo[activeSlot].ToString();

        if (ammo[activeSlot] <= 0)
        {
            //gun[activeSlot] = 0;
            //gunButtons[activeSlot].GetComponentInChildren<Text>().text = "0";

            gunButtons[activeSlot].GetComponent<Button>().interactable = false;            

            SetPistol();
        }
    }


    public void SetPistol()  //pistol
    {
        gunButtons[activeSlot].GetComponent<Image>().color = Color.white;
        activeSlot = 0;
            
        pl.plSpeed = 5f;
        pl.currWeapon = 0; pl.fireRange = 4; pl.fireRate = 10;

        AnimCtrl.bPistol = true;
        for (int i = 0; i <= 8; i++) {
            gunHolder.GetChild(i).gameObject.SetActive(false);
        }
        gunHolder.GetChild(0).gameObject.SetActive(true);
    }


    
}
