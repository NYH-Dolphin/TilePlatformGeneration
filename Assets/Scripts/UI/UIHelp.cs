using UnityEngine;
using UnityEngine.SceneManagement;

namespace UI
{
    public class UIHelp : MonoBehaviour
    {
        public void OnClickBack()
        {
            SceneManager.LoadScene("FiveDollarGame");
        }
    }
}