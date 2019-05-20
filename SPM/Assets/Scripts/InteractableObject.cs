using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableObject : MonoBehaviour
{
    
    public enum Object {OpenDoorsAnimated, ArenaButton, ArenaButtonLV2};
    public Object obj;

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
            default:
                Debug.LogWarning("Hittade inte det önskade objektet");
                break;
        }
    }
}
