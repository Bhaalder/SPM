using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuAudioSliders : MonoBehaviour{
    //Author: Patrik Ahlgren

    [SerializeField] private Slider masterVolumeSlider;
    [SerializeField] private Slider musicVolumeSlider;
    [SerializeField] private Slider sfxVolumeSlider;

    void Start()
    {
        masterVolumeSlider.onValueChanged.AddListener(delegate { MasterValueChangeCheck(); });
        musicVolumeSlider.onValueChanged.AddListener(delegate { MusicValueChangeCheck(); });
        sfxVolumeSlider.onValueChanged.AddListener(delegate { SFXValueChangeCheck(); });
    }

    public void MasterValueChangeCheck() {
        AudioController.Instance.AllSoundsSetVolume(masterVolumeSlider.value);//denna slider ska vara mellan -80 till 0
    }

    public void MusicValueChangeCheck() {
        AudioController.Instance.MusicSetVolume(musicVolumeSlider.value);//denna slider ska vara mellan 0 till 1;
    }

    public void SFXValueChangeCheck() {
        AudioController.Instance.SFXSetVolume(sfxVolumeSlider.value);//denna slider ska vara mellan 0 till 1;
    }
}
