using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GamePlayMusic : MonoBehaviour {

    private static bool isMute;
    private static ArrayList sounds;
    private static float volume;

    public static bool IsMute
    {
        set
        {
            if (!isMute && value)
            {
                isMute = value;
                StopAll();
            }
            else if (isMute && !value)
            {
                isMute = value;
                ResumeAll();
            }
            isMute = value;
        }
        get
        {
            return isMute;
        }
    }

    //void Awake()
    //{
    //    DontDestroyOnLoad(gameObject);
    //}

    void Start()
    {
        sounds = new ArrayList();
        SetVolume(0.5f);
        AudioSource[] comp = this.gameObject.GetComponents<AudioSource>();
        foreach (AudioSource a in comp)
        {
            sounds.Add(a);
        }
//        GamePlayMusic.IsMute = PlayerDataFile.IsSFXOff;
        if (!isMute)
            ResumeAll();
    }

    public static void play(string str)
    {
        if (!isMute)
        {
            for (int i = 0; i < sounds.Count; i++)
            {
                if (str == ((AudioSource)sounds[i]).clip.name)
                {
                    ((AudioSource)sounds[i]).volume = volume;
                    ((AudioSource)sounds[i]).Play();
                }
            }
        }
    }

    public static void play(string str, float vol)
    {
        if (!isMute)
        {
            for (int i = 0; i < sounds.Count; i++)
            {
                if (str == ((AudioSource)sounds[i]).clip.name)
                {
                    ((AudioSource)sounds[i]).volume = volume * vol;
                    ((AudioSource)sounds[i]).Play();
                }
            }
        }
    }

    public static void stop(string str)
    {
        if (!isMute)
        {
            for (int i = 0; i < sounds.Count; i++)
            {
                if (str == ((AudioSource)sounds[i]).clip.name)
                {
                    ((AudioSource)sounds[i]).Stop();
                }
            }
        }
    }

    public static void StopAll()
    {
       
        for (int i = 0; i < sounds.Count; i++)
        {
            ((AudioSource)sounds[i]).Stop();
        }
    }

    public static void ResumeAll()
    {
        if (!isMute)
        {
            for (int i = 0; i < sounds.Count; i++)
            {
                if (!((AudioSource)sounds[i]).isPlaying)
                    ((AudioSource)sounds[i]).Play();
            }
        }
    }

    public static void SetVolume(float vol)
    {
        for (int i = 0; i < sounds.Count; i++)
        {
            ((AudioSource)sounds[i]).volume = vol;
        }
        volume = vol;
    }
}
