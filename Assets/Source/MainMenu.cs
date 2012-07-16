using UnityEngine;
using System.Collections;

public class MainMenu : MonoBehaviour {
    public Texture2D mainmenu;
    public Texture2D play;

    public Texture2D story1;
    public Texture2D story2;
    public Texture2D story3;

    private Texture2D story;

    private enum MainMenuStates
    {
        MainMenu,
        Story
    }
    private MainMenuStates MainMenuState;
	// Use this for initialization
	void Start () {
        MainMenuState = MainMenuStates.MainMenu;
        story = story1;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnGUI()
    {
        if (MainMenuState == MainMenuStates.MainMenu)
        {
            GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), mainmenu);
            if (GUI.Button(new Rect(416f / 1024 * Screen.width, 460f / 768 * Screen.height,
                310f / 1024 * Screen.width, 214f / 768 * Screen.height), play))
            {
                MainMenuState = MainMenuStates.Story;
                StartCoroutine("show2");
            }
        }
        else
        {
            GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), story);
        }
        
    }

    IEnumerator show2()
    {
        yield return new WaitForSeconds(2.5f);
        story = story2;
        StartCoroutine("show3");
    }

    IEnumerator show3()
    {
        yield return new WaitForSeconds(2.5f);
        story = story3;
        StartCoroutine("start");
    }

    IEnumerator start()
    {
        yield return new WaitForSeconds(5);
        Application.LoadLevel("Level1");
    }
    
}
