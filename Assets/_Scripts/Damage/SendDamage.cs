using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;


public class SendDamage : HighMonoBehaviour
{
    [SerializeField] protected int damage = 10;
    public int Damage { get => damage; set => damage = value; }
    
    public void SendDamageTo(GameObject gameObject){
        EventSendDamage(gameObject);
    }
    protected virtual void EventSendDamage(GameObject gameObject){
        ReceiveDamage receiveDamage = gameObject.GetComponentInChildren<ReceiveDamage>();
        if(receiveDamage != null){
            receiveDamage.AddDamage(damage);
        }
    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        SendDamageTo(other.gameObject);
        DestroyObj();
    }
    protected virtual void DestroyObj(){
        Destroy(gameObject);
    }
}
