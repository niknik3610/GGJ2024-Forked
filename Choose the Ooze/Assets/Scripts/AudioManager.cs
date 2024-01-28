using System.Collections;
using UnityEngine.Audio;
using UnityEngine;
using System;

[System.Serializable]
public class Sound
{
    public string name;

    public AudioClip clip;

    [Range(0f, 1f)]
    public float volume;
    [Range(0.1f, 3f)]
    public float pitch;

    public bool loop;

    [HideInInspector]
    public AudioSource source;
}

public class AudioManager : MonoBehaviour
{
    public Sound[] sounds;
    public static AudioManager instance;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
            return;
        }
        DontDestroyOnLoad(gameObject);
        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
        }
    }

    public void Play(string name)
    {
        Debug.Log("playing " + name);
        if (name == "") return;
        Sound s = Array.Find(sounds, sound => sound.name == name);
        s.source.Play();
    }

    void Start()
    {
        StartCoroutine("playThemeLoop");
    }

    public IEnumerator playThemeLoop()
    {

        Debug.Log("Playing theme loop");
        Sound s1 = Array.Find(sounds, sound => sound.name == "theme");
        Sound s2 = Array.Find(sounds, sound => sound.name == "Theme_loop");
        s1.source.Play();
        while (s1.source.isPlaying)
        {
            yield return 0;
        }
        s2.source.Play();
    }
}
