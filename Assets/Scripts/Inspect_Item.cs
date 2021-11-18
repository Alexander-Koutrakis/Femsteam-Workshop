using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inspect_Item : MonoBehaviour
{
    public static Inspect_Item Instance;
    public static bool inspecting;
    private Item holding_Item;

    private void Awake()
    {
        Instance = this;
    }
    public void Inspect(Item item)
    {
        inspecting = true;
        item.gameObject.SetActive(true);
        LeanTween.move(item.gameObject, transform.position, 1);
        holding_Item = item;
    }

    public void Stop_Inspecting()
    {

    }
}
