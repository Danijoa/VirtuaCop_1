using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    //CameraCtrl m_CamCtrl;
    //PlayerMove m_playerMove;

    public WayPoint m_WayPoint;

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

        m_WayPoint = GetComponent<WayPoint>();

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
