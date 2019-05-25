using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoBox : MonoBehaviour
{
    //Author: Patrik Ahlgren
    [SerializeField] private int clipIncrease;


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "InteractionPlayer")
        {
            foreach (BaseWeapon weapon in GameController.Instance.PlayerWeapons)
            {
                weapon.IncreaseTotalAmmoLeft(weapon.GetMaxAmmoInClip() * clipIncrease);
            }
            GameController.Instance.UpdateSelectedWeapon_AmmoText();
            GetComponentInParent<PowerUpSpawner>().Respawner();
        }
        Destroy(gameObject);
    }
    
    public void setClipIncrease(int clipIncreaseAmount)
    {
        clipIncrease = clipIncreaseAmount;
    }
}