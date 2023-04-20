using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;
using FMOD.Studio;
using System;

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

    private void Start()
    {
        SwitchLevel(0);
    }

    public void SwitchLevel(int levelIndex)
    {
        if (levelIndex < 0 || levelIndex >= levels.Count || levelIndex == currentLevelIndex) return;

        // Perform level transition animation
        StartCoroutine(PerformTransition(levelIndex));
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

        // Wait until the transition animation is completed
        yield return new WaitForSeconds(transitionAnimator.GetCurrentAnimatorStateInfo(0).length);

        // Deactivate the current level object
        if (currentLevelIndex >= 0)
        {
            levels[currentLevelIndex].levelObject.SetActive(false);
        }

        // Activate the target level object
        levels[targetLevelIndex].levelObject.SetActive(true);

        // Update the current level index
        currentLevelIndex = targetLevelIndex;
    }

}
