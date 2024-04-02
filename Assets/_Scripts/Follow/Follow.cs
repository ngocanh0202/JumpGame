using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Follow : HighMonoBehaviour
{
    [SerializeField] protected Transform player_transform;
    protected override void LoadComponents()
    {
        base.LoadComponents();
        player_transform = GameObject.Find("Player").transform;
    }
    void LateUpdate()
    {
        _FollowPlayer();
    }
    protected abstract void _FollowPlayer();
}
