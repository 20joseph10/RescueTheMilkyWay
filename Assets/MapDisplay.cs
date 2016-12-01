using UnityEngine;
using System.Collections;

public class MapDisplay : MonoBehaviour{

    public GameObject map; // Assign in inspector
    public GameObject Dot;
    public GameObject mainCam;

    private bool flickFlag = false;
    public int flickRate = 20;

    private int count = 0;

    void Start()
    {
        map.SetActive(false);
        Dot.transform.localPosition = new Vector3(0.0f, 0.0f, 0.0f);
    }


    void Update()
    {
        float[] position = getRA_Dec(mainCam.transform.position.x, mainCam.transform.position.y, -mainCam.transform.position.z);
        Dot.transform.localPosition = new Vector3(position[0], position[1], 0.0f);

        if (Input.GetKeyDown(KeyCode.Tab) || Input.GetButtonDown("Jump"))
        {
            map.SetActive(true);
            flickFlag = true;
        }
        
        if (flickFlag && count == 0)
        {
            Dot.SetActive(!Dot.activeSelf);
        }

        count++;
        if (count == flickRate)
        {
            count = 0;
        }

        if (Input.GetKeyUp(KeyCode.Tab) || Input.GetButtonUp("Jump"))
        {
            map.SetActive(false);
            flickFlag = false;
        }
    }



    private float[] getRA_Dec(float tempx, float tempy, float tempz) {
        float[] result = new float[2];

        float r = Mathf.Sqrt(tempx * tempx + tempy * tempy + tempz * tempz);
        if (r < 1e-5) r = 1e-5f;
        float B = Mathf.Asin(tempz / r);
        float A = Mathf.Asin(tempy / r / Mathf.Cos(B));
        float A1 = Mathf.Acos(tempx / r / Mathf.Cos(B));

        if (A>0)
        {
            if ((Mathf.PI - A) == A1 || (Mathf.PI - A) == 2 * Mathf.PI - A1)
                A = Mathf.PI - A;
        } else if (A < 0) {
            if ((A + 2 * Mathf.PI) == A1 || (A + 2 * Mathf.PI) == 2 * Mathf.PI - A1)
                A = A + 2 * Mathf.PI;
            else
                A = Mathf.PI - A;
        }

        //change to degree
        B = B/Mathf.PI * 180f;
        A = A/Mathf.PI * 180f;

        float H = Mathf.Floor(A / 15);
        float Mi = Mathf.Floor((A - H*15) /0.25f);
        float Se = Mathf.Floor((A - H*15 - Mi*0.25f) / 0.004166f );
        
        H = H + Mi / 60.0f + Se / 3600.0f;

        result[0] = 88.2f - 88.2f / 24f * H;
        result[1] = -22.05f + 22.05f / 90f * B;

        return result; 

    }
}
