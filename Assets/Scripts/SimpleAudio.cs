using UnityEngine;

public class SimpleAudio : MonoBehaviour
{
    [Header("Audio Sources")]
    public AudioSource bgSource;  
    public AudioSource sfxSource; 

    [Header("Background Music")]
    public AudioClip bgMusic;

    [Header("SFX")]
    public AudioClip sfx1;
    public AudioClip sfx2;
    public AudioClip sfx3;
    public AudioClip sfx4;
    public AudioClip sfx5;

    private void Awake()
    {
        if (bgSource != null)
        {
            bgSource.loop = true;
            bgSource.clip = bgMusic;
            bgSource.Play();
        }
    }

    public void PlaySFX1() => PlaySFX(sfx1);
    public void PlaySFX2() => PlaySFX(sfx2);
    public void PlaySFX3() => PlaySFX(sfx3);
    public void PlaySFX4() => PlaySFX(sfx4);
    public void PlaySFX5() => PlaySFX(sfx5);

    private void PlaySFX(AudioClip clip)
    {
        if (clip == null || sfxSource == null) return;
        sfxSource.PlayOneShot(clip, 2f);
    }
}
