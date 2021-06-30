using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class crossHead : MonoBehaviour
{
    public RectTransform cursor;
    public GameObject cross;
    Image image;

    // Start is called before the first frame update
    void Start()
    {
        //Init_Cursor();
        cross = GameObject.Find("CrossHead");
        image = cross.GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 mousePos = Input.mousePosition;
        cursor.anchoredPosition = new Vector2(mousePos.x - 20, mousePos.y - 20);

        if (Input.GetMouseButtonDown(0))
        {
            image.sprite = Resources.Load<Sprite>("11");
        }

        if (Input.GetMouseButtonUp(0))
        {
            image.sprite = Resources.Load<Sprite>("30");
        }

    }

    private void Init_Cursor()
    {
        Cursor.visible = false;
        cursor.pivot = Vector2.up;

        if (cursor.GetComponent<Graphic>())
        {
            cursor.GetComponent<Graphic>().raycastTarget = false;
        }
    }


}
