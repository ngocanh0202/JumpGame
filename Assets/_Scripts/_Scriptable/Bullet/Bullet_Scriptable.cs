using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "_Scriptable", menuName = "ScriptableObjects/Bullet_Scriptable")]
public class BulletScriptable : ScriptableObject
{
    public string bulletName;
    public int damage;
    public float speed;
    public float distance;
    public BulletDebug debug;
    public BulletType type_bullet;
    public float countDown;
}
