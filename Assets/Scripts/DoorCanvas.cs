using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class DoorCanvas : MonoBehaviour
{
  public void LoadScene(string sceneName)
    {
        Player_UI.Instance.LoadScene(sceneName);
    }
}
