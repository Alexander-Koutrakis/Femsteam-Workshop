using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PasswordButton :MonoBehaviour , Interactable
{
    private bool interactable;
    private PasswordController passwordController;
    [SerializeField]
    private ButtonContent buttonContent;
    private void Awake()
    {
        passwordController = GetComponentInParent<PasswordController>();
    }

    public void Disable_Interactable()
    {
        interactable = false;
    }

    public void Enable_Interactable()
    {
        interactable = true;
    }

    public void Interact()
    {
        if (interactable)
        {
            Debug.Log("button pressed");
            if (buttonContent == ButtonContent.OK)
            {
                passwordController.Check();
            }else if (buttonContent == ButtonContent.X)
            {
                passwordController.RemoveLast();
            }
            else
            {
                passwordController.AddKey(ContentToString(buttonContent));
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

    private string ContentToString(ButtonContent content)
    {
        switch (content)
        {
            case ButtonContent.one:
                return "1";
            case ButtonContent.two:
                return "2";
            case ButtonContent.three:
                return "3";
            case ButtonContent.four:
                return "4";
            case ButtonContent.five:
                return "5";
            case ButtonContent.six:
                return "6";
            case ButtonContent.seven:
                return "7";
            case ButtonContent.eight:
                return "8";
            case ButtonContent.nine:
                return "9";
            default:
                return "0";
        }
    }

    public enum ButtonContent {
        one,two,three,four,five,six,seven,eight,nine,zero,OK,X}
}
