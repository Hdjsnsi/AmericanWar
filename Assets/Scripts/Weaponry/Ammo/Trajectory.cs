using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trajectory : MonoBehaviour
{
    public Transform transform1;
    public float increment = .025f;
    LayerMask placeAble;
    Cannon cannon;
    // Start is called before the first frame update
    void Start()
    {
       cannon = GetComponent<Cannon>(); 
       placeAble = LayerMask.GetMask("What is Ground");
    }

    // Update is called once per frame
    void Update()
    {
        Tracking(cannon.GetData());
    }
    void Tracking(ProjectilePropertites projectile)
    {
        Vector3 velocity = projectile.direction * (projectile.fireForce / projectile.mass);
        Vector3 position = projectile.initialPos;
        Vector3 nextPos;
        float overlap;
        for(int i = 1 ;i < Mathf.Infinity; i++)
        {
            if(i >= 500) break;
            velocity = VelocityWithPhysics(velocity,projectile.drag);
            nextPos = position + velocity * increment;
            overlap = Vector3.Distance(position,nextPos);
            if(Physics.Raycast(position,velocity.normalized,out RaycastHit hit,overlap,placeAble))
            {
                HitSomeThing(hit);
                break;
            }
            position = nextPos;
        }
    }
    Vector3 VelocityWithPhysics(Vector3 velocity,float drag)
    {
        velocity += Physics.gravity * increment;
        velocity *= Mathf.Clamp01(1f - drag * increment);
        return velocity;
    }
    void HitSomeThing(RaycastHit hit)
    {
        transform1.position = hit.point + hit.normal * 0.025f;
        
    }
    
}
