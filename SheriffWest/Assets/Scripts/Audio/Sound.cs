using UnityEngine;
using UnityEngine.Audio;

[System.Serializable]
public class Sound
{

    [Header("General Settings:")]
    public string name;
    public GameObject parentGroup;

    [Header("Audio")]
    public AudioClip clip;
    public AudioMixerGroup mixerGroup;

    [HideInInspector]
    public AudioSource source;

    [Header("Audio Settings")]
    public bool loop;

    [Range(0f, 1f)]
    public float volume;
    [Range(0.1f, 3f)]
    public float pitch;    

}
