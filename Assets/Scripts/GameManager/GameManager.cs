using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    static GameManager instance;
    private GameObject player;
    private GameObject canvas;

    public Sprite groundObject;
    public Sprite seedObject;
    public Sprite growSeedObject;
    public Sprite wateredSeedObject;
    public Sprite normalGrassObject;
    public Sprite grassObject;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        canvas = GameObject.FindGameObjectWithTag("Canvas");
        player = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 mousePos2D = new Vector2(mousePos.x, mousePos.y);

            RaycastHit2D hit = Physics2D.Raycast(mousePos2D, Vector2.zero);
            if (hit.collider != null)
            {
                if (canvas.GetComponent<Tools>().currentTool != null) {
                    switch (canvas.GetComponent<Tools>().currentTool)
                    {
                        case ("Hoe"):
                            if (hit.collider.tag == "NormalGrass")
                            {
                                hit.collider.gameObject.GetComponent<SpriteRenderer>().sprite = groundObject;
                                hit.collider.gameObject.GetComponent<SpriteRenderer>().sortingOrder = 1;
                                hit.collider.gameObject.AddComponent<Ground>();
                                hit.collider.gameObject.GetComponent<Ground>().normalGrassObject = normalGrassObject;
                                hit.collider.tag = "Ground";
                            }
                            break;                       
                        case ("Water"):
                            if (hit.collider.tag == "Seed")
                            {
                                if (player.GetComponent<Water>().decreaseWaterCan())
                                {
                                    hit.collider.gameObject.GetComponent<Seed>().watered = true;
                                }
                            }
                            else if (hit.collider.tag == "River")
                            {
                                player.GetComponent<Water>().fillWaterCan();
                            }
                            break;                      
                        case ("Seed"):
                            if (hit.collider.tag == "Ground")
                            {
                                Destroy(hit.collider.gameObject.GetComponent<Ground>());

                                hit.collider.gameObject.GetComponent<SpriteRenderer>().sprite = seedObject;
                                hit.collider.gameObject.GetComponent<SpriteRenderer>().sortingOrder = 1;

                                hit.collider.gameObject.GetComponent<BoxCollider2D>().isTrigger = false;

                                hit.collider.gameObject.AddComponent<Seed>();
                                hit.collider.gameObject.GetComponent<Seed>().normalGrassObject = normalGrassObject;
                                hit.collider.gameObject.GetComponent<Seed>().growSeed = growSeedObject;
                                hit.collider.gameObject.GetComponent<Seed>().wateredSeed = wateredSeedObject;

                                hit.collider.tag = "Seed";
                            }
                            break;
                    }
                }
            }
        }
    }
}
