using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    [SerializeField]
    private GameObject[] panels;
    public void ResetMenu()
    {
        for(int i = 1; i < panels.Length; i++)
        {
            panels[i].SetActive(false);
        }

        panels[0].SetActive(true);
    }

    public void ExitApp()
    {
        Application.Quit();
    }
    private void Start()
    {
       // Player_UI.Instance.gameObject.SetActive(false);
    }
}
