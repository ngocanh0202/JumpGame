using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    static ScoreManager Instance;
    public static ScoreManager _Instance
    {
        get
        {
            if (Instance == null)
            {
                Instance = FindObjectOfType<ScoreManager>();
            }
            return Instance;
        }
    }
    [SerializeField] private int score;
    public int Score { get => score; set => score = value; }

    public void AddScore(int _score){
        score += _score;
        Debug.Log("Score: " + score);
        LevelSystemManager._Instance.CheckScore(score);
        UiManager.Instance.UpdateScore(score);
    }
}
