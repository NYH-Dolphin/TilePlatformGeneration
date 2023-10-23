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

        public string[] levelDescriptions =
        {
            "The ground looks unique here. Fetch me some!",
            "My spaceship needs a stable landing. What can I use?",
            "I heard there's liquid on this planet. Can I see?",
            "The local vegetation looks appetizing!",
            "Mmm... Earth's barriers. Get me some!",
            "I love the way earthlings communicate. Fetch me some means!",
            "Need some resting spots. Get me some comfortable places.",
            "I've heard of your earthly treasures. Show me!",
            "Help me camouflage my spaceship!",
            "I need some textures for my space art!",
            "Looking for unique earth patterns!",
            "Construct me a pathway!",
            "Find me some beautiful earthly colors!",
            "I adore Earth's architecture. Fetch me some components!",
            "Curious about your planet's surfaces!",
            "Fetch me some items for my alien garden!",
            "I wish to make a terrain collage!",
            "Craft me an earthly aesthetic!",
            "Build me a miniature earth landscape!",
            "Design me an Earthly relaxation spot!",
            "Craft me a pathway to your world's wonders!",
            "Recreate me a piece of your homeland!",
            "I've seen Earthly wonders from above. Show them up close!",
            "I love Earth's natural formations!",
            "Construct me a colorful set!",
            "I need to send a message. Help me out!",
            "Design me a serene Earthly spot!",
            "Help me create an Earth maze!",
            "Need to create a hideout spot. Help!",
            "Design me a typical Earth courtyard!",
            "Craft me a collection of your planet's symbols!",
            "Want to experiment with textures. Fetch me some!",
            "Show me the beauty of your flora!",
            "I need to build a shelter. Help me out!",
            "Recreate me an earthly oasis!",
            "The colors of your planet amaze me! Show more!",
            "I'm craving Earth's natural beauty!",
            "I want to see more of Earth's geometric patterns!",
            "Seeking some of Earth's unique mysteries!",
            "Craft me a tiny Earth park!",
            "I'm looking for earth's toughest elements!",
            "Design me an Earth-style lounge area!",
            "Construct me a nature trail!",
            "I want to learn about Earth's textures!",
            "I'm looking for places to hide treasures!",
            "Give me some symbols of your world!",
            "I wish to see Earth's favorite spots!",
            "Help me design a pathway to beauty!",
            "I'm curious about your typical outdoor designs!",
            "Craft me a vibrant Earth scene!"
        };

        public int[][] tileLists =
        {
            new int[] { 0, 1, 6 },
            new int[] { 5, 7, 6 },
            new int[] { 11, 1, 6 },
            new int[] { 10, 2, 4 },
            new int[] { 9, 7, 10 },
            new int[] { 8, 14, 13 },
            new int[] { 0, 6, 5 },
            new int[] { 15, 8, 9 },
            new int[] { 10, 0, 12 },
            new int[] { 1, 11, 6 },
            new int[] { 4, 3, 1 },
            new int[] { 5, 0, 7 },
            new int[] { 2, 11, 1 },
            new int[] { 13, 14, 12 },
            new int[] { 6, 5, 1 },
            new int[] { 10, 3, 4 },
            new int[] { 7, 11, 6 },
            new int[] { 2, 5, 13 },
            new int[] { 0, 10, 11 },
            new int[] { 0, 2, 6 },
            new int[] { 5, 9, 0 },
            new int[] { 1, 10, 11 },
            new int[] { 3, 12, 10 },
            new int[] { 7, 10, 11 },
            new int[] { 3, 2, 11 },
            new int[] { 8, 9, 14 },
            new int[] { 0, 11, 10 },
            new int[] { 9, 7, 10 },
            new int[] { 10, 7, 13 },
            new int[] { 0, 8, 5 },
            new int[] { 15, 10, 14 },
            new int[] { 1, 6, 5 },
            new int[] { 3, 2, 4 },
            new int[] { 12, 14, 13 },
            new int[] { 11, 6, 10 },
            new int[] { 2, 1, 11 },
            new int[] { 10, 11, 3 },
            new int[] { 9, 14, 5 },
            new int[] { 15, 8, 13 },
            new int[] { 0, 10, 9 },
            new int[] { 7, 9, 10 },
            new int[] { 0, 12, 10 },
            new int[] { 1, 11, 10 },
            new int[] { 6, 7, 5 },
            new int[] { 8, 0, 10 },
            new int[] { 13, 14, 15 },
            new int[] { 5, 10, 11 },
            new int[] { 2, 5, 0 },
            new int[] { 9, 0, 8 },
            new int[] { 3, 11, 10 },
        };

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
        
        public string GetDescription()
        {
            // Get a random index
            System.Random rand = new System.Random();
            int randomIndex = rand.Next(levelDescriptions.Length);

            // Retrieve the description and tile list for the random index
            string randomDescription = levelDescriptions[randomIndex];
            int[] randomTileList = tileLists[randomIndex];
            UIManger.Instance.SetTilesHint(randomTileList);
            return randomDescription;
        }
    }
}