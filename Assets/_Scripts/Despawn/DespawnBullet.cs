using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DespawnBullet : DespawnByDistance
{
    [SerializeField] private BulletController bulletController;
    protected override void LoadComponents(){
        base.LoadComponents();
        bulletController = GetComponentInParent<BulletController>();
        despawnDistance = bulletController.BulletScriptable.distance;
    }
    protected override void DespawnObj(){
        BulletSpawner.Instance.Despawner(transform.parent.gameObject);
    }
}
