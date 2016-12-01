using UnityEngine;
using System.Collections;

public class show_panel : MonoBehaviour {

    public Select_Right_1 R1;
    public GameObject RP1;
    public GameObject B1;
    public Select_Right_2 R2;
    public GameObject RP2;
    public GameObject B2;
    public Select_Left L;

    public WalkInConstellation walk;

    public Canvas menu;
    public bool active;
    public bool push;


	// Use this for initialization
	void Start () {
        menu.enabled = false;
        active = false;
        push = true;
        Vector3 p =new Vector3(0, -100, 0);
        menu.transform.localPosition = p;
	}
	
	// Update is called once per frame
	void Update () {
        if (R1.active)
        {
            RP1.SetActive(true);
            B1.SetActive(true);
        }
        else
        {
            RP1.SetActive(false);
            B1.SetActive(false);
        }

        if (R2.active)
        {
            RP2.SetActive(true);
            B2.SetActive(true);
        }
        else
        {
            RP2.SetActive(false);
            B2.SetActive(false);
        }
        if (Input.GetAxis("Fire2") > 0 && push && walk.notInGame)
        {
            active = !active;
            if (active)
            {
                R1.active = false;
                R2.active = false;
                L.On = true;
                L.sel = 0;
                L.last = 1;
            }
            menu.enabled = active;
            push = !push;
        }

        if (Input.GetAxis("Fire2") == 0 && !push)
            push = !push;
    }
}
