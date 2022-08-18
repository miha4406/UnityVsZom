using UnityEngine;

public class Effects : MonoBehaviour
{    
    GameObject player;
    Transform gunHolder;

    public static Effects singl;

    AudioSource plAS;
    public AudioClip[] gunClips = new AudioClip[10];


    private void Awake()
    {
        singl = this;
    }


    private void Start()
    {
        player = GameObject.Find("PLAYER");
        gunHolder = player.GetComponent<PlayerCtrl>().gunHolder;
        plAS = player.GetComponent<AudioSource>();
    }




    public void GunVFX(int gunID)
    {
        gunHolder.transform.GetChild(gunID).transform.GetChild(0).gameObject.SetActive(true);

    }


    public void GunSFX(int gunID)
    {
        // 0 pistol, 1 shotgun, 2 smg, 3 burst, 4 rifle, 5 flame, 6 minigun, 7 grenade, 8 laser

        if(gunID != 8) { plAS.PlayOneShot(gunClips[gunID]); }
        else {
            plAS.clip = gunClips[8];
            if(! plAS.isPlaying) plAS.Play();
        }
        
    }



}
