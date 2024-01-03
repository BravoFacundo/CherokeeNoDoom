using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;
using UnityEngine;

public class PlayerHUD : MonoBehaviour
{
    [Header("Score")]
    public int enemyKillCount;
    [SerializeField] private TMP_Text text_Kills;
    [SerializeField] float timeAlive;
    [SerializeField] private TMP_Text text_Time;
    private bool timeIsOnPause = false;

    [Header("Hitmarker")]
    [SerializeField] GameObject Crosshair;
    [SerializeField] GameObject HitMark_Normal;
    [SerializeField] GameObject HitMark_Critical;
    [SerializeField] GameObject HitMark_Kill;

    [Header("Skills CoolDown")]
    [SerializeField] Button dashSkill;

    [Header("Navigation")]
    [SerializeField] GameObject screenDeath;

    private void Start()
    {
        timeAlive = 0.0f;
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
            timeAlive += Time.deltaTime;
            text_Time.text = NumberToText(timeAlive);
        }
    }
    public void PauseTime() => timeIsOnPause = true;
    public void ContinueTime() => timeIsOnPause = false;

    public string NumberToText(float time)
    {
        int minutes = Mathf.FloorToInt(time / 60);
        int seconds = Mathf.FloorToInt(time % 60);
        return string.Format("{0:00}:{1:00}", minutes, seconds);
    }
    public float TextToNumber(string text)
    {
        string[] parts = text.Split(':');
        int minutes = int.Parse(parts[0]);
        int seconds = int.Parse(parts[1]);
        return minutes * 60 + seconds;
    }

    public void CleanScore()
    {
        timeAlive = 0; text_Time.text = "00:00";
        enemyKillCount = 0; text_Kills.text = "000";
        timeIsOnPause = false;
    }
    public void SaveScore()
    {
        Debug.Log("Tiempo guardado: " + timeAlive);
    }

    public void UpdateEnemyKills()
    {
        enemyKillCount++;
        text_Kills.text = enemyKillCount.ToString("D3");
    }

    //---------- SKILLS -----------------------------------------------------------------------------------------------------------------//

    public void DashSkill(bool activeState) => dashSkill.interactable = activeState;

    //---------- NAVIGATION -----------------------------------------------------------------------------------------------------------------//

    public void DeathScreen(bool activeState) => screenDeath.SetActive(activeState);

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
