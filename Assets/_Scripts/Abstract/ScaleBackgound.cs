using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(SpriteRenderer))]
public class ScaleBackground : HighMonoBehaviour
{
    private SpriteRenderer spriteRenderer;

    protected override void LoadComponents()
    {
        base.LoadComponents();

        spriteRenderer = GetComponent<SpriteRenderer>();

        if (spriteRenderer == null)
        {
            Debug.LogError("No SpriteRenderer component found on this GameObject.");
            return;
        }

        ScaleToScreen();
    }

    private void ScaleToScreen()
    {
        if (spriteRenderer.sprite == null)
        {
            Debug.LogError("No sprite set for the SpriteRenderer.");
            return;
        }

        float spriteWidth = spriteRenderer.sprite.bounds.size.x;
        float spriteHeight = spriteRenderer.sprite.bounds.size.y;

        float worldScreenHeight = Camera.main.orthographicSize * 2.0f;
        float worldScreenWidth = worldScreenHeight / Screen.height * Screen.width;

        Vector2 spriteScale = new Vector2(worldScreenWidth / spriteWidth, worldScreenHeight / spriteHeight);
        transform.localScale = spriteScale;
    }
}
