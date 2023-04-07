using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpriteSwapper : MonoBehaviour
{
    [SerializeField] Sprite[] sprites;
    int spritePointer = 0;

    Image image;

    private void Start()
    {
        image = GetComponent<Image>();
    }

    public void ClickSound()
    {
        if (spritePointer < sprites.Length - 1)
        {
            spritePointer++;
        }

        else if (spritePointer >= sprites.Length - 1)
        {
            spritePointer = 0;
        }

        image.sprite = sprites[spritePointer];
    }
}
