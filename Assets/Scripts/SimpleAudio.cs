using UnityEngine;

public class SimpleAudio : MonoBehaviour
{
    [Header("Audio Source")]
    public AudioSource audioSource;

    [Header("SFX")]
    public AudioClip sfx1;
    public AudioClip sfx2;
    public AudioClip sfx3;
    public AudioClip sfx4;
    public AudioClip sfx5;

    private void Awake()
    {
        if (audioSource == null)
            audioSource = GetComponent<AudioSource>();
    }

    public void PlaySFX1() => Play(sfx1);
    public void PlaySFX2() => Play(sfx2);
    public void PlaySFX3() => Play(sfx3);
    public void PlaySFX4() => Play(sfx4);
    public void PlaySFX5() => Play(sfx5);

    private void Play(AudioClip clip)
    {
        if (clip == null || audioSource == null) return;
        audioSource.PlayOneShot(clip);
    }
}
