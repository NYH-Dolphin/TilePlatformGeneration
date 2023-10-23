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
        //private const string URL = "http://127.0.0.1:5000/generate";


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
            for (int i = 0; i < height; i++)
            {
                for (int j = 0; j < width; j++)
                {
                    GameObject tile = Instantiate(tilesPrefab[_mapArray[i, j]], transform);
                    tile.transform.position = new Vector3(j, i, 0);
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
            StartCoroutine(SendRequest(prompts));
            //RandomArray();
            LoadTiles();
        }


        public void SetMapArray(int[,] array)
        {
            _mapArray = array;
        }

        public IEnumerator SendRequest(string prompt)
        {
            PromptData data = new PromptData();
            data.prompt = prompt;
            string jsonData = JsonUtility.ToJson(data);
            Debug.Log("Sending JSON: " + jsonData);
            byte[] body = Encoding.UTF8.GetBytes(jsonData);

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
                    Debug.Log(Convert2DArrayToString(parsedArray));
                    SetMapArray(parsedArray);
                    LoadTiles();
                    Debug.Log(Convert2DArrayToString(_mapArray));
                    // Debug.Log(_mapArray.ToString());
                }
            }
        }

        public int[,] ParseStringToArray(string jsonString)
        {

            // Split the string to get individual arrays
            string[] rows = jsonString.Replace("\"", "").Trim('[', ']').Split(new string[] { "], [" }, StringSplitOptions.None);

            int[,] result = new int[height, width];

            for (int i = 0; i < height; i++)
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
    

    public static string Convert2DArrayToString(int[,] array)
    {
        StringBuilder sb = new StringBuilder();
        int rows = array.GetLength(0);
        int cols = array.GetLength(1);

        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < cols; j++)
            {
                sb.Append(array[i, j]);
                if (j != cols - 1)
                {
                    sb.Append(", ");
                }
            }
            sb.AppendLine();
        }

        return sb.ToString();
    }

    [System.Serializable]
    public class PromptData
    {
        public string prompt;
    }
}
}