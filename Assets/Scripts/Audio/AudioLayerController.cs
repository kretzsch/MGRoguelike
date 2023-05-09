using UnityEngine;
using FMOD.Studio;
using FMODUnity;

/// <summary>
/// AudioLayerController is responsible for playing different 
/// vertical layers of a song. 
/// this is reuseable
/// </summary>
public class AudioLayerController : MonoBehaviour
{
    [SerializeField] private string eventPath;
    private EventInstance eventInstance;
    private FMODAudioManager audioManager;

    private void OnEnable()
    {
        audioManager = FMODAudioManager.Instance;
        Debug.Log(audioManager);
        eventInstance = audioManager.CreateEventInstance(eventPath);
        audioManager.PlayEvent(eventInstance);
    }

    public void ChangeLayer(int layer)
    {
        if (eventInstance.isValid())
        {
            SetParameterValue(eventInstance, "Layer", layer);
        }
        else
        {
            Debug.LogWarning("Event instance is not valid. Cannot change layer.");
        }
    }


    private void SetParameterValue(EventInstance eventInstance, string parameterName, int index)
    {
        eventInstance.setParameterByName(parameterName, index);
    }

/// <summary>
/// the start and stopaudio methods are used in the audiocontrollermanager
/// </summary>
    public void StopAudio()
    {
        if (eventInstance.isValid())
        {
            FMODAudioManager.Instance.StopEvent(eventInstance);
        }
    }
}
