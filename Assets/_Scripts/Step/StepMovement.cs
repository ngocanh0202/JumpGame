using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StepMovement : HighMonoBehaviour
{
    [Header("Range Move")]
    [SerializeField] private float top_y;
    [SerializeField] private Vector2 newPositionTop;
    [SerializeField] private float bottom_y;
    [SerializeField] private Vector2 newPositionDown;
    [Header("Setting Move")]
    [SerializeField] private float speed;
    [SerializeField] private bool isMovingTop;
    [SerializeField] private int oldIndex;

    protected override void LoadComponents()
    {
        CatulatorPostionToMove();
    }
    void OnEnable()
    {
        CatulatorPostionToMove();
    }
    
    void FixedUpdate()
    {
        MoveStep();
    }
    void MoveStep(){
        if(isMovingTop)
            if(transform.parent.position.y != newPositionTop.y){
                transform.parent.position = Vector3.MoveTowards(transform.parent.position, newPositionTop,speed * Time.fixedDeltaTime);
            }else{
                ChangeDirection();
            }
        else{
            if(transform.parent.position.y != newPositionDown.y){
                transform.parent.position = Vector3.MoveTowards(transform.parent.position, newPositionDown,speed * Time.fixedDeltaTime);
            }else{
                ChangeDirection();
            }
        
        }
    }
    void ChangeDirection(){
        isMovingTop = !isMovingTop;
    }
    void CatulatorPostionToMove(){
        top_y = transform.parent.position.y + 3f;
        bottom_y = transform.parent.position.y - 3f;
        newPositionTop = new Vector2(transform.parent.position.x, top_y);
        newPositionDown = new Vector2(transform.parent.position.x, bottom_y);
        speed = 2f;

        isMovingTop =  Random.Range(0, 2) == 0 ? true : false;
    }
}
