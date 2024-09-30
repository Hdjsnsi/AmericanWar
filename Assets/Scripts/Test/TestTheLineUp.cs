using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestTheLineUp : MonoBehaviour
{
    public float radius;
    public enum LineUpDirection
    {
        Forward = 0,
        Back = 1,
        Right = 2,
        Left = 3
    }
    public LineUpDirection direction;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnDrawGizmosSelected()
    {
        int index = Direction();
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position,radius);
        Gizmos.color = Color.green;
        List<Vector3> pos = AllPointWeNeed(radius, transform.position);
        Gizmos.DrawWireSphere(pos[index],1);
    }
    int Direction()
    {
        int index = 0;
        switch(direction)
        {
            case LineUpDirection.Forward:
                index = 0;
                break;
            case LineUpDirection.Back:
                index = 1;
                break;
            case LineUpDirection.Right:
                index = 2;
                break;
            case LineUpDirection.Left:
                index = 3;
                break;
        }
        return index;
    }
    
    List<Vector3> AllPointWeNeed(float radius,Vector3 pos)
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
