using System;
using System.Collections;
using System.Collections.Generic;
using Manager.InputManager;
using UnityEngine;
using Random = UnityEngine.Random;

public class HandleChemicalReac : HighMonoBehaviour
{
    [SerializeField] protected PlayerController playerController;
    [SerializeField] protected ReceiveDamage receiveDamage;
    [SerializeField] protected JumpController JumpController;
    [SerializeField] protected Transform receiveDebug;
    [SerializeField] protected float timeFrozen;
    [SerializeField] protected float timePoisoned;
    [SerializeField] protected float timeExplode;
    [SerializeField] protected float timeMelting;
    protected override void LoadComponents(){
        base.LoadComponents();
        playerController = GetComponentInParent<PlayerController>();
        receiveDamage = playerController.receiveDamage.GetComponent<ReceiveDamage>();
        JumpController = playerController.playerMovement.GetComponent<JumpController>();
        receiveDebug = playerController.receiveDebug;

        timeExplode = 0.75f;
        timeMelting = 1.5f;
    }
    public void TriggerChemicalReac(ChemicalReac chemicalReac, float _timeReact)
    {
        receiveDebug.gameObject.SetActive(false);
        var eventReact = GetEventReact(chemicalReac, _timeReact);
        eventReact();
    }
    protected Action GetEventReact(ChemicalReac chemicalReac, float _timeReact)
    => chemicalReac switch{
        ChemicalReac.Explode => Explode,
        ChemicalReac.Melting => Melting,
        ChemicalReac.Frozen => () => Frozen(_timeReact),
        ChemicalReac.Poisoned => () => Poisoned(_timeReact),
        _ => () => Debug.Log("No Reaction")
    };
    protected void Explode(){
        Debug.Log("Explode");
        UiManager.Instance.UpdateStatus("Explode");
        MusicManager.Instance.PlayMusic("Explore");
        float vectoX = Random.Range(-1f, 1f);
        float vectoY = 1f;
        Vector2 direction = new Vector2(vectoX, vectoY);
        playerController.Player_rigidbody2D.velocity = Vector2.zero;
        playerController.Player_rigidbody2D.AddForce(direction * 1000f);
        PreventMovement();
        StartCoroutine(_IEtimeReact(timeExplode));
        //StartCoroutine(GroundedToExplore());
    }

    protected void Melting(){
        Debug.Log("Melting");
        UiManager.Instance.UpdateStatus("Melting");
        receiveDamage.AddDamage(10f);
        StartCoroutine(_IEtimeReact(timeMelting));
    }
    protected void Frozen(float _timeReact){
        Debug.Log("Frozen");
        UiManager.Instance.UpdateStatus("Frozen");
        playerController.Player_rigidbody2D.velocity = Vector2.zero;
        PreventMovement();
        timeFrozen = _timeReact;
        StartCoroutine(_IEtimeReact(_timeReact));
    }
    protected void Poisoned(float _timeReact){
        Debug.Log("Poisoned: " + _timeReact);
        UiManager.Instance.UpdateStatus("Poisoned");
        timePoisoned = _timeReact;
        StartCoroutine(DecreaseHPDuringTimeReact(timePoisoned));
    }
    // IEnumerator GroundedToExplore(){
    //     playerController.Player_rigidbody2D.AddForce(Vector2.up * 1000f);
    //     while(JumpController.isGround == false){

    //     }
    //     AllowMovement();
    //     yield return new WaitForSeconds(1f);
    //     receiveDebug.gameObject.SetActive(true);
    // }
    IEnumerator DecreaseHPDuringTimeReact(float _timeReact)
    {
        float elapsedTime = 0f;
        while (elapsedTime < _timeReact)
        {
            receiveDamage.AddDamage(1f);
            yield return new WaitForSeconds(1f);
            elapsedTime += 1f;
        }
        receiveDebug.gameObject.SetActive(true);
        UiManager.Instance.UpdateStatus("Normal");
    }
    protected IEnumerator _IEtimeReact(float _timeReact){
        yield return new WaitForSeconds(_timeReact);
        receiveDebug.gameObject.SetActive(true);
        UiManager.Instance.UpdateStatus("Normal");
        AllowMovement();
    }
    protected void PreventMovement(){
        playerController.CanMove = false;
        playerController.CanDash = false;
        playerController.CanJump = false;
    }
    protected void AllowMovement(){
        playerController.CanMove = true;
        playerController.CanDash = true;
        playerController.CanJump = true;
    }

}
