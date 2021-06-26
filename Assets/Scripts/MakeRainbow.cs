using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MakeRainbow : MonoBehaviour
{

    public Text text;
    float index = 0;

    // Start is called before the first frame update
    private void Awake()
    {
        text = GetComponent<Text>();
    }

	private void Update()
	{
        index+=10;
        if(index == 360)
		{
            index = 0;
		}

        text.color = Color.HSVToRGB(index / 360, 1, 1);
    }

}
