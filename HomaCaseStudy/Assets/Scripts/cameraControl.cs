using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraControl : MonoBehaviour
{
    public Vector3 target = new Vector3(0,6,0);
    public float distance = 10f;

    float tempX;
    float tempY;

    float x = 0.0f;
    float y = 0.0f;

    masterLogic ml;

    void Start()
    {
        Vector3 a = transform.eulerAngles;
        x = a.y;
        tempX = a.y;
        y = a.x;

        ml = GameObject.Find("Master").GetComponent<masterLogic>();
    }

    void LateUpdate()
    {
        Quaternion rotation = Quaternion.Euler(y, tempX, 0);

        distance = 20f;
        Vector3 negDistance = new Vector3(0.0f, 0.0f, -distance);
        Vector3 position = rotation * negDistance + target;
        if (Input.GetTouch(0).phase == TouchPhase.Moved)
        {
            x += Input.GetTouch(0).deltaPosition.x * distance * 0.1f;

        }

        tempX = Mathf.Lerp(x, tempX, 0.01f);

        transform.rotation = rotation;
        transform.position = position;

        tempY = ml.yLocal - 1;

        if (target.y <= 0.9f)
            target.y = 0.9f;

        tempY = Mathf.Lerp(target.y, tempY, 0.1f);

        target.y = tempY;
    }
}
