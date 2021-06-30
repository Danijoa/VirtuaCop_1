using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDrop : MonoBehaviour
{

    public GameObject m_Heart;

    public void Drop(Transform m_Transform)
    {
        int random = Random.Range(0, 10);

        if(random == 2 || random == 4 || random == 6 || random == 9)
        {
            Instantiate(m_Heart, new Vector3(m_Transform.position.x, m_Transform.position.y + 1, m_Transform.position.z), m_Transform.rotation);
        }
        else
        {

        }
    }
}
