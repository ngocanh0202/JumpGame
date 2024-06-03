using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UiManager : HighMonoBehaviour
{
    static UiManager instance;
    public static UiManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<UiManager>();
            }
            return instance;
        }
    }
    [Header("Menu")]
    [SerializeField] Transform GameManager;
    [Header("Menu Menu")]
    [SerializeField] Transform menu;
    [SerializeField] Button btnStart;
    [SerializeField] Button btnQuit;
    [Header("Menu Score")]
    [SerializeField] Transform scoreMenu;
    [SerializeField] TextMeshProUGUI scoreMenu_txt;
    [Header("InGame")]
    [SerializeField] Transform uiINGame;
    [Header("In Game HP")]
    [SerializeField] Transform hp;
    [SerializeField] Slider hp_slider;
    [Header("In Game Score")]
    [SerializeField] Transform score;
    [SerializeField] TextMeshProUGUI score_txt;
    [SerializeField] Button btnReStart;
    [Header("In Game Level")]
    [SerializeField] Transform lv;
    [SerializeField] TextMeshProUGUI lv_txt;
    [Header("In Game Status")]
    [SerializeField] Transform status;
    [SerializeField] TextMeshProUGUI status_txt;

    [Header("Manager Score")]
    [SerializeField] int currentScore;
    [SerializeField] int currentLv;

    
    protected override void LoadComponents()
    {
        base.LoadComponents();
        LoadUIInGame();
        LoadUIInMenu();

        currentScore = 0;
        currentLv = 1;
    }
    protected virtual void LoadUIInMenu()
    {
        GameManager = GameObject.Find("UIGameManager").transform;
        Time.timeScale = 0;
        menu = GameManager.Find("Menu");
        btnStart = menu.Find("HandleButton/StartGame").GetComponent<Button>();
        btnQuit = menu.Find("HandleButton/QuitGame").GetComponent<Button>();
        
        btnStart.onClick.AddListener(TriggerMenu);
        btnQuit.onClick.AddListener(Application.Quit);

        scoreMenu = GameManager.Find("ScoreMenu");
        scoreMenu_txt = scoreMenu.Find("Score").GetComponent<TextMeshProUGUI>();
        btnReStart = scoreMenu.Find("Button").GetComponent<Button>();
        btnReStart.onClick.AddListener(ReStartGame);

        
    }
    protected virtual void LoadUIInGame()
    {
        uiINGame = GameObject.Find("UIINGame").transform;

        score = uiINGame.Find("Score");
        score_txt = score.GetComponent<TextMeshProUGUI>();

        hp = uiINGame.Find("HP");
        hp_slider = hp.GetComponent<Slider>();

        lv = uiINGame.Find("Level");
        lv_txt = lv.GetComponent<TextMeshProUGUI>();

        status = uiINGame.Find("Status");
        status_txt = status.GetComponent<TextMeshProUGUI>();
    }
    public void UpdateStatus(string status)
    {
        status_txt.text = status;
    }
    public void LastScore(string score)
    {
        scoreMenu_txt.text = "Your score: " + score;
    }
    public void UpdateScore(int score)
    {
        score_txt.text = score.ToString();
        currentScore = score;
    }
    public void UpdateLv(int lv)
    {
        lv_txt.text = "Difficult level: " + lv.ToString();
        currentLv = lv;
    }
    public void UpdateHp(float hp, float maxHp)
    {
        hp_slider.maxValue = maxHp;
        if(hp <= maxHp){
            hp_slider.value = hp;
        }
    }
    
    public void TriggerMenu()
    {
        menu.gameObject.SetActive(!menu.gameObject.activeSelf);
        if (Time.timeScale == 0)
        {
            Time.timeScale = 1;
        }
        else
        {
            Time.timeScale = 0;
        }
    }
    public void DisableScoreMenu()
    {
        Time.timeScale = 0;
        scoreMenu.gameObject.SetActive(true);
        string catulateScore = currentLv + " x " + currentScore + " = " + (currentLv * currentScore);
        LastScore(catulateScore);
    }
    public void ReStartGame(){
        LoadScene("MainScene");
    }
    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}
