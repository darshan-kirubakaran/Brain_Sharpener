using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class WindowGraph : MonoBehaviour
{
    [SerializeField] private Sprite circleSprite;
    public RectTransform graphContainer;
    private RectTransform labelTemplateX;
    private RectTransform labelTemplateY;
    private RectTransform lineTemplateX;
    private RectTransform lineTemplateY;
    public float yMaximum;
    public string gameName;

    private void Awake()
    {
        graphContainer = graphContainer.GetComponent<RectTransform>();
        labelTemplateX = graphContainer.Find("Label TemplateX").GetComponent<RectTransform>();
        labelTemplateY = graphContainer.Find("Label TemplateY").GetComponent<RectTransform>();
        lineTemplateX = graphContainer.Find("xAxisLine").GetComponent<RectTransform>();
        lineTemplateY = graphContainer.Find("yAxisLine").GetComponent<RectTransform>();

        lineTemplateX.sizeDelta = new Vector2(graphContainer.sizeDelta.x, 2f);
        lineTemplateY.sizeDelta = new Vector2(2f, graphContainer.sizeDelta.y);

        PlayerData data = SaveSystem.LoadPlayer(gameName);

        //List<int> valueList = new List<int>();

        if(data != null)
        {
            ShowGraph(data.scores);
        }

        //List<int> valueList = new List<int> { 60, 10, 20, 31, 25, 56, 90, 60, 10, 22, 33, 50, 60, 10, 20, 31, 25, 56, 90, 60, 10, 22, 33, 50, 30, 22, 5, 40, 99, 20, 30, 50, 66, 70, 18, 98, 60, 10, 20, 31, 25, 56, 90, 60, 10, 22, 33, 50, 60, 10, 20, 31, 25, 56, 90, 60, 10, 22, 33, 50, 30, 22, 5, 40, 99, 20, 30, 50, 66, 70, 18, 98, 60, 10, 20, 31, 25, 56, 90, 60, 10, 22, 33, 50, 60, 10, 20, 31, 25, 56, 90, 60, 10, 22, 33, 50, 30, 22, 5, 40, 99, 20, 30, 50, 66, 70, 18, 98 };
        //List<int> valueList = new List<int> { 60, 10, 20 };
        //ShowGraph(valueList);
    }

    private GameObject CreateCircle(Vector2 anchoredPosition)
    {
        GameObject gameObject = new GameObject("circle", typeof(Image));
        gameObject.transform.SetParent(graphContainer, false);
        gameObject.GetComponent<Image>().sprite = circleSprite;
        RectTransform rectTransform = gameObject.GetComponent<RectTransform>();
        rectTransform.anchoredPosition = anchoredPosition;
        rectTransform.sizeDelta = new Vector2(11, 11);
        rectTransform.anchorMin = new Vector2(0, 0);
        rectTransform.anchorMax = new Vector2(0, 0);
        return gameObject;
    }

    private void ShowGraph(List<int> valueList)
    {
        float graphHeight = graphContainer.sizeDelta.y;
        float yMaximum = valueList.Max();
        yMaximum = yMaximum + (25 - (yMaximum % 25));
        var xMaximum = valueList.Count;
        xMaximum = xMaximum + (25 - (xMaximum % 25));
        float xSize = graphContainer.sizeDelta.x / 5;
        float ySize = graphContainer.sizeDelta.y / 5;

        GameObject lastCircleGameObject = CreateCircle(new Vector2(0, 0));
        //xSize = xSize / (unitsCount / 5);

        for(int i = 0; i < valueList.Count; i++)
        {
            float xPosition;
            // setting xSize value for 2 cases(when total game number is under 5 and when total game number is above 5)
            if (valueList.Count < 5)
            {
                xPosition = (xSize) + i * (xSize);
            }
            else
            {
                xPosition = (xSize / (xMaximum / 5)) + i * (xSize / (xMaximum / 5));
            }
            //float xPosition = (xSize / (xMaximum / 5)) + i * (xSize / (xMaximum / 5));
            float yPosition = valueList[i] * (ySize / (yMaximum / 5));
            GameObject circleGameObject = CreateCircle(new Vector2(xPosition, yPosition));
            CreateDotConnection(lastCircleGameObject.GetComponent<RectTransform>().anchoredPosition, circleGameObject.GetComponent<RectTransform>().anchoredPosition);
            lastCircleGameObject = circleGameObject;

        }

        int seperatorCount = 5;

        // for Y-Axis
        for (int i = 0; i <= seperatorCount; i++)
        {
            float yPosition = i * ySize;

            RectTransform labelY = Instantiate(labelTemplateY);
            labelY.SetParent(graphContainer, false);
            labelY.gameObject.SetActive(true);
            float normalizedValue = i * 1f / seperatorCount;
            labelY.anchoredPosition = new Vector2(-7f, yPosition);
            labelY.GetComponent<Text>().text = ((yMaximum / 5) * i).ToString();

        }
        
        // for X-Axis
        for (int i = 0; i <= seperatorCount; i++)
        {
            float xPosition = i * xSize;

            if (valueList.Count > 5)
            {
                RectTransform labelX = Instantiate(labelTemplateX);
                labelX.SetParent(graphContainer);
                labelX.gameObject.SetActive(true);
                labelX.anchoredPosition = new Vector2(xPosition, -20f);
                labelX.GetComponent<Text>().text = ((xMaximum / 5) * i).ToString();
            }
            else
            {
                RectTransform labelX = Instantiate(labelTemplateX);
                labelX.SetParent(graphContainer);
                labelX.gameObject.SetActive(true);
                labelX.anchoredPosition = new Vector2(xPosition, -20f);
                labelX.GetComponent<Text>().text = i.ToString();
            }

        }
    }

    private void CreateDotConnection(Vector2 dotPositionA, Vector2 dotPositionB)
    {
        GameObject gameObject = new GameObject("dotConnection", typeof(Image));
        gameObject.transform.SetParent(graphContainer, false);
        gameObject.GetComponent<Image>().color = new Color(1, 1, 1, 0.5f);
        RectTransform rectTransform = gameObject.GetComponent<RectTransform>();
        Vector2 dir = (dotPositionB - dotPositionA).normalized;
        float distance = Vector2.Distance(dotPositionA, dotPositionB);
        rectTransform.anchorMin = new Vector2(0, 0);
        rectTransform.anchorMax = new Vector2(0, 0);
        rectTransform.sizeDelta = new Vector2(distance, 3f);
        rectTransform.anchoredPosition = dotPositionA + dir * distance * 0.5f;

        // To GetAngleFromVectorFloat
        dir = dir.normalized;
        float n = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        if (n < 0) n += 360;

        rectTransform.localEulerAngles = new Vector3(0, 0, n);
    }
}
