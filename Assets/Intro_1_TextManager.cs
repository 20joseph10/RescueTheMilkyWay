using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Intro_1_TextManager : MonoBehaviour {


    public Text tu_text;

    private string[] tutorial;
    private int count=0;

	// Use this for initialization
	void Start () {
        tutorial = new string[6];
        tutorial[0] = "Welcome to <b>VR Universe</b>" + "\nHere are some toturials" + "\nUse the red dot and <b><color=green>A</color></b> to choose";
        tutorial[1] = "Great!\n" + "This world used data from\n<b><color=red>Gaia Dataset</color></b> and other sources." + "\nThe positions of the stars are real" ;
        tutorial[2] = "In this world you \ncould travel around using <b><color=cyan>GAMEPAD</color></b>";
        tutorial[3] = "A constellation is a region of\nthe celestial sphere\n" + "There are <color=yellow>88</color> of them";
        tutorial[4] = "Constellations are <color=red>projections</color> to\nthe earth, they may be far away from each other\n" + "The graph will be <color=red>different</color>\nif not observing from the earth";
        tutorial[5] = "For safety\nuse <b><color=yellow>Y</color></b> to see where you are in a map\n" + "Try it!";
    }
	
	// Update is called once per frame
	void Update () {
        if (count >= tutorial.Length || Input.GetKeyDown(KeyCode.J))
        {
            SceneManager.LoadScene("Intro_2_Movie");
        }
    }

    public void next() {
        count++;
        tu_text.text  = tutorial[count];
        if (count>=tutorial.Length || Input.GetKeyDown(KeyCode.J))
        {
            SceneManager.LoadScene("Intro_2_Movie");
        }
    }

    public void back()
    {
        if (count > 0) { count--; }
        tu_text.text = tutorial[count];
    }
}
