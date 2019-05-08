using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoBox : MonoBehaviour
{
    //Author: Patrik Ahlgren
    public int clipIncrease;


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "InteractionPlayer")
        {
            foreach (BaseWeapon weapon in GameController.Instance.playerWeapons)
            {
                weapon.IncreaseTotalAmmoLeft(weapon.GetMaxAmmoInClip() * clipIncrease);
            }
            GameController.Instance.UpdateSelectedWeaponAmmoText();
            GetComponentInParent<PowerUpSpawner>().Respawner();
        }
        Destroy(gameObject);
    }
    
    public void setClipIncrease(int clipIncreaseAmount)
    {
        clipIncrease = clipIncreaseAmount;
    }
}