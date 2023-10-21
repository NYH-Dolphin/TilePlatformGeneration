using System;
using System.Collections;
using Character;
using TMPro;
using UnityEngine;

namespace UI
{
    public class UITypeWriter : MonoBehaviour
    {
        public TextMeshProUGUI dialogueText;
        public float fDelay = 0.1f;

        public static UITypeWriter Instance;

        private string _showText;
        public string _fullDescription = "I want a beautiful city.";

        private void Awake()
        {
            Instance = this;
        }

        private void Start()
        {
            _showText = _fullDescription;
        }

        public void OnTriggerTypeWriter()
        {
            StartCoroutine(PlayText(_showText));
        }

        public void OnResetTypeWriter()
        {
            _showText = _fullDescription;
        }

        public void OnCleanText()
        {
            dialogueText.text = String.Empty;
        }

        IEnumerator PlayText(string text, bool showDescription = false)
        {
            for (int i = 0; i <= text.Length; i++)
            {
                dialogueText.text = text.Substring(0, i);
                yield return new WaitForSeconds(fDelay);
            }

            yield return new WaitForSeconds(0.5f);

            if (!PlayerController.BEnd && showDescription)
            {
                dialogueText.text = string.Empty;
                for (int i = 0; i <= _fullDescription.Length; i++)
                {
                    dialogueText.text = _fullDescription.Substring(0, i);
                    yield return new WaitForSeconds(fDelay);
                }
            }
        }

        public void SetDialogue(string dialogue, bool play = false, bool showDescription = false)
        {
            _showText = dialogue;
            if (play)
            {
                StartCoroutine(PlayText(_showText, showDescription));
            }
        }
    }
}