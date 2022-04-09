using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColliderResize : MonoBehaviour
{
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private Vector3 spriteSizeScale;

    void Start()
    {        
        spriteRenderer = gameObject.transform.parent.Find("EnemySprite").GetComponent<SpriteRenderer>();
    }
        
    void Update()
    {

        Vector3 spriteSize = spriteRenderer.sprite.bounds.size;

        spriteSizeScale = gameObject.transform.parent.Find("EnemySprite").transform.localScale;

        transform.localScale = new Vector3(spriteSize.x * spriteSizeScale.x, spriteSize.y * spriteSizeScale.y, spriteSize.z);

    }
}
