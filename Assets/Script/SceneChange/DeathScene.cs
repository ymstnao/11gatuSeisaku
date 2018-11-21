using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class DeathScene : MonoBehaviour {
    [SerializeField]
    private string NextSceneName;
    // Use this for initializatio
    [SerializeField]
    private float speed = 0.01f;
    [SerializeField]
    private float time = 3f;
    private float alfa;
    private float red, green, blue;
    private bool changeFlag;
    [SerializeField]
    private GameObject fadePanel;
	void Start () {
        red = fadePanel.GetComponent<Image>().color.r;
        green = fadePanel.GetComponent<Image>().color.g;
        blue = fadePanel.GetComponent<Image>().color.b;
        alfa = fadePanel.GetComponent<Image>().color.a;

        changeFlag = false;
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Space)||Input.GetButtonDown("Space"))
        {
            changeFlag = true;
        }
        if(changeFlag)
        {
            SceneChange();
        }
	}
    void SceneChange()
    {
        fadePanel.GetComponent<Image>().color = new Color(red, green, blue, alfa);
        alfa += speed * Time.deltaTime;
        time -= speed * Time.deltaTime;
        if (time <= 0)
        {
            SceneManager.LoadScene(NextSceneName);
        }
    }
}
