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

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "shield")
        {
            other.GetComponent<Shield>().UpdateShieldLivesText();
            Die();
        }
        if (this.tag == "ShipBullet")
        {
            if (other.tag == "Alien")
            {
                other.GetComponent<Alien>().Die();
                this.Die();
            }
        }
        else if (this.tag == "AlienBullet")
        {
            if (other.tag == "Ship")
            {
                other.GetComponent<Ship>().Die();
                this.Die();
            }
        }
    }

    void Die()
    {
        Destroy(gameObject);
    }
}