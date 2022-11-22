using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//Individual buttons or graphics in the UI. UICursor calls functions on this script. Classes 
//extending this script can therefore implement unique functionality. EX: A scrollbar that scrolls up
//and down when up and down are clicked, rather than moving to the above or below element. This script
//is a generic version with the most common functions (or the first I think of, to be honest.)

public class UIElement : MonoBehaviour
{
    public Sprite normal;
    public Sprite highlighted;
    public Sprite highlighted2;
    public Sprite highlighted3;
    public UIElement above;
    public UIElement below;
    public UIElement left;
    public UIElement right;
    public Screen nextScreen;
    public int highlightedBy; //Number of players highlighting

    public virtual void Select(UICursor cursor)
    {
        if(nextScreen != null)
        FindObjectOfType<GameController>().ChangeScreen(nextScreen);
    }

    public void Deselect()
    {
        highlightedBy -= 1;
        SetHighlightImage();
    }

    public void SetHighlightImage()
    {
        switch (highlightedBy)
        {
            case 0:
                GetComponent<Image>().sprite = normal;
                break;
            case 1:
                GetComponent<Image>().sprite = highlighted;
                break;
            case 2:
                GetComponent<Image>().sprite = highlighted2;
                break;
        }
    }

    public void Highlight()
    {
        highlightedBy += 1;
        SetHighlightImage();
    }
    public void Up(UICursor cursor)
    {
        if (above != null)
        {
            Deselect();
            cursor.Highlight(above);
        }
    }
    public void Down(UICursor cursor)
    {
        if (below != null)
        {
            Deselect();
            cursor.Highlight(below);
        }
            
    }
    public void Left(UICursor cursor)
    {
        if(left != null)
        {
            Deselect();
            cursor.Highlight(left);
        }
        
    }
    public void Right(UICursor cursor)
    {
        if(right != null)
        {
            Deselect();
            cursor.Highlight(right);
        }
        
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
