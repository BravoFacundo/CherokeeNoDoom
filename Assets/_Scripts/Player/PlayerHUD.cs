using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;
using UnityEngine;

public class PlayerHUD : MonoBehaviour
{
    [Header("Score")]
    [SerializeField] float currentTimeAlive;
    public int currentKillCount;
    [SerializeField] bool timeIsOnPause = false;
    [SerializeField] private TMP_Text HUD_Time;
    [SerializeField] private TMP_Text HUD_Kills;
    [SerializeField] private TMP_Text Current_Time;
    [SerializeField] private TMP_Text Current_Kills;
    [SerializeField] private TMP_Text Best_Time;
    [SerializeField] private TMP_Text Best_Kills;

    [Header("Hitmarker")]
    [SerializeField] GameObject Crosshair;
    [SerializeField] GameObject HitMark_Normal;
    [SerializeField] GameObject HitMark_Critical;
    [SerializeField] GameObject HitMark_Kill;

    [Header("Skills CoolDown")]
    [SerializeField] Button dashSkill;

    [Header("Navigation")]
    [SerializeField] GameObject screenObjetive;
    [SerializeField] GameObject screenDeath;

    [Header("References")]
    [SerializeField] PlayerScore playerScore;

    private void Start()
    {
        currentTimeAlive = 0.0f;
    }
    private void Update()
    {
        CountTime();
    }

    //---------- SCORE -----------------------------------------------------------------------------------------------------------------//

    private void CountTime()
    {
        if (!timeIsOnPause)
        {
            currentTimeAlive += Time.deltaTime;
            HUD_Time.text = playerScore.NumberToText(currentTimeAlive);
        }
    }
    public void PauseTime() => timeIsOnPause = true;
    public void ContinueTime() => timeIsOnPause = false;

    public void UpdateEnemyKills()
    {
        currentKillCount++;
        HUD_Kills.text = currentKillCount.ToString("D3");
    }

    public void CleanScore()
    {
        currentTimeAlive = 0; HUD_Time.text = "00:00";
        currentKillCount = 0; HUD_Kills.text = "000";
        timeIsOnPause = false;
    }

    //---------- SKILLS -----------------------------------------------------------------------------------------------------------------//

    public void DashSkill(bool activeState) => dashSkill.interactable = activeState;

    //---------- NAVIGATION -----------------------------------------------------------------------------------------------------------------//

    public void ObjetiveScreen(bool activeState) => screenObjetive.SetActive(activeState);
    public void DeathScreen(bool activeState) => screenDeath.SetActive(activeState);
    public void PlayerDie()
    {
        PauseTime();
        ObjetiveScreen(false);
        DeathScreen(true);
                
        ScoreData currentScore = new ScoreData();
        currentScore.timeAlive = playerScore.NumberToText(currentTimeAlive);
        currentScore.killCount = currentKillCount.ToString("D3");
        
        Current_Time.text = currentScore.timeAlive;
        Current_Kills.text = currentScore.killCount;

        ScoreData bestScore;
        if (!playerScore.CheckForSavedData()) bestScore = currentScore;
        else bestScore = playerScore.CheckForBestScore(currentScore);

        Best_Time.text = bestScore.timeAlive;
        Best_Kills.text = bestScore.killCount;

        playerScore.SaveToJson(bestScore);
    } 

    //---------- CROSSHAIR -----------------------------------------------------------------------------------------------------------------//

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
