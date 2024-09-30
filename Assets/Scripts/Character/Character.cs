using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UIElements;

public class Character : MonoBehaviour
{
    protected Rigidbody rb;
    [Header("Basic status")]
    public Transform spawnFrom;

    [Header("Character status")]
    public float speed;
    public float fireRange;
    public GameObject ammoType;
    protected GameObject[] allEnemy;
    protected GameObject nearestEnemy;
    
    private Vector3 targetDistance;
    protected Transform[] allObject;
    protected Transform shootPoint;
    protected virtual void Awake()
    {
        GetAllObject();
        rb = GetComponent<Rigidbody>();
        rb.drag = 5;
        InvokeRepeating("Detecting",0,2f);
    }
    void GetAllObject()
    {
        allObject = GetComponentsInChildren<Transform>();
        shootPoint = allObject.Where(a => a.gameObject.name == "Shoot Point").FirstOrDefault();
    }
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    void Detecting()
    {
        allEnemy = GameObject.FindGameObjectsWithTag("Enemy");
        nearestEnemy = allEnemy.OrderBy(a => (transform.position - a.transform.position).sqrMagnitude).FirstOrDefault();
        if(nearestEnemy == null) return;
        Vector3 enemyDistance = nearestEnemy.transform.position - transform.position;
        targetDistance = enemyDistance;
    }
    protected virtual void Move()
    {
        if(targetDistance.sqrMagnitude < fireRange * fireRange)return; 
        if(Physics.Raycast(transform.position,Vector3.down * 2,out RaycastHit hit))
        {
            LimitVelocity();
            rb.AddForce(transform.forward * speed * 10,ForceMode.Force);
        }
        
    }
    protected virtual void Movement()
    {   
        float directed = Vector3.Angle(transform.forward,targetDistance);
        Rotate();

        if(directed > 10) return;
        Move();
    }
    protected virtual void Rotate()
    {
        if(targetDistance == Vector3.zero) return;
        Quaternion targetRotate = Quaternion.LookRotation(targetDistance);
        transform.rotation = Quaternion.Lerp(transform.rotation,targetRotate,Time.deltaTime);
    }
    void LimitVelocity()
    {
        Vector3 currentVelocity = new Vector3(rb.velocity.x, 0, rb.velocity.z);
        float currentSpeed = currentVelocity.sqrMagnitude;
        if(currentSpeed >= speed * speed)
        {
            Vector3 limitedSpeed = currentVelocity.normalized * speed;
            rb.velocity = new Vector3(limitedSpeed.x,rb.velocity.y,limitedSpeed.z);
        }
    }
}
