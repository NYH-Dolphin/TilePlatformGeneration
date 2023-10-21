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
        public static bool BEnd;

        private void Start()
        {
            PlayerInfo = new PlayerInfo();
        }

        void Update()
        {
            // Game End
            if (BEnd && Input.GetKeyDown(KeyCode.Return))
            {
                OnResetGame();
            }

            // UI is displayed, player can not move
            if (UIManger.BEnable) return;


            // Player is able to move
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

            // Check whether the game is over
            Vector2 playerPos = transform.position;
            if (Math.Abs(playerPos.x - (MapGenerator.Instance.width - 1)) < 0.01f &&
                Math.Abs(playerPos.y - (MapGenerator.Instance.height - 1)) < 0.01f)
            {
                OnGameEnd();
            }
        }

        private void OnGameEnd()
        {
            BEnd = true;
            UITypeWriter.Instance.SetDialogue("Game Over!\n (Press [Enter] to play again)");
            UIManger.Instance.OnEnableDialogue();
        }


        public void OnResetGame()
        {
            BEnd = false;
            PlayerInfo.OnResetPlayerInfo();
            MapGenerator.Instance.OnResetMap();
            OnResetPlayer();
            UIManger.Instance.OnResetUI();
            UITypeWriter.Instance.OnResetTypeWriter();
            UITypeWriter.Instance.SetDialogue(UITypeWriter.Instance._fullDescription, play: true);
        }

        public void OnResetPlayer()
        {
            _xMove = 0;
            _yMove = 0;
            transform.position = Vector3.zero;
        }
    }
}