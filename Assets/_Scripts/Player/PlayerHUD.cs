using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class PlayerHUD : MonoBehaviour
{
    [Header("Skills CoolDown")]
    [SerializeField] private Button dashSkill;

    public void DashSkill(bool activeState)
    {
        dashSkill.interactable = activeState;
    }
}
