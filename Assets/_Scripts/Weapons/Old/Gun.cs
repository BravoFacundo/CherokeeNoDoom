using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gun : MonoBehaviour
{
    [Header("References")]
    [SerializeField] WeaponData gunData;
    [SerializeField] Transform cam;
    [SerializeField] Transform muzzle;
    [SerializeField] Rigidbody playerRB;

    float timeSinceLastShot;

    private void Start()
    {
        PlayerShoot.shootInput += Shoot;
        PlayerShoot.reloadInput += StartReload;
    }

    private void OnDisable() => gunData.reloading = false;

    public void StartReload()
    {
        if (!gunData.reloading && this.gameObject.activeSelf)
        {
            StartCoroutine(Reload());
        }
    }
    private IEnumerator Reload()
    {
        gunData.reloading = true;
        yield return new WaitForSeconds(gunData.reloadTime);
        gunData.currentAmmo = gunData.magSize;
        gunData.reloading = false;
    }

    private bool CanShoot() => !gunData.reloading && timeSinceLastShot > 1f / (gunData.fireRate / 60f);
    public void Shoot()
    {
        //Debug.Log("Shoot Gun!");
        if (gunData.currentAmmo > 0)
        {
            if (CanShoot())
            {
                //NUEVO:

                if (Physics.Raycast(cam.position, cam.forward, out RaycastHit hitInfo, gunData.maxDistance))
                {
                    //Debug.Log(hit.transform.name); //Debug.Log(hit.point)

                    if (hitInfo.transform.parent != null && hitInfo.transform.parent.gameObject.GetComponentInChildren<SpriteRenderer>() != null)
                    {
                        Texture2D characterTexture = (Texture2D)hitInfo.transform.parent.gameObject.GetComponentInChildren<SpriteRenderer>().sprite.texture;

                        //Obtener tambien la headshot texture (En este caso esta en la mesh renderer de quad que toma la hit.coord)
                        Texture2D CriticAreaTexture = (Texture2D)hitInfo.collider.GetComponent<MeshRenderer>().material.mainTexture;

                        //Convert hit coordinates
                        Vector2 pixelUV = hitInfo.textureCoord;
                        int uvX = Mathf.FloorToInt(pixelUV.x * characterTexture.width);
                        int uvY = Mathf.FloorToInt(pixelUV.y * characterTexture.height);

                        //Alpha Check                    
                        Color hitColor = characterTexture.GetPixel(uvX, uvY);
                        if (hitColor.a > 0.05)
                        {
                            Debug.Log("La coordenada " + uvX + "/" + uvY + " en " + hitInfo.transform.name + " NO ES ALPHA");

                            Color criticColor = CriticAreaTexture.GetPixel(uvX, uvY);
                            if (criticColor.a > 0.05)
                            {
                                Debug.Log("CRITICO");
                            }
                        }
                        else
                        {
                            Debug.Log("La coordenada " + uvX + "/" + uvY + " en " + hitInfo.transform.name + " ES ALPHA");
                        }
                    }

                }

                /*VIEJO:

                //Cambiar segun sea necesario el origen y direccion del disparo
                //Calculos con la camara y Efectos con el muzzle
                if (Physics.Raycast(cam.position, cam.forward, out RaycastHit hitInfo, gunData.maxDistance))
                {
                    print(hitInfo.transform.name);
                    IDamageable damageable = hitInfo.transform.GetComponent<IDamageable>();
                    damageable?.TakeDamage(gunData.damage);

                    //hit.rigidbody.AddForce(ray.direction * hitForce);
                    //rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
                    playerRB.AddForce(-cam.forward * gunData.nockback, ForceMode.Impulse);
                }
                */

                //OBLIGATORIO:

                gunData.currentAmmo--;
                timeSinceLastShot = 0;
                OnGunShot();

            }
        }
    }

    private void Update()
    {
        timeSinceLastShot += Time.deltaTime;
        Debug.DrawRay(cam.position, cam.forward * gunData.maxDistance, Color.blue);
        Debug.DrawRay(muzzle.position, cam.forward * gunData.maxDistance, Color.red);

    }

    private void OnGunShot()
    {

    }
}
