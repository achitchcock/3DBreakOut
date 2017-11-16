using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class createWorld : MonoBehaviour {

    public List<GameObject> blocks;
    public GameObject block;


	// Use this for initialization
	void Start () {
        Random rand = new Random();
        for (int i = -9; i < 10; i+= 2)
        {
            for (int j = 1; j < 20; j+=2)
            {
                //back and front
                //createBlock(i,j,-11);
                //createBlock(i, j, 11);
                //left and right
                createBlock(-11, j, i);
                createBlock(11, j, i);
                //top and bottom
                //createBlock(j-10, 21, i);
               // createBlock(j-10, -1, i);
            }
        }
    }
	
	// Update is called once per frame
	void Update () {
		
	}


    void createBlock(int x, int y, int z)
    {
        // GameObject temp = GameObject.CreatePrimitive(PrimitiveType.Cube);
        GameObject temp = (GameObject)Instantiate(block);
        temp.transform.localPosition = new Vector3(x,y,z);
        temp.transform.parent = transform;
        temp.transform.localScale = new Vector3(2, 2, 2);
        temp.GetComponent<Renderer>().material.shader = Shader.Find("Specular");
        temp.GetComponent<Renderer>().material.color = Random.ColorHSV(0f, 1f, 1f, 1f, 1f, 1f);
        blocks.Add(temp);
    }

}

    