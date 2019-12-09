using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shoot : MonoBehaviour
{
    public GameObject projectile;
    //public Rigidbody rb;
    public int clickForce = 500;
    private Plane plane = new Plane(Vector3.up, Vector3.zero);

    Vector2 currentPos;
    Vector2 deltaPos;

    void Update()
    {
        //Hold temporary information about mouse poistion in order to only shoot projectiles on a tap gesture, and never a drag
        if (Input.GetMouseButtonDown(0))
        {
            currentPos = Input.mousePosition;
        }

        if (Input.GetMouseButtonUp(0))
        {
            deltaPos = currentPos - (Vector2)Input.mousePosition;

            if (deltaPos.magnitude < 2)
            {
                //Use raycasts to tell when a users taps on an object
                //Spawn projectile and move it towards where the user taps
                var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit enter;
                if (Physics.Raycast(ray, out enter))
                {
                    var hitPoint = enter.point;
                    var mouseDir = hitPoint - gameObject.transform.position;
                    mouseDir = mouseDir.normalized;
                    GameObject bullet = Instantiate(projectile, transform.position + transform.forward * 3, Quaternion.identity) as GameObject;
                    bullet.GetComponent<Rigidbody>().AddForce(mouseDir * 2600);
                }
            }
        }
    }
}
