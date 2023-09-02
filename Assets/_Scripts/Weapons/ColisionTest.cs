using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColisionTest : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (Physics.Raycast(collision.GetContact(0).point, -collision.GetContact(0).normal, out RaycastHit hit))
        {
            Transform hitTransform = hit.transform;
            Transform parentTransform = hitTransform.parent;

            if (hitTransform.CompareTag("Enemy") && parentTransform != null)
            {
                print("Here");
                transform.GetComponent<Rigidbody>().isKinematic = true;
                transform.parent = collision.gameObject.transform;

                SpriteRenderer spriteRenderer = parentTransform.GetComponentInChildren<SpriteRenderer>();
                if (spriteRenderer != null)
                {
                    

                    /*
                    Vector2 textureCoord = hit.textureCoord;

                    Texture2D dataTexture = (Texture2D)hit.collider.GetComponent<MeshRenderer>().material.mainTexture;
                    Texture2D animTexture = (Texture2D)spriteRenderer.sprite.texture;

                    int uvX = Mathf.FloorToInt(textureCoord.x * animTexture.width);
                    int uvY = Mathf.FloorToInt(textureCoord.y * animTexture.height);

                    Color hitColor = animTexture.GetPixel(uvX, uvY);
                    if (hitColor.a > 0.05)
                    {
                        Color pxColor = dataTexture.GetPixel(uvX, uvY);
                        if (pxColor.a > 0.05)
                        {
                            Debug.Log("The coordinates " + uvX + "/" + uvY + " in " + hitTransform.name + " IS NOT ALPHA | GOLPE CRITICO");
                        }
                        else
                        {
                            Debug.Log("The coordinates " + uvX + "/" + uvY + " in " + hitTransform.name + " IS NOT ALPHA");
                        }
                    }
                    else
                    {
                        Debug.Log("The coordinates " + uvX + "/" + uvY + " in " + hitTransform.name + " IS ALPHA");
                    }
                    */
                }
            }
        }        
    }
}

/*
        RaycastHit hit = new RaycastHit();
        Ray ray = new Ray(collision.GetContact(0).point, -collision.GetContact(0).normal);
        if (Physics.Raycast(ray, out hit))
        {
            if (hit.transform.CompareTag("Enemy") && hit.transform.parent != null)
            {
                if (hit.transform.parent.gameObject.GetComponentInChildren<SpriteRenderer>() != null)
                {
                    print("Projectile Contact Point: " + collision.contacts[0].point
                    + " with Normal: " + collision.contacts[0].normal
                    + " | Texture Coord Hit is: " + hit.textureCoord);

                    //Get Textures
                    Texture2D dataTexture = (Texture2D)hit.collider.GetComponent<MeshRenderer>().material.mainTexture;
                    Texture2D animTexture = (Texture2D)hit.transform.parent.gameObject.GetComponentInChildren<SpriteRenderer>().sprite.texture;

                    //Convert hit coordinates
                    int uvX = Mathf.FloorToInt(hit.textureCoord.x * animTexture.width);
                    int uvY = Mathf.FloorToInt(hit.textureCoord.y * animTexture.height);

                    //Alpha Check                    
                    Color hitColor = animTexture.GetPixel(uvX, uvY);
                    if (hitColor.a > 0.05)
                    {
                        Color criticColor = dataTexture.GetPixel(uvX, uvY);
                        if (criticColor.a > 0.05)
                        {
                            print("The coordinates " + uvX + "/" + uvY + " in " + hit.transform.name + " IS NOT ALPHA | GOLPE CRITICO");
                        }
                        else
                        {
                            print("The coordinates " + uvX + "/" + uvY + " in " + hit.transform.name + " IS NOT ALPHA");
                        }
                    }
                    else
                    {
                        Debug.Log("The coordinates " + uvX + "/" + uvY + " in " + hit.transform.name + " IS ALPHA");
                    }
                }
            }
        }


            /*

            if (hit.transform.CompareTag("Enemy") && hit.transform.parent != null && hit.transform.parent.gameObject.GetComponentInChildren<SpriteRenderer>() != null)
            {
                //print("Ocurrio Colision con " + collision.gameObject.name);
                print("Projectile Contact Point: " + collision.contacts[0].point 
                    + " with Normal: " + collision.contacts[0].normal
                    + " | Texture Coord Hit is: " + hit.textureCoord);

                Texture2D characterTexture =
                (Texture2D)hit.transform.parent.gameObject.GetComponentInChildren<SpriteRenderer>().sprite.texture;
                Texture2D CriticAreaTexture = (Texture2D)hit.collider.GetComponent<MeshRenderer>().material.mainTexture;

                //Convert hit coordinates
                Vector2 pixelUV = hit.textureCoord;
                int uvX = Mathf.FloorToInt(pixelUV.x * characterTexture.width);
                int uvY = Mathf.FloorToInt(pixelUV.y * characterTexture.height);

                //Alpha Check                    
                Color hitColor = characterTexture.GetPixel(uvX, uvY);
                if (hitColor.a > 0.05)
                {

                    Color criticColor = CriticAreaTexture.GetPixel(uvX, uvY);
                    if (criticColor.a > 0.05)
                    {
                        print("The coordinates " + uvX + "/" + uvY + " in " + hit.transform.name + " IS NOT ALPHA | GOLPE CRITICO");
                    }
                    else print("The coordinates " + uvX + "/" + uvY + " in " + hit.transform.name + " IS NOT ALPHA");
                }
                else
                {
                    Debug.Log("The coordinates " + uvX + "/" + uvY + " in " + hit.transform.name + " IS ALPHA");
                    //Physics.IgnoreCollision(collision.collider, GetComponent<Collider>());
                }
            }
            */

/*
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
*/
