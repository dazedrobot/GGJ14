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
        playing = audioSources ["Denial"];
        playing.Play ();
        
        StartCoroutine (testSwitch (2,"Anger"));
        StartCoroutine (testSwitch (20,"Bargaining"));
        StartCoroutine (testSwitch (40,"Depression"));
        StartCoroutine (testSwitch (60,"Acceptance"));
    }

    public void SwitchTrack(string name)
    {
        double timeSwitch = AudioSettings.dspTime + (playing.clip.length - playing.time);
        audioSources[name].PlayScheduled(timeSwitch);
        playing.SetScheduledEndTime (timeSwitch);
        playing = audioSources [name];
    }

    IEnumerator testSwitch(float time, string name)
    {
        yield return new WaitForSeconds(time);
        SwitchTrack (name);
    }
    
    // Update is called once per frame
    void Update ()
    {
        
    }
}
