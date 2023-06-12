using UnityEngine;
using FMODUnity;

/// <summary>
/// Plays events from FMOD. 
/// attach to gamecomponent. (think of oneshots for enemies, players, anything that makes sound on certain conditions. )
/// </summary>
public class AudioEventPlayer : MonoBehaviour
{
    private bool is3D;

    public void Initialize(bool is3D)
    {
        this.is3D = is3D;
    }

    public void PlayOneShotSound(AudioEvent audioEvent)
    {
        string eventPath = is3D ? audioEvent.EventPath3D : audioEvent.EventPath2D;
        FMODUnity.RuntimeManager.PlayOneShot(eventPath, transform.position);
    }
}
[System.Serializable]
public class AudioEvent
{
    public string EventPath2D;
    public string EventPath3D;
}
