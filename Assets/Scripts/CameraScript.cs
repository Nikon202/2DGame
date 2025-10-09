using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    public float speed = 20;
    [SerializeField] private Transform person;
    private Transform cameraTransform;

    private void Awake()
    {
        cameraTransform = GetComponent<Transform>();    
    }
    private void Update()
    {   
        
        FolowCamera();
    }
    private void FolowCamera()
    {
        cameraTransform.position = Vector2.Lerp(cameraTransform.position, person.position, Time.deltaTime*speed);
        cameraTransform.position = new Vector3(cameraTransform.position.x, cameraTransform.position.y, -10);
    }
}
