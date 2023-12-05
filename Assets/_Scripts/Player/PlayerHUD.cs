using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class PlayerHUD : MonoBehaviour
{
    [SerializeField] private Button dashSkill;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DashSkill(bool activeState)
    {
        dashSkill.interactable = activeState;
    }
}
