using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class PlayVideo_UrsaMajor : MonoBehaviour {
    private MovieTexture movie;

    // Use this for initialization
    void Start()
    {
        Renderer r = GetComponent<Renderer>();
        movie = (MovieTexture)r.material.mainTexture;
        movie.Play();
    }

    // Update is called once per frame
    void Update()
    {
        if (!movie.isPlaying || Input.GetKeyDown(KeyCode.J))
        {
            SceneManager.LoadScene("Intro_2");
        }
    }
}
