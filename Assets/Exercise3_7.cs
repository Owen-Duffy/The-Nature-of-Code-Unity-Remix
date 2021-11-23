using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Exercise3_7 : MonoBehaviour
{

    List<oscillatorNew> oscilattors = new List<oscillatorNew>();
    ozzieMover oMover;

    // Start is called before the first frame update
    void Start()
    {
        oMover = new ozzieMover();


        while (oscilattors.Count < 10)
        {
            oscilattors.Add(new oscillatorNew(oMover.mover.transform.position));
            
        }

        foreach (oscillatorNew o in oscilattors)
        {
            o.oGameObject.transform.SetParent(oMover.mover.transform);
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        oMover.move();
        oMover.checkBoundaries();


        foreach (oscillatorNew o in oscilattors)
        {
            //Each oscillator object oscillating on the x-axis
            float x = Mathf.Sin(o.angle.x) * o.amplitude.x;
            //Each oscillator object oscillating on the y-axis
            float y = Mathf.Sin(o.angle.y) * o.amplitude.y;
            //Add the oscillator's velocity to its angle
            o.angle += o.velocity;
            // Draw the line for each oscillator
            o.lineRender.SetPosition(0, oMover.mover.transform.position);
            Debug.Log(oMover.mover.transform.position);
            o.lineRender.SetPosition(1, o.oGameObject.transform.position);
            //Move the oscillator
            o.oGameObject.transform.transform.Translate(new Vector2(x, y) * Time.deltaTime);
        }

        


    }
}

public class ozzieMover
{
    // Variables for the location and speed of mover
    private float x = 0F;
    private float y = 0F;
    private float xSpeed = 0.1F;
    private float ySpeed = 0.2F;

    // Variables to limit the mover within the screen space
    private float xMin, yMin, xMax, yMax;

    // A Variable to represent our mover in the scene
    public GameObject mover;

    public ozzieMover()
    {
        mover = new GameObject();
        mover.transform.position = new Vector2(x, y);
        mover.name = "Ozzie";
        setUpCamera();
    }


    public void move()
    {
        // Lets now update the location of the mover
        x += xSpeed;
        y += ySpeed;

        // Now we apply the positions to the mover to put it in it's place
        mover.transform.position = new Vector2(x, y);
    }



    public void setUpCamera()
    {
        // We want to start by setting the camera's projection to Orthographic mode
        Camera.main.orthographic = true;

        // Next we grab the minimum and maximum position for the screen
        Vector2 minimumPosition = Camera.main.ScreenToWorldPoint(Vector2.zero);
        Vector2 maximumPosition = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));

        // We can now properly assign the Min and Max for out scene
        xMin = minimumPosition.x;
        xMax = maximumPosition.x;
        yMin = minimumPosition.y;
        yMax = maximumPosition.y;

    }

    public void checkBoundaries()
    {
        // Each frame, we will check to see if the mover has touched a boarder
        // We check if the X/Y position is greater than the max position OR if it's less than the minimum position
        bool xHitBoarder = x > xMax || x < xMin;
        bool yHitBoarder = y > yMax || y < yMin;

        // If the mover has hit at all, we will mirror it's speed with the corrisponding boarder

        if (xHitBoarder)
        {
            xSpeed = -xSpeed;
        }

        if (yHitBoarder)
        {
            ySpeed = -ySpeed;
        }
    }

}



public class oscillatorNew
{

    // The basic properties of an oscillator class
    public Vector2 velocity, angle, amplitude;

    // The window limits
    private Vector2 maximumPos;

    // Gives the class a GameObject to draw on the screen
    public GameObject oGameObject = GameObject.CreatePrimitive(PrimitiveType.Sphere);

    //Create variables for rendering the line between two vectors
    public LineRenderer lineRender;

    public oscillatorNew(Vector3 startPosition)
    {
        oGameObject.transform.position = startPosition;

        findWindowLimits();
        angle = Vector2.zero;
        velocity = new Vector2(Random.Range(-.05f, .05f), Random.Range(-0.05f, 0.05f));
        amplitude = new Vector2(Random.Range(-maximumPos.x / 2, maximumPos.x / 2), Random.Range(-maximumPos.y / 2, maximumPos.y / 2));

        //We need to create a new material for WebGL
        Renderer r = oGameObject.GetComponent<Renderer>();
        r.material = new Material(Shader.Find("Diffuse"));

        // Create a GameObject that will be the line
        GameObject lineDrawing = new GameObject();
        lineDrawing.transform.SetParent(oGameObject.transform);
        //Add the Unity Component "LineRenderer" to the GameObject lineDrawing.
        lineRender = lineDrawing.AddComponent<LineRenderer>();
        lineRender.material = new Material(Shader.Find("Diffuse"));
        //Begin rendering the line between the two objects. Set the first point (0) at the centerSphere Position
        //Make sure the end of the line (1) appears at the new Vector3
        Vector2 center = new Vector2(oGameObject.transform.position.x, oGameObject.transform.position.y);
        lineRender.SetPosition(0, center);
    }

    private void findWindowLimits()
    {
        // We want to start by setting the camera's projection to Orthographic mode
        Camera.main.orthographic = true;
        // Next we grab the minimum and maximum position for the screen
        maximumPos = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));
    }
}