using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCam : MonoBehaviour
{
    
    public float MouseSens = 100f;
    public Transform PlayerOrientation;

    private float xRotation;
    private float yRotation;
    
    
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        CameraMovement();
    }

    void CameraMovement()
    {
        //Mouse input
        float mouseX = Input.GetAxisRaw("Mouse X") * Time.fixedDeltaTime * MouseSens;
        float mouseY = Input.GetAxisRaw("Mouse Y") * Time.fixedDeltaTime * MouseSens;

        //Se usa el mouseX para rotar el eje Y
        yRotation += mouseX;
        //Se usa el mouse Y para rotar el eje X
        xRotation -= mouseY;
        //Limite de roatcion (arriba-abajo)
        xRotation = Mathf.Clamp(xRotation, -89f, 89f);
        
        //Rotacion de la camara
        transform.rotation = Quaternion.Euler(xRotation,yRotation,0);
        //Rotacion del jugador
        PlayerOrientation.rotation = Quaternion.Euler(0,yRotation,0);
    }
}
