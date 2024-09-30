using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Building : MonoBehaviour
{
    
    [Header("List of prefab")]
    public GameObject prefab;
    [Header("Spawn status")]
    public float spawnCountdown;
    public int onSpawn;
    public float spawnRate,spawnTime;
    public int canSpawn,maxSpawn;
    [Header("Building Status")]
    public float buildingRadius;
    public int buildingLevel;
    public AiSystem.LineUpDirection direction;
    void Awake()
    {
        
    }
    
    
    public void SpawnSoldier(GameObject soldier)
    {
        GameObject game = Instantiate(soldier,transform.position,soldier.transform.rotation);
        game.transform.SetParent(transform);
    }
    public List<Transform> CheckingOurForce(Transform baseObject)
    {
        Transform[] forceOfArm = baseObject.gameObject.GetComponentsInChildren<Transform>();
        List<Transform> allForce = new List<Transform>();
        foreach(Transform transform in forceOfArm)
        {
            if(transform.parent == baseObject)
            {
                allForce.Add(transform);
            }
        }
        return allForce;
    }
    public void GetInTheLine(Transform baseObject)
    {
        
    }
    void OnDrawGizmosSelected()
    {
        int index = Direction();
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position,buildingRadius);
        Gizmos.color = Color.green;
        List<Vector3> pos = AiSystem.AllPointWeNeed(buildingRadius, transform.position);
        Gizmos.DrawWireSphere(pos[index],1);
    }
    void GhostObject(GameObject baseObject)
    {
        GameObject ghostObject = baseObject;
        //Disable this game object collider
        Collider collider = ghostObject.GetComponent<Collider>();
        collider.enabled = false;
        //Make game object translucent
        Material ghostMaterial = ghostObject.GetComponent<Material>();
        Color ghostColor = ghostMaterial.color;
        ghostColor.a = 0.3f;
        ghostMaterial.color = ghostColor;

    }
    public void SpawnSoldier()
    {
        if(spawnCountdown > 0) 
        {
            spawnCountdown -= Time.deltaTime;
        }
        
        if(spawnCountdown < 0 && onSpawn < canSpawn)
        {
            SpawnSoldier();
            spawnCountdown = spawnTime;
        }
    }
    int Direction()
    {
        int index = 0;
        switch(direction)
        {
            case AiSystem.LineUpDirection.Forward:
                index = 0;
                break;
            case AiSystem.LineUpDirection.Back:
                index = 1;
                break;
            case AiSystem.LineUpDirection.Right:
                index = 2;
                break;
            case AiSystem.LineUpDirection.Left:
                index = 3;
                break;
        }
        return index;
    }
}
