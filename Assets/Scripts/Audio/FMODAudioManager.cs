using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMOD.Studio;
using FMODUnity;

public class FMODAudioManager : MonoBehaviour
{
    public static FMODAudioManager Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
    }
    public EventInstance CreateEventInstance(string eventPath)
    {
        return RuntimeManager.CreateInstance(eventPath);
    }

    public void PlayEvent(EventInstance eventInstance)
    {
        eventInstance.start();
    }

    public void StopEvent(EventInstance eventInstance, FMOD.Studio.STOP_MODE stopMode = FMOD.Studio.STOP_MODE.ALLOWFADEOUT)
    {
        eventInstance.stop(stopMode);
    }

    public void SetParameterValue(EventInstance eventInstance, string parameterName, float value)
    {
        eventInstance.setParameterByName(parameterName, value);
    }

    public float GetEventDuration(string eventPath)
    {
        EventDescription eventDescription;
        RuntimeManager.StudioSystem.getEvent(eventPath, out eventDescription);
        eventDescription.getLength(out int duration);
        return duration / 1000f;
    }
}
