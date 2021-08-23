using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paddle : MonoBehaviour
{
    public float Speed = 2.0f;
    public float MaxMovement = 2.0f;

    // Update is called once per frame
    void Update()
    {
        if (MainManager.m_Started && MainManager.m_EnteringName == false && MainManager.m_GameOver == false)
        {
            float input = Input.GetAxis("Horizontal");

            Vector3 pos = transform.position;
            pos.x += input * Speed * Time.deltaTime;

            if (pos.x > MaxMovement)
                pos.x = MaxMovement;
            else if (pos.x < -MaxMovement)
                pos.x = -MaxMovement;

            transform.position = pos;
        }
    }
}
