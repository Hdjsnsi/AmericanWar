using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Cannon : MonoBehaviour
{
    private Transform[] allTransform;
    private Transform angle,launchPoint;
    private float shootAngle;
    Rigidbody rb;
    public GameObject ammo;
    public float fireForce;
    [Header("Trajetory status")]
    [Range(0.01f,0.5f)]public float increment;
    public int maxPoint;
    public float rayOverLap;
    public LineRenderer lineRenderer;
    public Transform hitMarker;
    void Awake()
    {
        rb = GetComponent<Rigidbody>();
        GetAllTransform();
    }
    void GetAllTransform()
    {
        allTransform = GetComponentsInChildren<Transform>();
        foreach(Transform transform in allTransform)
        {
            if(transform.gameObject.name == "Angle")
            {
                angle = transform;
            }
            if(transform.gameObject.name == "LaunchPoint")
            {
                launchPoint = transform;
            }
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        Fire();
    }
    void Move()
    {
        Movement();
        Rotation();
    }
    void Movement()
    {
        if(Input.GetAxisRaw("Vertical") == 0) return;
        rb.AddForce(transform.forward.normalized * 50 * Input.GetAxisRaw("Vertical"),ForceMode.Force);
    }
    void Rotation()
    {
        if (Input.GetAxisRaw("Horizontal") == 0) return;
        float rotationAmount = Input.GetAxisRaw("Horizontal") * 2; // rotationSpeed là tốc độ xoay
        Quaternion turnOffset = Quaternion.Euler(0, rotationAmount, 0);
        rb.MoveRotation(rb.rotation * turnOffset);
    }
    void Fire()
    {
        ShootPointRotation();
        ProjectilePropertites projectile = GetData();
        //PredictTrajetory(projectile);
        if(Input.GetKeyDown(KeyCode.Space))
        {
            GameObject gameObject = Instantiate(ammo, launchPoint.position, transform.rotation);
            gameObject.GetComponent<Rigidbody>().AddForce(launchPoint.up.normalized * fireForce, ForceMode.Impulse);
        }
    }
    void ShootPointRotation()
    {
        bool codeE = Input.GetKey(KeyCode.E);
        bool codeQ = Input.GetKey(KeyCode.Q);
        if(codeE)
        {
            shootAngle -= 1;
        }else if(codeQ)
        {
            shootAngle += 1;
        }
        shootAngle = Mathf.Clamp(shootAngle,30,70);
        Vector3 currentRotation = angle.localEulerAngles;
        currentRotation.x = shootAngle;
        angle.localEulerAngles = currentRotation;
        
    }
    public ProjectilePropertites GetData()
    {
        ProjectilePropertites propertites = new ProjectilePropertites();
        Rigidbody rbAmmo = ammo.gameObject.GetComponent<Rigidbody>();

        propertites.fireForce = this.fireForce;
        propertites.drag = rbAmmo.drag;
        propertites.mass = rbAmmo.mass;
        propertites.direction = launchPoint.up;
        propertites.initialPos = launchPoint.position;
        return propertites;
    }
    void PredictTrajetory(ProjectilePropertites projectile)
    {
        Vector3 velocity = projectile.direction * (projectile.fireForce / projectile.mass);
        Vector3 position = projectile.initialPos;
        Vector3 nextPosition ;
        float overlap;
        
        UpdateLineRender(maxPoint,(0,position));
        for(int i = 1 ; i < maxPoint; i++)
        {
            velocity = CalculateNewVelocity(velocity,projectile.drag, increment);
            nextPosition = position + velocity * increment;

            overlap = Vector3.Distance(position,nextPosition);
            if(Physics.Raycast(position, velocity.normalized, out RaycastHit hit, overlap))
            {
                UpdateLineRender(i,(i-1,hit.point));
                MoveHitMarker(hit);
                break;
            }
            hitMarker.gameObject.SetActive(false);
            position = nextPosition;
            UpdateLineRender(maxPoint,(i,position));
        }

        

    }
    void UpdateLineRender(int count, (int point, Vector3 pos) pointPos)
    {
        lineRenderer.positionCount = count;
        lineRenderer.SetPosition(pointPos.point,pointPos.pos);
    }
    Vector3 CalculateNewVelocity(Vector3 velocity, float drag , float increment)
    {
        //bị rơi xuống bởi trọng lực
        velocity += Physics.gravity * increment;
        //bị cản lại bởi lực cản
        velocity *= Mathf.Clamp01(1f - drag * increment);
        return velocity;
    }
    void MoveHitMarker(RaycastHit hit)
    {
        hitMarker.gameObject.SetActive(true);

        float offset = 0.025f;
        hitMarker.position = hit.point + hit.normal * offset;
        
    }
    
}

