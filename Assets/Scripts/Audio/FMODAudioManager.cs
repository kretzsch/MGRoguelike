using UnityEngine;
using FMOD.Studio;
using FMODUnity;

/// <summary>
/// FMODAudioManager is a singleton class responsible for 
/// creating, playing, stopping, and modifying FMOD audio events.
/// </summary>
public class FMODAudioManager : MonoBehaviour
{
    // Singleton instance of FMODAudioManager
    public static FMODAudioManager Instance;

    // Ensure there's only one instance of FMODAudioManager in the scene
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


    // this method is used for oneshots like menu hover sounds etc. 
    // use this piece of code to trigger it : FMODAudioManager.Instance.PlayOneShot("path/to/your/event");

    public void PlayOneShot(string eventPath, Vector3 position = default(Vector3))
    {
        RuntimeManager.PlayOneShot(eventPath, position);
    }

    // Create an FMOD event instance using the given event path
    public EventInstance CreateEventInstance(string eventPath)
    {
        return RuntimeManager.CreateInstance(eventPath);
    }

    // Start playing the given event instance
    public void PlayEvent(EventInstance eventInstance)
    {
        eventInstance.start();
    }

    // Stop the given event instance using the specified stop mode
    public void StopEvent(EventInstance eventInstance, FMOD.Studio.STOP_MODE stopMode = FMOD.Studio.STOP_MODE.ALLOWFADEOUT)
    {
        eventInstance.stop(stopMode);
    }

    // Set the value of a parameter within an event instance
    public void SetParameterValue(EventInstance eventInstance, string parameterName, float value)
    {
        eventInstance.setParameterByName(parameterName, value);
    }

    // Get the duration of an FMOD event using its event path
    public float GetEventDuration(string eventPath)
    {
        EventDescription eventDescription;
        RuntimeManager.StudioSystem.getEvent(eventPath, out eventDescription);
        eventDescription.getLength(out int duration);
        return duration / 1000f;
    }
}
