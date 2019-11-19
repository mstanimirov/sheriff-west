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
    public Sound[] basicSounds;
    public Sound[] animalSounds;
    public Sound[] impactSounds;
    public Sound[] gunshotSounds;
    public static AudioManager instance;

    #region Private Vars

    private List<Sound> allSounds = new List<Sound>();

    #endregion

    void Awake()
    {

        if (instance != null)
            Destroy(gameObject);

        instance = this;

        foreach(Sound s in basicSounds)
        {

            allSounds.Add(s);

        }
        
        foreach (Sound s in animalSounds)
        {

            allSounds.Add(s);

        }

        foreach (Sound s in impactSounds)
        {

            allSounds.Add(s);

        }

        foreach (Sound s in gunshotSounds)
        {

            allSounds.Add(s);

        }

        foreach (Sound s in allSounds)
        {

            s.source = s.parentGroup.gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;

            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;

            s.source.outputAudioMixerGroup = s.mixerGroup;

        }

    }

    private void Start()
    {

        audioMixer.SetFloat("MusicVolume", PlayerPrefs.GetFloat("musicVolume") * 5);
        audioMixer.SetFloat("SoundVolume", PlayerPrefs.GetFloat("soundVolume") * 5);

    }

    public void PlaySound(string name)
    {        

        Sound s = allSounds.Find(sound => sound.name == name);
        if (s != null)
        {

            s.source.Play();

        }

    }

    public void StopSound(string name)
    {

        Sound s = allSounds.Find(sound => sound.name == name);
        if (s != null)
        {

            s.source.Stop();

        }

    }

    public void SetSoundVolume(string name, float volume)
    {

        Sound s = allSounds.Find(sound => sound.name == name);
        if (s != null)
        {

            s.source.volume = volume;

        }

    }

    public bool IsSoundPlaying(string name)
    {

        Sound s = allSounds.Find(sound => sound.name == name);
        if (s != null)
        {

            return s.source.isPlaying;

        }

        return false;

    }

}
