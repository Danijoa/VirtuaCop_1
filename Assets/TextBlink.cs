using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class TextBlink : MonoBehaviour
{

    Text text;

    // Start is called before the first frame update
    void Start()
    {
        text = GetComponent<Text>();
        StartCoroutine(Blink());
    }
    public IEnumerator Blink()
	{
        while(true)
		{
            text.text = "";
            yield return new WaitForSeconds(.5f);
            text.text = "Game Start"; 
            yield return new WaitForSeconds(.5f);

        }
	}
}
