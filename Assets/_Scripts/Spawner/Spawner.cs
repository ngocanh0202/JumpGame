using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Spawner : HighMonoBehaviour
{
    [SerializeField] private Transform holder;
    [SerializeField] protected List<GameObject> prefabs;
    [SerializeField] protected List<GameObject> poolsObjects;

    public Transform Holder { get => holder;}

    protected override void LoadComponents()
    {
        base.LoadComponents();
        

        holder = transform.Find("Holder");
        
        // Get all bullet prefabs
        prefabs.Clear();
        Transform bullets = transform.Find("Prefabs");
        foreach (Transform child in bullets)
        {
            prefabs.Add(child.gameObject);
        }
    }
    public virtual void Despawner(GameObject gameObject)
    {
        gameObject.SetActive(false);
        poolsObjects.Add(gameObject);
        
    }
    protected virtual GameObject GetObjectWithName(string name)
    {
        foreach(GameObject obj in prefabs)
        {
            if(obj.name == name)
            {
                return obj;
            }
        }
        return null;
    }
    public GameObject SpawnObj(Vector3 position, Vector3 direction, string name)
    {
        GameObject prefab = GetObjectWithName(name);
        if(prefab == null)
        {
            Debug.LogError("There is no bullet with name: " + name);
            return null;
        }
        
        Quaternion rotation = Quaternion.LookRotation(Vector3.forward, direction);
        
        GameObject bullet = GetObjFromHoler(prefab, position, rotation);
        bullet.transform.SetParent(holder);
        return bullet;
    }
    protected GameObject GetObjFromHoler(GameObject prefab, Vector3 position, Quaternion rotation){
        foreach(GameObject obj in poolsObjects){
            if(obj.name == prefab.name +"(Clone)" ){
                obj.transform.position = position;
                obj.transform.rotation = rotation;
                poolsObjects.Remove(obj);
                return obj;
            }
        }
        GameObject bullet = Instantiate(prefab, position, rotation);
        return bullet;
    }
}
