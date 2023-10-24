using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyHitBoxResize : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Transform spriteTransform;
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private NavMeshAgent navMeshAgent;

    private Vector3 spriteSizeScale;

    void Start()
    {
        if (spriteRenderer == null || spriteTransform == null)
        {
            navMeshAgent = GetComponentInParent<NavMeshAgent>();
            spriteRenderer = navMeshAgent.GetComponentInChildren<SpriteRenderer>();
            spriteTransform = spriteRenderer.transform;
        }
        SetValuesUsingSpriteSize();
    }

    void SetValuesUsingSpriteSize()
    {
        Vector3 spriteSize = spriteRenderer.sprite.bounds.size;
        var spritePosY = spriteTransform.localScale.y * spriteSize.y * 0.5f;
        transform.localPosition = new(0, spritePosY, 0.01f);

        if (navMeshAgent != null) navMeshAgent.height = spritePosY * 2;
    }
        
    void Update()
    {
        RescaleSpriteCollider();
    }

    void RescaleSpriteCollider()
    {
        Vector3 spriteSize = spriteRenderer.sprite.bounds.size;

        spriteSizeScale = spriteTransform.localScale;

        transform.localScale = new Vector3(spriteSize.x * spriteSizeScale.x, spriteSize.y * spriteSizeScale.y, 1);
    }
}