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

    private void StartGame()
    {
        if(scenename == "MainScene") loadoutManager.TransferDataToLoadoutData();
        SceneManager.LoadScene(scenename);
    }
}
