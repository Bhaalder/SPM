using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CameraShake : MonoBehaviour {
    //Author: Patrik Ahlgren
    public float shakeValue;
    public float shakeDuration;

    private float shakePercentage;
    private float startValue;
    private float startDuration;

    bool isShaking = false;

    public float recoilValue;//
    public float recoilDuration;

    private float recoilPercentage;
    private float startRecoilValue;
    private float startRecoilDuration;

    bool isRecoiling = false;//

    public bool isSmooth;
    public float smoothValue = 3f;

    void Shake() {

        startValue = shakeValue;
        startDuration = shakeDuration;

        if (!isShaking) {
            StartCoroutine(ShakeCamera());
        }
    }

    public void Shake(float value, float duration) {

        shakeValue += value;
        startValue = shakeValue;
        shakeDuration = duration;
        startDuration = shakeDuration;

        if (!isShaking) {
            StartCoroutine(ShakeCamera());
        }
    }

    public void RecoilShake(float value, float duration) {
        recoilValue += value;
        startRecoilValue = recoilValue;
        recoilDuration = duration;
        startRecoilDuration = recoilDuration;

        if (!isRecoiling) {
            StartCoroutine(Recoil());
        }          
    }

    public void ShakeIncrease(float value, float duration) {
        shakeValue += value;
        startValue = shakeValue;
        shakeDuration = duration;
        startDuration = shakeDuration;

        if (!isShaking) {
            StartCoroutine(ShakeCamera());
        }       
    }

    public void ShakeIncreaseDistance(float value, float duration, GameObject player, GameObject explosion) {
        shakeValue += value;
        startValue = shakeValue;
        shakeDuration = duration;
        startDuration = shakeDuration;

        float distance = Vector3.Distance(player.transform.position, explosion.transform.position);

        if (!isShaking) {
            StartCoroutine(ShakeCameraDistance(distance));
        }
    }

    private IEnumerator Recoil() {
        isRecoiling = true;

        while (recoilDuration > 0.01f) {
            Vector3 rotationAmount = new Vector3(-1, 0, 0) * recoilValue;

            recoilPercentage = recoilDuration / startRecoilDuration;

            recoilValue = startRecoilValue * recoilPercentage;
            recoilDuration -= 1 * Time.deltaTime;

            if (isSmooth) {//kommer antagligen inte ha smooth på recoil
                //transform.localRotation = Quaternion.Lerp(transform.localRotation, Quaternion.Euler(rotationAmount), Time.deltaTime * 4);
                transform.localRotation = Quaternion.Euler(rotationAmount);
            } else {
                transform.localRotation = Quaternion.Euler(rotationAmount);
            }
            yield return null;
        }
        transform.localRotation = Quaternion.identity;
        isRecoiling = false;      
    }

    private IEnumerator ShakeCameraDistance(float distance) {
        isShaking = true;
        float shaking = 0;

        while (shakeDuration > 0.01f) {
            if (shakeValue-(distance*0.65) <= 1) {
                shaking = 1;
            } else {
                shaking = shakeValue - distance;
            }
            Vector3 rotationAmount = Random.insideUnitSphere * shaking;
            rotationAmount.z = 0;//

            shakePercentage = shakeDuration / startDuration;

            shakeValue = startValue * shakePercentage;
            //shakeDuration = Mathf.Lerp(shakeDuration, 0, Time.deltaTime);//lerpa eller inte?
            shakeDuration -= 1 * Time.deltaTime;

            if (isSmooth) {
                transform.localRotation = Quaternion.Lerp(transform.localRotation, Quaternion.Euler(rotationAmount), Time.deltaTime * smoothValue);
            } else {
                transform.localRotation = Quaternion.Euler(rotationAmount);
            }

            yield return null;
        }
        transform.localRotation = Quaternion.identity;
        isShaking = false;
    }

    private IEnumerator ShakeCamera() {
        isShaking = true;

        while (shakeDuration > 0.01f) {
            Vector3 rotationAmount = Random.insideUnitSphere * shakeValue;
            rotationAmount.z = 0;//

            shakePercentage = shakeDuration / startDuration;

            shakeValue = startValue * shakePercentage;
            //shakeDuration = Mathf.Lerp(shakeDuration, 0, Time.deltaTime);//lerpa eller inte?
            shakeDuration -= 1* Time.deltaTime;

            if (isSmooth) {
                transform.localRotation = Quaternion.Lerp(transform.localRotation, Quaternion.Euler(rotationAmount), Time.deltaTime * smoothValue);
            } else {
                transform.localRotation = Quaternion.Euler(rotationAmount);
            }

            yield return null;
        }
        transform.localRotation = Quaternion.identity;
        isShaking = false;
    }
}