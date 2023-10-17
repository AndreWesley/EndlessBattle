using UnityEngine;

public class AudioProxy : MonoBehaviour
{
    [SerializeField] private AudioClip _sceneMusic;

    private void Start()
    {
        if (_sceneMusic)
        {
            PlayMusic(_sceneMusic);
        }
    }

    public void PlayMusic(AudioClip clip)
    {
        AudioManager.Instance.PlayMusic(clip);
    }

    public void PlaySFX(AudioClip clip)
    {
        AudioManager.Instance.PlaySFX(clip);
    }
}