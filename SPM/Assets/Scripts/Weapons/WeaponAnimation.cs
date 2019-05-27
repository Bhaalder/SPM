using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponAnimation : MonoBehaviour{

    [SerializeField] private GameObject rifle, shotgun, rocketLauncher;


    private void Start()
    {
        rifle = Camera.main.transform.GetChild(0).GetChild(0).gameObject;
        shotgun = Camera.main.transform.GetChild(0).GetChild(1).gameObject;
        rocketLauncher = Camera.main.transform.GetChild(0).GetChild(2).gameObject;
    }

    public void RaiseWeapon() {
        Debug.Log("Penis!");
        rifle.transform.position += new Vector3(0, 0, 0.4f);
    }

    public void ReloadWeapon() {

    }

    public void ShootWeapon(BaseWeapon weapon) {

    }

}
