using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class PageSwiper : MonoBehaviour, IDragHandler, IEndDragHandler
{
    private Vector3 panelLocation;
    public float percentThreshhold = 0.2f;
    public float easing = 0.5f;
    public int totalPages = 1;
    public int currentPage = 1;
    public Transform PageHolder;
    public GameObject PageItem;
    public Color currentPageColor;
    public Color notCurrentPageColor;

    private void Awake()
    {
        panelLocation = transform.position;
    }

    // Start is called before the first frame update
    void Start()
    {
        // create all the item for each page according to total number of page
        for (int i = 1; i <= totalPages; i++)
        {
            GameObject pageItem = Instantiate(PageItem, PageHolder.transform.position, Quaternion.identity);
            pageItem.transform.SetParent(PageHolder);
            pageItem.name = i.ToString();
            if (i == currentPage)
            {
                pageItem.GetComponent<Image>().color = currentPageColor;
            }
            else
            {
                pageItem.GetComponent<Image>().color = notCurrentPageColor;
            }
        }
    }

    public void OnDrag(PointerEventData data)
    {
        float difference = data.pressPosition.x - data.position.x;
        transform.position = panelLocation - new Vector3(difference, 0, 0);
    }

    public void OnEndDrag(PointerEventData data)
    {
        float percentage = (data.pressPosition.x - data.position.x) / Screen.width;
        if (Mathf.Abs(percentage) >= percentThreshhold)
        {
            Vector3 newLocation = panelLocation;
            if (percentage > 0 && currentPage < totalPages)
            {
                currentPage++;
                newLocation += new Vector3(-Screen.width, 0, 0);
            }
            else if (percentage < 0 && currentPage > 1)
            {
                currentPage--;
                newLocation += new Vector3(Screen.width, 0, 0);
            }
            StartCoroutine(SmoothMove(transform.position, newLocation, easing));
            panelLocation = newLocation;
        }
        else
        {
            StartCoroutine(SmoothMove(transform.position, panelLocation, easing));
        }

        foreach (Transform child in PageHolder)
        {
            if (child.name == currentPage.ToString())
            {
                child.GetComponent<Image>().color = currentPageColor;
            }
            else
            {
                child.GetComponent<Image>().color = notCurrentPageColor;
            }
        }
    }

    IEnumerator SmoothMove(Vector3 startpos, Vector3 endpos, float seconds)
    {
        float t = 0f;
        while (t <= 1.0)
        {
            t += Time.deltaTime / seconds;
            transform.position = Vector3.Lerp(startpos, endpos, Mathf.SmoothStep(0f, 1f, t));
            yield return null;
        }
    }
}
