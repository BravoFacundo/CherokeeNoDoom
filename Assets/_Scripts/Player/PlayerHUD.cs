using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;
using UnityEngine;

public class PlayerHUD : MonoBehaviour
{
    [Header("Hitmarker")]
    [SerializeField] GameObject Crosshair;
    [SerializeField] GameObject HitMark_Normal;
    [SerializeField] GameObject HitMark_Critical;
    [SerializeField] GameObject HitMark_Kill;

    [Header("Skills CoolDown")]
    [SerializeField] Button dashSkill;

    [Header("Navigation")]
    [SerializeField] GameObject screenDeath;

    public void DashSkill(bool activeState) => dashSkill.interactable = activeState;

    public void DeathScreen(bool activeState) => screenDeath.SetActive(activeState);

    public void NormalHit()
    {
        if (!HitMark_Normal.activeSelf)
        {
            HitMark_Normal.SetActive(true);
            Invoke(nameof(NormalHit), 0.25f);
        }
        else HitMark_Normal.SetActive(false);
    }
    public void CriticalHit()
    {
        if (!HitMark_Critical.activeSelf)
        {
            HitMark_Critical.SetActive(true);
            Invoke(nameof(CriticalHit), 0.3f);
        }
        else HitMark_Critical.SetActive(false);
    }
    public void KillHit()
    {
        if (!HitMark_Kill.activeSelf)
        {
            Crosshair.SetActive(false);
            HitMark_Kill.SetActive(true);
            Invoke(nameof(KillHit), 0.3f);
        }
        else
        {
            Crosshair.SetActive(true);
            HitMark_Kill.SetActive(false);
        }
    }

}
