using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class Locked_Item : MonoBehaviour, Interactable
{
    [SerializeField]
    private string item_required;
    private Item item;
    private bool inspectable = false;
    private Collider interact_collider;
    private bool used;
    [SerializeField]
    private string feedback_text;
    private void Awake()
    {
        item = GetComponent<Item>();
        interact_collider = GetComponent<Collider>();
    }

    private void Start()
    {
        item.Locked = true;
    }
    public void Disable_Interactable()
    {

        inspectable = false;
    }

    public void Enable_Interactable()
    {
       
        inspectable = true;
    }

    public void Interact()
    {
        if (!used&& inspectable && MoveCamera.Instance.canClick)
        {
            if (Inventory.Instance.ItemHolding != null)
            {
                if (Inventory.Instance.ItemHolding.name == item_required)
                {
                    Inventory.Instance.DestroyItem(item_required);
                    item.Locked = false;
                    used = true;
                    item.Interact();
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
    }

    public void Lock()
    {
        throw new System.NotImplementedException();
    }

    public void Unlock()
    {
        throw new System.NotImplementedException();
    }
}
