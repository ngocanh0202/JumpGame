using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetPlayer : HighMonoBehaviour
{
    [SerializeField] private BulletController bulletController;
    [SerializeField] float speed;
    
    
    protected override void LoadComponents()
    {
        base.LoadComponents();
        bulletController = GetComponentInParent<BulletController>();
        speed = bulletController.BulletScriptable.speed;
    }
    void Update()
    {
        MoveToPlayer();
    }
    void MoveToPlayer(){
        // Move forward
        transform.parent.position += transform.parent.up * speed * Time.deltaTime;
    }
    
}
