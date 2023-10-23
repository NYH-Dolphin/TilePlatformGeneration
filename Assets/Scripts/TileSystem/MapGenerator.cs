using System;
using System.Collections;
using System.Text;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

using Random = UnityEngine.Random;

namespace Editor.TileSystem
{
    public class MapGenerator : MonoBehaviour
    {
        [Header("Map Configuration")] public int width = 11;
        public int height = 7;

        [Header("Setting your tileset. The name of a tile has to be number")]
        public List<GameObject> tilesPrefab;

        private const string URL = "https://sleepy-waters-75825.herokuapp.com/https://aigame.engineering.nyu.edu/generate";


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
            // TODO generate MapArray based on the prompts;
            // StartCoroutine(SendRequest(prompts));
            RandomArray();
            LoadTiles();
        }


        public void SetMapArray(int[,] array)
        {
            _mapArray = array;
        }

        public IEnumerator SendRequest(string prompt)
        {
            string json = JsonUtility.ToJson(new { prompt = prompt });
            byte[] body = Encoding.UTF8.GetBytes(json);

            using (UnityWebRequest www = new UnityWebRequest(URL, "POST"))
            {
                www.uploadHandler = (UploadHandler)new UploadHandlerRaw(body);
                www.downloadHandler = (DownloadHandler)new DownloadHandlerBuffer();

                www.SetRequestHeader("Content-Type", "application/json");
                www.SetRequestHeader("x-requested-with", "XMLHttpRequest");
                www.SetRequestHeader("origin", "https://crocbot-site.herokuapp.com/fixmap");

                yield return www.SendWebRequest();

                if (www.result == UnityWebRequest.Result.ConnectionError)
                {
                    Debug.Log(www.error);
                }
                else
                {
                    // Parse the returned data
                    Debug.Log(www.downloadHandler.text);
                    int[,] parsedArray = ParseStringToArray(www.downloadHandler.text);
                    SetMapArray(parsedArray);
                }
            }
        }

        public int[,] ParseStringToArray(string jsonString)
        {

            // Split the string to get individual arrays
            string[] rows = jsonString.Trim('[', ']').Split(new string[] { "], [" }, StringSplitOptions.None);

            int[,] result = new int[10, 10];

            for (int i = 0; i < rows.Length; i++)
            {
                // Strip any remaining opening or closing square brackets from the row
                string cleanedRow = rows[i].Replace("[", "").Replace("]", "");
                Debug.Log("Row " + (i + 1) + ": " + cleanedRow); // Printing each row

                // For each row, split by comma
                string[] values = cleanedRow.Split(',');

                for (int j = 0; j < values.Length; j++)
                {
                    result[i, j] = int.Parse(values[j].Trim()); // Ensure no spaces before parsing
                }
            }

            return result;
        }
    }
}