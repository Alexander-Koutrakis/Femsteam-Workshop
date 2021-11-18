using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Inventory : MonoBehaviour
{
    [SerializeField]
    private GameObject[] itemIcons = new GameObject[0];
    [SerializeField]
    private GameObject[] iconSelections = new GameObject[0];
    public static Inventory Instance;
    private Dictionary<string, GameObject> inventoryItems = new Dictionary<string, GameObject>();
    public Item ItemHolding;
    [SerializeField]
    private GameObject inventory;
    private bool opened=false;

    void Awake()
    {
        Instance = this;
    }
    public void TakeItem(Item item)
    {
        item.transform.position = transform.position;
        item.Taken = true;
        inventoryItems.Add(item.gameObject.name, item.gameObject);
        SetItem(item, FreeSlot());
    }

    public void DestroyItem(string name)
    {
        Item.Destroy_Item(name);
        inventoryItems[name].gameObject.SetActive(false);
        inventoryItems.Remove(name);
        for(int i = 0; i < iconSelections.Length; i++)
        {
            if (iconSelections[i].transform.parent.name == name)
            {
                iconSelections[i].transform.parent.gameObject.SetActive(false);
                iconSelections[i].gameObject.SetActive(false);
            }
        }
        ItemHolding = null;
    }

    private GameObject FreeSlot()
    {
        for(int i = 0; i < itemIcons.Length; i++)
        {
            if (itemIcons[i].gameObject.activeSelf == false)
            {
                return itemIcons[i];
            }
        }

        return null;
    }

    private void SetItem(Item item,GameObject item_GO)
    {       
       
        Image image = item_GO.GetComponent<Image>();
        image.sprite = item.InventorySprite;
        item_GO.name = item.gameObject.name;
        Button button = item_GO.GetComponent<Button>();
        button.onClick.RemoveAllListeners();
        button.onClick.AddListener(delegate { Select_Item(item);});
        item_GO.SetActive(true);
    }

    private void Select_Item(Item item)
    {

        if (Inventory.Instance.ItemHolding != item)
        {
            Inventory.Instance.ItemHolding = item;           
        }
        else
        {
            item.Interact();
        }

    }

    public void CloseSelections()
    {
        for(int i = 0; i < iconSelections.Length; i++)
        {
            iconSelections[i].gameObject.SetActive(false);
        }
    }

   public void Open_Close()
    {
        if (!opened) {
            LeanTween.moveLocalY(inventory,0, 0.5f);
            opened = true;
        }
        else
        {
            LeanTween.moveLocalY(inventory,400, 0.5f);
            opened = false;
        }

        
    }
}
