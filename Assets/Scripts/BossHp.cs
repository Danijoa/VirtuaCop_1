using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossHp : MonoBehaviour
{
	// ���� ü��
    Slider slHp;
    float fSliderBarTime = 0.1f;

	// ����
	public Text scoreText;
	private GameManager gameManager;

	private void Start()
	{
		gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
		slHp = GetComponent<Slider>();
	}

	private void Update()
	{
		if(Input.GetKeyDown("p"))
		{
			HpDelete();
		}

		if (slHp.value <= 0f)
		{
			scoreText.text = "SCORE " + gameManager.totalScore;
			scoreText.gameObject.SetActive(true);
		}
	}

	public void HpDelete()
	{
		slHp.value -= fSliderBarTime;
	}
}
