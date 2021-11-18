using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

// Focus points are used as points of interest that the player can Move/Zoom into
// Focus points must contain a CinemachineFreeLook camera as child gameobject that focuses on the Focus Gameobject
// Items and other Interactables can only be interacted when the player has Moved?Zoomed intο each specific Focus point

[RequireComponent(typeof(BoxCollider))]
public class Focus : MonoBehaviour,CameraMover
{
    public static List<Focus> All_Focus = new List<Focus>();  
    private Collider m_collider;
    private CameraMover parent;// Return camera to parentGO when pressing Back Button
    private Interactable[] interactables = new Interactable[0];
    [SerializeField]
    private Canvas[] interactableCanvas = new Canvas[0];// Used for local UI canvas - Now its only used for Room Selection
    [SerializeField]
    private Vector2 orbitalRestrictions;//Restrict the Camera Movement to focus on specific points in the area and/or avoid cliping on obstacles
    [SerializeField]
    private bool wraped;// Used for Cinemachine Camera Restriction
    [SerializeField]
    private Vector2 orbit_Start;// Used for Cinemachine Camera Restriction
    private CinemachineFreeLook focus_Camera;
    [SerializeField]
    private string location_Name;// Location name

    private void Awake()
    {
        All_Focus.Add(this);
        m_collider = GetComponent<Collider>();
        parent = GetComponentInParent<CameraMover>();
        interactables = GetComponentsInChildren<Interactable>(true);
        focus_Camera = GetComponentInChildren<CinemachineFreeLook>(true);
    }

    private void OnDestroy()
    {
        All_Focus.Remove(this);
    }

    public void EnableColliders()
    {
        m_collider.enabled = true;
    }

    public void DisableColliders()
    {
        m_collider.enabled = false;
    }

    public void Move_Camera()
    {
        if (MoveCamera.Instance.canClick && m_collider.enabled) {
            DisableAllInteractables();
            EnableFocus();
            DisableColliders();
            Player_UI.Instance.Focus_Area(this);
            Player_UI.Instance.Show_Location(location_Name);
            MoveCamera.Instance.Focus_Camera(transform, orbitalRestrictions,wraped,orbit_Start,focus_Camera);
            StartCoroutine(WaitforCameraMove(1));
        }
    }

    public void ReturnToparent()
    {
        if (parent != null)
        {
            parent.Move_Camera();
        }
        EnableColliders();
        DisableAllInteractables();
        MoveCamera.Instance.UnFocus();
        Player_UI.Instance.Show_Location("");
    }

   public static void EnableFocus()
   {
        for (int i = 0; i < All_Focus.Count; i++)
        {
            All_Focus[i].EnableColliders();
        }
   }

    public static void DisableFocus()
    {
        for(int i = 0; i < All_Focus.Count; i++)
        {
            All_Focus[i].DisableColliders();
        }
    }

    private void Enable_Interactables()
    {
        for(int i = 0; i < interactables.Length; i++)
        {
            interactables[i].Enable_Interactable();
        }

        if (interactableCanvas.Length <= 0)
        {
            return;
        }

        for (int i = 0; i < interactableCanvas.Length; i++)
        {
            interactableCanvas[i].gameObject.SetActive(true);
        }
    }

    private void Disable_Interactables()
    {
        for (int i = 0; i < interactables.Length; i++)
        {
            interactables[i].Disable_Interactable();
        }

        if (interactableCanvas.Length <= 0)
        {
            return;
        }
        for (int i = 0; i < interactableCanvas.Length; i++)
        {
            interactableCanvas[i].gameObject.SetActive(false);
        }
    }

    private static void DisableAllInteractables()
    {
        for (int i = 0; i < All_Focus.Count; i++)
        {
            All_Focus[i].Disable_Interactables();
        }
    }
  


    private IEnumerator WaitforCameraMove(float duration)
    {
        while (duration > 0)
        {
            duration -= Time.deltaTime;
            yield return null;
        }
        Enable_Interactables();
    }
}
