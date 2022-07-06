using System;
using System.Linq;
using System.Text.RegularExpressions;
using GameData.GameDataScripts;
using JetBrains.Annotations;
using UnityEngine.Audio;
using UnityEngine;
using UnityEngine.SceneManagement;
using Random = System.Random;

public class AudioManager : MonoBehaviour
{
    [SerializeField] [Range(0f, 1f)] private float masterVolume = 1f;
    [SerializeField] private GameData.GameDataScripts.GameData data;
    private Sound[] _sounds;
    private Sound[] _musicThemes;
    public static AudioManager instance;
    
    [CanBeNull] private Sound _currentMusicClip;

    public void Awake()
    {
        if (instance == null) {
            instance = this;
        } else {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);

        _sounds = data.Sounds;
        _musicThemes = data.MusicThemes;
        foreach (Sound s in _sounds.Concat(_musicThemes)) {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.volume = masterVolume * s.volume;
            s.source.loop = s.loop;
        }
    }

    public void Start()
    {
        PlaySceneTheme();
    }

    public void PlaySceneTheme()
    {
        string sceneName = SceneManager.GetActiveScene().name;
        PlayMusic(sceneName);
    }

    public void PlaySound(string soundName)
    {
        Sound sound = Array.Find(_sounds, sound => sound.name == soundName);
        if (sound == null) {
            Debug.LogWarning("Sound: " + soundName + " not found!");
            return;
        }
        sound.source.Play();
    }

    public void PlayRandomSound(string soundName)
    {
        Sound[] soundOptions = Array.FindAll(_sounds, sound => Regex.IsMatch(sound.name, soundName + "-[0-9]+"));
        if (soundOptions.Length == 0) {
            Debug.LogWarning("No sounds with name matching " + soundName + " could be found!");
            return;
        }
        int randomIndex = UnityEngine.Random.Range(0, soundOptions.Length - 1);
        Sound randomSound = soundOptions[randomIndex];
        randomSound.source.Play();
    }

    // Starts playing a new music theme is one exists.
    // Otherwise continues playing the current theme.
    public void PlayMusic(string musicName)
    {
        Sound sound = Array.Find(_musicThemes, sound => sound.name == musicName);
        if (sound == null) {
            Debug.Log("Music for this scene not found");
            return;
        }

        if (_currentMusicClip == sound) {
            return;
        }
        
        _currentMusicClip?.source.Stop();
        _currentMusicClip = sound;
        sound.source.Play();
    }

    public void StopMusic()
    {
        _currentMusicClip?.source.Stop();
        _currentMusicClip = null;
    }
}

[Serializable]
public class Sound
{
    public string name;
    public AudioClip clip;
    [Range(0f, 1f)] public float volume = 1f;
    public bool loop;
    [HideInInspector] public AudioSource source;
}
