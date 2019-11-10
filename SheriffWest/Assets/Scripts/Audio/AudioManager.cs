using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{

    [Header("General Settings:")]
    public AudioMixer audioMixer;
    public AudioMixerGroup musicMixer;
    public AudioMixerGroup soundMixer;

    [Header("Sound Settings:")]
    public Sound[] sounds;
    public static AudioManager instance;

    void Awake()
    {

        if (instance != null)
            Destroy(gameObject);

        instance = this;

        foreach (Sound s in sounds)
        {

            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;

            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;

            if (s.isSound)
                s.source.outputAudioMixerGroup = soundMixer;
            else
                s.source.outputAudioMixerGroup = musicMixer;

        }

    }

    private void Start()
    {

        audioMixer.SetFloat("MusicVolume", PlayerPrefs.GetFloat("musicVolume") * 5);
        audioMixer.SetFloat("SoundVolume", PlayerPrefs.GetFloat("soundVolume") * 5);

    }

    public void PlaySound(string name)
    {

        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s != null)
        {

            s.source.Play();

        }

    }

    public void StopSound(string name)
    {

        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s != null)
        {

            s.source.Stop();

        }

    }

    public void SetSoundVolume(string name, float volume)
    {

        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s != null)
        {

            s.source.volume = volume;

        }

    }

    public bool IsSoundPlaying(string name)
    {

        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s != null)
        {

            return s.source.isPlaying;

        }

        return false;

    }

}
