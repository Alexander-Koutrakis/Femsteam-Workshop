using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Audio_Subtitles_Controller : MonoBehaviour
{
    public static Audio_Subtitles_Controller Instance;
    private TMP_Text text;
    private int index = 0;
    private SubtitlesTrack track;

    private void Awake()
    {
        text = GetComponent<TMP_Text>();
        Instance = this;
    }

    public void Start_SubtitleTrack(SubtitlesTrack newTrack)
    {
        StopAllCoroutines();
        track = newTrack;
        index = 0;
        Show_Subtitle();
    }

   private void Show_Subtitle()
    {
        if (index >= track.Texts.Length) {
            return;
        }
        text.text = track.Texts[index];
        float wait = new float();
        wait = track.Text_Duration[index];

        StartCoroutine(WaitForNextText(wait));
    }

    private IEnumerator WaitForNextText(float waiting)
    {
        float textIntervals = 0.5f;
        while (waiting > 0)
        {
            waiting -= Time.deltaTime;
            yield return null;
        }
        text.text = "";
        while (textIntervals > 0)
        {
            textIntervals -= Time.deltaTime;
            yield return null;
        }
        index++;
        Show_Subtitle();

    }
}
