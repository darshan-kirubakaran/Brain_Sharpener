using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Threading;

public class MemoryMatrixColorBlock : MonoBehaviour
{
    public bool rightCube;
    public bool hasBeenClicked = false;
    public bool canClick = false;

    bool canChangeColor = true;

    int numOfRows;
    int numOfColumns;

    CubeSpawner cubeSpawner;
    NumberChecker numberChecker;
    Image image;

    private void Awake()
    {
        cubeSpawner = FindObjectOfType<CubeSpawner>();
        numberChecker = FindObjectOfType<NumberChecker>();

        image = this.gameObject.GetComponent<Image>();
    }

    // Start is called before the first frame update
    void Start()
    {
        this.transform.localScale = new Vector3(1, 1, 0);

        numOfRows = (int)Mathf.Sqrt(cubeSpawner.numberOfBlock);
        numOfColumns = (int)Mathf.Sqrt(cubeSpawner.numberOfBlock);

        this.GetComponent<RectTransform>().sizeDelta = new Vector2(cubeSpawner.spawnArea.sizeDelta.x / numOfColumns, cubeSpawner.spawnArea.sizeDelta.y / numOfRows);

        Button btn = this.GetComponent<Button>();
        btn.onClick.AddListener(TaskOnClick);

    }

    private void TaskOnClick()
    {
        if(canClick)
        {
            hasBeenClicked = true;
            numberChecker.CheckMemoryMatrix(rightCube);
            if(rightCube)
            {
                image.color = cubeSpawner.rightColor;
            }
        }
    }

    public void ChangeColorOfCube(float waitTime)
    {
        FindObjectOfType<CubeSpawner>().waitTime = 2;
        StartCoroutine(ChangeColorOfCubeImpl(waitTime));
    }

    IEnumerator ChangeColorOfCubeImpl(float timeToWait)
    {
        yield return new WaitForSeconds(timeToWait);
        this.gameObject.GetComponent<Image>().color = cubeSpawner.baseColor;
        canClick = true;
        canChangeColor = false;
    }

    public void ChangeColorOfCubeToDefault()
    {
        cubeSpawner = FindObjectOfType<CubeSpawner>();
        if (rightCube)
        {
            image.color = cubeSpawner.rightColor;
        }
        else
        {
            image.color = cubeSpawner.baseColor;
        }

        canClick = false;
        canChangeColor = true;
    }
    
    public void ChangeColorOfCubeToBase()
    {
        cubeSpawner = FindObjectOfType<CubeSpawner>();
        image.color = cubeSpawner.baseColor;

        canClick = true;
        canChangeColor = false;
    }
}
