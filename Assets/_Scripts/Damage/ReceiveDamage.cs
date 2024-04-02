using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReceiveDamage : HighMonoBehaviour
{
    [SerializeField] protected float maxhealth = 100;
    [SerializeField] protected float currentHealth = 100;
    public virtual void AddDamage(float damage){
        currentHealth -= damage;
        if(currentHealth <= 0){
            Die();
        }
    }
    public virtual void Die(){
        Destroy(transform.parent.gameObject);
    }
}
