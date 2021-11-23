using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Exercise3_4 : MonoBehaviour
{
    float r = 0f;
    float theta = 45;

    private GameObject sphere;
    private GameObject sphereTrail;


    //Create variables for rendering the line between two vectors
    private GameObject lineDrawing;
    private LineRenderer lineRender;

    // Start is called before the first frame update
    void Start()
    {
        //Set the camera to orthographic and make it's size 8
        Camera.main.orthographic = true;
        Camera.main.orthographicSize = 8;
        // Create a GameObject that will be the line
        lineDrawing = new GameObject();
        // Make the sphere as a primitive sphere type.
        sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        Renderer srenderer = sphere.GetComponent<Renderer>();
        srenderer.material = new Material(Shader.Find("Diffuse"));
        srenderer.material.color = Color.black;


        //Add the Unity Component "LineRenderer" to the GameObject lineDrawing. We will see a bright pink line.
        lineRender = lineDrawing.AddComponent<LineRenderer>();
        //Make the line smaller for aesthetics
        lineRender.GetComponent<LineRenderer>().startWidth = 0.1f;
        lineRender.GetComponent<LineRenderer>().endWidth = 0.1f;
        //We need to create a new material for WebGL
        lineRender.material = new Material(Shader.Find("Diffuse"));

        //Create sphere trail that's smaller
        sphereTrail = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        sphereTrail.transform.localScale = new Vector3(.3f, .3f, .3f);

        Renderer strenderer = sphereTrail.GetComponent<Renderer>();
        strenderer.material = new Material(Shader.Find("Diffuse"));
        strenderer.material.color = Color.red;
    }

    // Update is called once per frame
    void Update()
    {
        float x = r * Mathf.Cos(theta);
        float y = r * Mathf.Sin(theta);

        sphere.transform.position = new Vector2(x, y);

        theta += 2f * Time.deltaTime; //Time.deltaTime to keep the speed consistant

        //Begin rendering the line between the two objects. Set the first point (0) at the centerSphere Position
        //Make sure the end of the line (1) appears at the new Vector3
        Vector2 center = new Vector2(0f, 0f);
        lineRender.SetPosition(0, center);
        lineRender.SetPosition(1, sphere.transform.position);

        r = r + 0.001f;

        Instantiate(sphereTrail, sphere.transform.position, sphere.transform.rotation);
    }
}
