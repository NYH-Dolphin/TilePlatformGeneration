using System;
using Editor.TileSystem;
using UI;
using UnityEngine;

namespace Character
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class PlayerController : MonoBehaviour
    {
        private int _xMove;
        private int _yMove;
        
        public PlayerInfo PlayerInfo;

        private void Start()
        {
            PlayerInfo = new PlayerInfo();
        }

        void Update()
        {
            // UI is displayed, player can not move
            if (UIManger.BEnable) return;

            if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
            {
                if (_yMove++ < MapGenerator.Instance.height - 1)
                {
                    Vector2 pos = transform.position;
                    pos.y += 1;
                    transform.position = pos;
                }
            }

            if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
            {
                if (_xMove++ < MapGenerator.Instance.width - 1)
                {
                    Vector2 pos = transform.position;
                    pos.x += 1;
                    transform.position = pos;
                }
            }

            if (_xMove == MapGenerator.Instance.width - 1 && _yMove == MapGenerator.Instance.height - 1)
            {
                OnGameEnd();
            }
        }

        private void OnGameEnd()
        {
        }


        public void OnResetPlayer()
        {
            _xMove = 0;
            _yMove = 0;
            transform.position = Vector3.zero;
        }
    }
}