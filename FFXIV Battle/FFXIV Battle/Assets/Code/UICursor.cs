using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//PlayerController calls functions on this script based on player Inputs
public class UICursor : MonoBehaviour
{
    public UIElement highlightedElement;
    public int playerNumber;
    
    public void Highlight(UIElement element)
    {
        highlightedElement = element;
        element.Highlight();
    }
    // Start is called before the first frame update
    void Start()
    {
        FindObjectOfType<GameController>().ResetCursor(this);
        playerNumber = FindObjectsOfType<UICursor>().Length;
    }

    public void Select()
    {
        highlightedElement.Select(this);
    }

    public void Up()
    {
        highlightedElement.Up(this);
    }
    public void Down()
    {
        highlightedElement.Down(this);
    }
    public void Left()
    {
        highlightedElement.Left(this);
    }
    public void Right()
    {
        highlightedElement.Right(this);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
