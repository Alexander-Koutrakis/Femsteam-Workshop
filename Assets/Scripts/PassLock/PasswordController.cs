using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class PasswordController : MonoBehaviour
{
    [SerializeField]
    private string[] password=new string[4];
    private string[] passwordEntered=new string[] { "", "", "", "", "" };
    private int currentIndex = 0;
    [SerializeField]
    private UnlockType unlockType;
    [SerializeField]
    private GameObject[] gameObjectAffected;
    private TMP_Text tmp_Text;

    private void Awake()
    {
        tmp_Text = GetComponentInChildren<TMP_Text>();
    }

    public void AddKey(string key)
    {
       
        passwordEntered[currentIndex] = key;
        currentIndex++;

        if (currentIndex >= password.Length)
        {
            currentIndex = 0;
        }
        ShowPassword();
    }

    public void Check()
    {
        bool unlock = true ;
        for(int i = 0; i < password.Length; i++)
        {
            if (password[i] != passwordEntered[i])
            {
                unlock = false;
                break;
            }
        }
        if (unlock)
        {
            Unlock();
        }
    }

    public void RemoveLast()
    {
       for(int i=0;i< passwordEntered.Length; i++)
        {
            passwordEntered[i] = "";
        }
        currentIndex = 0;
        ShowPassword();
    }
    private void Unlock()
    {
        if (unlockType == UnlockType.DisableObject)
        {
            for(int i = 0; i < gameObjectAffected.Length; i++)
            {
                gameObjectAffected[i].SetActive(false);
            }
           
        }
        else if(unlockType== UnlockType.PlayAnimation)
        {
            for (int i = 0; i < gameObjectAffected.Length; i++)
            {
                gameObjectAffected[i].GetComponent<Animation>().Play();
            }
                
        }else if (unlockType == UnlockType.UnlockInteractable)
        {
            for (int i = 0; i < gameObjectAffected.Length; i++)
            {
                gameObjectAffected[i].GetComponent<Interactable>().Unlock();
            }
        }
        else
        {
            for (int i = 0; i < gameObjectAffected.Length; i++)
            {
                gameObjectAffected[i].SetActive(true);
            }
        }
    }


    private enum UnlockType {
    DisableObject,PlayAnimation,EnableObject,UnlockInteractable
    }

    private void ShowPassword()
    {
        tmp_Text.text = passwordEntered[0] + passwordEntered[1] + passwordEntered[2] + passwordEntered[3];
    }

    private void ShortArray()
    {
        int lastIndex = 0;
        for (int i = 0; i < 4; i++)
        {
            if (passwordEntered[i] == "")
            {
                lastIndex = i;
                break;
            }
        }
    }
}
