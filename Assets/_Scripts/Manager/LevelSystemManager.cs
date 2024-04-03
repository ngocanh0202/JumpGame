using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelSystemManager : HighMonoBehaviour
{
    static LevelSystemManager Instance;
    public static LevelSystemManager _Instance
    {
        get
        {
            if (Instance == null)
            {
                Instance = FindObjectOfType<LevelSystemManager>();
            }
            return Instance;
        }
    }
    [SerializeField] private int level;
    [SerializeField] private int maxLevel;
    [SerializeField] private int scoreToNextLevel;
    [SerializeField] private int scoreIncrement = 5;
    protected override void LoadComponents()
    {
        base.LoadComponents();
        level = 1;
        maxLevel = 40;
        scoreToNextLevel = 5;
        scoreIncrement = 5;
    }
    public void CheckScore(float score){
        if(score >= scoreToNextLevel){
            LevelUp();
        }
    }
    public void LevelUp(){
        if(level <= maxLevel){
            level++;
            scoreToNextLevel += scoreIncrement;
            Debug.Log("Level up! Level: " + level);

            UiManager.Instance.UpdateLv(level);
            StepSetup._Instance.RangeWidth += 0.1f;
            EnemyRandomAttack.Instance?.SetUpAttack(0.1f);
        }
    }
}
