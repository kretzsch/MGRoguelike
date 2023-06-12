using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ResetButton : MonoBehaviour
{
    public LoadoutManager loadoutManager;
    public TextMeshProUGUI budgetText;
    public Transform purchasedItemsParent;

    private void Start()
    {
        Button button = GetComponent<Button>();
        ResetLoadout();
        button.onClick.AddListener(ResetLoadout);
    }

    private void ResetLoadout()
    {
        loadoutManager.ResetLoadout(budgetText, purchasedItemsParent);
    }
}
