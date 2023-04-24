using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartButton : MonoBehaviour
{
    public LoadoutManager loadoutManager;
        private void Start()
        {
            Button button = GetComponent<Button>();
            button.onClick.AddListener(StartGame);
        }

private void StartGame()
    {
        loadoutManager.TransferDataToLoadoutData();
        SceneManager.LoadScene("MainScene");
    }
}
