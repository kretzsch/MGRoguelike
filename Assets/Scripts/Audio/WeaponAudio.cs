using UnityEngine;
using FMODUnity;

public class WeaponAudio : MonoBehaviour
{
    private WeaponData weaponData;
    private bool is3D;

    public void Initialize(WeaponData weaponData, bool is3D)
    {
        this.weaponData = weaponData;
        this.is3D = is3D;
    }

    public void PlayFireSound()
    {
        string eventPath = is3D ? weaponData.weaponAudioEvent.EventPath3D : weaponData.weaponAudioEvent.EventPath2D;
        FMODUnity.RuntimeManager.PlayOneShot(eventPath, transform.position);
    }
}
[System.Serializable]
public class AudioEvent
{
    public string EventPath2D;
    public string EventPath3D;
}


