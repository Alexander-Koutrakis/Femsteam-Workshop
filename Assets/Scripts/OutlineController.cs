using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OutlineController 
{
    private Outline[] outlines;

    public OutlineController(GameObject gameObject)
    {
        outlines = gameObject.GetComponentsInChildren<Outline>(true);
        HideOutline();
    }
   
    public void ShowOutline()
    {
        for(int i = 0; i < outlines.Length; i++)
        {
            outlines[i].enabled = true;
        }
    }

    public void HideOutline()
    {
        for (int i = 0; i < outlines.Length; i++)
        {
            outlines[i].enabled = false;
        }
    }
}
