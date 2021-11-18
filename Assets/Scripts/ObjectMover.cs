using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectMover : MonoBehaviour, Interactable
{
    private bool interactable = false;
    [System.Serializable]
    private enum Movement
    {
        Move,Rotate,MoveRotate
    }
    [SerializeField]
    private Movement TypeOfMovement;
    private bool objectMoved;
    [SerializeField]
    private Vector3 StandardLocation;
    [SerializeField]
    private Vector3 StandardRotation;
    [SerializeField]
    private Vector3 TargetLocation;
    [SerializeField]
    private Vector3 TargetRotation;
    [SerializeField]
    private bool interactOnce = true;

    private void MoveObject()
    {
        StopAllCoroutines();
        if (TypeOfMovement == Movement.Move|| TypeOfMovement == Movement.MoveRotate)
        {
            LeanTween.moveLocal(gameObject, TargetLocation, 1);
        }

        if (TypeOfMovement == Movement.Rotate || TypeOfMovement == Movement.MoveRotate)
        {
            LeanTween.rotateLocal(gameObject, TargetRotation, 1);
        }
        objectMoved = true;
    }

    private void ReturnObject()
    {

        StopAllCoroutines();
        if (TypeOfMovement == Movement.Move || TypeOfMovement == Movement.MoveRotate)
        {
            LeanTween.moveLocal(gameObject, StandardLocation, 1);
        }

        if (TypeOfMovement == Movement.Rotate || TypeOfMovement == Movement.MoveRotate)
        {
            LeanTween.rotateLocal(gameObject, StandardRotation, 1);
        }
        objectMoved = false;
    }
    public void Interact()
    {
        if (interactable && MoveCamera.Instance.canClick)
        {
            if (objectMoved&&!interactOnce)
            {
                ReturnObject();
            }
            else
            {
                MoveObject();
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
        throw new System.NotImplementedException();
    }

    public void Unlock()
    {
        throw new System.NotImplementedException();
    }
}
