using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour {
    //Author: Patrik Ahlgren
    public List<MonoBehaviour> SubscribedScripts = new List<MonoBehaviour>();
    public List<BaseWeapon> PlayerWeapons = new List<BaseWeapon>();
    public int GameEventID = 1;
    public bool SceneCompleted;

    public GameObject Player;

    public BaseWeapon SelectedWeapon;

    public Slider HealthSlider, ArmorSlider, SlowmotionSlider, ReloadSlider;
    [SerializeField] private Text weaponNameText, weaponAmmoText;
    [SerializeField] private GameObject weaponImage;

    public Text TipText;
    public float PlayerHP, PlayerArmor;

    public bool PlayerIsInteracting;
    public bool GameIsPaused;
    public bool GameIsSlowmotion = false;
    public bool PauseAudio;

    [SerializeField] private float invulnerableStateTime;
    private float invulnerableState;

    [SerializeField] private Text interactionText;
    [SerializeField] private Image crosshair, hitmark;

    public int KillCount { get; set; }

    private static GameController _instance;

    public static GameController Instance {
        get {
            if (_instance == null) {
                _instance = FindObjectOfType<GameController>();
#if UNITY_EDITOR
                if (FindObjectsOfType<GameController>().Length > 1) {
                    Debug.LogError("Found more than one gamecontroller");
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

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        DontDestroyOnLoad(gameObject);
        //FindObjectOfType<DataStorage>().LoadPlayerData();

        PlayerHP = 100;
        PlayerArmor = 100;
        HealthSlider.value = PlayerHP;
        ArmorSlider.value = PlayerArmor;
        SlowmotionSlider.value = SlowmotionSlider.maxValue;
        ReloadSlider.value = 0;

        TipText = GameObject.Find("TipText").GetComponent<Text>();
        TipText.color = new Color(TipText.color.r, TipText.color.g, TipText.color.b, 0);
        weaponNameText = GameObject.Find("WeaponText").GetComponent<Text>();
        weaponAmmoText = GameObject.Find("AmmunitionText").GetComponent<Text>();
        weaponImage = GameObject.Find("Weapon Image");

        BaseWeapon rifle = WeaponController.Instance.GetRifle();
        PlayerWeapons.Add(rifle);
        BaseWeapon shotgun = WeaponController.Instance.GetShotgun();
        PlayerWeapons.Add(shotgun);
        BaseWeapon rocketLaucher = WeaponController.Instance.GetRocketLauncher();
        PlayerWeapons.Add(rocketLaucher);

        SelectedWeapon = PlayerWeapons[0];
        UpdateSelectedWeapon();
    }

    public void UpdateSelectedWeapon() {
        weaponNameText.text = SelectedWeapon.GetName();
        foreach (Transform child in weaponImage.transform) {
            child.GetComponent<Image>().enabled = false;
        }
        for (int weapon = 0; weapon < PlayerWeapons.Count; weapon++) {
            if (PlayerWeapons[weapon] == SelectedWeapon) {
                weaponImage.transform.GetChild(weapon).GetComponent<Image>().enabled = true;
                break;
            }
        }
        switch (SelectedWeapon.GetName()) {
            case "Rifle":
                crosshair.sprite = WeaponController.Instance.Crosshair[0];
                break;
            case "Shotgun":
                crosshair.sprite = WeaponController.Instance.Crosshair[1];
                break;
            case "Rocket Launcher":
                crosshair.sprite = WeaponController.Instance.Crosshair[2];
                break;
            default:
                crosshair.sprite = WeaponController.Instance.Crosshair[0];
                break;
        }
        UpdateSelectedWeapon_AmmoText();
    }

    public void UpdateSelectedWeapon_AmmoText() {
        if (SelectedWeapon.GetTotalAmmoLeft() == 0 && SelectedWeapon.GetAmmoInClip() == 0) {
            weaponAmmoText.color = Color.red;
        } else if (SelectedWeapon.GetTotalAmmoLeft() <= SelectedWeapon.GetMaxAmmoInClip()) {
            weaponAmmoText.color = Color.yellow;
        } else {
            weaponAmmoText.color = Color.white;
        }
        weaponAmmoText.text = SelectedWeapon.GetAmmoInClip() + "/" + SelectedWeapon.GetTotalAmmoLeft();
    }

    public void SceneCompletedSequence(bool b) {
        SceneCompleted = b;
    }

    public void PlayerPassedEvent() {
        GameEventID++;
        Debug.Log("Game event =" + GameEventID);
    }

    public void GamePaused() {
        if (GameIsPaused) {
            GameIsPaused = false;
            if (PauseAudio) {
                AudioController.Instance.PauseAllSound(false);
                PauseAudio = false;
            }           
            if (GameIsSlowmotion) {
                GetComponent<Slowmotion>().SlowTime();
            } else {
                Time.timeScale = 1f;
                Time.fixedDeltaTime = Time.timeScale * 0.02f;
            }
        } else {
            GameIsPaused = true;
            if (PauseAudio) {
                AudioController.Instance.PauseAllSound(true);
            }
            Time.timeScale = 0f;
            Time.fixedDeltaTime = Time.timeScale * 0.02f;
        }
    }

    private void Update() {
        HealthSlider.value = PlayerHP;
        ArmorSlider.value = PlayerArmor;
    }

    public void TakeDamage(float damage){
        if (Time.time >= invulnerableState) {
            invulnerableState = Time.time + invulnerableStateTime;
            if (PlayerArmor <= 0) {
                PlayerHP -= damage;
                Debug.Log("Player took: "+damage + " to health");
                GetComponent<BloodyScreenScript>().ShowHurtScreen("Health");
                if(damage > PlayerHP) {
                    AudioController.Instance.PlayRandomSFX("Die1", "Die2", "Die3");
                } else {
                    AudioController.Instance.PlayRandomSFX("Hurt1", "Hurt2", "Hurt3");
                }            
            } else {
                PlayerArmor -= damage;
                GetComponent<BloodyScreenScript>().ShowHurtScreen("Armor");

            }
        } else {
            Debug.Log("InvulnerableState active, no damage");
        }
    }

    public void ShowHitmark(float hitmarkTimer) {
        StopAllCoroutines();
        hitmark.enabled = true;
        StartCoroutine(Hitmark(hitmarkTimer));
    }

    private IEnumerator Hitmark(float hitmarkTimer) {
        yield return new WaitForSecondsRealtime(hitmarkTimer);
        hitmark.enabled = false;
    }

    public void SavePlayerData()
    {
        SaveSystem.SavePlayer(this);
    }
}
