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
    private LevelManager levelManager;

    private void Awake()
    {
        // Find the LevelManager in the scene
        levelManager = FindObjectOfType<LevelManager>();

        // If LevelManager is present, subscribe to the OnAllEnemiesDeadEvent from the EnemyManager component
        if (levelManager != null)
        {
            EnemyManager enemyManager = FindObjectOfType<EnemyManager>();
            if (enemyManager != null)
            {
                enemyManager.OnAllEnemiesDeadEvent += OnAllEnemiesDead;
            }
        }
    }
    private void OnEnable()
    {
        if (levelManager != null)
        {
            // Create and play the main FMOD event instance
            _instance = FMODAudioManager.Instance.CreateEventInstance("event:/FmodEvents/MainEvent"); // lets not hardcode this
            FMODAudioManager.Instance.PlayEvent(_instance);
        }
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


    public void StopAudio()
    {
        if (_instance.isValid())
        {
            FMODAudioManager.Instance.StopEvent(_instance);
        }
    }
}
