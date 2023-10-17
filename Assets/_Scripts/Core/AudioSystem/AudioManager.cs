using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    #region Singleton
    private static AudioManager _instance;
    private void Awake()
    {
        if (_instance)
        {
            Destroy(gameObject);
        }
        _instance = this;
    }

    public static AudioManager Instance => _instance;
    #endregion

    [SerializeField] private AudioSource _musicSourceA;
    [SerializeField] private AudioSource _musicSourceB;
    [SerializeField] private AudioSource _sfxSource;
    [SerializeField] private float _fadeTime = 1f;
    private AudioSource _currentSource;
    private bool _isFading;

    private void Start()
    {
        if (ReferenceEquals(_currentSource, null))
        {
            _currentSource = _musicSourceB;
        }
        _isFading = false;
    }

    public void PlayMusic(AudioClip clip)
    {
        if (_isFading) return;
        AudioSource newSource = _currentSource == _musicSourceA ? _musicSourceB : _musicSourceA;
        StartCoroutine(CrossFade(_currentSource, newSource, clip));
    }

    public void PlaySFX(AudioClip clip)
    {
        _sfxSource.PlayOneShot(clip);
    }

    private IEnumerator CrossFade(AudioSource oldSource, AudioSource newSource, AudioClip clip)
    {
        _isFading = true;
        oldSource.volume = 1f;
        newSource.volume = 0f;

        newSource.clip = clip;
        newSource.Play();

        while(oldSource.volume != 0)
        {
            oldSource.volume -= Time.fixedDeltaTime * _fadeTime;
            newSource.volume += Time.fixedDeltaTime * _fadeTime;

            yield return new WaitForFixedUpdate();
        }

        oldSource.volume = 0f;
        newSource.volume = 1f;
        _currentSource = newSource;

        _isFading = false;
    }
}
