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

    private static AudioController instance;

    public static AudioController Instance {
        get {
            if (instance == null) {
                instance = FindObjectOfType<AudioController>();
#if UNITY_EDITOR
                if (FindObjectsOfType<AudioController>().Length > 1) {
                    Debug.LogError("There is more than one game controller in the scene");
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

    public void SFXSetPitch(float f) {
        foreach (Sound s in sfxList) {
            s.source.pitch = f;
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



    public void Play(string name) {
        try {
            foreach (Sound s in allSounds) {
                if (s.name == name) {
                    s.source.Play();
                    return;
                }
            }
        } catch (System.NullReferenceException) {
            Debug.LogWarning("The audio with name '" + name + "' could not be found in list. Is it spelled correctly? (NullReferenceException)");
        }
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
            Debug.LogWarning("The audio with name '" + name + "' could not be found in list. Is it spelled correctly? (NullReferenceException)");
        }
    }

    public void Play_RandomPitch(string name, float minPitch, float maxPitch) {
        try {
            foreach (Sound s in allSounds) {
                if (s.name == name) {
                    s.source.pitch = Random.Range(minPitch, maxPitch);
                    s.source.Play();
                    return;
                }
            }
        } catch (System.NullReferenceException) {
            Debug.LogWarning("The audio with name '" + name + "' could not be found in list. Is it spelled correctly? (NullReferenceException)");
        }
    }

    public void Play_RandomPitch_InWorldspace(string name, GameObject gameObjectLocation, float minPitch, float maxPitch) {
        try {
            foreach (Sound s in allSounds) {
                if (s.name == name) {
                    GameObject soundAtLocationGO = Instantiate(soundObject, gameObjectLocation.transform.position, Quaternion.identity);
                    s.source = soundAtLocationGO.GetComponent<AudioSource>();
                    s.source.clip = s.clip;
                    s.source.volume = s.volume;
                    s.source.pitch = Random.Range(minPitch, maxPitch);
                    s.source.spatialBlend = s.spatialBlend_2D_3D;
                    s.source.rolloffMode = (AudioRolloffMode)s.rolloffMode;
                    s.source.minDistance = s.minDistance;
                    s.source.maxDistance = s.maxDistance;
                    s.source.loop = s.loop;
                    s.source.Play();
                    if (!s.source.loop) {
                        Destroy(soundAtLocationGO, s.clip.length);
                    }
                    return;
                }
            }
        } catch (System.NullReferenceException) {
            Debug.LogWarning("The audio with name '" + name + "' could not be found in list. Is it spelled correctly? (NullReferenceException)");
        }
    }

    public void Play_InWorldspace(string name, GameObject gameObjectLocation) {
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
                    return;
                }
            }
        } catch (System.NullReferenceException) {
            Debug.LogWarning("The audio with name '" + name + "' could not be found in list. Is it spelled correctly? (NullReferenceException)");
        }
    }



}
