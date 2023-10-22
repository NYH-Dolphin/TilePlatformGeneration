using UnityEngine;

namespace Audio
{
    public class BGMBehaviour : MonoBehaviour
    {
        private static BGMBehaviour instance;

        void Awake()
        {
            if (instance == null)
            {
                instance = this;
                DontDestroyOnLoad(gameObject);
            }
            else
            {
                Destroy(gameObject);
            }
        }
    }
}