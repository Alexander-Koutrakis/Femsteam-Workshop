using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class Door : MonoBehaviour, Interactable
{
    [SerializeField]
    private Vector3 ClosedRot;
    [SerializeField]
    private Vector3 OpenRot;
    private bool doorOpen = false;
    private bool interactable = false;
    [SerializeField]
    private string item_required;
    public bool locked=false;
    [SerializeField]
    private string feedback_text;
    private OutlineController outlineController;

    private void Awake()
    {
        outlineController = new OutlineController(gameObject);
    }
    private void OpenDoor()
    {
        StopAllCoroutines();
        LeanTween.rotateLocal(gameObject, OpenRot, 1);
        doorOpen = true;
    }

    private void CloseDoor()
    {
        StopAllCoroutines();
        LeanTween.rotateLocal(gameObject, ClosedRot, 1);
        doorOpen = false;
    }

    public void Interact()
    {
        if (interactable && MoveCamera.Instance.canClick)
        {
            if (locked&&!doorOpen)
            {
                if (Inventory.Instance.ItemHolding != null)
                {
                    if (Inventory.Instance.ItemHolding.gameObject.name == item_required)
                    {
                        Inventory.Instance.DestroyItem(item_required);
                        locked = false;
                        if (doorOpen)
                        {
                            CloseDoor();
                        }
                        else
                        {
                            OpenDoor();
                        }
                    }
                    else
                    {
                        
                            Player_UI.Instance.ShowText(feedback_text);
                    }
                }
                else
                {
                    if(!doorOpen)
                    Player_UI.Instance.ShowText(feedback_text);
                }
            }
            else
            {
                if (doorOpen)
                {
                    CloseDoor();
                }
                else
                {
                    OpenDoor();
                }
            }


        }

       
    }

  

    public void Enable_Interactable()
    {
        interactable = true;
    }

    public void Disable_Interactable()
    {
        interactable = false;
    }

    public void Lock()
    {
        locked = true;
    }

    public void Unlock()
    {
        locked = false;
    }

    public void OnMouseEnter()
    {
        if (interactable && MoveCamera.Instance.canClick)
        {
            outlineController.ShowOutline();
        }
    }

    public void OnMouseExit()
    {
        outlineController.HideOutline();

    }
}
