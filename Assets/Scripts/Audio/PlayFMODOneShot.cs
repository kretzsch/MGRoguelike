using UnityEngine;
using FMODUnity;

public class PlayFMODOneShot : MonoBehaviour
{
    public enum TriggerType { OnStart, OnEnable, OnClick, OnMouseEnter, OnMouseExit }

    [SerializeField] private string eventPath;
    [SerializeField] private TriggerType triggerType;

    private void Start()
    {
        if (triggerType == TriggerType.OnStart)
        {
            PlayOneShot();
        }
    }

    private void OnEnable()
    {
        if (triggerType == TriggerType.OnEnable)
        {
            PlayOneShot();
        }
    }

    private void OnMouseDown()
    {
        if (triggerType == TriggerType.OnClick)
        {
            PlayOneShot();
        }
    }

    private void OnMouseEnter()
    {
        if (triggerType == TriggerType.OnMouseEnter)
        {
            PlayOneShot();
        }
    }

    private void OnMouseExit()
    {
        if (triggerType == TriggerType.OnMouseExit)
        {
            PlayOneShot();
        }
    }

    private void PlayOneShot()
    {
        FMODUnity.RuntimeManager.PlayOneShot(eventPath, transform.position);
    }
}
