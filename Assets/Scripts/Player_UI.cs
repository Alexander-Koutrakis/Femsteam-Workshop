using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

// Player UI is responsible for changing the Buttons when the player interacts with Items or moves around in the Room
public class Player_UI : MonoBehaviour
{
    private static CameraMover currentMover;
    private static Item focusingItem;
    [SerializeField]
    private Button backButton;
    [SerializeField]
    private Button takeItem_Button;
    [SerializeField]
    private Button backItemButton;
    public static Player_UI Instance;
    [SerializeField]
    private TMP_Text character_Text;
    [SerializeField]
    private TMP_Text location_Text;
    [SerializeField]
    private Image fadeToBlackImage;
    public bool Faded;
    private void Awake()
    {
        Instance = this;
        backButton.gameObject.SetActive(false);
        takeItem_Button.gameObject.SetActive(false);
        backItemButton.gameObject.SetActive(false);
        //character_Text = GetComponentsInChildren<TMP_Text>()[0];
        //location_Text = GetComponentsInChildren<TMP_Text>()[1];
    }

    private void Start()
    {
        StartCoroutine(FadeFromBlack(0.5f));
    }
    public void GoBack()
    {
        backButton.gameObject.SetActive(false);
        currentMover.ReturnToparent();
    }

    public void Focus_Area(CameraMover mover)
    {
        currentMover = mover;
        backButton.gameObject.SetActive(true);
        takeItem_Button.gameObject.SetActive(false);
        backItemButton.gameObject.SetActive(false);
    }

    public void Focus_Item(bool canTake,bool taken,Item item)
    {
        focusingItem = item;
        backButton.gameObject.SetActive(false);
        backItemButton.gameObject.SetActive(true);
        if (canTake&&!taken)
        {
            takeItem_Button.gameObject.SetActive(true);
        }
        else
        {
            takeItem_Button.gameObject.SetActive(false);
        }
    }
    public void Take_Item()
    {
        Inventory.Instance.TakeItem(focusingItem);
    }

    public void ReturnFromFocus_Item()
    {
        Item.Enable_All_Items();
        MoveCamera.Instance.ReturnFromItem();
    }

    public void Show_Location(string text_string)
    {
        location_Text.text = text_string;
    }

    public void ShowText(string text_string)
    {
        StopAllCoroutines();
        character_Text.text = "";
        StartCoroutine(ShowText_Enum(text_string));
    }

    private IEnumerator ShowText_Enum(string text_string)
    {
        character_Text.text = text_string;
        yield return new WaitForSeconds(4);
        character_Text.text = "";
    }

    private IEnumerator FadeToBlack(float duration)
    {
        fadeToBlackImage.gameObject.SetActive(true);
        float alpha = fadeToBlackImage.color.a;
        while (duration > 0)
        {
            alpha = Mathf.MoveTowards(alpha,1, Time.deltaTime/duration);
            fadeToBlackImage.color = new Color(fadeToBlackImage.color.r, fadeToBlackImage.color.g, fadeToBlackImage.color.b,alpha);
            duration -= Time.deltaTime;
            yield return null;
        }
        Faded = true;
    }

    private IEnumerator FadeFromBlack(float duration)
    {
        float alpha = fadeToBlackImage.color.a;
        while (duration > 0)
        {
            alpha = Mathf.MoveTowards(alpha, 0, Time.deltaTime/duration);
            fadeToBlackImage.color = new Color(fadeToBlackImage.color.r, fadeToBlackImage.color.g, fadeToBlackImage.color.b, alpha); ;
            duration -= Time.deltaTime;
            yield return null;
        }
        fadeToBlackImage.gameObject.SetActive(false);
        Faded = false;
    }

    private IEnumerator LoadSceneEnum(string sceneName)
    {
        StartCoroutine(FadeToBlack(0.5f));
        yield return new WaitForSeconds(0.5f);
        SceneManager.LoadScene(sceneName);      
    }

    private IEnumerator IEFade(float duration)
    {
        StartCoroutine(FadeToBlack(duration));
        yield return new WaitForSeconds(duration);
        StartCoroutine(FadeFromBlack(duration));
    }

    public void Fade(float duration)
    {
        StartCoroutine(IEFade(duration));
    }

    public void LoadScene(string sceneName)
    {
        StartCoroutine(LoadSceneEnum(sceneName));
    }

    public void Quit()
    {
        Application.Quit();

    }
}
