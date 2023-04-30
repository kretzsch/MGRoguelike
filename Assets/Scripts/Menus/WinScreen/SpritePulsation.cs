using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class SpritePulsation : MonoBehaviour
{
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private float pulsationSpeed = 4f;
    [SerializeField] private float minScale = 0.5f;
    [SerializeField] private float maxScale = 1.5f;

    private float time;

    private void Start()
    {
        if (spriteRenderer == null)
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
        }
    }

    private void Update()
    {
        time += Time.deltaTime * pulsationSpeed;
        float scale = Mathf.Lerp(minScale, maxScale, (Mathf.Sin(time) + 1f) * 0.5f);
        transform.localScale = new Vector3(scale, scale, 1f);
    }
}

