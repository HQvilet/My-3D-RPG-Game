using System.Collections;
using System.Collections.Generic;
using UnityEditor.Timeline.Actions;
using UnityEngine;
using UnityEngine.Playables;

public class TimelineController : Singleton<TimelineController>
{
    public PlayableDirector playableDirector;

    public void Play() => playableDirector.Play();

    public void Pause() => playableDirector.Pause();
}
