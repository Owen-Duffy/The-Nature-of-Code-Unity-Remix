using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Exercise1_1 : MonoBehaviour
{
    //Pig location and speed
    Vector2 location = new Vector2(0f, 0f);  //Vector2.zero
    Vector2 velocity = new Vector2(.01f, .01f);

    //Screen information

    Vector2 minimumPos, maximumPos;

    GameObject pig;


    // Start is called before the first frame update
    void Start()
    {
        CameraSetup();

        //Create the pig

        pig = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        
    }

    // Update is called once per frame
    void Update()
    {

        
        

        //Borders
        bool xHitBorder = location.x > maximumPos.x || location.x < minimumPos.x;
        bool yHitBorder = location.y > maximumPos.y || location.y < minimumPos.y;


        //If the pig touches a border, they reverse
        if (xHitBorder)
        {
            velocity.x = -velocity.x;
            //velocity.x = -velocity.x *1.2f;
        }

        if (yHitBorder)
        {
            velocity.y = -velocity.y;
            //velocity.y = -velocity.y * 1.2f;
        }

        location += velocity;

        pig.transform.position = new Vector2(location.x, location.y);
    }

    void CameraSetup()
    {
        Camera.main.orthographic = true;
        minimumPos = Camera.main.ScreenToWorldPoint(Vector2.zero);
        maximumPos = Camera.main.ScreenToWorldPoint(new Vector2(Screen.width, Screen.height));
    }

}
