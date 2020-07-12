using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TextEdit : MonoBehaviour
{
   // public static Color CurrentSelectedColor = Color.black;
   // public static int CurrentSelectedFontSize1 = 14;
    public TMP_InputField tmpInput;
     // Start is called before the first frame update
    void Start()
    {

        tmpInput.onSelect.AddListener(OnTextInputSelect);

    }

    void OnTextInputSelect(string text)
    {
        Debug.Log("OnTextInputSelect:" + text);
        HeaderTextScript.selectd_TMP_InputField = tmpInput;
    //    if(HeaderTextScript.CurrentSelectedColor
    //     tmpInput.textComponent.color = HeaderTextScript.CurrentSelectedColor;
    //tmpInput.textComponent.fontSize = HeaderTextScript.CurrentSelectedFontSize;
    }

    // Update is called once per frame
        void Update()
    {
        
    }
}
