using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Manager.InputManager;

public class DashController : HighMonoBehaviour
{
    [SerializeField] float dashingTime;
    [SerializeField] bool canDash = true;
    [SerializeField] bool isDashing = false;
    [SerializeField] Vector2 dashDirection;
    [SerializeField] JumpController jumpController;
    [SerializeField] private PlayerController playerController;

    protected override void LoadComponents()
    {
        playerController = GetComponentInParent<PlayerController>();
        jumpController = GetComponent<JumpController>();
        dashingTime = 0.3f;
    }
    void FixedUpdate()
    {
        DashInput();
    }
    void DashInput(){
        bool shiftInput = InputManager.Instance.ShiftInput;
        float vetorX = InputManager.Instance.HorizontalInput;
        float vetorY = InputManager.Instance.VerticalInput;
        // Check if the player can dash
        if(shiftInput && canDash && playerController.CanDash){
            canDash = false;
            isDashing = true;

            dashDirection = new Vector2(vetorX, vetorY).normalized;
            if(dashDirection == Vector2.zero){
                dashDirection = new Vector2(0,0).normalized;
            }
            StartCoroutine(StopDashTime());
        }
        // Check if the player is dashing
        if(isDashing){
            playerController.Player_rigidbody2D.velocity = dashDirection * playerController.DashPower;
            return;
        }
        // Check if the player is on the ground
        // To do: should be fix later
        if(jumpController.isGround){
            canDash = true;
        }
    }
    IEnumerator StopDashTime(){
        yield return new WaitForSeconds(dashingTime);
        isDashing = false;
    }
}
