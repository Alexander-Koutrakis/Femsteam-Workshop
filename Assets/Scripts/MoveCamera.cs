using System.Collections;
using UnityEngine;
using Cinemachine;
using UnityEngine.EventSystems;

// Move camera determines the movement/look of the camera
// In both POV and CinemachineFreeLook the camera is rotating Body/Facing
// by dragging the mouse on the screen

public class MoveCamera : MonoBehaviour
{
    public static MoveCamera Instance;
    private CinemachineVirtualCamera vcam;
    private CinemachinePOV POV;
    private CinemachineFreeLook vcam_ItemFocus;
    private CinemachineFreeLook focus_Cam;

    [SerializeField]
    private Camera playerCamera;// Camera used for navigation around the room
    [SerializeField]
    private Camera itemCamera;// Camera used for inspection , Renders only Item Layer
    [SerializeField]
    private LayerMask whatIsItem;

    public bool canClick=true;// Determines if the player can Interact with items/Focus areas or he presses the screen to Look Around
    private float draggingDistance = 0;
    private Vector2 currentPosition;
    private Vector2 lastPosition;
    private bool pressing;


    [SerializeField]
    private bool lockControl=false;// Locks the player movement

    private bool itemLooking = false;// determines if the player is inspecting an item

    private void OnEnable()
    {
        Instance = this;
    }
    private void Awake()
    {       
        vcam = GetComponentInChildren<CinemachineVirtualCamera>();
        POV = vcam.GetCinemachineComponent<CinemachinePOV>();
        vcam_ItemFocus= GetComponentInChildren<CinemachineFreeLook>();      
    }

    public void MoveToPosition(Transform nextPos)
    {       
        StopAllCoroutines();
        StartCoroutine(Move(nextPos));
    }


  

    private IEnumerator Move(Transform nextPos)
    {
        while(Vector3.Distance(transform.position,nextPos.position)>0.1f)
        {
            transform.position = Vector3.MoveTowards(transform.position, nextPos.position, 4 * Time.deltaTime);
            yield return null;
        }
    }

    private void Start()
    {
        DisableLook();
    }

    //When the player presses the mouse start calculating the distance of the dragging
    //To distinguish pressing mouse to interact from pressing mouse to rotate camera
    //If the player is dragging the mouse disable Interactions to avoid unwanted Focus movement/interaction
    private void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            EnableLook();
            lastPosition = Input.mousePosition;
            draggingDistance = 0;
            pressing = true;
        }

        if (pressing)
        {
            currentPosition = Input.mousePosition;
            draggingDistance += Vector2.Distance(currentPosition, lastPosition);
            if (draggingDistance > 80)
            {
                canClick = false;
            }
            lastPosition = currentPosition;
        }


        if (Input.GetMouseButtonUp(0))
        {
            DisableLook();
            pressing = false;
            if (draggingDistance > 80)
            {
                StartCoroutine(WaitForRelease(0.25f));// Delay interaction after releasing the Mouse Button to avoid unwanted Interactions
            }

            RayCast();
        }
    }

    // Set the Camera Speed when the player is looking Around for both POV and FreeLook
   private void EnableLook()
    {
        if (!lockControl)
        {
            POV.m_VerticalAxis.m_MaxSpeed = 100;
            POV.m_HorizontalAxis.m_MaxSpeed = 100;
            vcam_ItemFocus.m_XAxis.m_MaxSpeed = 100;
            vcam_ItemFocus.m_YAxis.m_MaxSpeed = 4;
            if (focus_Cam != null)
            {
                focus_Cam.m_XAxis.m_MaxSpeed = 100;
                focus_Cam.m_YAxis.m_MaxSpeed = 4;
            }
        }


        if (vcam.Priority <= 0) 
        {
            POV.m_VerticalAxis.m_MaxSpeed = 0;
            POV.m_HorizontalAxis.m_MaxSpeed = 0;
        }

        if (vcam_ItemFocus.Priority <= 0)
        {
            vcam_ItemFocus.m_XAxis.m_MaxSpeed = 0;
            vcam_ItemFocus.m_YAxis.m_MaxSpeed = 0;
        }

        if (focus_Cam != null)
        {
            if (focus_Cam.Priority <= 0)
            {
                focus_Cam.m_XAxis.m_MaxSpeed = 0;
                focus_Cam.m_YAxis.m_MaxSpeed = 0;
            }
        }
    }

    //Stop the Camera Looking when the player releases the mouse Button
    private void DisableLook()
    {
        POV.m_VerticalAxis.m_MaxSpeed = 0;
        POV.m_HorizontalAxis.m_MaxSpeed = 0;
        vcam_ItemFocus.m_XAxis.m_MaxSpeed = 0;
        vcam_ItemFocus.m_YAxis.m_MaxSpeed = 0;
        if (focus_Cam != null)
        {
            focus_Cam.m_XAxis.m_MaxSpeed = 0;
            focus_Cam.m_YAxis.m_MaxSpeed = 0;
        }

    }
  

    private IEnumerator WaitForRelease(float time)
    {
        canClick = false;     
        while (time > 0)
        {
            time -= Time.deltaTime;
            yield return null;
        }
        canClick = true;
    }


    // Focus on a Focus Area of the Room
    // Disable the POV camera and enable the FreeLook camera of the Focus Area
    // Pass on the FreeLoock camera restrictions and starting position
   public void Focus_Camera(Transform targetFocus,Vector2 orbitalRestrictions,bool wraped,Vector2 orbitStart, CinemachineFreeLook camera)
   {

        playerCamera.enabled = true;
        itemCamera.enabled = false;
       
        vcam_ItemFocus.Priority = 0;
        vcam.Priority = 0;

        if (focus_Cam != null)
        {
            focus_Cam.Priority = 0;
            focus_Cam.enabled = false;
        }

        focus_Cam = camera;
        focus_Cam.enabled = true;
        focus_Cam.Priority = 1;        
        focus_Cam.m_XAxis.Value = orbitStart.x;
        focus_Cam.m_YAxis.Value = orbitStart.y;
        focus_Cam.m_XAxis.m_MaxValue = orbitalRestrictions.y;
        focus_Cam.m_XAxis.m_MinValue = orbitalRestrictions.x;
        focus_Cam.m_XAxis.m_Wrap = wraped;
        focus_Cam.LookAt = targetFocus;
        focus_Cam.Follow = targetFocus;

        DisableLook();
    }


    // Inspect an Item
    // Disable the POV camera and enable the FreeLook camera of the Player
    // Pass on the FreeLoock camera target ,restrictions and starting position
    public void Focus_Item(Item item,CinemachineFreeLook.Orbit top_orbit, CinemachineFreeLook.Orbit mid_orbit, CinemachineFreeLook.Orbit bot_orbit)
    {
        Item.Show_Only_One_Item(item);
        playerCamera.enabled = false;
    
        vcam_ItemFocus.enabled = true;
        vcam_ItemFocus.Follow = item.transform;
        vcam_ItemFocus.LookAt = item.transform;
        vcam_ItemFocus.Priority = 1;
        vcam_ItemFocus.m_XAxis.Value = item.CameraStartPosition.x;
        vcam_ItemFocus.m_YAxis.Value = item.CameraStartPosition.y;

        vcam_ItemFocus.m_Orbits[0] = top_orbit;
        vcam_ItemFocus.m_Orbits[1] = mid_orbit;
        vcam_ItemFocus.m_Orbits[2] = bot_orbit;

        vcam.Priority = 0;
        focus_Cam.Priority = 0;
        itemCamera.enabled = true;
        item.gameObject.SetActive(true);
        itemLooking = true;
    }

    //Return to the Freelook Focus Camera
    public void ReturnFromItem()
    {
        itemLooking = false;
        playerCamera.enabled = true;
        itemCamera.enabled = false;
        vcam_ItemFocus.enabled = false;
        vcam.Priority = 0;
        vcam_ItemFocus.Priority = 0;
        focus_Cam.Priority = 1;       
    }

    //Return to the POV Camera
    public void UnFocus()
    {
        vcam.Priority = 1;
        vcam_ItemFocus.Priority = 0;
        focus_Cam.Priority = 0;
        focus_Cam.enabled = false;

    }

    // Raycast a line from the active camera to the world
    // if it hits an interactable , interact with it
    private void RayCast()
    {
        if (!EventSystem.current.IsPointerOverGameObject()&&!itemLooking)
        {           
            Ray ray = playerCamera.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(ray.origin, ray.direction, out hit, Mathf.Infinity))
            {
                if (hit.collider.GetComponent<Interactable>() != null)
                {

                    foreach (Interactable interactable in hit.collider.GetComponents<Interactable>())
                    {
                        interactable.Interact();
                    }
                }
                if (hit.collider.GetComponent<Focus>() != null&&hit.collider.enabled)
                {
                    hit.collider.GetComponent<Focus>().Move_Camera();
                }               
            }
        }

    }


    public void LockCamera(bool value)
    {
        lockControl = value;
    }

}
