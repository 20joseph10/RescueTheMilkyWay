using UnityEngine;
using System.Collections;

public class ScriptCaller : MonoBehaviour {
    public GameObject walk;


	// Use this for initialization
	void Start () {
	
	}

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.U))
        {
            GetComponent<WalkInConstellation>().startGame("Sgr");
        }

        if (Input.GetKeyDown(KeyCode.Y))
        {
            GetComponent<WalkInConstellation>().startGame("Gem");
        }

        if (Input.GetKeyDown(KeyCode.End))
        {
            GetComponent<WalkInConstellation>().showAll();
        }

    }
}
