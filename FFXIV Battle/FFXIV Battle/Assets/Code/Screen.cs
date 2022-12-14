using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//A representation of a UI Canvas, for setting defaults and being organized and affected by the GameController
public class Screen : MonoBehaviour
{
    private void OnEnable()
    {
        UIElement[] elements = GetComponentsInChildren<UIElement>();
        foreach(UIElement element in elements)
        {
            element.highlightedBy = 0;
            element.SetHighlightImage();
        }
    }
    public UIElement defaultElement; 
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
