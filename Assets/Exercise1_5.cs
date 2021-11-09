using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Exercise1_5 : MonoBehaviour
{
    car c;

    //public float xVelocity;
    //public float yVelocity;

    // Start is called before the first frame update
    void Start()
    {
        c = new car();
        

    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKey("up"))
        {

            c.acceleration += new Vector2(1.1f, 1.2f);

            print("accelerate");
        }
        else if(Input.GetKey("down"))
        {

            c.acceleration += new Vector2(-1.1f, -1.2f);


            //if (c.velocity != Vector2.zero)
            //{
            //    c.velocity -= new Vector2(1.1f, 1.2f);
            //}

            //if (c.velocity.x <= 0f || c.velocity.y <= 0f)
            //{
            //    c.velocity = Vector2.zero;
            //}

            //c.acceleration = new Vector2(0f, 0f);




            print("stop");
            
        }

        c.moveMe();
    }
}

public class car
{
    public Vector2 velocity, location, acceleration;
    float topSpeed;
    float botSpeed;

    //window limits
    Vector2 minPos, maxPos;

    //car
    GameObject engine = GameObject.CreatePrimitive(PrimitiveType.Cube);

    public car()
    {
        CameraSetup();
        location = Vector2.zero;
        velocity = Vector2.zero;
        acceleration = new Vector2(0.04f, 0.06f);
        topSpeed = 20f;
        botSpeed = 0f;

    }

    public void moveMe()
    {
        //Speeds up the car
        velocity += acceleration * Time.deltaTime;

        //Limit velocity to top speed
        velocity = Vector2.ClampMagnitude(velocity, topSpeed);



       



        //if (velocity != Vector2.zero)
        //{

         location += velocity * Time.deltaTime;

        //}

        if(velocity.x > Vector2.zero.x || velocity.y > Vector2.zero.y)
        {
            engine.transform.position = new Vector2(location.x, location.y);
        }
        else
        {
            velocity = Vector2.zero;
        }

        
        

        //engine.transform.position = new Vector2(location.x, location.y);
    }

    void CameraSetup()
    {
        Camera.main.orthographic = true;
        minPos = Camera.main.ScreenToWorldPoint(Vector2.zero);
        maxPos = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));
    }

}