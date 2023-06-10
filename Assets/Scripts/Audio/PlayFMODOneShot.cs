using UnityEngine;
using UnityEngine.EventSystems;
using FMODUnity;
using System.Collections.Generic;

public class PlayFMODOneShot : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    [System.Serializable]
    public class FMODEventTriggerPair
    {
        public string eventPath;
        public TriggerType triggerType;
    }

    public enum TriggerType { OnStart, OnEnable, OnClick, OnMouseEnter, OnMouseExit }

    [SerializeField] private List<FMODEventTriggerPair> fmodEvents = new List<FMODEventTriggerPair>();

    private void Start()
    {
        PlayEventsOfType(TriggerType.OnStart);
    }

    private void OnEnable()
    {
        PlayEventsOfType(TriggerType.OnEnable);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        PlayEventsOfType(TriggerType.OnClick);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        PlayEventsOfType(TriggerType.OnMouseEnter);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        PlayEventsOfType(TriggerType.OnMouseExit);
    }

    private void PlayEventsOfType(TriggerType type)
    {
        foreach (var fmodEvent in fmodEvents)
        {
            if (fmodEvent.triggerType == type)
            {
                PlayOneShot(fmodEvent.eventPath);
            }
        }
    }

    private void PlayOneShot(string eventPath)
    {
        FMODUnity.RuntimeManager.PlayOneShot(eventPath, transform.position);
    }
}
