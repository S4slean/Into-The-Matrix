using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Sound
{
    public string name;
    public AudioClip clip;

    [Range(0f,1f)]
    public float volume=0.3f;
    [Range(0.5f, 1.5f)]
    public float pitch= 1f;

    private AudioSource source;
    public void setsource (AudioSource _source)
    {
        source = _source;
        source.clip = clip;
    }

    public void Play ()
    {
        source.volume = volume;
        source.pitch = pitch;
        source.Play();
    }
}

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;
    [SerializeField]
    Sound[] sounds;

    private void Awake()
    {
        if(instance != null)
        {
            Debug.LogError("Plusieur audioManger dans la scéne");
        }
        else
        {
            instance = this;
        }
        
    }
    private void Start()
    {
        for (int i = 0; i < sounds.Length;i++)
        {
            GameObject _go = new GameObject("Sound_" + i+"_" + sounds[i].name);
            _go.transform.SetParent(this.transform);
            sounds[i].setsource(_go.AddComponent<AudioSource>());
        }

        
    }

    public void Playsound (string _name)
    {
        for (int i = 0; i < sounds.Length; i++)
        {
            if (sounds[i].name == _name)
            {
                sounds[i].Play();
                return;
            }
        }

        // Il ne trouve pas le son

        Debug.LogWarning("AudioManager : Sound not found in list :" + _name);
    }
}
