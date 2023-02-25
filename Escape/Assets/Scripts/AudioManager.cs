using UnityEngine.Audio;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public class AudioManager : MonoBehaviour{
    public Sound[] sounds;
    public static AudioManager Instance;


    void Awake(){
        if(Instance == null){
            Instance = this;
        }else{
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);
        foreach(Sound s in sounds){
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
        }
    }

    void Start(){
        foreach(Sound s in sounds){
            if(s.playOnAwake){
                Play(s.name);
            }
        }
    }

    public void ChangeVolume(float volume){
        foreach(Sound s in sounds){
            s.source.volume = s.volume * volume;
        }
    }


    public void Play(string name){
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if(s == null){
            Debug.LogWarning("Sound " + name + " not found");
            return;
        }
        if(name == "Thrust"){
            s.source.time = 0.2f;
        }
        s.source.Play();
    }
    public void Stop(string name){
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if(s == null){
            Debug.LogWarning("Sound " + name + " not found");
            return;
        }
        s.source.Stop();
    }

    void Update(){
        if(SceneManager.GetActiveScene().buildIndex == 0){
            foreach(Sound s in sounds){
                Stop(s.name);
            }
        }
    }
}

[System.Serializable]
public class Sound {
    public string name;
    public AudioClip clip;

    [Range(0, 1)]
    public float volume;
    [Range(0.1f, 3)]
    public float pitch;

    public bool loop;
    public bool playOnAwake;

    [HideInInspector]
    public AudioSource source;
}
