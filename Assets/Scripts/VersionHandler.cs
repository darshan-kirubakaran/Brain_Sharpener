using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class VersionHandler : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        this.gameObject.GetComponent<TextMeshProUGUI>().text = ("V" + Application.version.ToString());
    }
}
