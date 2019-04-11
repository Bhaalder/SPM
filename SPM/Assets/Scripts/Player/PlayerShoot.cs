using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerShoot : MonoBehaviour
{
    //public LayerMask layerMask;
    public float damage;
    public Camera cam;
    public Text ammoText;

    [SerializeField] private Transform camFocusTrans;

    private readonly float shootRange = 100f;
    private readonly float automaticFireRate = 0.1f;
    private readonly float reloadTime = 5.0f;
    private IEnumerator reload;
    private IEnumerator courotineAutomaticFire;

    private void Start()
    {
        courotineAutomaticFire = AutomaticFire(automaticFireRate);
        reload = ReloadTimer(reloadTime);
        UpdateAmmoText();
    }

    void Update()
    {
        ShootInput();
        ReloadInput();
        transform.LookAt(camFocusTrans);
    }

    string UpdateAmmoText()
    {
        return ammoText.text = "Ammo: " + GameController.Instance.playerAmmunition.ToString();
    }

    void ReloadInput()
    { //funkar inte som det ska, tar ingen tid att ladda om
        if (Input.GetKey(KeyCode.R) && GameController.Instance.playerAmmunition != 50)
        {
            StartCoroutine(reload);
        }
        else
        {
            StopCoroutine(reload);
        }
    }

    void ShootInput()
    {
        if (Input.GetMouseButtonDown(0))
        {
            StartCoroutine(courotineAutomaticFire);
        }
        else if (Input.GetMouseButtonUp(0))
        {
            StopCoroutine(courotineAutomaticFire);
        }
    }

    IEnumerator ReloadTimer(float timeToReload)
    { //funkar inte som det ska, tar ingen tid att ladda om
        while (true)
        {
            yield return new WaitForSeconds(timeToReload);
            GameController.Instance.playerAmmunition = 50;
            UpdateAmmoText();
        }
    }

    IEnumerator AutomaticFire(float timeBetweenShots)
    {
        while (true)
        {
            yield return new WaitForSeconds(timeBetweenShots);
            Shot();
        }
    }

    void Shot()
    {
        if (GameController.Instance.playerAmmunition != 0)
        {
            GameController.Instance.playerAmmunition--;
            bool hitTarget = Physics.Raycast(transform.position, transform.forward, out RaycastHit hit, shootRange);
            if (hitTarget)
            {
                Debug.Log("Hit " + hit.transform.name);
                if (hit.collider.gameObject.layer == 9) { hit.transform.GetComponent<EnemyController>().TakeDamage(damage); }
            }
            if (!hitTarget)
            {
                Debug.Log("Miss!");
            }
        }
        else if (GameController.Instance.playerAmmunition <= 0)
        {
            Debug.Log("Out of Ammo");
        }
        UpdateAmmoText();
    }
}