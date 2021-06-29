using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuController : MonoBehaviour
{
    public void StartGame() {
      SceneFader.instance.LoadScene("Release");
    }

    public void MusicOnOff () {
        if (MusicController.instance.audioSource.isPlaying == true) {
            MusicController.instance.PlayMusic(false);
        } else {
            MusicController.instance.PlayMusic(true);
        }
    }
}
