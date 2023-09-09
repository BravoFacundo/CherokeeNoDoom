using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This set of classes are an example of the impact detection system for this game's billboard enemies.
// These enemies are composed of a Sprite Renderer always facing the camera and a Mesh Collider Plane...
// which constantly mimics the size of the Sprite Renderer in the world. Using the following script:

public class EnemyColliderResize : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    private Transform enemySpriteTransform;
    private Vector3 spriteSizeScale;

    void Start()
    {
        spriteRenderer = enemySpriteTransform.GetComponent<SpriteRenderer>();
        enemySpriteTransform = gameObject.transform.parent.Find("EnemySprite").transform;
    }

    void Update()
    {
        Vector3 spriteSize = spriteRenderer.sprite.bounds.size;

        spriteSizeScale = enemySpriteTransform.localScale;

        transform.localScale = new Vector3(spriteSize.x * spriteSizeScale.x, spriteSize.y * spriteSizeScale.y, 1);
    }
}

// Now the sprite and the collider have the same size, position and orientation. By firing a Raycast and..
// using hit.textureCoord it is possible to check if the impacted pixel belongs to the visible part of the image.

// The following script is responsible for dismissing or validating the hit using the pixel's alpha value.
// You can even use a second texture to compare simultaneous hits and determine critical damage areas!

public class CheckCollisionOnSpriteRenderer : MonoBehaviour
{
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit, 100))
            {
                Texture2D enemyTexture = GetTextureFromHit(hit);
                Texture2D dataTexture = GetDataTextureFromHit(hit);

                if (enemyTexture != null)
                {
                    CheckAlphaAtHitPosition(enemyTexture, dataTexture, hit.textureCoord, hit.transform.name);
                }
            }
        }
    }

    private Texture2D GetTextureFromHit(RaycastHit hit)
    {
        Transform enemyTransform = hit.transform.parent;

        if (enemyTransform != null && enemyTransform.CompareTag("Enemy"))
        {
            SpriteRenderer spriteRenderer = enemyTransform.GetComponentInChildren<SpriteRenderer>();
            if (spriteRenderer != null)
            {
                return (Texture2D)spriteRenderer.sprite.texture;
            }
        }
        return null;
    }

    private Texture2D GetDataTextureFromHit(RaycastHit hit)
    {
        MeshRenderer meshRenderer = hit.collider.GetComponent<MeshRenderer>();
        if (meshRenderer != null)
        {
            return (Texture2D)meshRenderer.material.mainTexture;
        }
        return null;
    }

    private void CheckAlphaAtHitPosition(Texture2D texture, Texture2D dataTexture, Vector2 textureCoord, string transformName)
    {

        int uvX = Mathf.FloorToInt(textureCoord.x * texture.width);
        int uvY = Mathf.FloorToInt(textureCoord.y * texture.height);

        var hitColor = texture.GetPixel(uvX, uvY);

        if (hitColor.a <= 0.05)
        {
            Debug.Log("The coordinates " + uvX + "|" + uvY + " in " + transformName + " correspond to an alpha pixel");
        }
        else
        {
            Color pxColor = dataTexture.GetPixel(uvX, uvY);
            if (pxColor.a <= 0.05)
            {
                Debug.Log("The coordinates " + uvX + "/" + uvY + " in " + transformName + " correspond to a non-alpha pixel");
            }
            else
            {
                Debug.Log("The coordinates " + uvX + "/" + uvY + " in " + transformName + " correspond to a non-alpha pixel and hit a critical area");
            }
        }

    }

}
