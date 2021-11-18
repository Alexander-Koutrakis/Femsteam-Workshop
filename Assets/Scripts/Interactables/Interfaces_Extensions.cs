using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public interface Interactable
{
    void Interact();

    void Enable_Interactable();

    void Disable_Interactable();

    void Lock();

    void Unlock();
}

public interface CameraMover
{

    void Move_Camera();

    void ReturnToparent();
}

public interface ResultChecker {
    bool CheckResults();
}

public static class Extensions {
    public static bool IsPointerOverUIObject()
    {
        PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current);
        eventDataCurrentPosition.position = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventDataCurrentPosition, results);
        return results.Count > 0;
    }
}



