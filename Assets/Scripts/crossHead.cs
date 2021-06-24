using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class crossHead : MonoBehaviour
{
    public RectTransform cursor;

    // Start is called before the first frame update
    void Start()
    {
        Init_Cursor();
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 mousePos = Input.mousePosition;
        cursor.position = mousePos;
    }

    private void Init_Cursor()
    {
        Cursor.visible = false;
        cursor.pivot = Vector2.up;

        if(cursor.GetComponent<Graphic>())
        {
            cursor.GetComponent<Graphic>().raycastTarget = false;
        }
    }
}
