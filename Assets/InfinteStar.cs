﻿using UnityEngine;
using System.Collections;

public class InfinteStar : MonoBehaviour {

    private Transform tx;
    private ParticleSystem.Particle[] points;

    public int starMax = 100;
    public float starSize = 1;
    public float starDistance = 10;
    public float starClipDistance = 1;
    private float starDistanceSqr;
    private float starClipDistanceSqr;

	// Use this for initialization
	void Start () {
        tx = transform;
        starDistanceSqr = starDistance * starDistance;
        starClipDistanceSqr = starClipDistance * starClipDistance;
	}

    private void CreateStars() {
        points = new ParticleSystem.Particle[starMax];

        for (int i =0; i< starMax; i++)
        {
            points[i].position = Random.insideUnitSphere * starDistance + tx.position;
            points[i].startColor = new Color(1, 1, 1, 1);
            points[i].startSize = starSize;
        }
    }

	// Update is called once per frame
	void Update () {
        if (points == null) CreateStars();

        for(int i = 0; i<starMax; i++)
        {
            if ((points[i].position - tx.position).sqrMagnitude > starDistanceSqr) {
                points[i].position = Random.insideUnitSphere * starDistance + tx.position;
            }

            if ((points[i].position - tx.position).sqrMagnitude <= starClipDistanceSqr)
            {
                float percent = (points[i].position - tx.position).sqrMagnitude / starClipDistanceSqr;

                points[i].startColor = new Color(1, 1, 1, percent);
                points[i].startSize = starSize;
            }

        }
        GetComponent<ParticleSystem>().SetParticles(points, points.Length);
	}
}
