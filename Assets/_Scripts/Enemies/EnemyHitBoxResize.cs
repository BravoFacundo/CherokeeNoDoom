using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHitBoxResize : MonoBehaviour
{
    private Transform enemySpriteTransform;
    private SpriteRenderer spriteRenderer;
    private Vector3 spriteSizeScale;

    void Start()
    {
        enemySpriteTransform = gameObject.transform.parent.Find("EnemySprite").transform;
        spriteRenderer = enemySpriteTransform.GetComponent<SpriteRenderer>();
    }
        
    void Update()
    {
        Vector3 spriteSize = spriteRenderer.sprite.bounds.size;

        spriteSizeScale = enemySpriteTransform.localScale;

        transform.localScale = new Vector3(spriteSize.x * spriteSizeScale.x, spriteSize.y * spriteSizeScale.y, 1);
    }
}