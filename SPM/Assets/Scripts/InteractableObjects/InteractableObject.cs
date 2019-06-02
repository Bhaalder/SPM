using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableObject : MonoBehaviour
{
    //Author: Patrik Ahlgren
    [SerializeField] private enum Object {OpenDoorsAnimated, ArenaButton, ArenaButtonLV2, WeaponPickup, ElevatorTrigger};
    [SerializeField] private Object obj;

    public void Interact() {
        switch (obj) {
            case (Object.OpenDoorsAnimated):
                GetComponent<OpenDoorsAnimated>().OpenAndClose();
                break;
            case (Object.ArenaButton):
                GetComponent<ArenaButton>().PressButton();
                break;
            case (Object.ArenaButtonLV2):
                GetComponent<ArenaButton_L2>().PressButton();
                break;
            case (Object.WeaponPickup):
                GetComponent<WeaponPickup>().GetWeapon();
                break;
            case (Object.ElevatorTrigger):
                Debug.Log("Nu åker hissen!");
                break;
            default:
                Debug.LogWarning("Hittade inte det önskade objektet");
                break;
        }
    }
}
