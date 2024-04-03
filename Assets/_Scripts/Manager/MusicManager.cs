using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class MusicManager : HighMonoBehaviour
{
    static MusicManager instance;
    public static MusicManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<MusicManager>();
            }
            return instance;
        }
    }
    [SerializeField] private List<Audio> audioList;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        if (instance != null)
        {
            Debug.LogWarning("There are multiple MusicManager in the scene");
        }
        instance = this;

        foreach (Audio audio in audioList)
        {
            audio.source = gameObject.AddComponent<AudioSource>();
            audio.source.clip = audio.clip;
            audio.source.volume = audio.volume;
        }
    }
    public void PlayMusic(string name)
    {
        Audio audio = audioList.Find(x => x.name == name);
        if (audio == null)
        {
            Debug.LogWarning("There is no music with the name " + name);
            return;
        }
        audio.source.Play();
    }

}