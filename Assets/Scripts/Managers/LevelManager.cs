using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
    private bool isTransitionRunning = false;
    private List<int> completedLevelIndices = new List<int>();

    private void Awake()
    {
       // int randomStartIndex = GetRandomLevelIndex();
        StartCoroutine(SwitchLevel(GetRandomLevelIndex()));
        //SwitchLevel(randomStartIndex);
    }

    public IEnumerator SwitchLevel(int levelIndex, bool playTransition = true)
    {
        if (isTransitionRunning || levelIndex < 0 || levelIndex >= levels.Count || levelIndex == currentLevelIndex) yield break;

        isTransitionRunning = true;

        // Perform level transition animation
        if (playTransition)
        {
            yield return PerformTransition(levelIndex);
        }
        else
        {
            SwitchLevelWithoutTransition(levelIndex);
        }

        // Add the completed level index to the list of completed levels
        if (currentLevelIndex != -1)
        {
            completedLevelIndices.Add(currentLevelIndex);
        }

        // Update the current level index
        currentLevelIndex = levelIndex;

        isTransitionRunning = false;
    }

    public void SwitchToNextLevel()
    {
        if (completedLevelIndices.Count == levels.Count)
        {
            // All levels are completed, load the new scene
            LoadNextScene();
        }
        else if (completedLevelIndices.Count < levels.Count)
        {
            StartCoroutine(SwitchLevel(GetRandomLevelIndex()));
        }
    }

    private int GetRandomLevelIndex()
    {
        int randomIndex;
        do
        {
            randomIndex = Random.Range(0, levels.Count);
        } while (completedLevelIndices.Contains(randomIndex));

        return randomIndex;
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
        // Switch the audio
        levelAudioController.SetFmodParameter("level", levels[targetLevelIndex].audioParameterLabel);

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

    private void LoadNextScene()
    {
        // Assuming the next scene is in the build settings, just load it by index
        int nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;
        SceneManager.LoadScene(nextSceneIndex);
    }

    public void SubscribeToEnemyManager(EnemyManager enemyManager)
    {
        if (enemyManager != null)
        {
            enemyManager.OnAllEnemiesDeadEvent += SwitchToNextLevel;
        }
    }

    public void UnsubscribeFromEnemyManager(EnemyManager enemyManager)
    {
        if (enemyManager != null)
        {
            enemyManager.OnAllEnemiesDeadEvent -= SwitchToNextLevel;
        }
    }
}
