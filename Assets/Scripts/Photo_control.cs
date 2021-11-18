using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Photo_control : MonoBehaviour,ResultChecker
{
    [SerializeField]
    private Sprite[] sprites = new Sprite[0];
    private int index;
    [SerializeField]
    private Image image;
    [SerializeField]
    private Sprite correctSprite;
    private Notes_Control notes_Control;

    private void Awake()
    {
        notes_Control = GetComponentInParent<Notes_Control>();
    }
    private void Start()
    {
        index = 0;
        NextPhoto();
    }
    public void NextPhoto()
    {
        index++;
        if (index >= sprites.Length)
        {
            index = 0;
        }
        image.sprite = sprites[index];
        notes_Control.OnAnswerChange();
    }

    public void PreviousPhoto()
    {
        index--;
        if (index < 0)
        {
            index = sprites.Length-1;
        }
        image.sprite = sprites[index];
        notes_Control.OnAnswerChange();
    }

    public bool CheckResults()
    {
        if (image.sprite == correctSprite)
        {
            return true;
        }
        else
        {
            return false;
        }

    }
}
