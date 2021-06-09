using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Firebase.Database;

public class CubeSpawner : MonoBehaviour
{
    public int numberOfBlock = 25;
    public int highestNumber;
    public float paddingAmount;
    public bool continuousMode;
    public GameObject numBlock;
    public Transform numCubeParent;
    public RectTransform spawnArea;
    public List<int> blockNumbers;
    public Color[] colors;
    public bool MemoryMatrix;
    public Color baseColor;
    public Color rightColor;
    public int totalRightCubes;
    public float waitTime = 5;

    int numOfRows;
    int numOfColumns;

    public bool finishedSpawning = false;

    NumberChecker numberChecker;

    // Start is called before the first frame update
    void Awake()
    {
        numberChecker = FindObjectOfType<NumberChecker>();

        highestNumber = numberOfBlock;

        if(!MemoryMatrix)
        {
            NewSpawnMethods(0);
        }
    }

    public void NewSpawnMethods(int rightCubes)
    {
        AjustSizeAccorddingToGrid();
        CreateList();
        SpawnCubes(rightCubes);
    }

    private void AjustSizeAccorddingToGrid()
    {
        // Set size of Grid Layout Group to change the size of the numCube to scale with total number of cubes and spawn area length
        numOfRows = (int)Mathf.Sqrt(numberOfBlock);
        numOfColumns = (int)Mathf.Sqrt(numberOfBlock);
        spawnArea.GetComponent<GridLayoutGroup>().cellSize = new Vector2((spawnArea.sizeDelta.x - (paddingAmount * numOfColumns)) / numOfColumns, (spawnArea.sizeDelta.y - (paddingAmount * numOfRows)) / numOfRows);
        spawnArea.GetComponent<GridLayoutGroup>().spacing = new Vector2 (paddingAmount, paddingAmount);
    }

    private void CreateList()
    {
        int Count = 1;

        while (Count <= numberOfBlock)
        {
            blockNumbers.Add(Count);
            Count++;
        }
    }

    private void SpawnCubes(int rightCubes)
    {
        totalRightCubes = rightCubes;

        var currentspawnNum = 1;

        while(currentspawnNum <= numberOfBlock)
        {
            if(MemoryMatrix)
            {
                var textComponent = numBlock.GetComponentInChildren<TextMeshProUGUI>();
                var randomNum = UnityEngine.Random.Range(0, blockNumbers.Count);

                if (blockNumbers[randomNum] <= totalRightCubes)
                {
                    numBlock.GetComponent<Image>().color = rightColor;
                    numBlock.GetComponent<MemoryMatrixColorBlock>().rightCube = true;
                }
                else
                {
                    numBlock.GetComponent<Image>().color = baseColor;
                    numBlock.GetComponent<MemoryMatrixColorBlock>().rightCube = false;
                }

                blockNumbers.RemoveAt(randomNum);

                var instansiatedBlock = Instantiate(numBlock, numCubeParent.transform.position, Quaternion.identity);
                instansiatedBlock.transform.SetParent(numCubeParent);

                currentspawnNum += 1;
            }
            else
            {
                var textComponent = numBlock.GetComponentInChildren<TextMeshProUGUI>();
                var randomNum = UnityEngine.Random.Range(0, blockNumbers.Count);

                textComponent.text = blockNumbers[randomNum].ToString();
                numBlock.name = blockNumbers[randomNum].ToString();

                blockNumbers.RemoveAt(randomNum);

                numBlock.GetComponentInChildren<TextMeshProUGUI>().color = colors[UnityEngine.Random.Range(0, colors.Length)];

                var instansiatedBlock = Instantiate(numBlock, numCubeParent.transform.position, Quaternion.identity);
                instansiatedBlock.transform.SetParent(numCubeParent);

                currentspawnNum += 1;
            }
        }
    }

    public void ClearNumCubes()
    {
        foreach (Transform child in numCubeParent.transform)
        {
            GameObject.Destroy(child.gameObject);
        }
    }
}
