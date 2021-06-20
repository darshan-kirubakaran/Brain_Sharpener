using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MA_ChoiceManager : MonoBehaviour
{

    public Button[] choices;

    MA_Checker ma_Checker;
    Spawner spawner;

    private void Start()
    {
        ma_Checker = FindObjectOfType<MA_Checker>();
        spawner = FindObjectOfType<Spawner>();
    }

    public void ChoiceValueChanger()
    {
        foreach(Button choice in choices)
        {
            choice.GetComponentInChildren<TextMeshProUGUI>().text = spawner.BlockQueue.Peek().name;
        }
    }

    public void ChoiceButton()
    {
        ma_Checker.CheckMathAnswer(this.gameObject.GetComponentInChildren<TextMeshProUGUI>().text);
    }
    
    public void Choice1()
    {
        ma_Checker.CheckMathAnswer(choices[0].gameObject.GetComponentInChildren<TextMeshProUGUI>().text);
    }
    
    public void Choice2()
    {
        ma_Checker.CheckMathAnswer(choices[1].gameObject.GetComponentInChildren<TextMeshProUGUI>().text);
    }
    public void Choice3()
    {
        ma_Checker.CheckMathAnswer(choices[2].gameObject.GetComponentInChildren<TextMeshProUGUI>().text);
    }
    public void Choice4()
    {
        ma_Checker.CheckMathAnswer(choices[3].gameObject.GetComponentInChildren<TextMeshProUGUI>().text);
    }
    public void Choice5()
    {
        ma_Checker.CheckMathAnswer(choices[4].gameObject.GetComponentInChildren<TextMeshProUGUI>().text);
    }
    public void Choice6()
    {
        ma_Checker.CheckMathAnswer(choices[5].gameObject.GetComponentInChildren<TextMeshProUGUI>().text);
    }

}
