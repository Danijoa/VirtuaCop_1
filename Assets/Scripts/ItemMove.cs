using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemMove : MonoBehaviour
{
    PlayerHp m_PlayerHp;

    private void Awake()
    {
        m_PlayerHp = GameObject.Find("Player").GetComponent<PlayerHp>();
    }

    private void Update()
    {
        gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, m_PlayerHp.gameObject.transform.position, Time.deltaTime * 10f);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "Player")
        {
            m_PlayerHp.PlayerHell(3);
            Destroy(this.gameObject);
        }
    }
   
}
