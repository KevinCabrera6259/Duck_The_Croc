using UnityEngine;

public class LevelAudioManager : MonoBehaviour
{
    [Header("River Sound")]
    [SerializeField] private AudioSource riverSource;
    [SerializeField] private AudioClip riverClip;

    void Start()
    {
        PlayRiver();
    }

    public void PlayRiver()
    {
        if (riverSource == null || riverClip == null) return;

        riverSource.clip = riverClip;
        riverSource.loop = true;
        riverSource.Play();
    }

    public void StopRiver()
    {
        if (riverSource != null && riverSource.isPlaying)
            riverSource.Stop();
    }

    public void PauseRiver()
    {
        if (riverSource != null && riverSource.isPlaying)
            riverSource.Pause();
    }

    public void ResumeRiver()
    {
        if (riverSource != null)
            riverSource.UnPause();
    }
}
