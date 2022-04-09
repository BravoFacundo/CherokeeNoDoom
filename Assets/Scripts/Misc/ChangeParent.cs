using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Simple script to change an object's order in the hierarchy
//In this project this script is used to facilitate the placement of some objects relative to others.
public class ChangeParent : MonoBehaviour
{
   
    void Awake()
    {
        //Make parent of parent the parent of this object.
        gameObject.transform.parent = gameObject.transform.parent.parent;
    }

}
