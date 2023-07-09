using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public float smoothing;
    public float rotSmoothing;
    public Transform player;
    public GameObject rearViewMirror;
    private Vector3 offset;

    // Start is called before the first frame update
    void Start()
    {

       offset = transform.position - player.position;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.position = Vector3.Lerp(transform.position, player.position, smoothing);
        transform.rotation = Quaternion.Slerp(transform.rotation, player.rotation, rotSmoothing);
        transform.rotation = Quaternion.Euler(new Vector3(0, transform.rotation.eulerAngles.y, 0));
        Vector3 targetPosition = player.position + offset;
        transform.position = Vector3.Lerp(transform.position, targetPosition, smoothing);

        Quaternion targetRotation;
        if (Input.GetKey(KeyCode.S))
        {
            targetRotation =player.rotation* Quaternion.Euler(0, 180 ,0);
           // rearViewMirror.SetActive(true);
        }
        else
        {
            targetRotation = player.rotation;
           // rearViewMirror.SetActive(false);

        }    
        
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotSmoothing);
    }
}