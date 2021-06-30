using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHp : MonoBehaviour
{
    private PlayerInformation m_PlayerInfo;
    
    public Image[] m_HpImage;
    public Sprite m_FullHp;
    public Sprite m_EmptyHp;

	private void Start()
	{
        m_PlayerInfo = gameObject.GetComponent<PlayerInformation>();
    }
	// Start is called before the first frame update

	// Update is called once per frame
	void Update()
    {
        if(m_PlayerInfo.m_Health > m_PlayerInfo.m_OfHealth)
		{
            m_PlayerInfo.m_Health = m_PlayerInfo.m_OfHealth;
        }

        for(int i = 0; i < m_HpImage.Length; i++)
		{
            if(i < m_PlayerInfo.m_Health)
			{
                m_HpImage[i].sprite = m_FullHp;

            }
            else
			{
                m_HpImage[i].sprite = m_EmptyHp;
            }

            if(i < m_PlayerInfo.m_OfHealth)
			{
                m_HpImage[i].enabled = true;
            }
            else
			{
                m_HpImage[i].enabled = true;
			}
		}
    }

    public void PlayerHit(int damage)
	{
        if(m_PlayerInfo.m_Health > 0)
		{
            m_PlayerInfo.m_Health = m_PlayerInfo.m_Health - damage;
        }
	}

    public void PlayerHell(int hp)
    {
        if (m_PlayerInfo.m_Health < m_PlayerInfo.m_OfHealth)
        {
            m_PlayerInfo.m_Health = m_PlayerInfo.m_Health + hp;
        }
    }

}
