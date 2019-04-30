using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {

    internal GameObject player;
    internal GameObject canvas;

    public Sprite groundObject;
    public Sprite seedObject;
    public Sprite growSeedObject;
    public Sprite wateredSeedObject;
    public Sprite normalGrassObject;
    public Sprite grassObject;

	void Start () {
        player = GameObject.FindGameObjectWithTag("Player");
        canvas = GameObject.FindGameObjectWithTag("Canvas");
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
                    case ("Scyther"):
                        if (hit.collider.tag == "Grass")
                        {
                            Destroy(hit.collider.gameObject);
                        }
                        break;
                    case ("Hammer"):
                        if (hit.collider.tag == "Stone")
                        {
                            Destroy(hit.collider.gameObject);
                        }
                        break;
                    case ("Water"):
                        if (hit.collider.tag == "Seed")
                        {
                            /*if (player.GetComponent<PlayerTool>().decreaseWaterCan())
                            {
                                hit.collider.gameObject.GetComponent<Seed>().watered = true;
                            }*/
                        }
                        else if (hit.collider.tag == "River")
                        {
                            //player.GetComponent<PlayerTool>().fillWaterCan();
                        }
                        break;
                    case ("Axe"):
                        if (hit.collider.tag == "Tree")
                        {
                            Destroy(hit.collider.gameObject);
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
                    case ("Fish"):
                        if (hit.collider.tag == "River")
                        {

                        }
                        break;
                }
            }
        }
    }
}
