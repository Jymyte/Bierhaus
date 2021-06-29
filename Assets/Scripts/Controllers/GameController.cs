using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
  [SerializeField]
  private Globals globals;
  [SerializeField]
  private GameObject restartMenu;

  

  private void Start() {
    Time.timeScale = 1f;
    StartCoroutine(HandleGameover());
  }

  private  IEnumerator HandleGameover() {
    yield return new WaitForSeconds(globals.roundTime);
    Time.timeScale = 0f;
    restartMenu.SetActive(true);
  }

  public void restartGame() {
    SceneFader.instance.LoadScene("Release");
  }

  public void SFXOnOff () {
    MusicController.instance.MuteSFX();
  }
}
