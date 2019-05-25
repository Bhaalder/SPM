using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioController : MonoBehaviour {
    //Author: Patrik Ahlgren

    [Header("Player")]
    [SerializeField] private Sound[] playerSounds;
    [Header("Enemies")]
    [SerializeField] private Sound[] enemySounds;
    [Header("Environment")]
    [SerializeField] private Sound[] environmentSounds;
    [Header("Music")]
    [SerializeField] private Sound[] musicSounds;
    [Header("Sound Object")]
    [SerializeField] private GameObject soundObject;
    [Header("AudioMixer")]
    [SerializeField] private AudioMixer audioMixer;
    [SerializeField] private AudioMixerGroup audioMixerGroup;
    

    private Dictionary<string, Sound> musicList = new Dictionary<string, Sound>();
    private Dictionary<string, Sound> sfxList = new Dictionary<string, Sound>();
    private Dictionary<string, Sound> allSounds = new Dictionary<string, Sound>();

    private Dictionary<string, float> soundTimerDictonary = new Dictionary<string, float>();

    private float musicSoundLevel = 1;
    private float sfxSoundLevel = 1;

    private bool continueFadeIn;
    private bool continueFadeOut;

    private Sound sound;

    private static AudioController _instance;

    public static AudioController Instance {
        get {
            if (_instance == null) {
                _instance = FindObjectOfType<AudioController>();
#if UNITY_EDITOR
                if (FindObjectsOfType<AudioController>().Length > 1) {
                    Debug.LogError("There is more than one AudioController in the scene");
                }
#endif
            }
            return _instance;
        }
    }



    private void Awake() {
        if (_instance != null && _instance != this) {
            Destroy(gameObject);
            Debug.LogWarning("Destroyed other Singleton with name: " + gameObject.name);
        }

        DontDestroyOnLoad(gameObject);

        foreach (Sound player_S in playerSounds) { allSounds[player_S.name] = player_S; sfxList[player_S.name] = player_S; }
        foreach (Sound enemy_S in enemySounds) { allSounds[enemy_S.name] = enemy_S; sfxList[enemy_S.name] = enemy_S; }
        foreach (Sound environment_S in environmentSounds) { allSounds[environment_S.name] = environment_S; sfxList[environment_S.name] = environment_S; }
        foreach (Sound music_S in musicSounds) { allSounds[music_S.name] = music_S; musicList[music_S.name] = music_S; }

        foreach (KeyValuePair<string, Sound> s in allSounds) {
            s.Value.source = gameObject.AddComponent<AudioSource>();
            s.Value.source.clip = s.Value.clip;
            s.Value.source.volume = s.Value.volume;
            s.Value.source.pitch = s.Value.pitch;
            s.Value.source.spatialBlend = s.Value.spatialBlend_2D_3D;
            s.Value.source.rolloffMode = (AudioRolloffMode)s.Value.rolloffMode;
            s.Value.source.minDistance = s.Value.minDistance;
            s.Value.source.maxDistance = s.Value.maxDistance;
            s.Value.source.loop = s.Value.loop;
            s.Value.source.outputAudioMixerGroup = audioMixerGroup;
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

            if (allSounds.ContainsKey(name)) {
                GameObject soundAtLocationGO = Instantiate(soundObject, gameObjectLocation.transform.position, Quaternion.identity);
                sound = allSounds[name];
                sound.source = soundAtLocationGO.GetComponent<AudioSource>();
                sound.source.clip = sound.clip;
                sound.source.volume = sound.volume;
                sound.source.pitch = sound.pitch;
                sound.source.spatialBlend = sound.spatialBlend_2D_3D;
                sound.source.rolloffMode = (AudioRolloffMode)sound.rolloffMode;
                sound.source.minDistance = sound.minDistance;
                sound.source.maxDistance = sound.maxDistance;
                sound.source.loop = sound.loop;
                sound.source.Play();
                if (!sound.source.loop) {
                    Destroy(soundAtLocationGO, sound.clip.length);
                }
                return soundAtLocationGO;
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
        sound = FindSound(name);
        try {
            if (!GameController.Instance.GameIsSlowmotion) {
                sound.source.pitch = Random.Range(minPitch, maxPitch);
            }
            sound.source.Play();
        } catch (System.NullReferenceException) {
            AudioNotFound(name);
        }
    }

    public void PlaySFX_RandomPitch(string name, float minPitch, float maxPitch) {
        sound = FindSFX(name);
        try {
            if (!GameController.Instance.GameIsSlowmotion) {
                sound.source.pitch = Random.Range(minPitch, maxPitch);
            }
            sound.source.Play();
        } catch (System.NullReferenceException) {
            AudioNotFound(name);
        }       
    }

    public GameObject Play_RandomPitch_InWorldspace(string name, GameObject gameObjectLocation, float minPitch, float maxPitch) {
        try {
            if (allSounds.ContainsKey(name)) {
                GameObject soundAtLocationGO = Instantiate(soundObject, gameObjectLocation.transform.position, Quaternion.identity);
                sound = allSounds[name];
                sound.source = soundAtLocationGO.GetComponent<AudioSource>();
                sound.source.clip = sound.clip;
                sound.source.volume = sound.volume;
                if (!GameController.Instance.GameIsSlowmotion) {
                    sound.source.pitch = Random.Range(minPitch, maxPitch);
                }
                sound.source.spatialBlend = sound.spatialBlend_2D_3D;
                sound.source.rolloffMode = (AudioRolloffMode)sound.rolloffMode;
                sound.source.minDistance = sound.minDistance;
                sound.source.maxDistance = sound.maxDistance;
                sound.source.loop = sound.loop;
                sound.source.Play();
                if (!sound.source.loop) {
                    Destroy(soundAtLocationGO, sound.clip.length);
                }
                return soundAtLocationGO;
            }
        } catch (System.NullReferenceException) {            
            AudioNotFound(name);
        }
        return null;
    }
    #endregion

    #region Volume / Pitch Methods
    public void SFXSetPitch(float pitch) {
        foreach (KeyValuePair<string, Sound> s in sfxList) {
            try {
                s.Value.source.pitch = pitch;
            } catch (System.Exception) {

            }         
        }
    }

    public void SFXSetVolume(float volume) {
        foreach (KeyValuePair<string, Sound> s in sfxList) {
            sfxSoundLevel = volume;
            s.Value.source.volume = sfxSoundLevel;
        }
    }

    public void MusicSetVolume(float volume) {
        foreach (KeyValuePair<string, Sound> s in musicList) {
            musicSoundLevel = volume;
            s.Value.source.volume = musicSoundLevel;
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
            foreach (KeyValuePair<string, Sound> s in sfxList) {
                try {
                    s.Value.source.Pause();
                } catch (System.Exception) {

                }
            }
        } else if (!pause) {
            foreach (KeyValuePair<string, Sound> s in sfxList) {
                try {
                    s.Value.source.UnPause();
                } catch (System.Exception) {

                }
            }
        }
    }

    public void PauseAllSound(bool pause) {
        if (pause) {
            foreach (KeyValuePair<string, Sound> s in allSounds) {
                try {
                    s.Value.source.Pause();
                } catch (System.Exception) {

                }                
            }                    
        } else if (!pause) {
            foreach (KeyValuePair<string, Sound> s in allSounds) {

                try {
                    s.Value.source.UnPause();
                } catch (System.Exception) {

                }
            }
        }

    }
    #endregion

    #region FadeIn/Out Methods
    public void FadeIn(string name, float fadeDuration, float soundVolumePercentage) {
        try {
            sound = FindMusic(name);
            if (sound != null) {
                sound.source.volume = 0;
                sound.source.Play();
                if (musicSoundLevel < (soundVolumePercentage / 100)) {
                    soundVolumePercentage = (musicSoundLevel * 100);
                }
                StartCoroutine(FadeInAudio(fadeDuration, (soundVolumePercentage / 100), sound));
                return;
            }
            sound = FindSFX(name);
            if (sound != null) {
                sound.source.volume = 0;
                sound.source.Play();
                if (sfxSoundLevel < (soundVolumePercentage / 100)) {
                    soundVolumePercentage = (sfxSoundLevel * 100);
                }
                StartCoroutine(FadeInAudio(fadeDuration, (soundVolumePercentage / 100), sound));
                return;
            }
        } catch (System.NullReferenceException) {
            AudioNotFound(name);
        }

    }

    private float FadeInAudioLevelCheck(float soundLevel, float soundVolumePercentage) {
        return 1;//Ska fixa en sen
    }

    private float FadeOutAudioLevelCheck(float soundLevel, float soundVolumePercentage) {
        return 1;//Ska fixa en sen
    }

    public void FadeOut(string name, float fadeDuration, float soundVolumePercentage) {
        try {
            sound = FindMusic(name);
            if(sound != null) {
                if (musicSoundLevel < (soundVolumePercentage / 100)) {
                    soundVolumePercentage = (musicSoundLevel * 100);
                }
                StartCoroutine(FadeOutAudio(fadeDuration, (soundVolumePercentage / 100), sound));
                return;
            }
            sound = FindSFX(name);
            if(sound != null) {
                if (sfxSoundLevel < (soundVolumePercentage / 100)) {
                    soundVolumePercentage = (sfxSoundLevel * 100);
                }
                StartCoroutine(FadeOutAudio(fadeDuration, (soundVolumePercentage / 100), sound));
                return;
            }
        } catch (System.NullReferenceException) {
            AudioNotFound(name);
        }
    }

    private IEnumerator FadeInAudio(float fadeDuration, float soundVolume, Sound sound) {
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

    private IEnumerator FadeOutAudio(float fadeDuration, float soundVolume, Sound sound) {
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
        if (allSounds.ContainsKey(name)) {
            return allSounds[name];
        }
        return null;
    }

    private Sound FindMusic(string name) {
        if (musicList.ContainsKey(name)) {
            return musicList[name];
        }
        return null;
    }

    private Sound FindSFX(string name) {
        if (sfxList.ContainsKey(name)) {
            return sfxList[name];
        }
        return null;
    }

    #endregion

    #region WaitForFinish Methods

    public void PlaySFX_RandomPitchAndVolume_Finish(string name, float minPitch, float maxPitch) {
        sound = FindSFX(name);
        try {
            if (!soundTimerDictonary.ContainsKey(sound.name)) {
                soundTimerDictonary[sound.name] = 0f;
            }
            if (!GameController.Instance.GameIsSlowmotion) {
                sound.source.pitch = Random.Range(minPitch, maxPitch);
                sound.source.volume = Random.Range(sfxSoundLevel * 0.6f, sfxSoundLevel);
            }
            if (CanPlaySound(sound)) {
                sound.source.Play();
            }
        } catch (System.NullReferenceException) {
            AudioNotFound(name);
        }
    }

    private bool CanPlaySound(Sound sound) {
        if (soundTimerDictonary.ContainsKey(sound.name)) {
            float lastTimePlayed = soundTimerDictonary[sound.name];
            float playerMoveTimerMax = sound.source.clip.length;
            if (lastTimePlayed + playerMoveTimerMax < Time.time) {
                soundTimerDictonary[sound.name] = Time.time;
                return true;
            } else {
                return false;
            }
        }
        return true;

    }

    #endregion

    private void AudioNotFound(string name) {
        Debug.LogWarning("The sound with name '" + name + "' could not be found in list. Is it spelled correctly? (NullReferenceException)");
    }

}
