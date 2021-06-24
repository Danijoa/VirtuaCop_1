using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Fire : MonoBehaviour
{

    private Animator animt;
    Image image;

    public bool fire = false;
    public bool reload = false;
    public int index = 0;

    // Start is called before the first frame update
    void Start()
    {
        animt = gameObject.GetComponent<Animator>();
        image = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        animt.SetBool("Fire", fire);
        animt.SetBool("Reload", reload);

        if (Input.GetMouseButtonDown(0))
        {
            fire = true;
            animt.SetBool("Fire", fire);

        }

        if (Input.GetKeyDown("r"))
        {
            reload = true;
            animt.SetBool("Reload", reload);
        }

        image.sprite = GetComponent<SpriteRenderer>().sprite;
        fire = false;
        reload = false;

    }
}
