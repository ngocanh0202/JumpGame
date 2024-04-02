using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Manager.InputManager;

public class MoveController : HighMonoBehaviour
{
    [SerializeField] PlayerController playerController;
    protected override void LoadComponents()
    {
        playerController = GetComponentInParent<PlayerController>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        MoveHorizonDirection();
    }
    void MoveHorizonDirection(){
        if(playerController.CanMove == false) return;
        float horizontalInput = InputManager.Instance.HorizontalInput;
        playerController.Player_rigidbody2D.velocity = new Vector2(horizontalInput * playerController.Speed, playerController.Player_rigidbody2D.velocity.y);
    }
}
