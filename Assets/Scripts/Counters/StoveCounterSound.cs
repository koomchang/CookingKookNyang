using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoveCounterSound : MonoBehaviour
{   
    [SerializeField] private StoveCounter stoveCounter;
    private AudioSource audioSource;
    private float warningSoundTimer;
    private bool playWarningSound;

    private void Awake() {
        audioSource = GetComponent<AudioSource>();
    }

    private void Start() {
        stoveCounter.OnStateChanged += stoveCounter_OnStateChanged;
        stoveCounter.OnProgressChanged += stoveCounter_OnProgressChanged;
    }

    private void stoveCounter_OnProgressChanged(object sender, IHasProgress.OnProgressChangedEventArgs e) {
        float burnShowProgressAmount = 0.5f;
        playWarningSound = stoveCounter.IsFried() && e.progressNormalized >= burnShowProgressAmount;
    }

    private void stoveCounter_OnStateChanged(object sender, StoveCounter.OnStateChangedEventArgs e) {
        // 굽기 시작하거나, 알맞게 익었을 때에만 사운드 플레이
        bool playSound = e.state == StoveCounter.State.Frying || e.state == StoveCounter.State.Fried;
        if (playSound) {
            audioSource.Play();
        } else { // 만약 타버린다면 사운드 중지
            audioSource.Pause();
        }
    }

    private void Update() {
        if (playWarningSound) {
            warningSoundTimer -= Time.deltaTime;
            if (warningSoundTimer < 0f) {
                float warningSoundTimerMax = 0.2f;
                warningSoundTimer = warningSoundTimerMax;
                
                SoundManager.Instance.PlayWarningSound(stoveCounter.transform.position);
            }
        }
    }
}
