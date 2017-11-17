using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class createWorld : MonoBehaviour {

    public List<GameObject> blocks; 
    public GameObject block;  // prefab
    public GameObject boundingBox;
    public int gameSize;
    private int blockScale;


	// Use this for initialization
	void Start () {
        blockScale = 2;
        if (gameSize < 10) { gameSize = 10; }     // ensure minimum game size
        if (gameSize % 2 == 1) { gameSize -= 1; } //ensure game is evnly balanced
        generateLevel();
        placeBoundingBox();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    void generateLevel()
    {
        for (int i = -gameSize; i < gameSize; i += blockScale)
        {
            for (int j = -gameSize; j < gameSize; j += blockScale)
            {
                //back and front
                createBlock(i, j, -gameSize-blockScale, Color.red);
                createBlock(i, j, gameSize, Color.green);
                //left and right
                createBlock(-gameSize-blockScale, j, i, Color.blue);
                createBlock(gameSize, j, i, Color.yellow);
                //top and bottom
                createBlock(j , gameSize, i, Color.magenta);
                createBlock(j , -gameSize-blockScale, i, Color.cyan);
            }
        }
    }


    void createBlock(int x, int y, int z, Color color)
    {
        // GameObject temp = GameObject.CreatePrimitive(PrimitiveType.Cube);
        GameObject temp = (GameObject)Instantiate(block);
        temp.transform.localPosition = new Vector3(x,y,z);
        temp.transform.parent = transform;
        temp.transform.localScale = new Vector3(blockScale, blockScale, blockScale);
        temp.GetComponent<Renderer>().material.shader = Shader.Find("Specular");
        temp.GetComponent<Renderer>().material.color = color;
        //temp.GetComponent<Renderer>().material.color = Random.ColorHSV(0f, 1f, 1f, 1f, 1f, 1f);
        blocks.Add(temp);
    }

    void placeBoundingBox()
    {
        boundingBox.transform.localPosition = new Vector3(-blockScale, -blockScale/2, -blockScale);
        float boundSize = (gameSize + blockScale) * 2 + blockScale;
        List<Vector3> rotation = new List<Vector3> { new Vector3(0,0,0), new Vector3(0,90,0), new Vector3(90,0,0)  };
        List<Vector3> translation = new List<Vector3> { new Vector3(0,0,-gameSize - 2*blockScale),
                                                        new Vector3(-gameSize-2*blockScale,0,0),
                                                        new Vector3(0,-gameSize - 2*blockScale, 0),
                                                        new Vector3(0,0,gameSize+2*blockScale),
                                                        new Vector3(gameSize+2*blockScale,0,0),
                                                        new Vector3(0,gameSize+2*blockScale,0)};


        List<GameObject> sides = new List<GameObject>();
        for (int i = 0; i < 6; i++)
        {
            GameObject side = GameObject.CreatePrimitive(PrimitiveType.Cube);
            side.transform.parent = boundingBox.transform;
            side.transform.localScale = new Vector3(boundSize, boundSize, blockScale);
            //side.tag = "Bound: "+ i;
            Quaternion rot = new Quaternion();
            rot.eulerAngles = rotation[i % 3];
            side.transform.localRotation = rot;
            side.transform.localPosition = translation[i];
            sides.Add(side);
        }
    }
}

    