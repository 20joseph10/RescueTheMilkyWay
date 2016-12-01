using UnityEngine;
using System;
using System.IO;
using System.Collections.Generic;
using VRStandardAssets.Examples;

public class Creat_Constellation : MonoBehaviour {
    // Use this for initialization
    public long N = 20;
    public long M = 2;
    public float scale = 1f;
    public Boolean mat = true;

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

        foreach (KeyValuePair<string, Constellation> current_constellation in cons.getConstellations())
        {

            //if (String.Equals(current_constellation.Value.Name, "Sgr")) {
                int count = 0;
                foreach (KeyValuePair<string, Star> current_star in current_constellation.Value.stars)
                {
                count++;
                if (count > 20) break;
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

            /*foreach (Constellation.StarLine starLine in current_constellation.Value.topoMap) {
                try
                {
                    line[i] = (GameObject)Instantiate(Resources.Load("Line", typeof(GameObject)));

                    Star tempStar;
                    current_constellation.Value.stars.TryGetValue(starLine.starA, out tempStar);
                    line[i].GetComponent<ConnectLine>().a = tempStar.starObject.transform.position;
                    current_constellation.Value.stars.TryGetValue(starLine.starB, out tempStar);
                    line[i].GetComponent<ConnectLine>().b = tempStar.starObject.transform.position;
                    line[i].SetActive(true);
                }
                catch (Exception e) {
                    continue;
                }
            }
            */
            //}
        }

        Debug.Log("stars_num:" + Count);
    }

    void Update()
    {

    }
    public static Constellations getConstellations()
    {
        return cons;
    }

}
