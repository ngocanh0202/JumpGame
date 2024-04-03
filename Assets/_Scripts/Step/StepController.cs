using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(BoxCollider2D))]
public class StepController : HighMonoBehaviour
{
    [SerializeField] private BulletDebug debug;
    [SerializeField] private int score;
    public BulletDebug Debug { get => debug;}
    public int Score { get => score; set => score = value; }

    protected override void LoadComponents()
    {
        base.LoadComponents();
        score = 1;
    }

    protected void GetSendDebug(Collision2D gameObj,BulletDebug bulletDebug)
    {
        var receiveDebug = gameObj.gameObject.GetComponent<PlayerController>().receiveDebug.GetComponent<ReceiveDebug>();
        if(receiveDebug.isActiveAndEnabled)
            receiveDebug._ReceiveDebug(bulletDebug, 0.5f);
    }
    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if(score > 0){
                MusicManager.Instance.PlayMusic("Increase Score");
            }
            GetSendDebug(other,debug);
            StepSetup._Instance.ActionSetAndDespawn(transform.gameObject);
            ScoreManager._Instance.AddScore(score);
            score = 0;
        }
    }
}
