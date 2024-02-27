using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteract : MonoBehaviour
{
    // Start is called before the first frame update
    private Camera cam;
    [SerializeField]
    private float distance = 3f;
    [SerializeField]
    private LayerMask mask;
    private PlayerUI PlayerUI;

    void Start()
    {
        cam = GetComponent<PlayerLook>().cam;
        PlayerUI = GetComponent<PlayerUI>();
    }

    // Update is called once per frame
    void Update()
    {
        PlayerUI.UpdateText(string.Empty);
        Ray ray = new Ray(cam.transform.position, cam.transform.forward);
        RaycastHit hitInfo;
        Debug.DrawRay(ray.origin, ray.direction * distance);
        if(Physics.Raycast(ray, out hitInfo, distance, mask))
        {
            if(hitInfo.collider.GetComponent<Interacatble>() != null) 
            {
                PlayerUI.UpdateText(hitInfo.collider.GetComponent<Interacatble>().promptMessage);
            }
        }

    }
}
