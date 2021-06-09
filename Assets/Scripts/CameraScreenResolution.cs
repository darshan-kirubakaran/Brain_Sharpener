using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScreenResolution : MonoBehaviour
{
    public bool maintainWidth = true;

    [Range(-1, 1)]
    public int adoptPosition;

    float defaultWidth;
    float defaultHeight;

    Vector3 CameraPos;

    // Start is called before the first frame update
    void Start()
    {
        CameraPos = Camera.main.transform.position;

        defaultHeight = Camera.main.orthographicSize;
        defaultWidth = Camera.main.orthographicSize * Camera.main.aspect;
    }

    // Update is called once per frame
    void Update()
    {
        if(maintainWidth)
        {
            Camera.main.orthographicSize = defaultWidth / Camera.main.aspect;

            Camera.main.transform.position = new Vector3(CameraPos.x, adoptPosition * (defaultHeight - Camera.main.orthographicSize), CameraPos.z);
        }
        else
        {

        }
    }
}
