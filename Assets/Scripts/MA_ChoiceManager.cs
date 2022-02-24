using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using System.Linq;

public class MA_ChoiceManager : MonoBehaviour
{

    public GameObject[] choices;

    MA_Checker ma_Checker;
    Spawner spawner;

    private void Awake()
    {
        ma_Checker = FindObjectOfType<MA_Checker>();
        spawner = FindObjectOfType<Spawner>();
    }

    public void ChoiceValueChanger()
    {
        int answer = int.Parse(spawner.BlockQueue.Peek().gameObject.name);

        List<int> choiceList = CreateChoiceList(answer);

        foreach (GameObject choice in choices)
        {
            var val = UnityEngine.Random.Range(0, choiceList.Count);

            choice.GetComponentInChildren<TextMeshProUGUI>().text = choiceList[val].ToString();

            choiceList.RemoveAt(val);
        }
    }

    private List<int> CreateChoiceList(int answer)
    {
        int[] choiceArray = { answer, answer + 1, answer + 4, answer - 1, answer - 3, answer + 5 };

        List<int> choiceList = choiceArray.ToList();

        return choiceList;
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
