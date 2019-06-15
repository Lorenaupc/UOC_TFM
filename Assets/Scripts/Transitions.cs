using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Transitions : MonoBehaviour {

    public Vector2 cameraChange;
    public Vector3 playerChange;
    private SmoothCamera cam;
    bool colliding = false;

    private void Start()
    {
        cam = Camera.main.GetComponent<SmoothCamera>(); 
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player" && !colliding && this.isActiveAndEnabled)
        {
            collision.transform.position += playerChange;
            cam.minPosition += cameraChange;
            cam.maxPosition += cameraChange;
            colliding = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (colliding)  colliding = false;
    }

}
