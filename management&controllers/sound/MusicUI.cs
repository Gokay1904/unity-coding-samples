using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MusicUI : MonoBehaviour
{
    public Button musicButton;

    public Sprite musicWithAmbient;
    public Sprite onlyMusic;
    public Sprite onlyAmbient;
    public Sprite noSound;

    public int soundTypeIndex;

    private void Awake()
    {
        soundTypeIndex = 0;
    }
    public void Start()
    {
        SoundManagement.soundManagement.PlaySound("MainTheme");
    }
    private void Update()
    {
    
    }

    public void OnClickSoundIcon()
    {
        if (soundTypeIndex <= 2 && soundTypeIndex >= 0)
        {
            soundTypeIndex++;
        }
        else if(soundTypeIndex == 3)
        {
            soundTypeIndex = 0;
        }

        OnIndexChanged();
    }

    void OnIndexChanged()
    {
        if (SoundManagement.soundManagement.playingCoroutine != null)
        {
            StopCoroutine(SoundManagement.soundManagement.playingCoroutine);
            SoundManagement.soundManagement.playingCoroutine = null;
        }

        if (soundTypeIndex == 0)
        {
            musicButton.image.sprite = musicWithAmbient;
            SoundManagement.soundManagement.playingCoroutine = SoundManagement.soundManagement.StartCoroutine(SoundManagement.soundManagement.AmbientWithMusic());
        }
        if (soundTypeIndex == 1)
        {
            musicButton.image.sprite = onlyMusic;
            SoundManagement.soundManagement.playingCoroutine = SoundManagement.soundManagement.StartCoroutine(SoundManagement.soundManagement.OnlyMusic());
        }
        if (soundTypeIndex == 2)
        {
            musicButton.image.sprite = onlyAmbient;
            SoundManagement.soundManagement.playingCoroutine = SoundManagement.soundManagement.StartCoroutine(SoundManagement.soundManagement.OnlyAmbient());
        }
        if (soundTypeIndex == 3)
        {
            musicButton.image.sprite = noSound;
            SoundManagement.soundManagement.playingCoroutine =  SoundManagement.soundManagement.StartCoroutine(SoundManagement.soundManagement.SoundOff());
        }

    }
   

}
