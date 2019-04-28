using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoBox : MonoBehaviour
{

    public int clipIncrease;
    public float TimeToDestroy = 0.2f;


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            foreach (BaseWeapon weapon in GameController.Instance.playerWeapons)
            {
                weapon.IncreaseTotalAmmoLeft(weapon.GetMaxAmmoInClip() * clipIncrease);
            }
            GameController.Instance.UpdateSelectedWeaponAmmoText();
            GetComponentInParent<PowerUpSpawner>().Respawner();
            StartCoroutine(UsedBoost());
        }
    }


    IEnumerator UsedBoost()
    {
        yield return new WaitForSeconds(TimeToDestroy);
        Destroy(gameObject);
    }

    public void setClipIncrease(int clipIncreaseAmount)
    {
        clipIncrease = clipIncreaseAmount;
    }
}