using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class FollowPlayerX : Follow
{
    [SerializeField] BoxCollider2D boxCollider2D;
    [SerializeField] float setVectorY;
    protected override void LoadComponents()
    {
        base.LoadComponents();
        boxCollider2D = GetComponent<BoxCollider2D>();
        boxCollider2D.isTrigger = true;
        boxCollider2D.size = new Vector2(4, 1);
        setVectorY = -10f;
        
    }
    protected override void _FollowPlayer()
    {
        float playerPositionX = player_transform.position.x;
        Vector3 movePosition = new Vector3(playerPositionX, setVectorY, transform.position.z);
        transform.parent.position = movePosition;
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            UiManager.Instance.ReStartGame();
        }
    }
}
