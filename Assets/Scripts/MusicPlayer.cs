using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicPlayer : MonoBehaviour
{
    public AudioClip[] _audioClips;
    private AudioSource _audioSource;

    void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    void Start()
    {
        StartCoroutine(PlayMusic());
    }

    IEnumerator PlayMusic()
    {
        for (int i = 0; i < _audioClips.Length; i++)
        {
            _audioSource.PlayOneShot(_audioClips[i]);
            if (i + 1 == _audioClips.Length)
            {
                i = -1;
            }
            while (_audioSource.isPlaying)
                yield return null;
        }
    }
}
