using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class masterLogic : MonoBehaviour
{
    public int topLayer;
    public float yLocal;
    public int color;
    public int colorNext;
    public GameObject colorSphere;
    public GameObject colorSphereNext;

    public int layerAdd;
    public int balls;

    public Text myText;


    private void Awake()
    {
        //Progress system initilization and loading
        if(PlayerPrefs.HasKey("data") == false)
            PlayerPrefs.SetInt("data", 1);
        layerAdd = PlayerPrefs.GetInt("data");

        balls = 13;
    }

    void Start()
    {
        topLayer = 20;
        color = Random.Range(1,4);
        colorNext = Random.Range(1, 4);

        colorSphere = GameObject.Find("ColorSphere");
        colorSphereNext = GameObject.Find("ColorSphereNext");

    }

    void Update()
    {
        //GUI control and updating
        myText.text = balls.ToString();

        if (color == 1)
            colorSphere.GetComponent<Renderer>().material.color = Color.red;
        if (color == 2)
            colorSphere.GetComponent<Renderer>().material.color = Color.yellow;
        if (color == 3)
            colorSphere.GetComponent<Renderer>().material.color = Color.blue;

        if (colorNext == 1)
            colorSphereNext.GetComponent<Renderer>().material.color = Color.red;
        if (colorNext == 2)
            colorSphereNext.GetComponent<Renderer>().material.color = Color.yellow;
        if (colorNext == 3)
            colorSphereNext.GetComponent<Renderer>().material.color = Color.blue;

       //Sytem for inmovable layers, as well as logic for fail states and win states
       int totalUp = 0;

       var objects = GameObject.FindGameObjectsWithTag("blocks");
       bool found = false;
       foreach (var obj in objects)
        {
            var component = obj.GetComponent<cylinderBehavior>();
            if (component.fallen == false && component.myLayer == topLayer)
            {
                found = true;
                yLocal = component.initialPos.y;
            }
            if (component.fallen == false)
                totalUp += 1;

        }

        if (found == false)
            topLayer -= 1;

        if (totalUp < 15) {
            int value = PlayerPrefs.GetInt("data");
            value += 1;
            PlayerPrefs.SetInt("data", value);
            PlayerPrefs.Save();
            SceneManager.LoadScene("Scenes/SampleScene");
        }

        if(balls < 0)
            SceneManager.LoadScene("Scenes/SampleScene");
    }
}
