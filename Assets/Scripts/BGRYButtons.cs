using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGRYButtons : MonoBehaviour
{
    public string[] colorNames;
    public Color[] colors;
    Spawner spawner;

    // Start is called before the first frame update
    void Start()
    {
        spawner = FindObjectOfType<Spawner>();

        //var i = 0;
        //foreach(Color color in spawner.colors)
        //{
          //  colors[i] = color;
            //i++;
        //}
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Blue()
    {
        if(spawner.textMode)
        {
            spawner.CheckColorIfRight(colors[0]);
        }
        else if(!spawner.textMode)
        {
            spawner.CheckTextIfRight(colorNames[0]);
        }
    }
    
    public void Green()
    {
        if (spawner.textMode)
        {
            spawner.CheckColorIfRight(colors[1]);
        }
        else if (!spawner.textMode)
        {
            spawner.CheckTextIfRight(colorNames[1]);
        }
    }

    public void Red()
    {
        if (spawner.textMode == true)
        {
            spawner.CheckColorIfRight(colors[2]);
        }
        else if (spawner.textMode == false)
        {
            spawner.CheckTextIfRight(colorNames[2]);
        }
    }

    public void Yellow()
    {
        if (spawner.textMode)
        {
            spawner.CheckColorIfRight(colors[3]);
        }
        else if (!spawner.textMode)
        {
            spawner.CheckTextIfRight(colorNames[3]);
        }
    }
    
    public void Black()
    {
        if (spawner.textMode)
        {
            spawner.CheckColorIfRight(colors[4]);
        }
        else if (!spawner.textMode)
        {
            spawner.CheckTextIfRight(colorNames[4]);
        }
    }

}
