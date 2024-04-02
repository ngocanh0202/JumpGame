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
    [SerializeField] Transform ui;
    [SerializeField] Transform score;
    [SerializeField] Transform hp;
    [SerializeField] Transform GameManager;
    [SerializeField] Button btnStart;
    [SerializeField] Button btnQuit;
    [SerializeField] TextMeshProUGUI score_txt;
    [SerializeField] Slider hp_slider;
    protected override void LoadComponents()
    {
        base.LoadComponents();
        LoadUIInGame();
        LoadUIInMenu();
    }
    protected virtual void LoadUIInGame()
    {
        ui = GameObject.Find("UI").transform;
        score = ui.Find("Score");
        score_txt = score.GetComponent<TextMeshProUGUI>();

        hp = ui.Find("HP");
        hp_slider = hp.GetComponent<Slider>();
    }
    protected virtual void LoadUIInMenu()
    {
        GameManager = GameObject.Find("GameManager").transform;
        Time.timeScale = 0;
        Transform handleButton = GameManager.Find("HandleButton").transform;
        btnStart = handleButton.Find("StartGame").GetComponent<Button>();
        btnQuit = handleButton.Find("QuitGame").GetComponent<Button>();
        
        btnStart.onClick.AddListener(TriggerGameManager);
        btnQuit.onClick.AddListener(Application.Quit);
    }
    public void UpdateScore(int score)
    {
        score_txt.text = score.ToString();
    }
    public void UpdateHp(float hp, float maxHp)
    {
        hp_slider.maxValue = maxHp;
        hp_slider.value = hp;
    }
    public void ReStartGame(){
        LoadScene("MainScene");
        TriggerGameManager();
    }
    public void TriggerGameManager()
    {
        GameManager.gameObject.SetActive(!GameManager.gameObject.activeSelf);
        if (Time.timeScale == 0)
        {
            Time.timeScale = 1;
        }
        else
        {
            Time.timeScale = 0;
        }
    }
    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}
