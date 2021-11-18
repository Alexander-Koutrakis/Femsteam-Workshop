using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicalButton : MonoBehaviour, Interactable
{
    private bool interactable;
    [SerializeField]
    private bool active;
    [SerializeField]
    private Material active_MAT;
    [SerializeField]
    private Material innactive_MAT;
    [SerializeField]
    private GameObject[] interactablesAffected;
    [SerializeField]
    private GameObject[] gameObjectsAffected;
    [SerializeField]
    private MeshRenderer meshRenderer;

    private void Awake()
    {
        meshRenderer = GetComponent<MeshRenderer>();
    }
    public void Enable_Interactable()
    {
        interactable = true;
    }

    public void Disable_Interactable()
    {
        interactable = false;
    }

    public void Interact()
    {
        if (interactable && MoveCamera.Instance.canClick)
        {
            if (!active)
            {
                for(int i = 0; i < interactablesAffected.Length; i++)
                {
                    interactablesAffected[i].GetComponent<Interactable>().Unlock();
                }

                for(int i = 0; i < gameObjectsAffected.Length; i++)
                {
                    gameObjectsAffected[i].SetActive(true);
                }
                meshRenderer.material = active_MAT;
            }
            else
            {
                for (int i = 0; i < interactablesAffected.Length; i++)
                {
                    interactablesAffected[i].GetComponent<Interactable>().Lock();
                }
                for (int i = 0; i < gameObjectsAffected.Length; i++)
                {
                    gameObjectsAffected[i].SetActive(false);
                }
                meshRenderer.material = innactive_MAT;
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
