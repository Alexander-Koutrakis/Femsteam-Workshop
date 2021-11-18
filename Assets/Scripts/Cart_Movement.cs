using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
public class Cart_Movement : MonoBehaviour
{
    private CinemachineDollyCart cart;
    private CinemachineComposer composer;
    public static Cart_Movement Instance;
    public bool Enabled = true;
    private float directionX;
    private float directionY;
    private float speedY = 5f;
    private float speedX = 5f;
    private void Awake()
    {
        Instance = this;
        cart = GetComponent<CinemachineDollyCart>();
        CinemachineVirtualCamera camera = GetComponentInChildren<CinemachineVirtualCamera>();
        composer = camera.GetCinemachineComponent<CinemachineComposer>();
    }

    private void Update()
    {
        if (Enabled)
        {
            if (Input.GetMouseButton(0))
            {
                
                    directionX = -1 * Input.GetAxisRaw("Mouse X");                               
                    cart.m_Speed = directionX * speedX;
            
                    directionY = -1 * Input.GetAxisRaw("Mouse Y");
                    composer.m_ScreenY += directionY * speedY * Time.deltaTime;
                    composer.m_ScreenY = Mathf.Clamp(composer.m_ScreenY, -0.49f, 1.5f);
       
            }
        }
        else
        {
            cart.m_Speed = 0;
        }

        if (Input.GetMouseButtonUp(0))
        {
            cart.m_Speed = 0;
        }
    }

    public void ResetCamera(float Y)
    {
        composer.m_ScreenY = Y;
    }

}
