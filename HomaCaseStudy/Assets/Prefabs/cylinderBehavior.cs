using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cylinderBehavior : MonoBehaviour
{

    public int color;
    public Vector3 initialPos;
    public int myLayer;
    public bool fallen;
    public Rigidbody rbody;

    masterLogic ml;

    void Awake()
    {
        //Initilizing block behavior
        color = Random.Range(1, 4);

        if (color == 1) 
            this.GetComponent<Renderer>().material.color = Color.red;
        if (color == 2) 
            this.GetComponent<Renderer>().material.color = Color.yellow;
        if (color == 3)
            this.GetComponent<Renderer>().material.color = Color.blue;
    }

    private void Start()
    {
        rbody = GetComponent<Rigidbody>();
        initialPos = transform.position;
        fallen = false;

        ml = GameObject.Find("Master").GetComponent<masterLogic>();

        if (myLayer <= ml.topLayer - 8)
            rbody.isKinematic = true;

    }

    void Update()
    {
        //Detect if the block has fallen
        if (Vector3.Distance(initialPos, transform.position) > 0.8)
            fallen = true;

        //Set to inactive if far from the top layer
        if (myLayer <= ml.topLayer - 8)
        {
            this.GetComponent<Renderer>().material.color = Color.black;
            if (rbody.isKinematic == false)
                rbody.isKinematic = true;
        }
        else
        {
            //Set to alive if close to top layer. 
            if (color == 1)
                this.GetComponent<Renderer>().material.color = Color.red;
            if (color == 2)
                this.GetComponent<Renderer>().material.color = Color.yellow;
            if (color == 3)
                this.GetComponent<Renderer>().material.color = Color.blue;

            if (rbody.isKinematic == true)
                rbody.isKinematic = false;          
        }

    }

    //System to destroy all matching tiles within adjacent places. 
    private void OnDestroy()
    {
        Collider[] hitColliders = Physics.OverlapBox(transform.position, new Vector3(0.3f, 0.4f, 0.3f), Quaternion.identity);

        foreach (var q in hitColliders)
        {
            if ((q.gameObject.GetComponent("cylinderBehavior") as cylinderBehavior) != null)
            {
                if (q.gameObject.GetComponent<cylinderBehavior>().color == color && q.gameObject.GetComponent<cylinderBehavior>().rbody.isKinematic == false)
                {
                    Destroy(q.gameObject);
                }
            }
        }
    }

}
