using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Tilemaps;
using Random = System.Random;

namespace Editor.TileSystem
{
    [RequireComponent(typeof(Tilemap))]
    public class TilemapGenerator : MonoBehaviour
    {
        [Header("Setting your tileset. The name of a tile has to be number\n" +
                "Click [Update Tileset] after finishing your setting.")]
        public List<Tile> tileset;

        [Header("Drag your <map_name>.csv file here.\nClick [Update Tilemap Array] after finishing your setting.")]
        public TextAsset mapCSV;

        private int[][] _mapArray;

        private Tilemap _tilemap;
        private Dictionary<int, Tile> int2tile;


        /// <summary>
        /// Update tileset information to Dictionary with [int] as name, [Tile] as specific tile.
        /// </summary>
        public void OnUpdateTileset()
        {
            int2tile = new Dictionary<int, Tile>();

            List<Tile> incorrect = new List<Tile>();
            List<Tile> repetitive = new List<Tile>();

            foreach (Tile tile in tileset)
            {
                int index;
                if (!int.TryParse(tile.name, out index))
                {
                    Debug.LogError(
                        $"({DateTime.Now}) Tile Update Failure! " +
                        $"You are trying to include tile {tile.name} that are not named as number format.");
                    incorrect.Add(tile);
                }
                else if (int2tile.ContainsKey(index))
                {
                    repetitive.Add(tile);
                }
                else
                {
                    int2tile.Add(index, tile);
                }
            }

            if (repetitive.Count != 0)
            {
                Debug.LogWarning(
                    $"({DateTime.Now}) Contain Repetitive Tiles. " +
                    "Already remove them from your existing tileset");
            }

            foreach (var tile in repetitive)
            {
                tileset.Remove(tile);
            }

            foreach (var tile in incorrect)
            {
                tileset.Remove(tile);
            }

            Debug.Log($"({DateTime.Now}) Tile Update Success! ");
        }


        /// <summary>
        /// Read the Tilemap and the Tilemap.csv file together
        /// </summary>
        public void OnUpdateTilemap()
        {
            OnUpdateTileset();
            _tilemap = transform.GetComponent<Tilemap>();
            if (mapCSV == null)
            {
                Debug.LogError(
                    $"({DateTime.Now}) mapCSV is null. You are missing an important file for your map generation.");
                return;
            }

            bool readSuccess = ReadMapCsv(mapCSV);
            if (readSuccess)
            {
                Debug.Log($"({DateTime.Now}) Read MapCSV Success!");
            }
            else
            {
                Debug.Log($"({DateTime.Now}) Read MapCSV Failure!");
            }

            SetTilemap(_mapArray, _tilemap);
        }

        /// <summary>
        /// Clean the tilemap
        /// </summary>
        public void OnCleanTilemap()
        {
            CleanTilemap(_tilemap);
        }


        /// <summary>
        /// Random a 5x5 tilemap
        /// </summary>
        public void OnRandomTilemap5x5()
        {
            int[][] randomMap = RandomTilemap(5, 5);
            SetTilemap(randomMap, _tilemap);
        }


        /// <summary>
        /// Convert the .csv map file into int[][] map
        /// </summary>
        /// <param name="map"></param>
        /// <returns></returns>
        private bool ReadMapCsv(TextAsset map)
        {
            if (String.IsNullOrEmpty(map.text))
            {
                Debug.LogError($"({DateTime.Now}) mapCSV is empty.");
                return false;
            }

            string[] lineArray = map.text.Split("\r\n");
            // sometimes csv file contain blank content in the last row, just skip that part
            _mapArray = string.IsNullOrEmpty(lineArray[lineArray.Length - 1])
                ? new int[lineArray.Length - 1][]
                : new int[lineArray.Length][];


            for (int i = 0; i < _mapArray.Length; i++)
            {
                try
                {
                    _mapArray[i] = Array.ConvertAll(lineArray[i].Split(","), int.Parse);
                }
                catch (Exception)
                {
                    Debug.LogError(
                        $"({DateTime.Now}) mapCSV contain non-digital content.");
                    return false;
                }
            }

            return true;
        }


        /// <summary>
        /// Set the tilemap based on the int[][] map
        /// </summary>
        /// <param name="map"></param>
        /// <param name="tilemap"></param>
        private void SetTilemap(int[][] map, Tilemap tilemap)
        {
            CleanTilemap(tilemap);

            int row = map.Length;
            int col = map[0].Length;
            for (int i = 0; i < row; i++)
            {
                for (int j = 0; j < col; j++)
                {
                    int xPos = i - row / 2;
                    int yPos = j - col / 2;

                    int tileNumber = map[i][j];
                    tilemap.SetTile(new Vector3Int(xPos, yPos, 0), int2tile[tileNumber]);
                }
            }

            Debug.Log($"({DateTime.Now}) Tilemap is successfully generated by the given map!");
        }


        private int[][] RandomTilemap(int row, int col)
        {
            OnUpdateTileset();
            int[] tileNums = int2tile.Keys.ToArray();
            Random rand = new Random();
            int[][] map = new int[row][];
            for (int i = 0; i < row; i++)
            {
                map[row - i - 1] = new int[col];
                for (int j = 0; j < col; j++)
                {
                    map[row - i - 1][col - j - 1] = tileNums[rand.Next(tileNums.Length)];
                }
            }

            return map;
        }

        private void CleanTilemap(Tilemap tilemap)
        {
            tilemap.ClearAllTiles();
        }
    }
}