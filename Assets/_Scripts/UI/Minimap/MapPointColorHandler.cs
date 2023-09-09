using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapPointColorHandler : MonoBehaviour
{
    public void SetMapPointColor(Color mapPointColor)
    {
        Renderer renderer = GetComponent<Renderer>();
        if (renderer != null)
        {
            Material materialActual = renderer.material;
            materialActual.color = mapPointColor;
        }
    }
}
