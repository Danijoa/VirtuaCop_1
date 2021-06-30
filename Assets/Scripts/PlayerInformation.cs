using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayerInformation : MonoBehaviour
{
    //enum문으로 상태를 나누었다
    public enum State
    {
        Move,
        Battle,
        Die
    }

    //
    public State state;
    public bool isMove;
    public bool isRotate;

    public static int getScore;

    // 점수
    public Text scoreText;
    private GameManager gameManager;
    //public PlayerMove playermove;

    // 목숨
    [SerializeField]
    public int m_Health;
    [SerializeField]
    public int m_OfHealth = 10;

    private void Awake()
    {
        m_Health = m_OfHealth;
    }

    private void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        //playermove = GetComponent<PlayerMove>();
    }

    private void Update()
    {
        if (m_Health <= 0)
        {
            scoreText.text = "SCORE " + gameManager.totalScore;
            getScore = gameManager.totalScore;
            scoreText.gameObject.SetActive(true);
            isMove = false;
            state = State.Die;

            //SceneManager.LoadScene("GameOver");
        }
    }

}
