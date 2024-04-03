using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : HighMonoBehaviour
{
    [Header("Components")]
    [SerializeField] private Rigidbody2D player_rigidbody2D;
    [SerializeField] private BoxCollider2D player_collider2D;
    [SerializeField] private Transform handleChemicalReac;
    [SerializeField] public Transform playerMovement { get; set; }
    [SerializeField] public Transform receiveDebug { get; set; }
    [SerializeField] public Transform receiveDamage { get; set; }
    [Header("Movement")]
    [SerializeField] float speed;
    [SerializeField] float dashPower;
    [SerializeField] float jumpPower;
    [SerializeField] bool canMove;
    [SerializeField] bool canDash;
    [SerializeField] bool canJump;
    [Header("HP")]
    [SerializeField] private float maxHP;
    [SerializeField] private float currentHP;

    public Rigidbody2D Player_rigidbody2D { get => player_rigidbody2D; }
    public BoxCollider2D Player_collider2D { get => player_collider2D; }
    public Transform HandleChemicalReac { get => handleChemicalReac; }
    public float Speed { get => speed; set => speed = value; }
    public float DashPower { get => dashPower; set => dashPower = value; }
    public float JumpPower { get => jumpPower; set => jumpPower = value; }
    public float MaxHP { get => maxHP; set => maxHP = value; }
    public float CurrentHP { get => currentHP; set => currentHP = value; }
    public bool CanMove { get => canMove; set => canMove = value; }
    public bool CanDash { get => canDash; set => canDash = value; }
    public bool CanJump { get => canJump; set => canJump = value; }



    // Start is called before the first frame update
    protected override void LoadComponents()
    {
        try
        {
            player_rigidbody2D = GetComponent<Rigidbody2D>();
            player_collider2D = GetComponent<BoxCollider2D>();
            handleChemicalReac = transform.Find("ChemicalReac");
            receiveDebug = transform.Find("ReceiveDebug");
            receiveDamage = transform.Find("ReceiveDamage");
            playerMovement = transform.Find("PlayerMovement");

            speed = 13f;
            dashPower = 17f;
            jumpPower = 15f;
            maxHP = 100f;
            currentHP = maxHP;

            canMove = true;
            canDash = true;
            canJump = true;
        }
        catch (System.Exception e)
        {
            Debug.LogError("PlayerController.cs: " + e.Message);
        }
    }
    public void PlayerDeath()
    {
        canMove = false;
        canDash = false;
        canJump = false;

        float x = 0;
        float y = 1;
        player_rigidbody2D.velocity = new Vector2(x, y) * jumpPower;
        MusicManager.Instance.PlayMusic("Death");
        StartCoroutine(WaitPlayerDeath());
    }
    private IEnumerator WaitPlayerDeath(){
        yield return new WaitForSeconds(0.4f);
        UiManager.Instance.DisableScoreMenu();
    }
}
