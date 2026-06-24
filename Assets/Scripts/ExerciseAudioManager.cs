using UnityEngine;

public class ExerciseAudioManager : MonoBehaviour
{
    public static ExerciseAudioManager instance;

    [Header("Audio Source")]
    public AudioSource audioSource;

    [Header("Sounds")]
    public AudioClip stepCompleteSound;
    public AudioClip successSound;
    public AudioClip errorSound;
    public AudioClip hintSound;

    private void Awake()
    {
        instance = this;

        if (audioSource == null)
            audioSource = GetComponent<AudioSource>();
    }

    public void PlayStepComplete()
    {
        PlaySound(stepCompleteSound);
    }

    public void PlaySuccess()
    {
        PlaySound(successSound);
    }

    public void PlayError()
    {
        PlaySound(errorSound);
    }

    public void PlayHint()
    {
        PlaySound(hintSound);
    }

    private void PlaySound(AudioClip clip)
    {
        if (audioSource == null || clip == null)
            return;

        audioSource.PlayOneShot(clip);
    }
}