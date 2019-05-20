using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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

    private List<Sound> musicList = new List<Sound>();
    private List<Sound> sfxList = new List<Sound>();
    private List<Sound> allSounds = new List<Sound>();

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
        }
    }

    #region Play/Stop
    public void Play(string name) {
        try {
            foreach (Sound s in allSounds) {
                if (s.name == name) {
                    s.source.Play();
                    return;
                }
            }
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
            foreach (Sound s in allSounds) {
                if (s.name == name) {
                    s.source.Stop();
                    return;
                }
            }
        } catch (System.NullReferenceException) {
            AudioNotFound(name);
        }
    }
    #endregion

    #region PlayRandomPitch
    public void Play_RandomPitch(string name, float minPitch, float maxPitch) {
        try {
            foreach (Sound s in allSounds) {
                if (s.name == name) {
                    if (!GameController.Instance.gameIsSlowmotion) {
                        s.source.pitch = Random.Range(minPitch, maxPitch);
                    }                  
                    s.source.Play();
                    return;
                }
            }
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
    public void SFXSetPitch(float f) {
        foreach (Sound s in sfxList) {
            try {
                s.source.pitch = f;
            } catch (System.Exception) {

            }         
        }
    }

    public void SFXSetVolume(float f) {
        foreach (Sound s in sfxList) {

            s.source.volume = f;
        }
    }

    public void MusicSetVolume(float f) {
        foreach (Sound s in musicList) {
            s.source.volume = f;
        }
    }

    public void AllSoundsSetVolume(float f) {
        foreach (Sound s in allSounds) {
            s.source.volume *= f;
        }
    }
    #endregion

    #region FadeIn/Out Methods
    public void FadeIn(string name, float fadeDuration, float soundVolumePercentage) {
        try {
            foreach (Sound s in allSounds) {
                if (s.name == name) {
                    s.source.volume = 0;
                    s.source.Play();
                    StartCoroutine(FadeInAudio(name, fadeDuration, (soundVolumePercentage / 100), s));
                    return;
                }
            }
        } catch (System.NullReferenceException) {
            AudioNotFound(name);
        }

    }

    public void FadeOut(string name, float fadeDuration, float soundVolumePercentage) {
        try {
            foreach (Sound s in allSounds) {
                if (s.name == name) {
                    StartCoroutine(FadeOutAudio(name, fadeDuration, (soundVolumePercentage / 100), s));
                    return;
                }
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
            for (float time = 0f; time < fadeDuration; time += Time.deltaTime) {
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
            for (float time = 0f; time < fadeDuration; time += Time.deltaTime) {
                float normalizedTime = time / fadeDuration;
                sound.source.volume = Mathf.Lerp(startSoundValue, soundVolume, normalizedTime);
                yield return null;
            }
        }
    }
    #endregion

    private void AudioNotFound(string name) {
        Debug.LogWarning("The audio with name '" + name + "' could not be found in list. Is it spelled correctly? (NullReferenceException)");
    }

}
