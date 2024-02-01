using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Alien : MonoBehaviour
{
    public GameObject GlobalObject;
    public GameObject bullet;
    public float minShootDelay = 1.0f;
    public float maxShootDelay = 5.0f;

    public float rotation = 0;
    public bool moveRight;
    public float horizontalSpeed = 5f;
    public float descentAmount = 1f;

    public int score = 10;

    private bool enable = true;

    public AudioClip DeathSound;

    // Start is called before the first frame update
    void Start()
    {
        //rotation = 90;

        // Determine starting direction
        System.Random random = new System.Random();
        int randomNumber = random.Next(0, 2);
        if (randomNumber == 0) moveRight = true;
        else moveRight = false;

        this.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;

        StartCoroutine(ShootRandomly());
    }

    private IEnumerator ShootRandomly()
    {
        while (enable)
        {
            yield return new WaitForSeconds(UnityEngine.Random.Range(minShootDelay, maxShootDelay));

            Fire();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (enable) Movement();
    }

    void Movement()
    {
        if (moveRight) transform.Translate(Vector3.right * horizontalSpeed * Time.deltaTime);
        else transform.Translate(Vector3.left * horizontalSpeed * Time.deltaTime);
    }

    void Fire()
    {
            
        // Instantiate the Bullet
        Vector3 spawnPos = gameObject.transform.position;
        spawnPos.y -= 1.5f;
        //spawnPos.z -= 1.5f * Mathf.Sin(rotation * Mathf.PI / 180);
        GameObject obj = Instantiate(bullet, spawnPos, Quaternion.identity) as GameObject;

        Bullet b = obj.GetComponent<Bullet>();

        Quaternion rot = Quaternion.Euler(new Vector3(0, 0, -90));
        b.heading = rot;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (enable)
        {
            if (collision.collider.tag == "finishLine")
            {
                GlobalObject.GetComponent<Global>().Lose();
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (enable)
        {
            if (other.tag == "boundary")
            {
                moveRight = !moveRight;
                horizontalSpeed += 2;
                transform.position = new Vector3(transform.position.x, transform.position.y - descentAmount, transform.position.z);
            }
        }
    }

    public void Die()
    {
        GlobalObject.GetComponent<Global>().AlienDie(score);
        AudioSource.PlayClipAtPoint(DeathSound, gameObject.transform.position);

        enable = false;
        this.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation | RigidbodyConstraints.FreezePositionZ;
        this.GetComponent<Rigidbody>().useGravity = true;

        //Destroy(gameObject, 20f);
    }
}
