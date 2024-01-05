using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ProjectileBehaviour : MonoBehaviour
{
    [Header("Values")]
    public float shootDamage;
    public float initialSpeed = 110f;
    public float checkDistance = 2.5f;

    [Header("Data")]
    [SerializeField] List<Collider> ignoredColliders = new();
    RaycastHit[] hits;
    List<RaycastHit> newHits = new();

    [Header("Local References")]
    [SerializeField] Rigidbody rb;
    [SerializeField] GameObject colliderObj;
    [SerializeField] GameObject modelObj;
    [SerializeField] GameObject particlesObj;

    [Header("References")]
    [SerializeField] private EnemyHitData hitData;
    public PlayerHUD playerHUD;

    [Header("Check")]
    private Vector3 initialPos;
    private Vector3 lastPos;
    private Vector3 currentPosition;
    private Vector3 currentVelocity;
    private bool confirmKill;

    private void Awake() => colliderObj.SetActive(false);
    private void Start() => initialPos = lastPos = transform.position;
    private void Update() => ColisionDetection();

    private void ColisionDetection()
    {
        currentPosition = transform.position;
        currentVelocity = rb.velocity;

        hits = Physics.RaycastAll(lastPos, currentVelocity.normalized, checkDistance);
        Debug.DrawRay(lastPos, currentVelocity.normalized * checkDistance, Color.red);

        if (hits.Length > 0)        
        {
            newHits = new(hits.Where(x => x.transform.GetComponent<EnemyHitData>() != null));
            newHits = new(newHits.OrderBy(x => Vector3.Distance(x.transform.position, lastPos)));

            foreach (RaycastHit hit in newHits)
            {
                hitData = hit.transform.GetComponent<EnemyHitData>();

                Texture2D dataTexture = hitData.dataTexture;
                Texture2D animTexture = hitData.spriteRenderer.sprite.texture;

                Color dataPixelColor = GetPixelFromTextureCoord(hit, dataTexture);
                Color hitPixelColor = GetPixelFromTextureCoord(hit, animTexture);

                if (dataPixelColor.a > GameConstants.ALPHA_THRESHOLD) StartCoroutine(KillEnemy(2, hit));
                else if (hitPixelColor.a > GameConstants.ALPHA_THRESHOLD) StartCoroutine(KillEnemy(1, hit));

                break;
            }
        }

        lastPos = transform.position;
    }

    private Color GetPixelFromTextureCoord(RaycastHit hit, Texture2D texture)
    {
        Vector2 textureCoord = hit.textureCoord;

        int uvX = Mathf.FloorToInt(textureCoord.x * texture.width);
        int uvY = Mathf.FloorToInt(textureCoord.y * texture.height);

        return texture.GetPixel(uvX, uvY);
    }

    private IEnumerator KillEnemy(float damage, RaycastHit hit)
    {
        //AddCollidersAndSortByDistance(hit);
        //IgnoreColliders();

        yield return new WaitForEndOfFrame();

        if (!confirmKill)
        {
            confirmKill = true;
            
            transform.position = hit.point;
            rb.isKinematic = true;

            var enemyHitData = hit.transform.GetComponent<EnemyHitData>();
            var currentHealth = enemyHitData.enemyScript.health - shootDamage * damage;
            if (damage == 1) playerHUD.NormalHit(); else playerHUD.CriticalHit();
            if (currentHealth <= 0) playerHUD.KillHit();            
            enemyHitData.enemyScript.EnemyDamage(shootDamage * damage);            

            yield return new WaitForSeconds(0.2f);
            
            Destroy(particlesObj, 7f);
            particlesObj.transform.parent = null;
            
            Destroy(gameObject);
        }
    }

    private void AddCollidersAndSortByDistance(RaycastHit hit)
    {
        if (!ignoredColliders.Contains(hit.collider))
        {
            ignoredColliders.Add(hit.collider);
        }

        /*
        if (!ignoredColliders.Contains(hit.collider))
        {
            ignoredColliders.Add(hit.collider);

            ignoredColliders.Sort((a, b) =>
                Vector3.Distance(initialPos, a.transform.position)
                .CompareTo(Vector3.Distance(initialPos, b.transform.position))
            );
        } 
        */
    }
    private void IgnoreColliders()
    {
        foreach (Collider ignoredCollider in ignoredColliders)
        {
            Physics.IgnoreCollision(colliderObj.GetComponent<Collider>(), ignoredCollider, false);
        }
    }

}
