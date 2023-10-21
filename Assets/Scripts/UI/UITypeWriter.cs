using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace UI
{
    public class UITypeWriter : MonoBehaviour
    {
        public TextMeshProUGUI dialogueText;
        public float fDelay = 0.1f;

        private string _fullDescription = "I want a beautiful city";

        public void OnTriggerTypeWriter()
        {
            StartCoroutine(PlayText());
        }

        IEnumerator PlayText()
        {
            for (int i = 0; i <= _fullDescription.Length; i++)
            {
                dialogueText.text = _fullDescription.Substring(0, i);
                yield return new WaitForSeconds(fDelay);
            }
        }
    }
}