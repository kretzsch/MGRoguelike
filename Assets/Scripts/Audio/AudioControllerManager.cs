using UnityEngine;

/// <summary>
/// Quick and dirty fix for playing audio in main menu vs in game
/// this checks if theres a levelmanager in the scene or not and plays audio accordingly. 
/// this is done to meet a prototype deadline the day after. 
/// will polish at a later date 
/// </summary>
public class AudioControllerManager : MonoBehaviour
{
    [SerializeField] private AudioLayerController audioLayerController;
    [SerializeField] private LevelAudioController levelAudioController;

    private void Start()
    {
        LevelManager levelManager = FindObjectOfType<LevelManager>();

        if (levelManager == null)
        {
            // If LevelManager is not present, we are in the main menu
            audioLayerController.enabled = true;
            levelAudioController.enabled = false;
        }
        else
        {
            // If LevelManager is present, we are in the game scene
            audioLayerController.enabled = false;
            levelAudioController.enabled = true;
        }
    }
}
