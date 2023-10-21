using System;
using System.Collections;
using TMPro;
using UnityEngine;

namespace UI
{
    public class UITypeWriter : MonoBehaviour
    {
        public TextMeshProUGUI dialogueText;
        public float fDelay = 0.1f;

        private string _fullDescription = "I want a beautiful city.";

        private string _info = "\n\n<size=36>(Press <color=#ff0000>[Tab]</color> to toggle on/off the dialogue)</size>";

        public void OnTriggerTypeWriter()
        {
            StartCoroutine(PlayText());
        }


        public void OnCleanText()
        {
            dialogueText.text = String.Empty;
        }

        IEnumerator PlayText()
        {
            for (int i = 0; i <= _fullDescription.Length; i++)
            {
                dialogueText.text = _fullDescription.Substring(0, i);
                yield return new WaitForSeconds(fDelay);
            }

            yield return new WaitForSeconds(0.5f);
            dialogueText.text += _info;
        }
    }
}