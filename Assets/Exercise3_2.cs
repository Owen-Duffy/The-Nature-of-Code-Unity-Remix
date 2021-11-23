using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Exercise3_2 : MonoBehaviour
{

    cannon c;
    Ammo a;


    // Start is called before the first frame update
    void Start()
    {
        a = new Ammo(Vector3.zero, 1f);
        c = new cannon(a, Vector3.zero);
    }

    // Update is called once per frame
    void Update()
    {
        
        if (Input.GetKeyDown("space"))
        {
            Vector3 force = new Vector3(1f, 1f, 0f);
            c.shootAmmo(a.body, force);
        }

    }
}

public class cannon
{
    GameObject cannonObject;
    Vector3 cannonLocation;
    Quaternion cannonAngle;

    Vector3 ammoForce = new Vector3(1f, 5f, 0f);

    public cannon(Ammo ammo, Vector3 position)
    {
        cannonObject = GameObject.CreatePrimitive(PrimitiveType.Capsule);
        cannonObject.transform.localScale = new Vector3(1.5f, 1.5f, 1.5f);
        cannonLocation = position;
        cannonAngle = Quaternion.Euler(0f,0f,0f);

        cannonObject.transform.position = cannonLocation;
        cannonObject.transform.rotation = cannonAngle;
        //cannonObject.AddTorque(ammoForce, ForceMode.Impulse);

    }

    public void shootAmmo(Rigidbody b, Vector3 f)
    {



        b.AddForce(f, ForceMode.Impulse);
        //Angular rotation

        b.AddTorque(f, ForceMode.Impulse);

    }

}



public class Ammo
{
    GameObject ammoObject;
    public Rigidbody body;
    Vector3 location, velocity, acceleration, aVelocity, aAcceleration;


    public Ammo(Vector3 location, float _mass)
    {

        ammoObject = GameObject.CreatePrimitive(PrimitiveType.Cube);
        BoxCollider bc = ammoObject.GetComponent<BoxCollider>();
        bc.enabled = false;
        body = ammoObject.AddComponent<Rigidbody>();
        ammoObject.transform.position = location;
        body.mass = _mass;
        body.useGravity = false;


    }
}