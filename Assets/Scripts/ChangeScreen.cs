using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeScreen : MonoBehaviour, Interactable
{
    [SerializeField]
    private GameObject currentScreen;
    [SerializeField]
    private GameObject nextScreen;


    public void Enable_Interactable()
    {
        throw new System.NotImplementedException();
    }

    public void Disable_Interactable()
    {
        throw new System.NotImplementedException();
    }

    public void Lock()
    {
        throw new System.NotImplementedException();
    }

    public void Unlock()
    {
        throw new System.NotImplementedException();
    }

    public void Interact()
    {
        Player_UI.Instance.Fade(0.5f);
        StartCoroutine(WaitForFade());
    }

    private IEnumerator WaitForFade()
    {
        while(!Player_UI.Instance.Faded){
            yield return null;
        }
        currentScreen.SetActive(false);
        nextScreen.SetActive(true);
    }
}
