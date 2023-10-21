using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Editor.TileSystem
{
    public class MapGenerator : MonoBehaviour
    {
        [Header("Map Configuration")] public int width = 11;
        public int height = 7;

        [Header("Setting your tileset. The name of a tile has to be number")]
        public List<GameObject> tilesPrefab;


        private int[,] _mapArray;

        public static MapGenerator Instance;

        private void Awake()
        {
            Instance = this;
        }

        private void Start()
        {
            _mapArray = new int[width, height];
        }

        private void LoadTiles()
        {
            // Remove previous tile
            OnResetMap();

            // Load the new tile
            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    GameObject tile = Instantiate(tilesPrefab[_mapArray[i, j]], transform);
                    tile.transform.position = new Vector3(i, j, 0);
                }
            }
        }

        public void RandomArray()
        {
            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    _mapArray[i, j] = Random.Range(0, tilesPrefab.Count);
                }
            }
        }


        public void OnResetMap()
        {
            List<GameObject> prevTiles = new List<GameObject>();
            if (transform.childCount != 0)
            {
                for (int i = 0; i < transform.childCount; i++)
                {
                    prevTiles.Add(transform.GetChild(i).gameObject);
                }
            }

            for (int i = 0; i < prevTiles.Count; i++)
            {
                Destroy(prevTiles[i]);
            }
        }


        public void OnGenerateMap(string prompts)
        {
            RandomArray();
            LoadTiles();
        }


        public void SetMapArray(int[,] array)
        {
            _mapArray = array;
        }
    }
}