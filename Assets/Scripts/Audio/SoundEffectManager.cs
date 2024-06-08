using UnityEngine;
using System.Collections.Generic;

public class SoundEffectManager : MonoBehaviour
{
    public static SoundEffectManager instance;

    [System.Serializable]
    public class Sound
    {
        public string name;
        public AudioClip clip;
    }

    public Sound[] sounds;

    private Dictionary<string, AudioClip> _soundDictionary;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            InitializeSounds();
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void InitializeSounds()
    {
        _soundDictionary = new Dictionary<string, AudioClip>();
        foreach (Sound sound in sounds)
        {
            _soundDictionary.Add(sound.name, sound.clip);
        }
    }

    public void PlaySound(string soundName, Vector3 position)
    {
        if (_soundDictionary.ContainsKey(soundName))
        {
            AudioSource.PlayClipAtPoint(_soundDictionary[soundName], position);
        }
        else
        {
            Debug.LogWarning("Sound " + soundName + " not found!");
        }
    }
}