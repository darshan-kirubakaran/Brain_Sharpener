using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class NumBlock : MonoBehaviour
{
    int numOfRows;
    int numOfColumns;

    CubeSpawner cubeSpawner;
    NumberChecker numberChecker;

    // Start is called before the first frame update
    void Start()
    {
        cubeSpawner = FindObjectOfType<CubeSpawner>();
        numberChecker = FindObjectOfType<NumberChecker>();

        this.transform.localScale = new Vector3(1, 1, 0);

        numOfRows = (int)Mathf.Sqrt(cubeSpawner.numberOfBlock);
        numOfColumns = (int)Mathf.Sqrt(cubeSpawner.numberOfBlock);

        this.GetComponent<RectTransform>().sizeDelta = new Vector2(cubeSpawner.spawnArea.sizeDelta.x / numOfColumns, cubeSpawner.spawnArea.sizeDelta.y / numOfRows);

        Button btn = this.GetComponent<Button>();
        btn.onClick.AddListener(TaskOnClick);

    }

    private void TaskOnClick()
    {
        numberChecker.CheckIfRight(int.Parse(this.GetComponentInChildren<TextMeshProUGUI>().text), this.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
