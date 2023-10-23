using System.Collections;
using System.Collections.Generic;
using Character;
using Editor.TileSystem;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace UI
{
    public class UIManger : MonoBehaviour
    {
        public GameObject profile;
        public GameObject dialogueBox;
        public TMP_InputField inputField;
        public TextMeshProUGUI description;
        public Button btnGo;
        public AudioSource audAppear;
        public GameObject hint;
        public GameObject tileset;
        public List<GameObject> tileHints;

        private Animator _aProfile;
        private Animator _aDialogueBox;

        public static UIManger Instance;

        public static bool BEnable;
        private static readonly int Pop = Animator.StringToHash("pop");


        private void Awake()
        {
            Instance = this;
        }

        private void Start()
        {
            _aProfile = profile.GetComponent<Animator>();
            _aDialogueBox = dialogueBox.GetComponent<Animator>();

            // initially set it to true
            OnEnableDialogue();
        }

        private void Update()
        {
            if (!PlayerController.BEnd && BEnable && Input.GetKeyDown(KeyCode.Return))
            {
                OnClickGo();
            }
        }

        public void OnEnableDialogue()
        {
            BEnable = true;
            hint.SetActive(false);
            // tileset.SetActive(true);
            OnTriggerProfile();
            OnTriggerDialogueBox();
            StartCoroutine(PlayAppearAudio());
        }

        IEnumerator PlayAppearAudio()
        {
            yield return new WaitForSeconds(0.5f);
            audAppear.Play();
        }

        public void OnDisableDialogue()
        {
            BEnable = false;
            hint.SetActive(true);
            // tileset.SetActive(false);
            OnTriggerProfile();
            OnTriggerDialogueBox();
        }


        private void OnTriggerProfile()
        {
            _aProfile.SetBool(Pop, BEnable);
        }


        private void OnTriggerDialogueBox()
        {
            _aDialogueBox.SetBool(Pop, BEnable);
        }


        public void OnClickGo()
        {
            if (description.text.Length is 1 or 0)
            {
                UITypeWriter.Instance.SetDialogue("You don't enter any description!!", play: true,
                    showDescription: true);
            }
            else
            {
                inputField.text = "<color=#191970>" + description.text + "</color>";
                inputField.interactable = false;
                btnGo.enabled = false;
                OnDisableDialogue();
                MapGenerator.Instance.OnGenerateMap(description.text);
            }
        }

        public void OnResetUI()
        {
            btnGo.enabled = true;
            inputField.interactable = true;
            inputField.text = "";
            SetTilesHint(null);
        }

        public void OnClickHelp()
        {
            SceneManager.LoadScene("Help");
        }


        public void SetTilesHint(int[] tiles)
        {
            foreach (var tileHint in tileHints)
            {
                tileHint.SetActive(false);
            }

            if (tiles == null) return;
            foreach (var index in tiles)
            {
                tileHints[index].SetActive(true);
            }
        }
    }
}