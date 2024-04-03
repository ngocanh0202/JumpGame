using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;
using Step;
using Manager.InputManager;

public class StepSetup : HighMonoBehaviour
{
    static StepSetup Instance;
    public static StepSetup _Instance
    {
        get
        {
            if (Instance == null)
            {
                Instance = FindObjectOfType<StepSetup>();
            }
            return Instance;
        }
    }

    public float RangeWidth { get => rangeWidth; set => rangeWidth = value; }

    [SerializeField] private float _timeToSpawn;
    [SerializeField] private float rangeHeigh;
    [SerializeField] private float rangeWidth;
    [SerializeField] private float currentX;
    [SerializeField] private int spawnerLimit;
    [SerializeField] protected Transform player;
    [SerializeField] private List<BulletType> steptypes;
    [SerializeField] private BulletType oldStepType;
    [SerializeField] private List<GameObject> steps;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        rangeHeigh = 2.15f;
        rangeWidth = 10f;
        currentX = rangeWidth;
        spawnerLimit = 5;

        steptypes.Clear();
        foreach (BulletType _step in Enum.GetValues(typeof(BulletType)))
        {
            steptypes.Add(_step);
        }

        player = GameObject.Find("Player").transform;

        Init();
    }
    protected void Init()
    {
        currentX = rangeWidth;

        for (int i = 0; i < spawnerLimit; i++)
        {
            GameObject step = SetNewStep();
            steps.Add(step);
        }
    }
    public void ActionSetAndDespawn(GameObject gameObject)
    {
        if(IsMiddleStep(gameObject)){
            Despawn_Headstep();
            GameObject newStep = SetNewStep();
            newStep.GetComponent<StepController>().Score = 1;
            steps.Add(newStep);
        }
    }
    protected bool IsMiddleStep(GameObject gameObject){
        if(steps[steps.Count/2] == gameObject)
            return true;
        return false;
    }
    protected GameObject SetNewStep(){
        float y = Random.Range(-rangeHeigh, rangeHeigh); // Random y position
        Vector3 position = new Vector3(currentX, y, 0);
        
        BulletType stepType = GetNewType(); // Take step type
        string stepTypeString = stepTypeToString(stepType);

        var step = StepSpawner.Instance?.SpawnObj(position, Vector3.up, stepTypeString);
        currentX += rangeWidth;
        step?.SetActive(true);
        return step;
    }
    protected void Despawn_Headstep(){
        GameObject headStep = steps[0];
        steps.RemoveAt(0);
        StepSpawner.Instance?.Despawner(headStep);
    }
    protected BulletType GetNewType()
    {
        BulletType stepType = steptypes[Random.Range(0, steptypes.Count)]; // Take step type
        while (stepType == oldStepType)
        {
            stepType = steptypes[Random.Range(0, steptypes.Count)];
        }

        oldStepType = stepType;
        return oldStepType;
    }
    protected string stepTypeToString(BulletType stepType)
    => stepType switch
    {
        BulletType.Buttlet_fire => "Step_fire",
        BulletType.Buttlet_ice => "Step_ice",
        BulletType.Buttlet_posion => "Step_posion",
        BulletType.Buttlet_water => "Step_water",
        _ => "NoStep"
    };
}
