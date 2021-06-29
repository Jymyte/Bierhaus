using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MusicController : MonoBehaviour
{
    public static MusicController instance;
    public AudioSource audioSource;

    [SerializeField]  
    public List<AudioClip> songList;

    [SerializeField]
    public List<AudioClip> soundList;

    [SerializeField]
    private List<Sprite> muteButtonSprites;

    [SerializeField]
    private Image muteButtonImage;

    private bool mute = false;
    private bool muteSFX = false;
    
    void Awake() {
        MakeSingleton();
        audioSource = GetComponent<AudioSource> ();
    }

    private void Start() {
        //PlaySound("serving");
    }

    public void PlayMusic(bool play) {
        if (play) {
            if(!audioSource.isPlaying) {
                mute = !play;
                audioSource.Play();
                muteButtonImage.sprite = muteButtonSprites[1];
            }
        } else {
            if (audioSource.isPlaying) {
                mute = !play;
                audioSource.Stop();
                muteButtonImage.sprite = muteButtonSprites[0];
            }
        }
    }

    public void MusicOnOff () {
        if (audioSource.isPlaying == true) {
            PlayMusic(false);
        } else {
            PlayMusic(true);
        }
    }

    public void MuteSFX() {
        if (muteSFX) muteSFX = false; else muteSFX = true;
    }

    public void PlayTrack(string track) {
        for (int i = 0; i < songList.Count; i++) {
            if (songList[i].name.Contains(track)) {
                audioSource.clip = songList[i];
                if (mute == false) {
                    audioSource.Play();
                }
            }
        }
    }

    public void PlaySound(string track) {
        for (int i = 0; i < soundList.Count; i++) {
            if (soundList[i].name.Contains(track)) {
                if (muteSFX == false) {
                    audioSource.PlayOneShot(soundList[i], 1F);
                }
            }
        }
    }

    void MakeSingleton() {
        if (instance != null) {
            Destroy(gameObject);
        } else {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }
}
