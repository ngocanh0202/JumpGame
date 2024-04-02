using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletSpawner : Spawner
{
    private static BulletSpawner instance;
    public static BulletSpawner Instance { get => instance;}
    
    protected override void LoadComponents()
    {
        base.LoadComponents();
        // Singleton
        if(instance != null)
        {
            Debug.LogWarning("There are multiple BulletSpawner in the scene");
        }
        instance = this;
    }
    
    
}
