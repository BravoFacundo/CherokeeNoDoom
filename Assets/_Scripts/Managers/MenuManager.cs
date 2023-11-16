using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    [Header("Menu Configuration")]
    [SerializeField] private float buttonActionDelay = 1f;

    public void PlayButtonPressed() => StartCoroutine(nameof(PlayButton));
    private IEnumerator PlayButton()
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
