using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioControler : MonoBehaviour
{
    [Header("Player")]
    [SerializeField] private AudioSource _jump;
    [SerializeField] private AudioSource _land;
    [SerializeField] private AudioSource _throw;
    [SerializeField] private AudioSource _damage;

    [Header("Collection")]
    [SerializeField] private AudioSource _cherry;
    [SerializeField] private AudioSource _gem;

    // Player
    public void Jump()
    {
        PlayAudio(_jump);
    }
    public void Land()
    {
        PlayAudio(_land);
        Debug.Log("Land Methode Called with " + _land.name);
    }
    public void Throw()
    {
        PlayAudio(_throw);
    }
    public void Damage()
    {
        PlayAudio(_damage);
    }

    // Collection System
    public void Cherry()
    {
        PlayAudio(_cherry);
    }
    public void Gem()
    {
        PlayAudio(_gem);
    }

    private void PlayAudio(AudioSource source)
    {
        if (source == null) return;
        if(source.isPlaying) source.Stop();
        source.Play();
    }
}
