using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class Constellations
{
    private Dictionary<string, Constellation> constellations;

    public Constellations()
    {
        constellations = new Dictionary<string, Constellation>();
        readConsFromFile();
    }


    public Dictionary<string, Constellation> getConstellations()
    {
        return constellations;
    }

    public void readConsFromFile()
    {
        int count = 0;

        TextAsset nameData = Resources.Load("ConstellationData/ConstellationCSV/_names") as TextAsset;

        string[] names = nameData.text.ToString().Split('\n');



        for (int iname = 0; iname<names.Length; iname++)
        {
            string name = names[iname].Trim();
            string CSVname = "ConstellationData/ConstellationCSV/Cons_" + name;

            TextAsset tempCSVData = Resources.Load(CSVname) as TextAsset;

            bool RApositionFind = false;
            int RAposition = 0;
            Constellation cons = new Constellation(name);

            foreach (String line in tempCSVData.ToString().Split('\n'))
            {
                string[] temp = line.Split(',');
                if (temp.Length > 10)
                {
                    //string Starname = Time.time.ToString();
                    string Starname = DateTime.Now.ToString();
                    string B = "";
                    string Var = "";
                    string HD = "";
                    string HIP = "";
                    string RA = "";
                    string DEC = "";
                    string vismag = "";
                    string absmag = "";
                    string dist = "";
                    string Sp_class = "";
                    string notes = "";
                    if (temp[0].Equals("Name")) continue;
                    if (temp[0].Equals("[TABLE]\"")) continue;
                    if (!RApositionFind)
                    {
                        for (int i = 0; i < temp.Length; i++)
                        {
                            if (temp[i].Length > 6 && temp[i].ToCharArray()[2] == 'h' && temp[i].ToCharArray()[6] == 'm' && temp[i].ToCharArray()[temp[i].ToCharArray().Length - 1] == 's')
                            {
                                RApositionFind = true;
                                RAposition = i;
                            }
                        }
                    }
                    if (RApositionFind)
                    {
                        if (temp[RAposition].Length > 7 && temp[RAposition].ToCharArray()[2] == 'h' && temp[RAposition].ToCharArray()[temp[RAposition].ToCharArray().Length - 1] == 's')
                        {
                            Starname = temp[0];
                            B = temp[1];
                            Var = temp[RAposition - 3];
                            HD = temp[RAposition - 2];
                            HIP = temp[RAposition - 1];
                            RA = temp[RAposition];
                            DEC = temp[RAposition + 1];
                            vismag = temp[RAposition + 2];
                            absmag = temp[RAposition + 3];
                            dist = temp[RAposition + 4];
                            if (temp.Length > RAposition + 5)
                                Sp_class = temp[RAposition + 5];
                            if (temp.Length > RAposition + 6)
                                notes = temp[RAposition + 6];
                        }
                        else
                        {
                            for (int i = 0; i < temp.Length; i++)
                            {
                                if (temp[i].Length > 6 && temp[i].ToCharArray()[2] == 'h' && temp[i].ToCharArray()[6] == 'm' && temp[i].ToCharArray()[temp[i].ToCharArray().Length - 1] == 's')
                                {
                                    RApositionFind = true;
                                    RAposition = i;
                                }
                            }
                            Starname = temp[0];
                            B = temp[1];
                            Var = temp[RAposition - 3];
                            HD = temp[RAposition - 2];
                            HIP = temp[RAposition - 1];
                            RA = temp[RAposition];
                            DEC = temp[RAposition + 1];
                            vismag = temp[RAposition + 2];
                            absmag = temp[RAposition + 3];
                            dist = temp[RAposition + 4];
                            if (temp.Length > RAposition + 5)
                                Sp_class = temp[RAposition + 5];
                            if (temp.Length > RAposition + 6)
                                notes = temp[RAposition + 6];

                        }

                    }

                    if (dist == "") continue;
                    if (Starname == "")
                        Starname = string.Format(@"NA_Name{0}", Guid.NewGuid());
                    try
                    {
                        float distemp = float.Parse(dist);
                    }
                    catch (Exception e)
                    {
                        continue;
                    }
                    try
                    {
                        Star tempStar = new Star(Starname, B, Var, HD, HIP, RA, DEC, vismag, absmag, dist, Sp_class, notes);
                        try
                        {
                            cons.stars.Add(tempStar.Name, tempStar);                   
                        }
                        catch (Exception e)
                        {
                            tempStar.Name = tempStar.Name + string.Format(@"{0}", Guid.NewGuid());
                            cons.stars.Add(tempStar.Name, tempStar);
                        }

                    }
                    catch (Exception e)
                    {
                        continue;
                    }
                }
                count++;
            }

            constellations.Add(name, cons);
        }

        TextAsset LineData = Resources.Load("ConstellationData/Const_line") as TextAsset;

        Constellation currentCon = null;
        foreach (string temp in LineData.ToString().Split('\n'))
        {
            string[] topo = temp.Split(',');
            if (topo.Length == 1)
            {
                constellations.TryGetValue(topo[0].Trim(), out currentCon);
                continue;
            }
            currentCon.topoMap.Add(new Constellation.StarLine(topo[0].Trim(), topo[1].Trim()));
        }
        
    }

}
