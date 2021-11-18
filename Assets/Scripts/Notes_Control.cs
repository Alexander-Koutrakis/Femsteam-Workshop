using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Notes_Control : MonoBehaviour
{
    private ResultChecker[] resultCheckers;
    [SerializeField]
    private GameObject CorrectScreen;

    private void Awake()
    {
        resultCheckers = GetComponentsInChildren<ResultChecker>();

    }
    public void OnAnswerChange()
    {
       for(int i = 0; i < resultCheckers.Length; i++)
        {          
            if (!resultCheckers[i].CheckResults())
            {
                return;
            }
        }
        CorrectScreen.SetActive(true);
    }
}
