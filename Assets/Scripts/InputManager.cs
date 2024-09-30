using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    private Camera mainCamera;
    void Start()
    {
        mainCamera = GameObject.Find("Main Camera").GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void CursorOnTheGround()
    {
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        
        if(Physics.Raycast(ray,out RaycastHit hit))
        {
            GameObject hitObject = hit.collider.gameObject;
            if(hitObject.CompareTag("PlaceAble")) return;
            Vector3 hisPos = hit.point + hit.normal * .025f;

        }
    }
}
