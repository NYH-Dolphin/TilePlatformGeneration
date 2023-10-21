using System;
using UI;
using UnityEngine;

namespace Character
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class PlayerController : MonoBehaviour
    {
        void Update()
        {
            // UI is displayed, player can not move
            if (UIManger.BEnable) return;

            if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
            {
                Vector2 pos = transform.position;
                pos.y += 1;
                transform.position = pos;
            }

            if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
            {
                Vector2 pos = transform.position;
                pos.x += 1;
                transform.position = pos;
            }
        }
    }
}