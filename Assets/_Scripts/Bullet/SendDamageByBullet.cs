using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CircleCollider2D))]
public class SendDamageByBullet : SendDamage
{
    private CircleCollider2D circleCollider2D;
     [SerializeField] private BulletController bulletController;
    protected override void LoadComponents()
    {
        circleCollider2D = GetComponent<CircleCollider2D>();
        circleCollider2D.radius = 0.1f;
        bulletController = GetComponentInParent<BulletController>();
        damage = bulletController.BulletScriptable.damage;
    }
    protected override void EventSendDamage(GameObject gameObject){
        base.EventSendDamage(gameObject);
        ReceiveDebug receiveDebug = gameObject.GetComponentInChildren<ReceiveDebug>();
        if(receiveDebug != null)
            receiveDebug._ReceiveDebug(bulletController.BulletScriptable.debug, bulletController.BulletScriptable.countDown);
        
    }
    protected override void DestroyObj()
    {
        BulletSpawner.Instance.Despawner(transform.parent.gameObject);
    }
}
