using UnityEngine;
using System;
using System.IO;
using System.Collections.Generic;
using VRStandardAssets.Examples;
using UnityEngine.SceneManagement;

public class Intro_2_Creat : MonoBehaviour {
    // Use this for initialization
    public long N = 20;
    public long M = 2;
    public float scale = 1f;
    public Boolean mat = true;

    public GameObject gb;


    private bool FirstTimeGame = false;
    private static Constellations cons;
    void Start()
    {
        cons = new Constellations();
        Material newMat = Resources.Load("Sun", typeof(Material)) as Material;

        GameObject[] sphere = new GameObject[N];
        long Count = 0;
        System.Random rdm = new System.Random();

        GameObject[] line = new GameObject[180];
        int i = 0;

        Constellation current_constellation;

        cons.getConstellations().TryGetValue("UMa", out current_constellation);

        int count = 0;
        foreach (KeyValuePair<string, Star> current_star in current_constellation.stars)
        {
            count++;
            float tx = current_star.Value.location.x;
            float ty = current_star.Value.location.y;
            float tz = -current_star.Value.location.z;

            if (mat)
            {
                String name = "Sunpre" + rdm.Next(0, 3).ToString();
                sphere[Count] = (GameObject)Instantiate(Resources.Load(name, typeof(GameObject)));
            }
            else
            {
                sphere[Count] = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            }
            sphere[Count].transform.position = new Vector3(tx * M, ty * M, tz * M);
            sphere[Count].transform.localScale = new Vector3(scale, scale, scale);
            current_star.Value.starObject = sphere[Count];

            sphere[Count].GetComponent<ExampleInteractiveItem>().StarInfo = current_star.Value;

            Count++;
            if (Count > N - 1) return;
        }



        /*foreach (Constellation.StarLine starLine in current_constellation.topoMap) {
            try
            {
                line[i] = (GameObject)Instantiate(Resources.Load("Line", typeof(GameObject)));

                Star tempStar;
                current_constellation.stars.TryGetValue(starLine.starA, out tempStar);
                line[i].GetComponent<ConnectLine>().a = tempStar.starObject.transform.position;
                current_constellation.stars.TryGetValue(starLine.starB, out tempStar);
                line[i].GetComponent<ConnectLine>().b = tempStar.starObject.transform.position;
                line[i].SetActive(true);
            }
            catch (Exception e) {
                continue;
            }
        }
        
    */
        
        
    }

    void Update()
    {
        if (Input.GetAxis("Fire1") > 0 && !FirstTimeGame){
            gb.GetComponent<WalkInConstellation>().startGame("UMa");
            FirstTimeGame = true;
        }
    }
    public static Constellations getConstellations()
    {
        return cons;
    }
}
