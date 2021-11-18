using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class Broken_Frame : MonoBehaviour, Interactable
{
    [SerializeField]
    private string item_required;
    [SerializeField]
    private GameObject[] go_to_Disable;
    [SerializeField]
    private GameObject[] go_to_Enable;
    private bool inspectable = false;
    private Item item;
    private Collider interact_collider;
    private bool used;
    [SerializeField]
    private string feedback_text;
    private void Awake()
    {
        item = GetComponentInChildren<Item>(true);
        interact_collider = GetComponent<Collider>();
    }

    private void Start()
    {
        item.Locked = true;
    }
    public void Disable_Interactable()
    {
        interact_collider.enabled = false;
        inspectable = false;
    }

    public void Enable_Interactable()
    {
        interact_collider.enabled = true;
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
                    if (go_to_Enable.Length>0)
                    {
                        for (int i = 0; i < go_to_Enable.Length; i++)
                        {
                            go_to_Enable[i].SetActive(true);
                        }
                    }

                    if (go_to_Disable.Length>0)
                    {
                        for (int i = 0; i < go_to_Disable.Length; i++)
                        {
                            go_to_Disable[i].SetActive(false);
                        }
                    }

                    Inventory.Instance.DestroyItem(item_required);
                    StartCoroutine(WaitForUnlock(0.1f));                                                    
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

    private IEnumerator WaitForUnlock(float duration)
    {
        while(duration > 0){
            duration -= Time.deltaTime;
            yield return null;
        }
        item.Locked = false;
        Destroy(this);
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
