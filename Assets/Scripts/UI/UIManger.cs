using UnityEngine;

namespace UI
{
    public class UIManger : MonoBehaviour
    {
        public GameObject profile;
        public GameObject dialogueBox;
        private Animator _aProfile;
        private Animator _aDialogueBox;

        public static bool BEnable;
        private static readonly int Pop = Animator.StringToHash("pop");


        private void Start()
        {
            _aProfile = profile.GetComponent<Animator>();
            _aDialogueBox = dialogueBox.GetComponent<Animator>();

            // initially set it to true
            OnTriggerDialogue();
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Tab))
            {
                OnTriggerDialogue();
            }
        }

        private void OnTriggerDialogue()
        {
            BEnable = !BEnable;
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
    }
}