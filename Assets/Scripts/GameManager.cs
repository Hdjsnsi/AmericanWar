using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class GameManager : MonoBehaviour
{
    public  GameObject prefabShelter;
    private GameObject ghostObject;
    private Camera mainCamera;
    public Vector3 hitOffsetPos;
    // Start is called before the first frame update
    void Start()
    {
        mainCamera = GameObject.Find("Main Camera").GetComponent<Camera>();
        SetupGhostObject();
    }

    // Update is called once per frame
    void Update()
    {
        CursorOnTheGround();
    }
    void CursorOnTheGround()
    {
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if(Physics.Raycast(ray,out hit))
        {
            GameObject hitObject = hit.collider.gameObject;
            if(hitObject.CompareTag("PlaceAble"))
            {
                Vector3 hitPos = hit.point;
                ghostObject.transform.position = hitPos + hitOffsetPos;
                ghostObject.SetActive(true);
                if(Input.GetMouseButtonDown(0))
                {
                    PlaceObject(ghostObject.transform);
                }
            }
        }else
        {
            ghostObject.SetActive(false);
        }
    }
    void PlaceObject(Transform target)
    {
        Instantiate(prefabShelter,target.position,prefabShelter.transform.rotation);
    }
    void SetupGhostObject()
    {
        ghostObject = Instantiate(prefabShelter);
        Shelter turnOff = ghostObject.GetComponent<Shelter>();
        turnOff.enabled = false;
        Collider collider = ghostObject.GetComponent<Collider>();
        collider.enabled = false;
        Material ghostMat = ghostObject.GetComponent<Renderer>().material;
        Color ghostColor = ghostMat.color;
        ghostColor.a = 0.3f;
        ghostMat.color = ghostColor;
        
        
    }
}