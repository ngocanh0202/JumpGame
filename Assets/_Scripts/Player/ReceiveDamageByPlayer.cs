using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReceiveDamageByPlayer : ReceiveDamage
{
    [SerializeField] private PlayerController playerController;
    protected override void LoadComponents(){
        base.LoadComponents();
        playerController = GetComponentInParent<PlayerController>();
        maxhealth = playerController.MaxHP;
        currentHealth = maxhealth;
    }
    public override void AddDamage(float damage){
        playerController.CurrentHP -= damage;
        UiManager.Instance.UpdateHp(playerController.CurrentHP, playerController.MaxHP);
        if(playerController.CurrentHP <= 0){
            Die();
        }
    }
    public override void Die(){
        Debug.Log("Player died!");
        EnemyRandomAttack.Instance.SetUpAttack(); // Pass a float value as an argument
        UiManager.Instance.ReStartGame();
    }
}
