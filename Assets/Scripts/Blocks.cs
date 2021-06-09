using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Blocks : MonoBehaviour
{
    public Color[] colors;
    public string[] colorNames;
    public float speed = 12f;
    int randomBlockColor;
    int randomBlockColorName;

    Rigidbody2D rigidbody2D;
    SpriteRenderer spriteRenderer;
    TextMesh textMesh;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        textMesh = GetComponentInChildren<TextMesh>();

        randomBlockColor = Random.Range(0, colors.Length);
        randomBlockColorName = Random.Range(0, colorNames.Length);

        spriteRenderer.color = colors[randomBlockColor];
        textMesh.text = colorNames[randomBlockColorName];

        this.transform.localScale = new Vector3((Screen.dpi/160 * 2)/5, 0.5f, this.transform.localScale.z);
    }

    // Update is called once per frame
    void Update()
    {
        rigidbody2D.velocity = new Vector2(rigidbody2D.velocity.x, speed * -1);
    }
}
