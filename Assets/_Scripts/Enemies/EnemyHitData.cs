using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHitData : MonoBehaviour
{
    [Header("Stored Data References")]
    public Transform enemyTransform;
    public Enemy enemyScript;
    public SpriteRenderer spriteRenderer;
    public MeshRenderer meshRenderer;
    public Texture2D dataTexture;

    void Start()
    {
        if (enemyTransform == null) enemyTransform = GetComponentInParent<Enemy>().transform;
        if (spriteRenderer == null) spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        if (meshRenderer == null) meshRenderer.GetComponent<MeshRenderer>();
        if (enemyScript == null) enemyScript.GetComponent<Enemy>();
        if (dataTexture == null) dataTexture = enemyTransform.GetComponentInChildren<BillboardRenderer>().dataTexture;
    }
}
