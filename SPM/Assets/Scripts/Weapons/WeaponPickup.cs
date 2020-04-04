using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponPickup : MonoBehaviour {
    //Author: Patrik Ahlgren

    [SerializeField] private string weaponName;
    [SerializeField] private Animator anim;
    
    PopUpTrigger triggerScript;
    

    void Start()
    {
        anim = GameObject.Find("WeaponText").GetComponent<Animator>();

        if (TutorialController.Instance.isTutorialTypePopUp)
        {
            triggerScript = GameObject.Find("PopUpTrigger").GetComponent<PopUpTrigger>();
        }
        
    }

    public void GetWeapon() {
        BaseWeapon weaponPickup = null;
        anim.SetTrigger("WeaponPickUp");
        if (weaponName == "Rifle") {
            weaponPickup = WeaponController.Instance.GetRifle();
        }
        if (weaponName == "Shotgun") {
            Debug.Log("SHOTGUN PICKUP");
            weaponPickup = WeaponController.Instance.GetShotgun();
            if (!TutorialController.Instance.isTutorialTypePopUp)
            {
                StartCoroutine(TextShowDelay("SHOTGUN INFO"));
            }
            else
            {
                triggerScript.PopUpMethod("THE SHOTGUN", "Switch to Shotgun by clicking '2'\nThe shotgun is good against big enemies");
            }

        }
        if (weaponName == "RocketLauncher") {
            weaponPickup = WeaponController.Instance.GetRocketLauncher();
            if (!TutorialController.Instance.isTutorialTypePopUp)
            {
                StartCoroutine(TextShowDelay("ROCKET INFO"));
            }
            else
            {
                triggerScript.PopUpMethod("THE ROCKET LAUNCHER","Switch to Launcher by clicking '3'\n The Launcher is good vs clumped up enemies");
            }

        }
        if (weaponPickup != null) {
            foreach (BaseWeapon weapon in GameController.Instance.PlayerWeapons) {
                if (weaponPickup.GetName() == weapon.GetName()) {
                    Destroy(gameObject);
                    return;
                }
            }
            GameController.Instance.PlayerWeapons.Add(weaponPickup);
            GameController.Instance.Player.GetComponent<PlayerInput>().AbortReload();
            GameController.Instance.Player.GetComponent<PlayerInput>().SwitchWeaponAnimation(weaponPickup);
            GameController.Instance.SelectedWeapon = weaponPickup;
            GameController.Instance.UpdateSelectedWeapon();
        }
        Destroy(gameObject);
    }

    private IEnumerator TextShowDelay(string text)
    {
        Debug.Log("num started");
        TutorialController.Instance.TipMethod(text);
        yield return new WaitForSeconds(1);
        
        
        Debug.Log("weaponText identified");
        //yield return null;
        

        

    }
}
