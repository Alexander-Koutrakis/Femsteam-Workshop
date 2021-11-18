using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class SubtitlesTrack : ScriptableObject
{
    [TextArea]
    public string[] Texts = new string[0];
    public float[] Text_Duration = new float[0];
    public float Text_Intervals;
}
