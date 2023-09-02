using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class ChangeParent : MonoBehaviour
{
    void Awake()
    {
        gameObject.transform.parent = gameObject.transform.parent.parent;
    }
}
