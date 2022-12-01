using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public Screen screen;

    public static GameController Instance;
    public static List<GameObject> players = new List<GameObject>();
    public int lives  = 2;
    public Screen gameOver;
    public Screen gameEnd;

    public void GameEnd()
    {
        PlayerCharacterController[] playas = FindObjectsOfType<PlayerCharacterController>();
        foreach (PlayerCharacterController player in playas)
        {
            player.GetComponentInParent<PlayerController>().controllingUI = true;
            Destroy(player.gameObject);
        }
        Instance.ChangeScreen(gameEnd);
    }

    public Vector3 GetPlayerSpawn()
    {
        return FindObjectOfType<LevelManager>().NextSpawn();

    }

    private void Awake()
    {
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }



    public static void AddPlayer(GameObject player)
    {
        players.Add(player);
        DontDestroyOnLoad(player);
    }


    public static void StartGame(Screen screen)
    {
        if (AllPlayersReady())
        {
            Instance.ChangeScreen(screen);
            SceneManager.LoadScene(1);
            SceneManager.LoadScene(1, LoadSceneMode.Single);
        }
    }
    public void GameOver()
    {
        foreach(GameObject player in players)
        {
            player.GetComponent<PlayerController>().controllingUI = true;
            Destroy(player.gameObject);
        }
        Instance.ChangeScreen(gameOver);
    }

    public void GiveCharacterToPlayer(PlayerController pc, GameObject character)
    {
        
        GameObject playerChar = Instantiate(character, pc.gameObject.transform);
        pc.characterController = playerChar.GetComponent<PlayerCharacterController>();
        pc.controllingUI = false;
        if (pc.playerNumber > 1)
        {
            playerChar.GetComponentInChildren<SpriteRenderer>().color = Color.blue;
        }
        pc.characterController.Spawn();
    }

    public void SpawnPlayers(Transform[] spawns)
    {
        Character_Controller[] players = FindObjectsOfType<Character_Controller>();
        int i = 0;
        foreach(Character_Controller player in players)
        {
            player.transform.position = spawns[i].transform.position;
            i++;
        }
    }

    public static bool AllPlayersReady()
    {
        PlayerCharacterController[] characters = GameObject.FindObjectsOfType<PlayerCharacterController>();
        PlayerController[] players = GameObject.FindObjectsOfType<PlayerController>();
        if (characters.Length == players.Length)
        {
            return true;
        }
        return false;
    }

    public void EnableJoining()
    {
     
        FindObjectOfType<PlayerInputManager>().EnableJoining();
    }

    public void DisableJoining()
    {
        FindObjectOfType<PlayerInputManager>().DisableJoining();
    }

    public void ChangeScreen (Screen newScreen)
    {
        
        //Fade Effect or animation
        screen.gameObject.SetActive(false);
        //Reset newScreen to defaults if they exist: IE, if you want a fade in, make it invisible.
        newScreen.gameObject.SetActive(true);
        screen = newScreen;
        UICursor[] cursors = FindObjectsOfType<UICursor>();
        foreach (UICursor cursor in cursors)
        {
            ResetCursor(cursor);
        }
    }

    public void ResetCursor(UICursor cursor)
    {
        if(screen != null)
        cursor.Highlight(screen.defaultElement);
    }
}