using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public Vector3 thrust;
    public Quaternion heading;

    public float LifeTime = 10f;

    void Start()
    {
        Destroy(gameObject, LifeTime);
        thrust.x = 400.0f;
        GetComponent<Rigidbody>().drag = 0;
        GetComponent<Rigidbody>().MoveRotation(heading);
        GetComponent<Rigidbody>().AddRelativeForce(thrust);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (this.tag == "ShipBullet")
        {
            if (collision.collider.tag == "Alien")
            {
                collision.collider.GetComponent<Alien>().Die();
                this.Die();
            }
        }
        else if (this.tag == "AlienBullet")
        {
            if (collision.collider.tag == "Ship")
            {
                collision.collider.GetComponent<Ship>().Die();
                this.Die();
            }
        }
    }

    void Die()
    {
        Destroy(gameObject);
    }
}