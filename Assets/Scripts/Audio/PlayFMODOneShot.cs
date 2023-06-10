using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using FMODUnity;

public class PlayFMODOneShot : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
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

    public void OnPointerClick(PointerEventData eventData)
    {
        if (triggerType == TriggerType.OnClick)
        {
            Debug.Log("mouse click");
            PlayOneShot();
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (triggerType == TriggerType.OnMouseEnter)
        {
            Debug.Log("mouseenter");
            PlayOneShot();
        }
    }

    public void OnPointerExit(PointerEventData eventData)
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
