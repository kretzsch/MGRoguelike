using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GoToSceneButton : MonoBehaviour
{
    [SerializeField] private string scenename;

    public LoadoutManager loadoutManager;
    private void Start()
    {
        Button button = GetComponent<Button>();
        button.onClick.AddListener(StartGame);
    }

    /// <summary>
    /// come on we can do better than this....
    /// really need to refactor this shit
    /// </summary>
    private void StartGame()
    {
        Debug.Log("ayo are you printing ");
        if(scenename == "MainScene") loadoutManager.TransferDataToLoadoutData();
        if(scenename == "MainMenu") LoadoutData.ClearLoadoutData(); 
        SceneManager.LoadScene(scenename);
    }
}
