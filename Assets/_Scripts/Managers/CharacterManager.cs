using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterManager : MonoBehaviour
{
    [SerializeField] private int activeCharacter = 0;
    [SerializeField] GameObject[] characters;

    private void Start()
    {
        if (GameObject.FindGameObjectWithTag("Player") == null)
        {
            Transform player = Instantiate(characters[activeCharacter].transform, transform);
            player.parent = null;
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Keypad1))
        {
            ChangeCharacter(0);
        }
        else if (Input.GetKeyDown(KeyCode.Keypad2))
        {
            ChangeCharacter(1);
        }
        else if (Input.GetKeyDown(KeyCode.Keypad3))
        {
            ChangeCharacter(2);
        }
    }

    void ChangeCharacter(int newCharacter)
    {
        Destroy(GameObject.FindGameObjectWithTag("Player"));
        Destroy(GameObject.FindGameObjectWithTag("CameraHolder"));
        
        Transform newPlayer = Instantiate(characters[newCharacter].transform, transform);
        newPlayer.parent = null;
        
        activeCharacter = newCharacter;
    }
}
