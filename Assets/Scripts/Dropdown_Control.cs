using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
public class Dropdown_Control : MonoBehaviour,ResultChecker
{
    private TMP_Dropdown dropdown;
    [SerializeField]
    private string correct_answer;

    private void Awake()
    {
        dropdown = GetComponent<TMP_Dropdown>();
    }
   

    public bool CheckResults()
    {
        if (dropdown.options[dropdown.value].text == correct_answer)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
