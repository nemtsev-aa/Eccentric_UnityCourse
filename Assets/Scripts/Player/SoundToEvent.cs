using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundToEvent : MonoBehaviour
{
    [Tooltip("Звук - шаги")]
    [SerializeField] private AudioClip _runStepsSound;
    [Tooltip("Звук - атака")]
    [SerializeField] private AudioClip _attackSound;

    private AudioSource _audioSource;
    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    public void StepSoundPlay()
    {
        _audioSource.PlayOneShot(_runStepsSound);
    }

    public void StepSoundStop()
    {
        _audioSource.Stop();
    }

    public void AttackSoundPlay()
    {
        _audioSource.PlayOneShot(_attackSound);
    }
}
