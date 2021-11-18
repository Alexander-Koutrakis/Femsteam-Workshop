using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class StartingPosition : MonoBehaviour,CameraMover
{
    private Transform startingTransform;// the starting position of each room
    private CameraMover parent;// used in case of incubated starting positions - not used any more
    private Focus[] focuses;// focus areas of the room
    private void Awake()
    {
        focuses = GetComponentsInChildren<Focus>();
        Focus.All_Focus.Clear();
        startingTransform = transform.Find("CameraPos");
    }

    public void Move_Camera()
    {       
        MoveCamera.Instance.MoveToPosition(startingTransform);
        gameObject.SetActive(true);
       
        for(int i = 0; i < focuses.Length; i++)
        {
            focuses[i].EnableColliders();
        }
    }
    public void ReturnToparent()
    {
        if (parent != null)
        {
            parent.Move_Camera();
        }
    }
}
