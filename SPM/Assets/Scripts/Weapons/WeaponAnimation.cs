using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponAnimation : MonoBehaviour{

    [SerializeField] private GameObject rifle, shotgun, rocketLauncher;
    private string weaponName;

    private void Start()
    {
        rifle = Camera.main.transform.GetChild(0).GetChild(0).gameObject;
        shotgun = Camera.main.transform.GetChild(0).GetChild(1).gameObject;
        rocketLauncher = Camera.main.transform.GetChild(0).GetChild(2).gameObject;
    }

    public void RaiseWeapon(BaseWeapon weapon) {
        weaponName = "";
        switch(weaponName){
            case "Rifle":
                break;
            case "Shotgun":
                break;
            case "Rocket Launcher":
                break;
        }

        Debug.Log("Raising "+ weaponName + "!");
    }

    public void LowerWeapon() {
        Debug.Log("Lowering");
    }

    public void ReloadWeapon() {

    }

    public void ShootWeapon(BaseWeapon weapon) {

    }

}
