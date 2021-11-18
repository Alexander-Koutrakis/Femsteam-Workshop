using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ShowImage : MonoBehaviour,Interactable
{
    private Image shownImage;
    private Image fadeImage;
    private Canvas canvas;
    private bool fading;
    private bool interactable;
    private bool locked = false;
    private OutlineController outlineController;
    private IEnumerator FadeToBlack(float duration)
    {
        fadeImage.gameObject.SetActive(true);
        float alpha = fadeImage.color.a;
        while (duration > 0)
        {
            alpha = Mathf.MoveTowards(alpha, 1, Time.deltaTime / duration);
            fadeImage.color = new Color(fadeImage.color.r, fadeImage.color.g, fadeImage.color.b, alpha);
            duration -= Time.deltaTime;
            yield return null;
        }
    }

    private IEnumerator FadeFromBlack(float duration)
    {
        float alpha = fadeImage.color.a;
        while (duration > 0)
        {
            alpha = Mathf.MoveTowards(alpha, 0, Time.deltaTime / duration);
            fadeImage.color = new Color(fadeImage.color.r, fadeImage.color.g, fadeImage.color.b, alpha); ;
            duration -= Time.deltaTime;
            yield return null;
        }
        fadeImage.gameObject.SetActive(false);
    }

    private IEnumerator FadeInAndOutShow()
    {
        canvas.gameObject.SetActive(true);
        StartCoroutine(FadeToBlack(0.5f));
        yield return new WaitForSeconds(0.5f);
        shownImage.gameObject.SetActive(true);
        StartCoroutine(FadeFromBlack(0.5f));
    }

    private IEnumerator FadeInAndOutHide()
    {     
        StartCoroutine(FadeToBlack(0.5f));
        yield return new WaitForSeconds(0.5f);
        shownImage.gameObject.SetActive(false);
        StartCoroutine(FadeFromBlack(0.5f));
        canvas.gameObject.SetActive(false);
    }

    public void Interact()
    {
        if (interactable)
        {
            StartCoroutine(FadeInAndOutShow());
        }
    }

    public void GoBack()
    {
        StartCoroutine(FadeInAndOutHide());
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
        locked = true;
    }

    public void Unlock()
    {
        locked = false;
    }

    private void Awake()
    {
        canvas = GetComponentInChildren<Canvas>(true);
        shownImage = GetComponentsInChildren<Image>(true)[0];
        fadeImage = GetComponentsInChildren<Image>(true)[2];
        outlineController = new OutlineController(gameObject);
    }

    public void OnMouseEnter()
    {
        if (interactable && MoveCamera.Instance.canClick)
        {
            outlineController.ShowOutline();
        }
    }

    public void OnMouseExit()
    {
        outlineController.HideOutline();

    }
}
