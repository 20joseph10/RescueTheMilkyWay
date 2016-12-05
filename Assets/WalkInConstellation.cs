using UnityEngine;
using System.Collections;
using System;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class WalkInConstellation : MonoBehaviour {
    private static Constellations cons;
    public GameObject cam;
	// Use this for initialization
    public int distanceToTrigger = 10;
    public Text countText;
    public Text winText;
    public Transform textParent;
    public AudioSource starFindSound;

    private bool startGam = false;
    public bool notInGame = true;

    public bool firstTimeFinish = false;

    private bool notInCongradulation = false;
    private int currentIndex = -1;
    private GameObject[] line;
    private int totalLines = 0;
    private GameObject Arrow;
    private GameObject star;


    public string constellation_Walk = "Lib";
    public bool intro;

    private Star[] stars;

    void Start () {
        //line = new GameObject[20];
        //Arrow = new GameObject();
        //stars = new Star[20];
    }
	
	// Update is called once per frame
	void Update () {
        if (intro)
            cons = Intro_2_Creat.getConstellations();
        else
            cons = Creat_Constellation.getConstellations();

        if (startGam && notInGame)
        {
            //new game
            notInGame = false;
            line = new GameObject[20];
            Constellation current_constellation;
            stars = new Star[20];
            Arrow = new GameObject();
            currentIndex = 0;
            totalLines = 0;
            Arrow = (GameObject)Instantiate(Resources.Load("Arrow", typeof(GameObject)));

            cons.getConstellations().TryGetValue(constellation_Walk, out current_constellation);
            
            foreach (Constellation.StarLine starLine in current_constellation.topoMap)
            {
                try
                {
                    line[totalLines] = (GameObject)Instantiate(Resources.Load("Line", typeof(GameObject)));
                    Star tempStar;
                    current_constellation.stars.TryGetValue(starLine.starA, out tempStar);
                    line[totalLines].GetComponent<ConnectLine>().a = tempStar.starObject.transform.position;
                    current_constellation.stars.TryGetValue(starLine.starB, out tempStar);
                    line[totalLines].GetComponent<ConnectLine>().b = tempStar.starObject.transform.position;
                    stars[totalLines] = tempStar;
                    totalLines++;
                }
                catch (Exception e)
                {
                    continue;
                }
            }
            startGam = false;

        }


        if (!notInGame && currentIndex<totalLines && !notInCongradulation) {
            countText.text = "Line: " + constellation_Walk + "\nTotal Lines: <b><color=yellow>" + (totalLines + 1) + "</color></b>\nNext Star: <b><color=red>" + stars[currentIndex].Name + "</color></b>\nInfo:\nPosition: X:" + Mathf.Round(stars[currentIndex].location.x*100)/100 + " Y:" + Mathf.Round(stars[currentIndex].location.y*100)/100
                 + " Z:" + Mathf.Round(stars[currentIndex].location.z*100)/100 + "\nDistance: <b><color=red>" + Mathf.Round(distance(line[currentIndex].GetComponent<ConnectLine>().b, transform.position)*100)/200 + "</color></b> Light Year(s)";

            var rot = Quaternion.LookRotation(line[currentIndex].GetComponent<ConnectLine>().b - transform.position);
            Arrow.transform.rotation = Quaternion.Slerp(Arrow.transform.rotation, rot, Time.deltaTime * 0.9f);
            Vector3 moveArrowTo = transform.position + cam.transform.forward * 15.0f + Vector3.up * 1.0f + Vector3.right * 3.0f;
            float smoothFac = 0.6f;

            Arrow.transform.position = smoothFac * Arrow.transform.position + moveArrowTo * (1 - smoothFac);

            if (distance(line[currentIndex].GetComponent<ConnectLine>().b, transform.position) < distanceToTrigger) {
                starFindSound.PlayOneShot(starFindSound.clip, 0.7f);
                line[currentIndex].SetActive(true);
                currentIndex++;
            }


            //Only for Debug
            if (Input.GetKeyDown(KeyCode.G)) {
                starFindSound.PlayOneShot(starFindSound.clip, 0.7f);
                line[currentIndex].SetActive(true);
                currentIndex++;
            }


        }

        if (currentIndex >= totalLines) {
            //finished game
            countText.text = "";
            Debug.Log(constellation_Walk);
            star = (GameObject)Instantiate(Resources.Load(constellation_Walk.Trim(), typeof(GameObject)));
            star.transform.SetParent(textParent, false);
            star.transform.localPosition = new Vector3(-52.3f,137.6f,0.1f);
            star.transform.localScale = new Vector3(3.5f, 3.5f, 0.5f);

            winText.text = "You made it!!!\nYou linked " + constellation_Walk + ".\nGo around to see what it is like.\nPress <b><color=red>X</color></b> to quit Game Mode\nPress <b><color=green>B</color></b> to return to earth";
            currentIndex = -1;
            totalLines = 0;
            Destroy(Arrow);
            //transform.position = new Vector3(0f, 0f, 0f);
            notInCongradulation = true;

            firstTimeFinish = true;
        }

        if (notInCongradulation&&(Input.GetKeyDown(KeyCode.Q)||Input.GetAxis("Fire3")>0)) {
            Destroy(star);
            winText.text = "";
            transform.position = new Vector3(0f, 0f, 0f);
            if (intro) {
                SceneManager.LoadScene("Intro_2_Constellation");
            }
            notInGame = true;
        }

        if ((Input.GetKeyDown(KeyCode.R)||Input.GetAxis("Fire2") > 0 )&& !notInGame)
        {
            transform.position = new Vector3(0f, 0f, 0f);
        }

    }


    private float distance(Vector3 p1, Vector3 p2) {
        float x1_x2 = p1.x - p2.x;
        float y1_y2 = p1.y - p2.y;
        float z1_z2 = p1.z - p2.z;
        return Mathf.Sqrt(x1_x2 * x1_x2 + y1_y2 * y1_y2 + z1_z2 * z1_z2);
    }

    public void startGame(String consname) {

        if (notInGame)
        {
            Constellation current_constellation;
            try
            {
                cons.getConstellations().TryGetValue(consname, out current_constellation);
            }
            catch (Exception e)
            {
                Debug.Log("Too few stars generated in Constellation:" + consname);
                return;
            }
            constellation_Walk = consname;
            startGam = true;
            notInCongradulation = false;

        }
        else {
            Debug.Log("Already in a game");
        }
    }

    public void moveTo(String consname) {
        Constellation current_constellation;
        cons.getConstellations().TryGetValue(consname, out current_constellation);
        foreach (KeyValuePair<string, Star> current_star in current_constellation.stars) {
            transform.position = current_star.Value.starObject.transform.position;
            break;
        }
    }


    public void showAll()
    {
        GameObject[] line = new GameObject[180];
        int i = 0;
        foreach (KeyValuePair<string, Constellation> current_constellation in cons.getConstellations())
        {

            foreach (Constellation.StarLine starLine in current_constellation.Value.topoMap)
            {
                try
                {
                    line[i] = (GameObject)Instantiate(Resources.Load("Line", typeof(GameObject)));

                    Star tempStar;
                    current_constellation.Value.stars.TryGetValue(starLine.starA, out tempStar);
                    line[i].GetComponent<ConnectLine>().a = tempStar.starObject.transform.position;
                    current_constellation.Value.stars.TryGetValue(starLine.starB, out tempStar);
                    line[i].GetComponent<ConnectLine>().b = tempStar.starObject.transform.position;
                    line[i].SetActive(true);
                    i++;
                }
                catch (Exception e)
                {
                    continue;
                }
            }
        }
    }

}
