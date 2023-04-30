using System.Collections;
using TMPro;
using UnityEngine;

public class ColorFlashText : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI textMeshPro;
    [SerializeField] private float flashInterval = 0.5f;
    [SerializeField] private float rotationSpeed = 45f; // Degrees per second
    private Color[] colors = { Color.cyan, Color.yellow, Color.magenta}; // Cyan, Yellow, and Bright Green

    private void Start()
    {
        if (textMeshPro == null)
        {
            textMeshPro = GetComponent<TextMeshProUGUI>();
        }
        StartCoroutine(FlashColors());
    }

    private void Update()
    {
        // Rotate the text
        transform.Rotate(0, 0, rotationSpeed * Time.deltaTime);
    }

    private IEnumerator FlashColors()
    {
        int colorIndex = 0;
        while (true)
        {
            textMeshPro.color = colors[colorIndex];
            colorIndex = (colorIndex + 1) % colors.Length;
            yield return new WaitForSeconds(flashInterval);
        }
    }
}
