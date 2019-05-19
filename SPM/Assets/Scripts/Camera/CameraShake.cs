﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CameraShake : MonoBehaviour {

    public float shakeValue;
    public float shakeDuration;

    private float shakePercentage;
    private float startValue;
    private float startDuration;

    bool isShaking = false;

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
        shakeValue += value;
        startValue = shakeValue;
        shakeDuration = duration;
        startDuration = shakeDuration;

        if (!isShaking) {
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

    IEnumerator Recoil() {
        isShaking = true;

        while (shakeDuration > 0.01f) {
            Vector3 rotationAmount = new Vector3(-1, 0, 0) * shakeValue;

            shakePercentage = shakeDuration / startDuration;

            shakeValue = startValue * shakePercentage;
            shakeDuration -= 1 * Time.deltaTime;

            if (isSmooth) {//kommer antagligen inte ha smooth på recoil
                //transform.localRotation = Quaternion.Lerp(transform.localRotation, Quaternion.Euler(rotationAmount), Time.deltaTime * 3);
                transform.localRotation = Quaternion.Euler(rotationAmount);
            } else {
                transform.localRotation = Quaternion.Euler(rotationAmount);
            }
            yield return null;
        }
        transform.localRotation = Quaternion.identity;
        isShaking = false;


        
    }

    IEnumerator ShakeCamera() {
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