using System;
using System.Collections;
using Character;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

namespace UI
{
    public class UITypeWriter : MonoBehaviour
    {
        public TextMeshProUGUI dialogueText;
        public float fDelay = 0.1f;

        public static UITypeWriter Instance;

        private string _showText;
        public string fullDescription;

        private bool _bLock;

        private void Awake()
        {
            Instance = this;
        }

        private void Start()
        {
            OnResetTypeWriter();
        }

        public void OnTriggerTypeWriter()
        {
            StartCoroutine(PlayText(_showText));
        }

        public void OnResetTypeWriter()
        {
            fullDescription = GetDescription();
            _showText = fullDescription;
        }

        public void OnCleanText()
        {
            dialogueText.text = String.Empty;
        }

        IEnumerator PlayText(string text, bool showDescription = false)
        {
            if (_bLock) yield break;

            _bLock = true;
            for (int i = 0; i <= text.Length; i++)
            {
                dialogueText.text = text.Substring(0, i);
                yield return new WaitForSeconds(fDelay);
            }

            yield return new WaitForSeconds(0.5f);

            if (!PlayerController.BEnd && showDescription)
            {
                dialogueText.text = string.Empty;
                for (int i = 0; i <= fullDescription.Length; i++)
                {
                    dialogueText.text = fullDescription.Substring(0, i);
                    yield return new WaitForSeconds(fDelay);
                }
            }
            _bLock = false;
        }

        /// <summary>
        /// Set the Dialogue of the NPC
        /// </summary>
        /// <param name="dialogue">string to display</param>
        /// <param name="play">is true, play the animation</param>
        /// <param name="showDescription">is true, will overwrite the dialogue and show description few seconds later</param>
        public void SetDialogue(string dialogue, bool play = false, bool showDescription = false)
        {
            _showText = dialogue;
            if (play)
            {
                StartCoroutine(PlayText(_showText, showDescription));
            }
        }


        // TODO Get the desired description
        public string GetDescription()
        {
            return "I want a beautiful city.";
        }
    }
}