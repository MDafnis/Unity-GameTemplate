using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

namespace MD_Player
{
    public class MenuScript : MonoBehaviour
    {
        public Transform menuPanel;
        public Transform controlMenu;
        public static MenuScript instance; // create an instance

        //Buttons
        public KeyCode jump { get; set;}
        public KeyCode left { get; set; }
        public KeyCode crouch { get; set; }
        public KeyCode right { get; set; }
        public KeyCode attack { get; set; }
        public KeyCode use { get; set; }

        Event keyEvent; // will be used to detect what button the player is pressing
        public TextMeshProUGUI buttonText; // will be swapping out buttontext
        KeyCode newKey; // will be used to assign a new key
        bool waitingForKey; // will indicate that we'll wait for the player to enter their new key

        private void Awake()
        {
            if (instance == null)
            {
                //DontDestroyOnLoad(gameObject);
                instance = this;
            }
            else if (instance != this)
                Destroy(gameObject);

            jump = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("Jump", "UpArrow"));
            left = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("Left", "LeftArrow"));
            crouch = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("Crouch", "DownArrow"));
            right = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("Right", "RightArrow"));
            attack = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("Attack", "Mouse0"));
            use = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("Use", "RightControl"));
        }

        private void Start()
        {
            waitingForKey = false;

            for (int i = 0; i < controlMenu.childCount; i++)
            {
                if (controlMenu.GetChild(i).name == "Jump")
                    controlMenu.GetChild(i).GetComponentInChildren<TextMeshProUGUI>().text = jump.ToString();
                if (controlMenu.GetChild(i).name == "Left")
                    controlMenu.GetChild(i).GetComponentInChildren<TextMeshProUGUI>().text = left.ToString();
                if (controlMenu.GetChild(i).name == "Crouch")
                    controlMenu.GetChild(i).GetComponentInChildren<TextMeshProUGUI>().text = crouch.ToString();
                if (controlMenu.GetChild(i).name == "Right")
                    controlMenu.GetChild(i).GetComponentInChildren<TextMeshProUGUI>().text = right.ToString();
                if (controlMenu.GetChild(i).name == "Attack")
                    controlMenu.GetChild(i).GetComponentInChildren<TextMeshProUGUI>().text = attack.ToString();
                if (controlMenu.GetChild(i).name == "Use")
                    controlMenu.GetChild(i).GetComponentInChildren<TextMeshProUGUI>().text = use.ToString();
            }
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                if (!menuPanel.gameObject.activeSelf)
                {
                    menuPanel.gameObject.SetActive(true);
                    Time.timeScale = 0;
                }
                else if (menuPanel.gameObject.activeSelf)
                {
                    menuPanel.gameObject.SetActive(false);
                    Time.timeScale = 1;
                }
            }  
        }

        private void OnGUI()
        {
            keyEvent = Event.current;

            if(keyEvent.isKey && waitingForKey)
            {
                newKey = keyEvent.keyCode;
                waitingForKey = false;
            }
        }

        public void StartAssignment(string keyName)
        {
            buttonText.text = "";
            if(!waitingForKey)
            {
                StartCoroutine(AssignKey(keyName));
            }
        }

        public void ModifyText(TextMeshProUGUI text)
        {
            buttonText = text;
        }

        IEnumerator WaitForKey()
        {
            while (!keyEvent.isKey)
                yield return null;
        }

        public IEnumerator AssignKey(string keyName)
        {
            waitingForKey = true;
            yield return WaitForKey();

            switch(keyName)
            {
                case "jump":
                    jump = newKey;
                    buttonText.text = jump.ToString();
                    PlayerPrefs.SetString("Jump", jump.ToString());
                    break;

                case "left":
                    left = newKey;
                    buttonText.text = left.ToString();
                    PlayerPrefs.SetString("Left", left.ToString());
                    break;

                case "crouch":
                    crouch = newKey;
                    buttonText.text = crouch.ToString();
                    PlayerPrefs.SetString("Crouch", crouch.ToString());
                    break;

                case "right":
                    right = newKey;
                    buttonText.text = right.ToString();
                    PlayerPrefs.SetString("Right", right.ToString());
                    break;

                case "attack":
                    attack = newKey;
                    buttonText.text = attack.ToString();
                    PlayerPrefs.SetString("Attack", attack.ToString());
                    break;

                case "use":
                    use = newKey;
                    buttonText.text = use.ToString();
                    PlayerPrefs.SetString("Use", use.ToString());
                    break;
            }

            yield return null; // should wait an extra frame.
        }
    }
}
