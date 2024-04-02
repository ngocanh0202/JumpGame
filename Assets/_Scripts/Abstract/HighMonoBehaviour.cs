using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class HighMonoBehaviour : MonoBehaviour
{
    protected virtual void Reset()
    {
        this.LoadComponents();
    }

    protected virtual void Start()
    {
        this.LoadComponents();
    }

    protected virtual void LoadComponents()
    {
        // For override here
    }

}
