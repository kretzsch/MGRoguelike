using UnityEngine;
using UnityEngine.UI;

public class BouncingUIImage : MonoBehaviour
{
    public float speedX = 2f;
    public float speedY = 2f;
    private RectTransform rectTransform;
    private Vector2 screenSize;
    private Image image;
    private Color[] colors = { Color.green, Color.cyan, Color.yellow, Color.magenta, new Color(1, 0.5f, 0) };
    private int colorIndex = 0;

    private void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        screenSize = new Vector2(Screen.width, Screen.height) / 2;
        image = GetComponent<Image>();
    }

    private void Update()
    {
        Vector2 newPosition = rectTransform.anchoredPosition;
        newPosition.x += speedX * Time.deltaTime;
        newPosition.y += speedY * Time.deltaTime;

        float halfWidth = rectTransform.rect.width / 2;
        float halfHeight = rectTransform.rect.height / 2;

        if (newPosition.x > screenSize.x - halfWidth || newPosition.x < -screenSize.x + halfWidth)
        {
            speedX = -speedX;
            newPosition.x += speedX * Time.deltaTime; // Update the position after changing the direction
            ChangeColor();
        }

        if (newPosition.y > screenSize.y - halfHeight || newPosition.y < -screenSize.y + halfHeight)
        {
            speedY = -speedY;
            newPosition.y += speedY * Time.deltaTime; // Update the position after changing the direction
            ChangeColor();
        }

        rectTransform.anchoredPosition = newPosition;
    }

    private void ChangeColor()
    {
        colorIndex = (colorIndex + 1) % colors.Length;
        image.color = colors[colorIndex];
    }
}
