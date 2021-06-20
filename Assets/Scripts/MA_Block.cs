using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MA_Block : MonoBehaviour
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
    void Awake()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        textMesh = GetComponentInChildren<TextMesh>();

        randomBlockColor = Random.Range(0, colors.Length);
        randomBlockColorName = Random.Range(0, colorNames.Length);

        spriteRenderer.color = colors[randomBlockColor];
        int a = Random.Range(0, 9);
        int b = Random.Range(0, 9);
        textMesh.text = a.ToString() + " + " + b.ToString() + " =";
        this.gameObject.name = (a + b).ToString();

    }

    // Update is called once per frame
    void Update()
    {
        rigidbody2D.velocity = new Vector2(rigidbody2D.velocity.x, speed * -1);
    }
}
