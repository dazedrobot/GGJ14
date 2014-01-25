using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MusicController : MonoBehaviour
{
    public AudioClip[] audioLayers;

    Dictionary<string,AudioSource> audioSources = new Dictionary<string, AudioSource>();

    AudioSource playing;

    void Awake ()
    {
        foreach (AudioClip ac in audioLayers)
        {
            AudioSource source = gameObject.AddComponent<AudioSource>();
            source.clip = ac;
            source.loop = true;
            source.Stop();
            audioSources.Add(ac.name, source);
        }
    }

    public void SwitchTrack(string name)
    {
        if (playing) {
            double timeSwitch = AudioSettings.dspTime + (playing.clip.length - playing.time);
            audioSources [name].PlayScheduled (timeSwitch);
            playing.SetScheduledEndTime (timeSwitch);
        } else {
            audioSources [name].Play ();
        }
        playing = audioSources [name];
    }
}
