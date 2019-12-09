using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class generate : MonoBehaviour
{

    public GameObject cylinder;
    int pieces = 15;
    Vector3 center = new Vector3(0, -2.2f, 0);
    float radius = 1.19f;
    int layerAdd;

    void Awake()
    {
        //Level Generation
        //Gets information from Master after it loads the game information to control how tall the level is. 
        masterLogic ml = GameObject.Find("Master").GetComponent<masterLogic>();

        layerAdd = ml.layerAdd;

        float angle = 360f / (float)pieces;

        for (int layers = 1; layers < 16 + layerAdd; layers++)
        {
            float angleOffset = (layers % 2) * 0.5f * angle;
            for (int i = 0; i < pieces; i++)
            {
                Quaternion rotation = Quaternion.AngleAxis(i * angle + angleOffset, Vector3.up);

                Vector3 direction = rotation * Vector3.forward;

                Vector3 position = center + (direction * radius);

                GameObject go = (GameObject)Instantiate(cylinder, position, rotation);
                go.GetComponent<cylinderBehavior>().myLayer = layers;

            }
            center.y += .6f;
        }

    }

}
