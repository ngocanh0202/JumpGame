using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowBackGround : Follow
{
    protected override void _FollowPlayer()
    {
        Vector3 playerPosition = player_transform.position;
        transform.position = playerPosition;
    }
}
