using UnityEngine;
using FMOD.Studio;

/// <summary>
///LevelAudioController is responsible for playing the main FMOD event and
///updating the event's parameters when all enemies in the level are dead.
/// </summary>
public class LevelAudioController : MonoBehaviour
{
    // EventInstance to control the main FMOD event
    private EventInstance _instance;
    [SerializeField] private LevelManager levelManager;

    private void Awake()
    {
        // Subscribe to the OnAllEnemiesDeadEvent from the StoreChildren component
        EnemyManager enemyManager = FindObjectOfType<EnemyManager>();
        if (enemyManager != null)
        {
            enemyManager.OnAllEnemiesDeadEvent += OnAllEnemiesDead;
        }
    }

    private void Start()
    {
        // Create and play the main FMOD event instance
        _instance = FMODAudioManager.Instance.CreateEventInstance("event:/FmodEvents/MainEvent"); // lets not hardcode this
        FMODAudioManager.Instance.PlayEvent(_instance);
    }

    private void OnDestroy()
    {
        // Unsubscribe from the OnAllEnemiesDeadEvent when the object is destroyed
        EnemyManager storeChildren = FindObjectOfType<EnemyManager>();
        if (storeChildren != null)
        {
            storeChildren.OnAllEnemiesDeadEvent -= OnAllEnemiesDead;
        }
    }

    // Called when all enemies in the level are dead
    public void OnAllEnemiesDead()
    {
        // Assuming you have a method in LevelManager to switch to the next level
        levelManager.SwitchToNextLevel();
    }

    // Set the FMOD parameter value using a parameter name and a label
    public void SetFmodParameter(string parameter, string label)
    {
        _instance.setParameterByNameWithLabel(parameter, label);
    }
}
