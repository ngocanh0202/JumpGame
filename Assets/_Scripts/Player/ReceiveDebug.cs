using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ReceiveDebug : HighMonoBehaviour
{
    [Header("Debug")]
    [SerializeField] protected bool isBurn;
    [SerializeField] protected bool isBleed;
    [SerializeField] protected bool isSlow;
    [SerializeField] protected bool isWet;
    [Header("Debug Time")]
    [SerializeField] protected float _countDown;
    [SerializeField] protected float _timeLifeDebug;
    [SerializeField] PlayerController playerController;
    [SerializeField] ChemicalReac chemicalReac;
    [SerializeField] Dictionary<BulletDebug, bool> debugs;
    [SerializeField] BulletDebug currentDebug;
    [SerializeField] string debugName;
    [Header("Player Status")]
    [SerializeField] protected float _maxHP;
    [SerializeField] protected float _dashPower;
    [SerializeField] protected float _jumpPower;
    [SerializeField] protected float _speed;
    [Header("Component HandleChemicalReac")]
    [SerializeField] HandleChemicalReac handleChemicalReac;
    protected override void LoadComponents(){
        base.LoadComponents();
        playerController = GetComponentInParent<PlayerController>();
        _timeLifeDebug = 0;
        _countDown = 0;

        chemicalReac = ChemicalReac.NoReac;
        debugs = new Dictionary<BulletDebug, bool>();
        debugs.Add(BulletDebug.Bleed, false);
        debugs.Add(BulletDebug.Burn, false);
        debugs.Add(BulletDebug.Slow, false);
        debugs.Add(BulletDebug.Wet, false);

        _maxHP = playerController.MaxHP;
        _dashPower = playerController.DashPower;
        _jumpPower = playerController.JumpPower;
        _speed = playerController.Speed;

        handleChemicalReac = playerController.HandleChemicalReac.GetComponent<HandleChemicalReac>();
    }
    // public void Update(){
    //     if(_timeLifeDebug < _countDown){
    //         _timeLifeDebug += Time.deltaTime;
    //        return;
    //     }
    //     if(_countDown == 0) return;
    //     ResetDebug();
    // }
    public virtual void _ReceiveDebug(BulletDebug bulletDebug, float countDown){
        _countDown = countDown;
        // Check bug || Todo: Delete it later
        switch (bulletDebug)
        {
            case BulletDebug.Bleed:
                isBleed = true;
                // debugName = "Bleed";
                break;
            case BulletDebug.Burn:
                isBurn = true;
                // debugName = "Burn";
                break;
            case BulletDebug.Slow:
                isSlow = true; 
                // debugName = "Slow";
                break;
            case BulletDebug.Wet:
                isWet = true;
                // debugName = "Wet";
                break;
            default:
                break;
        }
       

        debugName = bulletDebug.ToString();
        StartCoroutine(debugName);

        currentDebug = bulletDebug;
        AddDebug(currentDebug);

    }
    protected virtual IEnumerator Bleed(){
        //Debug.Log("Bleed");
        //UiManager.Instance.UpdateStatus("Bleed");
        yield return new WaitForSeconds(_countDown);
        ResetDebug();
        
    }
    protected virtual IEnumerator Burn(){
        //Debug.Log("Burn");
        //UiManager.Instance.UpdateStatus("Burn");
        playerController.Speed = _speed * 2;
        yield return new WaitForSeconds(_countDown);
        ResetDebug();
    }
    protected virtual IEnumerator Slow(){
        //Debug.Log("Slow");
        //UiManager.Instance.UpdateStatus("Slow");
        playerController.Speed /= 1.5f;
        playerController.DashPower /= 1.5f;
        playerController.JumpPower -= 1.5f;
        yield return new WaitForSeconds(_countDown);
        ResetDebug();
    }
    protected virtual IEnumerator Wet(){ 
        //Debug.Log("Wet");
        //UiManager.Instance.UpdateStatus("Wet");
        yield return new WaitForSeconds(_countDown);  
        ResetDebug();
    }
    protected virtual void AddDebug(BulletDebug bulletDebug){
        BulletDebug oldDebug = Checkdebug();
        if(oldDebug == bulletDebug) return;
        if(oldDebug != BulletDebug.noDebug){
           CheckReaction(oldDebug, bulletDebug);
           RemoveDebugs(bulletDebug);
           RemoveDebugs(oldDebug);
        }
        else{
            debugs[bulletDebug] = true;
        }

         _timeLifeDebug += _countDown;
    }
    protected virtual void RemoveDebugs(BulletDebug bulletDebug){
        debugs[bulletDebug] = false;
    }
    protected virtual BulletDebug Checkdebug(){
        foreach (var item in debugs)
        {
            if(item.Value){
                return item.Key;
            }
        }
        return BulletDebug.noDebug;
    }
    protected virtual void CheckReaction(BulletDebug oldDebug, BulletDebug newDebug){
        if(oldDebug == BulletDebug.Bleed && newDebug == BulletDebug.Burn
        || oldDebug == BulletDebug.Burn && newDebug == BulletDebug.Bleed){
            chemicalReac = ChemicalReac.Explode;
        }
        else if(oldDebug == BulletDebug.Burn && newDebug == BulletDebug.Slow
        || oldDebug == BulletDebug.Slow && newDebug == BulletDebug.Burn){
            chemicalReac = ChemicalReac.Melting;
        }
        else if(oldDebug == BulletDebug.Slow && newDebug == BulletDebug.Wet
        || oldDebug == BulletDebug.Wet && newDebug == BulletDebug.Slow){
            chemicalReac = ChemicalReac.Frozen;
        }
        else if(oldDebug == BulletDebug.Wet && newDebug == BulletDebug.Bleed
        || oldDebug == BulletDebug.Bleed && newDebug == BulletDebug.Wet){
            chemicalReac = ChemicalReac.Poisoned;
        }else{
            chemicalReac = ChemicalReac.NoReac;
        }
        EvenReaction(chemicalReac);
        ResetDebug();
    }
    protected virtual void EvenReaction(ChemicalReac chemicalReac){
        if(chemicalReac == ChemicalReac.NoReac) return; 
        float calculateTimeReact = _timeLifeDebug/2f;
        handleChemicalReac.TriggerChemicalReac(chemicalReac,calculateTimeReact);
    }
    protected virtual void ResetDebug(){
        isBurn = false;
        isBleed = false;
        isSlow = false;
        isWet = false;

        _countDown = 0;
        _timeLifeDebug = 0f;

        chemicalReac = ChemicalReac.NoReac;
        RemoveDebugs(currentDebug);
        currentDebug = BulletDebug.noDebug;

        StopCoroutine(debugName);

        playerController.MaxHP = _maxHP;
        playerController.DashPower = _dashPower;
        playerController.JumpPower = _jumpPower;
        playerController.Speed = _speed;

        //UiManager.Instance.UpdateStatus("Normal");
    }   

}
