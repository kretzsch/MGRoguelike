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
    private EventInstance _instance;
    private FMODAudioManager audioManager;

    private void Start()
    {
        audioManager = FMODAudioManager.Instance;
        _instance = audioManager.CreateEventInstance(eventPath);
        audioManager.PlayEvent(_instance);
    }
    public void ChangeLayer(int layer)
    {
        if (_instance.isValid())
        {
            SetParameterValue(_instance, "Layer", layer);
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
    private void OnDestroy()
    {
        StopAudio();
    }
    public void StopAudio()
    {
        if (_instance.isValid())
        {
            FMODAudioManager.Instance.StopEvent(_instance);
        }
    }
}
