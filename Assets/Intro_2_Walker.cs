using UnityEngine;
using System.Collections;

public class Intro_2_Walker : MonoBehaviour
{

    public float speed = 30.0F;
    public Camera MainCam;

    void Update()
    {
      
            if (Input.GetKey(KeyCode.W) || Input.GetAxis("Vertical") < 0)
                transform.Translate(MainCam.transform.forward * speed * Time.deltaTime);

            if (Input.GetKey(KeyCode.A) || Input.GetAxis("Horizontal") < 0)
                transform.Translate(MainCam.transform.right * -1 * speed * Time.deltaTime);

            if (Input.GetKey(KeyCode.D) || Input.GetAxis("Horizontal") > 0)
                transform.Translate(MainCam.transform.right * speed * Time.deltaTime);

            if (Input.GetKey(KeyCode.S) || Input.GetAxis("Vertical") > 0)
                transform.Translate(MainCam.transform.forward * -1 * speed * Time.deltaTime);

    }
}
