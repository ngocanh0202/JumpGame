using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemyRandomAttack : HighMonoBehaviour
{
    private static EnemyRandomAttack instance;
    public static EnemyRandomAttack Instance { get => instance;}
    [SerializeField] protected List<GameObject> attack_points;
    [SerializeField] Transform player_transform;
    [SerializeField] Vector3 dir;
    [Header("Attack Settings")]
    [SerializeField] float time_start;
    [SerializeField] float time_repeting;
    // [SerializeField] string bullet_name = "Buttlet_fire";
    [SerializeField] List<string> bulletTypes;
    protected override void LoadComponents()
    {
        base.LoadComponents();
        if(instance != null)
        {
            Debug.LogWarning("There are multiple EnemyRandomAttack in the scene");
        }
        instance = this;
        // Get all attack points
        attack_points.Clear();
        Transform points = transform.Find("Points");
        foreach(Transform point in points)
        {
            attack_points.Add(point.gameObject);
        }
        player_transform = GameObject.Find("Player").transform;
        
        time_start = 0.5f;
        time_repeting = 2f;
        // Start attacking
        InvokeRepeating("RandomAttack", time_start, time_repeting);

        // Get all bullet types
        bulletTypes.Clear();
        foreach (BulletType bulletType in Enum.GetValues(typeof(BulletType)))
        {
            bulletTypes.Add(bulletType.ToString());
        }
    }
    public void RandomAttack()
    {
        int randomPoint = RandomPoint();
        string bullet_name = RandomBulletType();
        Vector3 positon = attack_points[randomPoint].transform.position;
        positon.z = 0;
        Vector3 direction = GetDirectionToPlayer(positon);

        GameObject attack_point = BulletSpawner.Instance.SpawnObj(positon,direction,bullet_name);
        attack_point.SetActive(true);
    }
    public int RandomPoint(){
        return Random.Range(0, attack_points.Count);
    }
    public string RandomBulletType(){
        return bulletTypes[Random.Range(0, bulletTypes.Count)];
    }
    public Vector3 GetDirectionToPlayer(Vector3 position_point)
    {
        Vector3 position_player = player_transform.position;
        dir = (position_player - position_point).normalized;
        return dir;
    }
    public void SetUpAttack()
    {
        CancelInvoke("RandomAttack");
        
    }
    public void SetUpAttack(float decreasetime){
        if (decreasetime.Equals(0f))
        {
            Debug.LogWarning("Time is 0, so the attack will not start");
            return;
        }
        CancelInvoke("RandomAttack");
        time_repeting -= decreasetime;
        InvokeRepeating("RandomAttack", time_start,time_repeting);
    }
}
