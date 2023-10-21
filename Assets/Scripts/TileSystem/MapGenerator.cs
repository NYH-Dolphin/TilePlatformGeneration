using System.Collections.Generic;
using UnityEngine;

namespace Editor.TileSystem
{
    public class MapGenerator : MonoBehaviour
    {
        [Header("Map Configuration")] 
        public int width = 11;
        public int height = 7;

        [Header("Setting your tileset. The name of a tile has to be number")]
        public List<GameObject> tilesPrefab;


        private int[,] _mapArray;

        private void Start()
        {
            _mapArray = new int[width, height];
            RandomArray();
            LoadTiles();
        }
        
        private void LoadTiles()
        {
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


        public void SetMapArray(int[,] array)
        {
            _mapArray = array;
        }
    }
}