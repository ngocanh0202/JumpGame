using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Manager.InputManager;

public class JumpController : HighMonoBehaviour
{
    [Header("Jump")]
    [SerializeField] LayerMask groundLayer;
    [SerializeField] public bool isGround;
    [Header("Gravity")]
    [SerializeField] Vector2 verGravity;
    [SerializeField] float fallMultiplier;
    [SerializeField] PlayerController playerController;
    protected override void LoadComponents()
    {
        verGravity = new Vector2(0, -Physics2D.gravity.y);
        playerController = GetComponentInParent<PlayerController>();
        fallMultiplier = 2.5f;
        
        groundLayer = LayerMask.GetMask("Ground");
    }
    void FixedUpdate()
    {
        Jumpbehavior();
    
    }
    void Jumpbehavior(){
        isGround = IsGround();
        JumpInput();
        isJumping();
    }
    void JumpInput(){
        float jumpInput = InputManager.Instance.JumpInput;
        if(jumpInput > 0 && isGround && playerController.CanJump){
            float vetorX = playerController.Player_rigidbody2D.velocity.x;
            playerController.Player_rigidbody2D.velocity = new Vector2(vetorX, playerController.JumpPower);
        }
    }
    public void isJumping(){
        if(isGround) return;
        if(playerController.Player_rigidbody2D.velocity.y < 0){
            playerController.Player_rigidbody2D.velocity -= verGravity * fallMultiplier * Time.fixedDeltaTime;
        }

    }
    public bool IsGround()
    {
        Vector3 playerBoxCenter = playerController.Player_collider2D.bounds.center;
        float _distanceToTheGround = playerController.Player_collider2D.bounds.extents.y;
        
        RaycastHit2D raycastHit2D = Physics2D.Raycast(playerBoxCenter, Vector2.down, _distanceToTheGround + 0.1f, groundLayer);
        Color rayColor = Color.yellow; // Assign a default color value to 'rayColor'
        if (raycastHit2D.collider != null)
        {
            rayColor = Color.green;
        }
        Debug.DrawRay(playerBoxCenter, Vector3.down * (_distanceToTheGround + 0.1f), rayColor);

        return raycastHit2D.collider != null;
    }
}
