using System.Collections;
using UnityEngine;

public class RainbowColor : MonoBehaviour
{
    SpriteRenderer spriteRenderer;
    [SerializeField] Color[] color;
    int colorIndex = 0;
    int colornxt = 1;
    float target;
    [SerializeField] float time;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        target += Time.deltaTime/time;
        spriteRenderer.color = color[colornxt];
        if(target >= 1f)
        {
            target = 0f;
            colorIndex = colornxt;
            colornxt++;
            if(colornxt == color.Length)
            {
                colornxt = 0;
            }
        }

    }
}
