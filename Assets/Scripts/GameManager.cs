using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    //PlayerMove m_playerMove;

    public WayPoint m_WayPoint;
    public GameObject m_Camera;
    public PlayerMove m_PlayerMove;

    public int totalScore = 0;

	private void Awake()
	{
        if (instance == null)
		{
            instance = this;
		}
        else
		{
            Debug.Log("이미 게임메이저가 있다");
            Destroy(gameObject);
		}

        m_Camera = GameObject.Find("Camera");
        m_WayPoint = GetComponent<WayPoint>();
        m_PlayerMove = GameObject.Find("Player").GetComponent<PlayerMove>();

    }

    // Start is called before the first frame update
    void Start()
    {
        //m_CamCtrl.GetComponent<CameraCtrl>();
        //m_playerMove.GetComponent<PlayerMove>();
       

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
