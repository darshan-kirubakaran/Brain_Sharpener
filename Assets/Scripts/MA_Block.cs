using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MA_Block : MonoBehaviour
{
    public string[] colorNames;
    public float speed = 12f;
    int randomBlockColor;
    int randomBlockColorName;

    Rigidbody2D rigidbody2D;
    SpriteRenderer spriteRenderer;
    TextMesh textMesh;
    MA_LevelManager ma_LevelManager;

    // Start is called before the first frame update
    void Awake()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        textMesh = GetComponentInChildren<TextMesh>();

        ma_LevelManager = FindObjectOfType<MA_LevelManager>();

        randomBlockColorName = Random.Range(0, colorNames.Length);

        int a = Random.Range(ma_LevelManager.minA, ma_LevelManager.maxA);
        int b = Random.Range(ma_LevelManager.minB, ma_LevelManager.maxB);

        if(ma_LevelManager.symbol == "ADD")
        {
            textMesh.text = a.ToString() + " + " + b.ToString();
            this.gameObject.name = (a + b).ToString();
        }
        else if (ma_LevelManager.symbol == "SUB")
        {
            textMesh.text = a.ToString() + " - " + b.ToString();
            this.gameObject.name = (a - b).ToString();
        }
        else if (ma_LevelManager.symbol == "MUL")
        {
            textMesh.text = b.ToString() + " X " + a.ToString();
            this.gameObject.name = (b * a).ToString();
        }
        else if (ma_LevelManager.symbol == "DIV")
        {
            int c = a * b;

            textMesh.text = c.ToString() + " / " + b.ToString();
            this.gameObject.name = (c / b).ToString();
        }
        else if (ma_LevelManager.symbol == "MIX")
        {
            textMesh.text = a.ToString() + " + " + b.ToString();
            this.gameObject.name = (a + b).ToString();
        }

    }

    // Update is called once per frame
    void Update()
    {
        rigidbody2D.velocity = new Vector2(rigidbody2D.velocity.x, speed * -1);
    }
}
