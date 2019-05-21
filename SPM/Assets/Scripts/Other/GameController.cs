using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour {
    //Author: Patrik Ahlgren
    public List<MonoBehaviour> subscribedScripts = new List<MonoBehaviour>();
    public List<BaseWeapon> playerWeapons = new List<BaseWeapon>();
    public int gameEventID = 1; //detta är till för att markera vissa händelser i spelet
    public bool sceneCompleted;
    public Text winText;

    public GameObject player;

    public BaseWeapon selectedWeapon;

    public Slider HealthSlider, ArmorSlider, SlowmotionSlider, ReloadSlider;
    public Text weaponNameText, weaponAmmoText;
    public GameObject weaponImage;

    public int playerHP, playerArmor;

    public bool gameIsPaused, playerIsInteracting;
    public bool gameIsSlowmotion = false;

    public float invulnerableStateTime;
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

        HealthSlider.value = playerHP;
        ArmorSlider.value = playerArmor;
        SlowmotionSlider.value = SlowmotionSlider.maxValue;
        ReloadSlider.value = 0;

        BaseWeapon rifle = WeaponController.Instance.GetRifle();
        playerWeapons.Add(rifle);
        //BaseWeapon shotgun = WeaponController.Instance.GetShotgun();
        //playerWeapons.Add(shotgun);
        //BaseWeapon rocketLaucher = WeaponController.Instance.GetRocketLauncher();
        //playerWeapons.Add(rocketLaucher);

        selectedWeapon = playerWeapons[0];
        UpdateSelectedWeapon();
    }

    public void UpdateSelectedWeapon() {
        weaponNameText.text = selectedWeapon.GetName();
        foreach (Transform child in weaponImage.transform) {
            child.GetComponent<Image>().enabled = false;
        }
        for (int weapon = 0; weapon < playerWeapons.Count; weapon++) {
            if (playerWeapons[weapon] == selectedWeapon) {
                weaponImage.transform.GetChild(weapon).GetComponent<Image>().enabled = true;
                break;
            }
        }
        crosshair.sprite = selectedWeapon.GetCrosshair();
        UpdateSelectedWeapon_AmmoText();
    }

    public void UpdateSelectedWeapon_AmmoText() {
        weaponAmmoText.text = selectedWeapon.GetAmmoInClip() + "/" + selectedWeapon.GetTotalAmmoLeft();
    }

    public void SceneCompletedSequence() {
        winText.gameObject.SetActive(true);
        sceneCompleted = true;
    }
    public void SceneNotCompletedSequence() {
        winText.gameObject.SetActive(false);
        sceneCompleted = false;
    }

    public void PlayerPassedEvent() {
        gameEventID++;
        Debug.Log("Game event =" + gameEventID);
    }

    public void GamePaused() {
        if (gameIsPaused) {
            gameIsPaused = false;
            if (gameIsSlowmotion) {
                GetComponent<Slowmotion>().SlowTime();
            } else {
                Time.timeScale = 1f;
                Time.fixedDeltaTime = Time.timeScale * 0.02f;
            }
        } else {
            gameIsPaused = true;
            Time.timeScale = 0f;
            Time.fixedDeltaTime = Time.timeScale * 0.02f;
        }
    }

    private void Update() {
        HealthSlider.value = playerHP;
        ArmorSlider.value = playerArmor;
    }

    public void TakeDamage(int damage){
        if (Time.time >= invulnerableState) {
            invulnerableState = Time.time + invulnerableStateTime;
            if (playerArmor <= 0) { playerHP -= damage; Debug.Log("damage has arrived"); } else { playerArmor -= damage; }
        } else {
            Debug.Log("InvulnerableState active, no damage");
        }
    }

    public void Hit(float hitmarkTimer) {
        StopAllCoroutines();
        hitmark.enabled = true;
        StartCoroutine(Hitmark(hitmarkTimer));
    }

    private IEnumerator Hitmark(float hitmarkTimer) {
        yield return new WaitForSecondsRealtime(hitmarkTimer);
        hitmark.enabled = false;
    }
}
