using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DespawnByDistance : Despawn
{
    [SerializeField] protected float despawnDistance;
    [SerializeField] protected float distance;
    [SerializeField] private Transform cameraTransform;
    

    protected override void LoadComponents()
    {
        base.LoadComponents();
        cameraTransform = Camera.main.transform.parent;
    }
    protected override void CanDespawn(){
        distance = Vector3.Distance(cameraTransform.position, transform.parent.position);
        if(distance < despawnDistance) return;
        DespawnObj();
    }
    protected override void DespawnObj(){
        Destroy(transform.parent.gameObject);
    }
}
