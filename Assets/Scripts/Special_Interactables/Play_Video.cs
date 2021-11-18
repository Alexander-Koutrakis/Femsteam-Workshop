using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class Play_Video : MonoBehaviour, Interactable
{
    [SerializeField]
    private string item_required;
    [SerializeField]
    private GameObject videoPlayer_GO;
    private bool inspectable = false;
    private bool used;
    private Collider interact_collider;
    [SerializeField]
    private string feedback_text;
    [SerializeField]
    private SubtitlesTrack track;
    private OutlineController outlineController;
    private void Awake()
    {
        interact_collider = GetComponent<Collider>();
        outlineController = new OutlineController(gameObject);
    }

    public void Disable_Interactable()
    {
        interact_collider.enabled = false;
        inspectable = false;
    }

    public void Enable_Interactable()
    {
        interact_collider.enabled = true;
        inspectable = true;
    }

    public void Interact()
    {
        if (!used&&inspectable && MoveCamera.Instance.canClick)
        {
            if (Inventory.Instance.ItemHolding != null)
            {
                if (Inventory.Instance.ItemHolding.name == item_required)
                {
                    Inventory.Instance.DestroyItem(item_required);
                    videoPlayer_GO.SetActive(true);
                    Audio_Subtitles_Controller.Instance.Start_SubtitleTrack(track);
                    used = true;
                }
                else
                {
                    Player_UI.Instance.ShowText(feedback_text);
                }
            }
            else
            {
                Player_UI.Instance.ShowText(feedback_text);
            }
        }
        else
        {
            foreach(VideoPlayer videoPlayer in videoPlayer_GO.GetComponentsInChildren<VideoPlayer>())
            {
                videoPlayer.frame = 0;
                videoPlayer.Play();
            }
        }

        if (used && MoveCamera.Instance.canClick)
        {
            Audio_Subtitles_Controller.Instance.Start_SubtitleTrack(track);
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
}
