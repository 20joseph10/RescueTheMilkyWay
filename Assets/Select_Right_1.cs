using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.IO;

public class Select_Right_1 : MonoBehaviour
{

    public Text[] texts;
    public int sel;
    public int len;
    public int last;
    public bool move;
    public Scrollbar SB;
    public bool On;
    public float scroll = (float)1 / 83;
    public bool active;
    public double up_bond;
    public double low_bond;

    public bool push;

    public WalkInConstellation Walk;
    public show_panel panel;

    public Select_Left L;

    private string[] cons_name;
    // Use this for initialization
    void Start()
    {
        texts = GetComponentsInChildren<Text>();
        foreach (Text t in texts)
            t.color = Color.white;
        cons_name = new string[texts.Length];
        sel = 0;
        last = 1;
        len = texts.Length;
        move = true;
        On = false;
        active = false;

        TextAsset nameData1 = Resources.Load("ConstellationData/ConstellationCSV/_names") as TextAsset;
        TextAsset nameData = Resources.Load("ConstellationData/ConstellationCSV/_names_full") as TextAsset;

        string[] name1 = nameData1.ToString().Split('\n');
        string[] name = nameData.ToString().Split('\n');

        for (int i = 0; i < name.Length; i++)
        {
            texts[i].text = name[i];
            cons_name[i] = name1[i].Trim();
            texts[i].fontSize = 40;
        }


        push = false;
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetAxis("Fire1") == 0)
            push = true;

        up_bond = (double)-44f - (1.0f - SB.value) * 83f * 88f;
        low_bond = up_bond - 4f * 88f;
        if (On)
        {
            if (!move)
                if (Input.GetAxis("Vertical") == 0)
                    move = true;
            if (move)
            {
                if (Input.GetAxis("Vertical") < 0)
                {
                    move = false;
                    if (sel > 0)
                    {
                        last = sel;
                        sel--;
                    }
                }
                if (Input.GetAxis("Vertical") > 0)
                {
                    move = false;
                    if (sel < len - 1)
                    {
                        last = sel;
                        sel++;
                    }
                }
            }
            texts[last].color = Color.white;
            texts[sel].color = Color.red;

            if (texts[sel].transform.localPosition.y < low_bond)
                SB.value -= scroll;
            if (texts[sel].transform.localPosition.y > up_bond)
                SB.value += scroll;

            if (Input.GetAxis("Fire2") > 0)
            {
                texts[sel].color = Color.white;
                sel = 0;
                On = false;
                L.On = true;
                active = false;
            }

            if (push && Input.GetAxis("Fire1") > 0)
            {
                panel.menu.enabled = false;
                panel.active = false;
                Walk.moveTo(cons_name[sel]);
                active = false;
            }
        }
    }
}

