using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public Vector3 thrust;
    public Quaternion heading;
    public bool enable = true;
    public Material deathMaterial;

    //public float LifeTime = 10f;

    void Start()
    {
        //Destroy(gameObject, LifeTime);
        thrust.x = 2000.0f;
        GetComponent<Rigidbody>().drag = 0;
        GetComponent<Rigidbody>().MoveRotation(heading);
        GetComponent<Rigidbody>().AddRelativeForce(thrust);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (enable)
        {
            if (collision.collider.tag == "finishLine")
            {
                enable = false;
                this.gameObject.GetComponent<MeshRenderer>().material = deathMaterial;
            }
            if (collision.collider.tag == "shield")
            {
                collision.collider.GetComponent<Shield>().UpdateShieldLivesText();
                Die();
            }
            if (this.tag == "ShipBullet")
            {
                if (collision.collider.tag == "Alien")
                {
                    if (collision.collider.GetComponent<Alien>().enable)
                    {
                        collision.collider.GetComponent<Alien>().Die();
                        this.Die();
                    }
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
    }

    void Die()
    {
        Destroy(gameObject);
    }
}