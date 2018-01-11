using UnityEngine;

public class BackgroundMusic : MonoBehaviour
{
    public AudioClip StartBgm;
    public AudioClip PlayBgm;
    public AudioClip GameOverBgm;

    private AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void PlayStart()
    {
        Switch(StartBgm);
    }

    public void PlayRun()
    {
        Switch(PlayBgm);
    }

    public void PlayGameOver()
    {
        Switch(GameOverBgm);
    }

    public void SetVolume(float value)
    {
        audioSource.volume = value;
    }

    private void Switch(AudioClip clip)
    {
        audioSource.clip = clip;
        audioSource.Play();
    }
}
