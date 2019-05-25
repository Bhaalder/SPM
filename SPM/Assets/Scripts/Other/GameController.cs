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
    public Text WinText;

    public GameObject Player;

    public BaseWeapon SelectedWeapon;

    public Slider HealthSlider, ArmorSlider, SlowmotionSlider, ReloadSlider;
    public Text WeaponNameText, WeaponAmmoText;
    public GameObject WeaponImage;

    public int PlayerHP, PlayerArmor;

    public bool GameIsPaused, PlayerIsInteracting;
    public bool GameIsSlowmotion = false;

    [SerializeField] private float invulnerableStateTime;
    private float invulnerableState;

    public Text interactionText;
    [SerializeField] private Image crosshair, hitmark;
    private static GameController instance;

    public static GameController Instance {
        get {
            if (instance == null) {
                instance = FindObjectOfType<GameController>();
#if UNITY_EDITOR
                if (FindObjectsOfType<GameController>().Length > 1) {
                    Debug.LogError("Found more than one gamecontroller");
                }
#endif
            }
            return instance;
        }
    }

    private void Start() {
        if (instance != null && instance != this) {
            Destroy(gameObject);
            Debug.LogWarning("Destroyed other Singleton with name: " + gameObject.name);
        }
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        DontDestroyOnLoad(gameObject);

        HealthSlider.value = PlayerHP;
        ArmorSlider.value = PlayerArmor;
        SlowmotionSlider.value = SlowmotionSlider.maxValue;
        ReloadSlider.value = 0;

        WeaponNameText = GameObject.Find("WeaponText").GetComponent<Text>();
        WeaponAmmoText = GameObject.Find("AmmunitionText").GetComponent<Text>();
        WeaponImage = GameObject.Find("Weapon Image");

        BaseWeapon rifle = WeaponController.Instance.GetRifle();
        PlayerWeapons.Add(rifle);
        //BaseWeapon shotgun = WeaponController.Instance.GetShotgun();
        //PlayerWeapons.Add(shotgun);
        //BaseWeapon rocketLaucher = WeaponController.Instance.GetRocketLauncher();
        //PlayerWeapons.Add(rocketLaucher);

        SelectedWeapon = PlayerWeapons[0];
        UpdateSelectedWeapon();
    }

    public void UpdateSelectedWeapon() {
        WeaponNameText.text = SelectedWeapon.GetName();
        foreach (Transform child in WeaponImage.transform) {
            child.GetComponent<Image>().enabled = false;
        }
        for (int weapon = 0; weapon < PlayerWeapons.Count; weapon++) {
            if (PlayerWeapons[weapon] == SelectedWeapon) {
                WeaponImage.transform.GetChild(weapon).GetComponent<Image>().enabled = true;
                break;
            }
        }
        crosshair.sprite = SelectedWeapon.GetCrosshair();
        UpdateSelectedWeapon_AmmoText();
    }

    public void UpdateSelectedWeapon_AmmoText() {
        WeaponAmmoText.text = SelectedWeapon.GetAmmoInClip() + "/" + SelectedWeapon.GetTotalAmmoLeft();
    }

    public void SceneCompletedSequence() {
        WinText.gameObject.SetActive(true);
        SceneCompleted = true;
    }
    public void SceneNotCompletedSequence() {
        WinText.gameObject.SetActive(false);
        SceneCompleted = false;
    }

    public void PlayerPassedEvent() {
        GameEventID++;
        Debug.Log("Game event =" + GameEventID);
    }

    public void GamePaused() {
        if (GameIsPaused) {
            GameIsPaused = false;
            if (GameIsSlowmotion) {
                GetComponent<Slowmotion>().SlowTime();
            } else {
                Time.timeScale = 1f;
                Time.fixedDeltaTime = Time.timeScale * 0.02f;
            }
        } else {
            GameIsPaused = true;
            Time.timeScale = 0f;
            Time.fixedDeltaTime = Time.timeScale * 0.02f;
        }
    }

    private void Update() {
        HealthSlider.value = PlayerHP;
        ArmorSlider.value = PlayerArmor;
    }

    public void TakeDamage(int damage){
        if (Time.time >= invulnerableState) {
            invulnerableState = Time.time + invulnerableStateTime;
            if (PlayerArmor <= 0) { PlayerHP -= damage; Debug.Log("Player took: "+damage + " to health"); } else { PlayerArmor -= damage; }
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
}
