using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddMapPoint : MonoBehaviour
{
    [SerializeField] Color mapPointColor;
    [SerializeField] GameObject mapPointPrefab;


    void Start()
    {
        AddMapPointWithColor(mapPointColor);
    }

    void AddMapPointWithColor(Color mapPointColor)
    {
        GameObject mapPoint = Instantiate(mapPointPrefab, transform.position, Quaternion.identity);
        mapPoint.transform.SetParent(transform);
        mapPoint.transform.position += new Vector3(0,3,0);
        MapPointColorHandler mapPointHandler = mapPoint.GetComponent<MapPointColorHandler>();
        mapPointHandler.SetMapPointColor(mapPointColor);
    }
}
