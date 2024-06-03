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
        float currentHP = playerController.CurrentHP -= damage;
        if(currentHP > playerController.MaxHP){
            return;
        }
        playerController.CurrentHP = currentHP;
        UiManager.Instance.UpdateHp(currentHP, maxhealth);
        if(currentHP <= 0){
            Die();    
        }
    }
    public override void Die(){
        Debug.Log("Player died!");
        EnemyRandomAttack.Instance.SetUpAttack(); 
        playerController.PlayerDeath();
    }
}
