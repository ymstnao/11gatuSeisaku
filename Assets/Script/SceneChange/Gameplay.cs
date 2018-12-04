using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Gameplay : MonoBehaviour
{
    [SerializeField]
    private string goal_Scene,death_Scene;
    private bool sceneFlag;
    [SerializeField]
    private float speed = 0.01f;
    [SerializeField]
    private float time = 3f;
    private float nowtime;
    private float alfa;
    private float red, green, blue;
    [SerializeField]
    private GameObject fadePanel;
    public static bool load;
    [SerializeField]
    private PlayerStatus status;
    [SerializeField]
    private Transform[] respownPostion;
    [SerializeField]
    private GameObject[] stageTitle;
    // Use this for initialization
    void Start () {
        nowtime = time;
        sceneFlag = false;

        red = fadePanel.GetComponent<Image>().color.r;
        green = fadePanel.GetComponent<Image>().color.g;
        blue = fadePanel.GetComponent<Image>().color.b;
        alfa = fadePanel.GetComponent<Image>().color.a;

        if (load)
        {
            status.MaxHp = PlayerPrefs.GetFloat("HP", 0);
            status.AttackPower = PlayerPrefs.GetFloat("AttackPower", 0);
        }
   }
    void Update()
    {
        if (sceneFlag)
        {
            MapChange();
        }
        if (PlayerFlagManager.death_flag)
        {
            Respown();
        }
    }
    void MapChange()
    {
        fadePanel.GetComponent<Image>().color = new Color(red, green, blue, alfa);
        alfa += speed * Time.deltaTime;
        nowtime -= speed * Time.deltaTime;
        if (nowtime <= 0)
        {
           SceneManager.LoadScene(goal_Scene);
        }
    }
    void Respown()
    {
        fadePanel.GetComponent<Image>().color = new Color(red, green, blue, alfa);
        alfa += speed * Time.deltaTime;
        nowtime -= speed * Time.deltaTime;
        if (nowtime <= 0)
        {
            RespownPositionChange();
            status.HpChange(status.MaxHp);
            alfa = 0;
            nowtime = time;
        }
    }
    void RespownPositionChange()
    {
        status.DeathPenalty();
        fadePanel.GetComponent<Image>().color = new Color(red, green, blue, 0);
        var number = PlayerPrefs.GetInt("RespownPosition", 0);
        status.RespownPosition(number, respownPostion[number]);
        stageTitle[number].SetActive(true);
        PlayerFlagManager.death_flag = false;
    }
    
    void OnTriggerEnter(Collider col)
    {
        if(col.gameObject.tag=="Player")
        {
            sceneFlag = true;
        }
    }
}
