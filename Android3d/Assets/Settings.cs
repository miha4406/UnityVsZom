using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class Settings : MonoBehaviour
{
    public static bool isEng = true;
    public GameObject menuBtn, menuPanel;
    Text mText, t1, t2, t3;
    public string eng1, eng2, eng3;
    public string jap1, jap2, jap3;
    bool isStageEnd = false;


    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }


    [System.Obsolete]
    private void OnLevelWasLoaded(int level)
    {
        isStageEnd = false;
        Time.timeScale = 1f;

        if (level == 0)
        {
            //SceneLoad.cs
        }

        if (level != 0)
        {
            menuBtn = GameObject.Find("Canvas/Menu/menuBtn");
            menuPanel = GameObject.Find("Canvas/Menu/menuPanel");
            mText = menuPanel.transform.GetChild(0).gameObject.GetComponent<Text>();
            t1 = menuPanel.transform.GetChild(1).gameObject.GetComponentInChildren<Text>();
            t2 = menuPanel.transform.GetChild(2).gameObject.GetComponentInChildren<Text>();
            t3 = menuPanel.transform.GetChild(3).gameObject.GetComponentInChildren<Text>();

            if (Settings.isEng)  
            {
                mText.text = "PAUSE";
                t1.text = eng1; t2.text = eng2; t3.text = eng3;
            }
            else
            {
                mText.text = "一時停止";
                t1.text = jap1; t2.text = jap2; t3.text = jap3;
            }

            menuBtn.GetComponent<Button>().onClick.AddListener(() => {
                menuPanel.active = !menuPanel.activeSelf;
                
                if (Time.timeScale != 0f) { Time.timeScale = 0f; }
                else Time.timeScale = 1f;
            });

            menuPanel.transform.GetChild(1).GetComponent<Button>().onClick.AddListener(() => {
                menuPanel.SetActive(false);
                Time.timeScale = 1f;
            });
            menuPanel.transform.GetChild(2).GetComponent<Button>().onClick.AddListener(() => { SceneManager.LoadScene(SceneManager.GetActiveScene().name); });
            menuPanel.transform.GetChild(3).GetComponent<Button>().onClick.AddListener(() => { SceneManager.LoadScene(0); });
            menuPanel.SetActive(false);
        }

    }

    private void Update()
    {
        if (SceneManager.GetActiveScene().buildIndex == 0) { return; }

        if ( AnimCtrl.win || AnimCtrl.death)
        {
            if (!isStageEnd)
            {
                menuBtn.GetComponent<Button>().interactable = false;
                menuPanel.SetActive(true);
                menuPanel.transform.GetChild(1).gameObject.GetComponent<Button>().interactable = false;

                if (AnimCtrl.win)
                {
                    mText.text = (Settings.isEng) ? "VICTORY" : "勝利";
                    mText.color = Color.green;
                }
                else
                {
                    mText.text = (Settings.isEng) ? "LOSE" : "負け";
                    mText.color = Color.red;
                }


                isStageEnd = true;
            }
        }
    }
}
