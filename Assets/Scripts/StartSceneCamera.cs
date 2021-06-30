using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartSceneCamera : MonoBehaviour
{
    private float m_RotateSpeed = 10f;

    void Update()
    {
        Camera.main.transform.Rotate(Vector3.up, m_RotateSpeed * Time.deltaTime);
    }
}
