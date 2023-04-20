using UnityEngine;
using FMOD.Studio;

public class LevelAudioController : MonoBehaviour
{
    private EventInstance _instance;
    [SerializeField] private LevelManager levelManager;

    private void Awake()
    {
        StoreChildren storeChildren = FindObjectOfType<StoreChildren>();
        if (storeChildren != null)
        {
            storeChildren.OnAllEnemiesDeadEvent += OnAllEnemiesDead;
        }
    }

    private void Start()
    {
        _instance = FMODAudioManager.Instance.CreateEventInstance("event:/FmodEvents/MainEvent");
        FMODAudioManager.Instance.PlayEvent(_instance);
    }

    private void OnDestroy()
    {
        StoreChildren storeChildren = FindObjectOfType<StoreChildren>();
        if (storeChildren != null)
        {
            storeChildren.OnAllEnemiesDeadEvent -= OnAllEnemiesDead;
        }
    }

    public void OnAllEnemiesDead()
    {
        // Assuming you have a method in LevelManager to switch to the next level
        levelManager.SwitchToNextLevel();
    }

    public void SetFmodParameter(string parameter, string label)
    {
        _instance.setParameterByNameWithLabel(parameter, label);
    }
}
