using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioControler : MonoBehaviour
{
    [Header("Player")]
    [SerializeField] private AudioSource _jump;
    [SerializeField] private AudioSource _throw;
    [SerializeField] private AudioSource _damage;

    // Player
    public void Jump()
    {
        PlayAudio(_jump);
    }
    public void Throw()
    {
        PlayAudio(_throw);
    }
    public void Damage()
    {
        PlayAudio(_damage);
    }

    private void PlayAudio(AudioSource source)
    {
        if (source == null) return;
        if(source.isPlaying) source.Stop();
        source.Play();
    }
}
