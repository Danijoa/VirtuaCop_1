using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShot : MonoBehaviour
{
	private void Awake()
	{
	}

	private void Update()
	{
		if(Input.GetKeyDown(KeyCode.G))
		{
			RaycastHit m_Hit;
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

			if (Physics.Raycast(ray, out m_Hit, Mathf.Infinity))
			{
				

				Debug.Log("hit point : " + m_Hit.point + ", distance : " + m_Hit.distance + ", name : " + m_Hit.collider.name);
				//Debug.DrawRay(m_Camera.transform.position, m_Camera.transform.forward * m_Hit.distance, Color.red);
				
				//Debug.DrawRay(m_Hit.transform.position, m_Hit.collider.transform.position);
			}
		}
		
		

		
	}
}
