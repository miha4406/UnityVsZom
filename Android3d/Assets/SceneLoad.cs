using UnityEngine.SceneManagement;
using UnityEngine;
using UnityEngine.UI;

public class SceneLoad : MonoBehaviour
{
    [SerializeField] GameObject but1, but2, but3, butM, butQ;
    [SerializeField] GameObject menuPanel;

    [SerializeField] GameObject setPref;
    [SerializeField] Toggle tog1, tog2;
    [SerializeField] Text aboutText;
    [Multiline] public string engAbout, japAbout;
    [SerializeField] Text t1, t2, t3;


    private void Awake()
    {
       if(GameObject.FindGameObjectWithTag("Settings") == null) Instantiate(setPref);
    }


    void Start()
    {
        but1.GetComponent<Button>().onClick.AddListener(() => { SceneManager.LoadScene(1); } );
        but1.GetComponentInChildren<Text>().text = (Settings.isEng) ? "1. GRAVEYARD" : "１ 墓地";

        but2.GetComponent<Button>().onClick.AddListener(() => { SceneManager.LoadScene(2); } );
        but2.GetComponentInChildren<Text>().text = (Settings.isEng) ? "2. CITY BLOCK" : "２ 町のブロック";

        but3.GetComponent<Button>().onClick.AddListener(() => { SceneManager.LoadScene(3); } );
        but3.GetComponentInChildren<Text>().text = (Settings.isEng) ? "3. ZOMBIE LAB" : "３ ゾンビ・ラボ";

        butM.GetComponent<Button>().onClick.AddListener(() => { 
            menuPanel.active = !menuPanel.activeSelf;
            if (Settings.isEng) {
                tog1.isOn = true;
                aboutText.text = engAbout; 
            }
            else {
                tog2.isOn = true;
                aboutText.text = japAbout; 
            }
        });

        tog1.onValueChanged.AddListener( b => { 
            if (! tog1.isOn) {
                Settings.isEng = false;
                aboutText.text = japAbout;
                t1.text = "１ 墓地";
                t2.text = "２ 町のブロック";
                t3.text = "３ ゾンビ・ラボ";
            }
            else {
                Settings.isEng = true;
                aboutText.text = engAbout;
                t1.text = "1. GRAVEYARD";
                t2.text = "2. CITY BLOCK";
                t3.text = "3. ZOMBIE LAB"; 
            }
        });


        butQ.GetComponent<Button>().onClick.AddListener(() => { Application.Quit(); });
    }

   
}
