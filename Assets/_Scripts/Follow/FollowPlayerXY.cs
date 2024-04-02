using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayerXY : Follow
{
    protected override void _FollowPlayer()
    {
        Vector3 playerPosition = player_transform.position;
        playerPosition.z = -10;
        playerPosition.y += 1.5f;
        transform.position = playerPosition;
    }

}
