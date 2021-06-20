using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BGRYButtons : MonoBehaviour
{
    public string[] colorNames;
    public Color[] colors;
    Spawner spawner;
    Hit_Checker hit_Checker;

    // Start is called before the first frame update
    void Start()
    {
        spawner = FindObjectOfType<Spawner>();
        hit_Checker = FindObjectOfType<Hit_Checker>();
    }


    public void Blue()
    {
        if(spawner.textMode)
        {
            hit_Checker.CheckColorIfRight(colors[0]);
        }
        else if(!spawner.textMode)
        {
            hit_Checker.CheckTextIfRight(colorNames[0]);
        }
    }
    
    public void Green()
    {
        if (spawner.textMode)
        {
            hit_Checker.CheckColorIfRight(colors[1]);
        }
        else if (!spawner.textMode)
        {
            hit_Checker.CheckTextIfRight(colorNames[1]);
        }
    }

    public void Red()
    {
        if (spawner.textMode == true)
        {
            hit_Checker.CheckColorIfRight(colors[2]);
        }
        else if (spawner.textMode == false)
        {
            hit_Checker.CheckTextIfRight(colorNames[2]);
        }
    }

    public void Yellow()
    {
        if (spawner.textMode)
        {
            hit_Checker.CheckColorIfRight(colors[3]);
        }
        else if (!spawner.textMode)
        {
            hit_Checker.CheckTextIfRight(colorNames[3]);
        }
    }
    
    public void Black()
    {
        if (spawner.textMode)
        {
            hit_Checker.CheckColorIfRight(colors[4]);
        }
        else if (!spawner.textMode)
        {
            hit_Checker.CheckTextIfRight(colorNames[4]);
        }
    }

}
