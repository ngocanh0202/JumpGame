using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Despawn : HighMonoBehaviour
{
    void Update()
    {
        CanDespawn();
    }
    protected abstract void CanDespawn();
    protected abstract void DespawnObj();
    
}
