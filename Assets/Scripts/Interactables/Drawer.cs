using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class Drawer : MonoBehaviour,Interactable
{
    private bool drawerOpen=false;
    [SerializeField]
    private Vector3 closedPos;
    [SerializeField]
    private Vector3 openPos;
    private bool interactable = false;
    [SerializeField]
    private string item_required;
    public bool locked = false;
    [SerializeField]
    private string feedback_text;
    private OutlineController outlineController;

    private void Awake()
    {
        outlineController = new OutlineController(gameObject);
    }
    private void OpenDrawer()
    {
        StopAllCoroutines();
        LeanTween.moveLocal(gameObject, openPos, 1);
        drawerOpen = true;
    }

    private void CloseDrawer()
    {
        StopAllCoroutines();
        LeanTween.moveLocal(gameObject, closedPos, 1);
        drawerOpen = false;
    }

    public void Interact()
    {
        if (interactable && MoveCamera.Instance.canClick)
        {
            if (locked&&!drawerOpen) 
            {
                if (Inventory.Instance.ItemHolding != null)
                {
                    if (Inventory.Instance.ItemHolding.gameObject.name == item_required)
                    {
                        Inventory.Instance.DestroyItem(item_required);
                        locked = false;
                        if (drawerOpen)
                        {
                            CloseDrawer();
                        }
                        else
                        {
                            OpenDrawer();
                        }
                    }
                    else
                    {                      
                            Player_UI.Instance.ShowText(feedback_text);
                    }
                }
                else
                {                  
                    Player_UI.Instance.ShowText(feedback_text);
                }
            }
            else
            {
                if (drawerOpen)
                {
                    CloseDrawer();
                }
                else
                {
                    OpenDrawer();
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
