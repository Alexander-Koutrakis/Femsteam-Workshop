using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

// Item is responsible for allowing the player to take and/or inspect an item in the room
// Item inspection happens by moving MoveCamera.Item_Camera next to the Item.
// MoveCamera.Item_Camera renders only the Item layer to give the Inspection View
// Disable all other Items to avoid rendering to Items that happen to be close by in the room
// Items Taken by the player are moved far away from the room
public class Item : MonoBehaviour, Interactable
{
    private bool inspectable = false;
    [SerializeField]
    private bool canTake = true;// Some items ,such as computer screens, can be inspected but not allowed to be taken by the player 
    public bool Taken = false;// determines if the item is in the invetory
    private static List<Item> items = new List<Item>();// disable all items on this list BUT the item you want to inspect
    public bool Locked = false;// item can be locked and not allowed to be inspected/taken
    public Vector2 CameraStartPosition = new Vector2(180, 0.5f);//Determines X and Y Axis values of the Freelook Camera
    public Sprite InventorySprite;// The Sprite Shown itn the inventory
    

    // Set the Orbit Values of the FreeLook Camera
    [SerializeField]
    private CinemachineFreeLook.Orbit top_orbit=new CinemachineFreeLook.Orbit(0.5f,0.1f);
    [SerializeField]
    private CinemachineFreeLook.Orbit mid_orbit = new CinemachineFreeLook.Orbit(0, 0.75f);
    [SerializeField]
    private CinemachineFreeLook.Orbit bot_orbit=new CinemachineFreeLook.Orbit(-0.5f,0.1f);
    private OutlineController outlineController;

    private void Awake()
    {
        items.Add(this);
        outlineController = new OutlineController(gameObject);
    }

    private void OnDestroy()
    {
        items.Remove(this);
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
        if (inspectable && MoveCamera.Instance.canClick && !Locked&&!Taken)
        {
            MoveCamera.Instance.Focus_Item(this,top_orbit,mid_orbit,bot_orbit);
            Player_UI.Instance.Focus_Item(canTake, Taken, this);
        }

        if (Taken && MoveCamera.Instance.canClick)
        {          
            MoveCamera.Instance.Focus_Item(this, top_orbit, mid_orbit, bot_orbit);
            Player_UI.Instance.Focus_Item(canTake, Taken, this);
        }
    }


    public static void Show_Only_One_Item(Item _item)
    {
        for(int i = 0; i < items.Count; i++)
        {
            if (items[i] != _item)
            {
                items[i].gameObject.SetActive(false);
            }
        }
    }

    public static void Enable_All_Items()
    {
        for (int i = 0; i < items.Count; i++)
        {
           
            items[i].gameObject.SetActive(true);
            
        }
    }

    public static void Destroy_Item(string name)
    {
        Item itemToDestroy=null;
        for(int i = 0; i < items.Count; i++)
        {
            if (items[i].name == name)
            {
                itemToDestroy = items[i];
                break;
            }
        }
        if (itemToDestroy!= null)
        {
            items.Remove(itemToDestroy);
            Destroy(itemToDestroy.gameObject);
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

    public void OnMouseEnter()
    {
        if (inspectable && MoveCamera.Instance.canClick)
        {
            outlineController.ShowOutline();
        }
    }

    public void OnMouseExit()
    {

            outlineController.HideOutline();
        
    }
}
