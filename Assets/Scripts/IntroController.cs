using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class IntroController : MonoBehaviour
{
    [System.Serializable]
    private struct IntroPage {
        public Sprite IntroSprite;
        public string IntroText;
    }
    [SerializeField]
    private Image introImage;
    [SerializeField]
    private TMP_Text introText;
    [SerializeField]
    private IntroPage[] introPages = new IntroPage[0];
    private int index=0;
    [SerializeField]
    private GameObject roomSelect;
    public void NextPage()
    {

        if (index < introPages.Length)
        {
            introImage.sprite = introPages[index].IntroSprite;
            introText.text = introPages[index].IntroText;
            index++;
        }
        else
        {
            Player_UI.Instance.gameObject.SetActive(true);
            StartGame();
        }                      
    }

    public void StartGame()
    {
        Player_UI.Instance.LoadScene("SelectRoom");
        //gameObject.SetActive(false);
        //Player_UI.Instance.gameObject.SetActive(true);
        //roomSelect.SetActive(true);
    }
    private void OnEnable()
    {
        index = 0;
        NextPage();
    }

    private void Start()
    {
        NextPage();       
    }

}
