using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameController : MonoBehaviour
{
    public Screen screen;

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
        cursor.Highlight(screen.defaultElement);
    }
}