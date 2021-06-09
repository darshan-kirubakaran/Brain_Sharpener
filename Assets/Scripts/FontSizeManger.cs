using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FontSizeManger : MonoBehaviour
{
    public bool headingText;
    public bool subheadingText;
    public bool subsubheadingText;

    float headingFontSize = 70;
    float subheadingFontSize = 50;
    float subsubheadingFontSize = 12;

    // Start is called before the first frame update
    void Start()
    {
        if(headingText)
        {
            this.gameObject.GetComponent<TextMeshProUGUI>().fontSize = headingFontSize;
        }
        
        if(subheadingText)
        {
            this.gameObject.GetComponent<TextMeshProUGUI>().fontSize = subheadingFontSize;
        }
        
        if(subsubheadingText)
        {
            this.gameObject.GetComponent<TextMeshProUGUI>().fontSize = subsubheadingFontSize;
        }
    }
}
