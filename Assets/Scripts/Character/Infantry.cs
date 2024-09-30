using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Infantry : Character
{
    [SerializeField] private GameObject weapon;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            Fire();
        }
        
    }
    void Fire()
    {
        GameObject ammo = Instantiate(ammoType,shootPoint.position,shootPoint.rotation);
        ammo.GetComponent<Rigidbody>().AddForce(shootPoint.forward * 100, ForceMode.Impulse);
    }
}
