using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.Tilemaps;

public class StepGenerator : MonoBehaviour
{
    [SerializeField] int currentX;
    [SerializeField] int currentY;
    [SerializeField] int maxX;
    [SerializeField] int maxY;
    [SerializeField] Tilemap tilemap;
    [SerializeField] Tile floor;

    void Start()
    {
        maxX = 12; // Range of the floor in the x axis (12 to -12)
        maxY = 100;

        currentX = -maxX;
        currentY = -9;

        tilemap = GameObject.Find("Tilemap").GetComponent<Tilemap>();

        StartCoroutine(GenerateFloor());
    }
    protected virtual IEnumerator GenerateFloor(){
        while(currentY < maxY){
            currentX = Random.Range(-maxX, maxX-1);

            SetFloor(3);
            
            currentY += Random.Range(4,5);
        }
        yield return new WaitForSeconds(0.1f);
        Debug.Log("Finish");
    }
    protected virtual void SetFloor(int rangeX){
        for(int i = currentX ; i < rangeX; i++){
            tilemap.SetTile(new Vector3Int(i, currentY, 0), floor);
        }
    }

}
