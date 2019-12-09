using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class projectileBehavior : MonoBehaviour
{

    public int myColor;

    void Awake()
    {
        masterLogic ml = GameObject.Find("Master").GetComponent<masterLogic>();

        //Initilizing the projectile's behavior and color
        GameObject cs = ml.colorSphere;
        this.GetComponent<Renderer>().material.color = cs.GetComponent<Renderer>().material.color;

        if (this.GetComponent<Renderer>().material.color == Color.red)
          myColor = 1;
        if ( this.GetComponent<Renderer>().material.color == Color.yellow )
          myColor = 2;
        if (this.GetComponent<Renderer>().material.color == Color.blue)
          myColor = 3;

        //Apply all the neccesary modifications to Master
        ml.balls -= 1;

        ml.color = ml.colorNext;
        ml.colorNext = Random.Range(1, 4);

    }


    void OnCollisionEnter(Collision collision)
    {
        //Destroy any blocks the projectile matches color with
        int otherCol = collision.gameObject.GetComponent<cylinderBehavior>().color;
        if (otherCol == myColor && collision.gameObject.GetComponent<cylinderBehavior>().rbody.isKinematic == false)
        {
            Destroy(collision.gameObject);
            Destroy(this.gameObject);
        }
    }
}
