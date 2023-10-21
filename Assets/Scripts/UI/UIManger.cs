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
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Tab))
            {
                BEnable = !BEnable;
                OnTriggerProfile();
                OnTriggerDialogueBox();
            }
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