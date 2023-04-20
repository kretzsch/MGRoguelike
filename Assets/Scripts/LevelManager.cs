using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [System.Serializable]
    public struct Level
    {
        public string levelName;
        public GameObject levelObject;
        public string audioParameterLabel;
    }

    [SerializeField] private List<Level> levels;
    [SerializeField] private LevelAudioController levelAudioController;
    [SerializeField] private Animator transitionAnimator;
    [SerializeField] private string transitionTriggerName = "Switch";

    private int currentLevelIndex = -1;

    private void  Awake()
    {
        SwitchLevel(0,false);
    }

    public void SwitchLevel(int levelIndex, bool playTransition = true)
    {
        if (levelIndex < 0 || levelIndex >= levels.Count || levelIndex == currentLevelIndex) return;

        // Perform level transition animation
        if (playTransition)
        {
            StartCoroutine(PerformTransition(levelIndex));
        }
        else
        {
            SwitchLevelWithoutTransition(levelIndex);
        }
    }


    public void SwitchToNextLevel()
    {
        int nextLevelIndex = (currentLevelIndex + 1) % levels.Count;
        SwitchLevel(nextLevelIndex);
    }

    private IEnumerator PerformTransition(int targetLevelIndex)
    {
     

        // Switch the audio
        levelAudioController.SetFmodParameter("level", levels[targetLevelIndex].audioParameterLabel);

        // Trigger the transition animation
        transitionAnimator.SetTrigger(transitionTriggerName);
        
        // Deactivate the current level object
        if (currentLevelIndex >= 0)
        {
            levels[currentLevelIndex].levelObject.SetActive(false);
        }

        // Wait until the transition animation is completed
        yield return new WaitForSeconds(transitionAnimator.GetCurrentAnimatorStateInfo(0).length);

     

        // Activate the target level object
        levels[targetLevelIndex].levelObject.SetActive(true);

      
        // Update the current level index
        currentLevelIndex = targetLevelIndex;
    }
    private void SwitchLevelWithoutTransition(int targetLevelIndex)
    {
        // Deactivate the current level object
        if (currentLevelIndex >= 0)
        {
            levels[currentLevelIndex].levelObject.SetActive(false);
        }

        // Activate the target level object
        levels[targetLevelIndex].levelObject.SetActive(true);

        // Switch the audio
        levelAudioController.SetFmodParameter("level", levels[targetLevelIndex].audioParameterLabel);

        // Update the current level index
        currentLevelIndex = targetLevelIndex;
    }


}
