 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class AiSystem 
{
    public enum LineUpDirection 
    {
        Forward,
        Back,
        Right,
        Left
    }
    public static Vector3 WhereToMove(Vector3 ShelterPos,LineUpDirection Direction,float BuildingRadius)
    {
        Vector3 goToThatLocation = new Vector3();
        
        return goToThatLocation;
    }
    
    
    public static List<Vector3> AllPointWeNeed(float radius,Vector3 pos)
    {
        List<Vector3> allPoint = new List<Vector3>();

        Vector3 endPointA = new Vector3(pos.x, pos.y, pos.z + radius);
        Vector3 endPointB = new Vector3(pos.x + radius, pos.y, pos.z);

        Vector3 forwardPoint = new Vector3(pos.x, pos.y, pos.z + radius / 2);
        allPoint.Add(forwardPoint);
        Vector3 backPoint = new Vector3(pos.x, pos.y, pos.z - radius /2 );
        allPoint.Add(backPoint);
        Vector3 rightPoint = new Vector3(pos.x + radius /2, pos.y, pos.z);
        allPoint.Add(rightPoint);
        Vector3 leftPoint = new Vector3(pos.x - radius / 2, pos.y, pos.z);
        allPoint.Add(leftPoint);
        
        return allPoint;
    }
}
