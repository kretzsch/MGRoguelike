using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{

    public int randomStartIndex;
   [System.Serializable]
    public struct Level
    {
        public string levelName;
        public GameObject levelObject;
        public string audioParameterLabel;
    }

    [SerializeField] private List<Level> levels;
    [SerializeField] private LevelAudioController levelAudioController;
    [SerializeField] private string transitionTriggerName = "Switch";
    [SerializeField] private Sprite[] transitionSprites;
    [SerializeField] private GameObject transitionObject;
    private SpriteRenderer _transitionRenderer;
    private Animator _transitionAnimator;

    private int _currentLevelIndex = -1;
    private bool _isTransitionRunning = false;
    private List<int> _completedLevelIndices = new List<int>();

    public void Awake()
    {
        _transitionRenderer = transitionObject.GetComponent<SpriteRenderer>();
        _transitionAnimator = transitionObject.GetComponent<Animator>();
         int randomStartIndex = GetRandomLevelIndex();
        StartCoroutine(SwitchLevel(randomStartIndex));
       // StartCoroutine(SwitchLevel(GetRandomLevelIndex()));
        //SwitchLevel(randomStartIndex);
    }
    private void Start()
    {
        levelAudioController.SetFmodParameter("level", levels[randomStartIndex].audioParameterLabel);
    }
    public IEnumerator SwitchLevel(int levelIndex, bool playTransition = true)
    {
        if (_isTransitionRunning || levelIndex < 0 || levelIndex >= levels.Count || levelIndex == _currentLevelIndex) yield break;

        _isTransitionRunning = true;

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
        if (_currentLevelIndex != -1)
        {
            _completedLevelIndices.Add(_currentLevelIndex);
        }

        // Update the current level index
        _currentLevelIndex = levelIndex;

        _isTransitionRunning = false;
    }

    public void SwitchToNextLevel()
    {
        if (_completedLevelIndices.Count == levels.Count)
        {
            // All levels are completed, load the new scene
            LoadNextScene();
        }
        else if (_completedLevelIndices.Count < levels.Count)
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
        } while (_completedLevelIndices.Contains(randomIndex));

        return randomIndex;
    }

    private IEnumerator PerformTransition(int targetLevelIndex)
    {
        // Switch the audio
        levelAudioController.SetFmodParameter("level", levels[targetLevelIndex].audioParameterLabel);

        // Choose a random sprite for the transition
        int randomSpriteIndex = Random.Range(0, transitionSprites.Length);
        Image transitionImage = transitionObject.GetComponent<Image>();
        transitionImage.sprite = transitionSprites[randomSpriteIndex];

        // Trigger the transition animation
        _transitionAnimator.SetTrigger(transitionTriggerName);

        // Deactivate the current level object
        if (_currentLevelIndex >= 0)
        {
            levels[_currentLevelIndex].levelObject.SetActive(false);
        }

        // Wait until the transition animation is completed
        yield return new WaitForSeconds(_transitionAnimator.GetCurrentAnimatorStateInfo(0).length);

        // Activate the target level object
        levels[targetLevelIndex].levelObject.SetActive(true);

        // Update the current level index
        _currentLevelIndex = targetLevelIndex;
    }

    private void SwitchLevelWithoutTransition(int targetLevelIndex)
    {
        // Switch the audio
        levelAudioController.SetFmodParameter("level", levels[targetLevelIndex].audioParameterLabel);

        // Deactivate the current level object
        if (_currentLevelIndex >= 0)
        {
            levels[_currentLevelIndex].levelObject.SetActive(false);
        }

        // Activate the target level object
        levels[targetLevelIndex].levelObject.SetActive(true);

        // Update the current level index
        _currentLevelIndex = targetLevelIndex;
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
