using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    [Header("Menu Configuration")]
    [SerializeField] float buttonActionDelay = 1f;

    [Header("Navigation")]
    [SerializeField] GameObject screenMenu;
    [SerializeField] GameObject screenTutorial;

    public void PlayButtonPressed() => StartCoroutine(nameof(PlayButton));
    private IEnumerator PlayButton()
    {
        yield return new WaitForSeconds(buttonActionDelay);
        screenTutorial.SetActive(true);
        screenMenu.SetActive(false);
    }

    public void StartButtonPressed() => StartCoroutine(nameof(StartButton));
    private IEnumerator StartButton()
    {        
        yield return new WaitForSeconds(buttonActionDelay);
        SceneManager.LoadScene("Gameplay");
    }

    public void ExitButtonPressed() => StartCoroutine(nameof(ExitButton));
    private IEnumerator ExitButton()
    {
        yield return new WaitForSeconds(buttonActionDelay);
        Application.Quit();
    }
}
