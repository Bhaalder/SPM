using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioController : MonoBehaviour {
    //Author: Patrik Ahlgren

    [Header("Player")]
    public Sound[] playerSounds;
    [Header("Enemies")]
    public Sound[] enemySounds;
    [Header("Environment")]
    public Sound[] environmentSounds;
    [Header("Music")]
    public Sound[] musicSounds;
    [Header("Sound Object")]
    public GameObject soundObject;
    [Header("AudioMixer")]
    public AudioMixer audioMixer;
    public AudioMixerGroup audioMixerGroup;

    private List<Sound> musicList = new List<Sound>();
    private List<Sound> sfxList = new List<Sound>();
    private List<Sound> allSounds = new List<Sound>();

    private float musicSoundLevel;
    private float sfxSoundLevel;

    private bool continueFadeIn;
    private bool continueFadeOut;

    private static AudioController instance;

    public static AudioController Instance {
        get {
            if (instance == null) {
                instance = FindObjectOfType<AudioController>();
#if UNITY_EDITOR
                if (FindObjectsOfType<AudioController>().Length > 1) {
                    Debug.LogError("There is more than one AudioController in the scene");
                }
#endif
            }
            return instance;
        }
    }



    private void Awake() {
        if (instance != null && instance != this) {
            Destroy(gameObject);
            Debug.LogWarning("Destroyed other Singleton with name: " + gameObject.name);
        }

        DontDestroyOnLoad(gameObject);

        foreach (Sound player_S in playerSounds) { allSounds.Add(player_S); sfxList.Add(player_S); }
        foreach (Sound enemy_S in enemySounds) { allSounds.Add(enemy_S); sfxList.Add(enemy_S); }
        foreach (Sound environment_S in environmentSounds) { allSounds.Add(environment_S); sfxList.Add(environment_S); }
        foreach (Sound music_S in musicSounds) { allSounds.Add(music_S); musicList.Add(music_S); }

        foreach (Sound s in allSounds) {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.spatialBlend = s.spatialBlend_2D_3D;
            s.source.rolloffMode = (AudioRolloffMode)s.rolloffMode;
            s.source.minDistance = s.minDistance;
            s.source.maxDistance = s.maxDistance;
            s.source.loop = s.loop;
            s.source.outputAudioMixerGroup = audioMixerGroup;
        }
    }

    #region Play/Stop Methods
    public void Play(string name) {       
        try {
            FindSound(name).source.Play();
        } catch (System.NullReferenceException) {
            AudioNotFound(name);
        }
    }

    public void PlaySFX(string name) {
        try {
            FindSFX(name).source.Play();
        } catch (System.NullReferenceException) {
            AudioNotFound(name);
        }
    }

    public void PlayMusic(string name) {
        try {
            FindMusic(name).source.Play();
        } catch (System.NullReferenceException) {
            AudioNotFound(name);
        }
    }

    public GameObject Play_InWorldspace(string name, GameObject gameObjectLocation) {
        try {
            foreach (Sound s in allSounds) {
                if (s.name == name) {
                    GameObject soundAtLocationGO = Instantiate(soundObject, gameObjectLocation.transform.position, Quaternion.identity);
                    s.source = soundAtLocationGO.GetComponent<AudioSource>();
                    s.source.clip = s.clip;
                    s.source.volume = s.volume;
                    s.source.pitch = s.pitch;
                    s.source.spatialBlend = s.spatialBlend_2D_3D;
                    s.source.rolloffMode = (AudioRolloffMode)s.rolloffMode;
                    s.source.minDistance = s.minDistance;
                    s.source.maxDistance = s.maxDistance;
                    s.source.loop = s.loop;
                    s.source.Play();
                    if (!s.source.loop) {
                        Destroy(soundAtLocationGO, s.clip.length);
                    }
                    return soundAtLocationGO;
                }
            }
        } catch (System.NullReferenceException) {
            AudioNotFound(name);
        }
        return null;
    }

    public void Stop(string name) {
        try {
            FindSound(name).source.Stop();
        } catch (System.NullReferenceException) {
            AudioNotFound(name);
        }       
    }



    #endregion

    #region PlayRandomPitch
    public void Play_RandomPitch(string name, float minPitch, float maxPitch) {
        Sound s = FindSound(name);
        try {
            if (!GameController.Instance.gameIsSlowmotion) {
                s.source.pitch = Random.Range(minPitch, maxPitch);
            }
            s.source.Play();
        } catch (System.NullReferenceException) {
            AudioNotFound(name);
        }
    }

    public void PlaySFX_RandomPitch(string name, float minPitch, float maxPitch) {
        Sound s = FindSFX(name);
        try {
            if (!GameController.Instance.gameIsSlowmotion) {
                s.source.pitch = Random.Range(minPitch, maxPitch);
            }
            s.source.Play();
        } catch (System.NullReferenceException) {
            AudioNotFound(name);
        }       
    }

    public GameObject Play_RandomPitch_InWorldspace(string name, GameObject gameObjectLocation, float minPitch, float maxPitch) {
        try {
            foreach (Sound s in allSounds) {
                if (s.name == name) {
                    GameObject soundAtLocationGO = Instantiate(soundObject, gameObjectLocation.transform.position, Quaternion.identity);
                    s.source = soundAtLocationGO.GetComponent<AudioSource>();
                    s.source.clip = s.clip;
                    s.source.volume = s.volume;
                    if (!GameController.Instance.gameIsSlowmotion) {
                        s.source.pitch = Random.Range(minPitch, maxPitch);
                    }
                    s.source.spatialBlend = s.spatialBlend_2D_3D;
                    s.source.rolloffMode = (AudioRolloffMode)s.rolloffMode;
                    s.source.minDistance = s.minDistance;
                    s.source.maxDistance = s.maxDistance;
                    s.source.loop = s.loop;
                    s.source.Play();
                    if (!s.source.loop) {
                        Destroy(soundAtLocationGO, s.clip.length);
                    }
                    return soundAtLocationGO;
                }
            }
        } catch (System.NullReferenceException) {            
            AudioNotFound(name);
        }
        return null;
    }
    #endregion

    #region Volume / Pitch Methods
    public void SFXSetPitch(float pitch) {
        foreach (Sound s in sfxList) {
            try {
                s.source.pitch = pitch;
            } catch (System.Exception) {

            }         
        }
    }

    public void SFXSetVolume(float volume) {
        foreach (Sound s in sfxList) {
            sfxSoundLevel = volume;
            s.source.volume = sfxSoundLevel;
        }
    }

    public void MusicSetVolume(float volume) {
        foreach (Sound s in musicList) {
            musicSoundLevel = volume;
            s.source.volume = musicSoundLevel;
        }
    }

    public void AllSoundsSetVolume(float volume) {
        if (volume == -80) {
            audioMixer.SetFloat("MasterVolume", volume);
        } else {
            audioMixer.SetFloat("MasterVolume", (volume / 4));
        }
    }
    #endregion

    #region Pause Methods
    public void Pause(string name, bool pause) {
        try {
            if (pause) {
                FindSound(name).source.Pause();
            } else if (!pause) {
                FindSound(name).source.UnPause();
            }           
        } catch (System.NullReferenceException) {
            AudioNotFound(name);
        }
    }

    public void PauseAllSFX(bool pause) {
        if (pause) {
            foreach (Sound s in sfxList) {
                try {
                    s.source.Pause();
                } catch (System.Exception) {

                }
            }
        } else if (!pause) {
            foreach (Sound s in sfxList) {
                try {
                    s.source.UnPause();
                } catch (System.Exception) {

                }
            }
        }
    }

    public void PauseAllSound(bool pause) {
        if (pause) {
            foreach (Sound s in allSounds) {
                try {
                    s.source.Pause();
                } catch (System.Exception) {

                }                
            }                    
        } else if (!pause) {
            foreach (Sound s in allSounds) {

                try {
                    s.source.UnPause();
                } catch (System.Exception) {

                }
            }
        }

    }
    #endregion

    #region FadeIn/Out Methods
    public void FadeIn(string name, float fadeDuration, float soundVolumePercentage) {
        try {
            Sound sound = FindMusic(name);
            if (sound != null) {
                sound.source.volume = 0;
                sound.source.Play();
                if (musicSoundLevel < (soundVolumePercentage / 100)) {
                    soundVolumePercentage = (musicSoundLevel * 100);
                }
                StartCoroutine(FadeInAudio(name, fadeDuration, (soundVolumePercentage / 100), sound));
                return;
            }
            sound = FindSFX(name);
            if (sound != null) {
                sound.source.volume = 0;
                sound.source.Play();
                if (sfxSoundLevel < (soundVolumePercentage / 100)) {
                    soundVolumePercentage = (sfxSoundLevel * 100);
                }
                StartCoroutine(FadeInAudio(name, fadeDuration, (soundVolumePercentage / 100), sound));
                return;
            }
        } catch (System.NullReferenceException) {
            AudioNotFound(name);
        }

    }

    public void FadeOut(string name, float fadeDuration, float soundVolumePercentage) {
        try {
            Sound sound = FindMusic(name);
            if(sound != null) {
                if (musicSoundLevel < (soundVolumePercentage / 100)) {
                    soundVolumePercentage = (musicSoundLevel * 100);
                }
                StartCoroutine(FadeOutAudio(name, fadeDuration, (soundVolumePercentage / 100), sound));
                return;
            }
            sound = FindSFX(name);
            if(sound != null) {
                if (sfxSoundLevel < (soundVolumePercentage / 100)) {
                    soundVolumePercentage = (sfxSoundLevel * 100);
                }
                StartCoroutine(FadeOutAudio(name, fadeDuration, (soundVolumePercentage / 100), sound));
                return;
            }
        } catch (System.NullReferenceException) {
            AudioNotFound(name);
        }
    }

    private IEnumerator FadeInAudio(string name, float fadeDuration, float soundVolume, Sound sound) {
        continueFadeIn = true;
        continueFadeOut = false;
        float startSoundValue = 0;
        if (continueFadeIn) {
            for (float time = 0f; time < fadeDuration; time += Time.unscaledDeltaTime) {
                float normalizedTime = time / fadeDuration;
                sound.source.volume = Mathf.Lerp(startSoundValue, soundVolume, normalizedTime);
                yield return null;
            }
        }
    }

    private IEnumerator FadeOutAudio(string name, float fadeDuration, float soundVolume, Sound sound) {
        continueFadeIn = false;
        continueFadeOut = true;
        float startSoundValue = sound.source.volume;
        if (continueFadeOut) {
            for (float time = 0f; time < fadeDuration; time += Time.unscaledDeltaTime) {
                float normalizedTime = time / fadeDuration;
                sound.source.volume = Mathf.Lerp(startSoundValue, soundVolume, normalizedTime);
                yield return null;
            }
        }
    }
    #endregion

    #region FindSound Methods

    private Sound FindSound(string name) {
        foreach (Sound s in allSounds) {
            if (s.name == name) {
                return s;
            }
        }
        return null;
    }

    private Sound FindMusic(string name) {
        foreach (Sound s in musicList) {
            if (s.name == name) {
                return s;
            }
        }
        return null;
    }

    private Sound FindSFX(string name) {
        foreach (Sound s in sfxList) {
            if (s.name == name) {
                return s;
            }
        }
        return null;
    }

    #endregion

    private void AudioNotFound(string name) {
        Debug.LogWarning("The sound with name '" + name + "' could not be found in list. Is it spelled correctly? (NullReferenceException)");
    }

}
