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

    public float rotation = 90;
    public bool moveRight;
    public float horizontalSpeed = 5f;
    public float descentAmount = 1f;

    public int score = 10;

    public AudioClip DeathSound;

    // Start is called before the first frame update
    void Start()
    {
        rotation = 90;

        // Determine starting direction
        System.Random random = new System.Random();
        int randomNumber = random.Next(0, 2);
        if (randomNumber == 0) moveRight = true;
        else moveRight = false;

        StartCoroutine(ShootRandomly());
    }

    private IEnumerator ShootRandomly()
    {
        while (true)
        {
            yield return new WaitForSeconds(UnityEngine.Random.Range(minShootDelay, maxShootDelay));

            Fire();
        }
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
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
        spawnPos.x += 1.5f * Mathf.Cos(rotation * Mathf.PI / 180);
        spawnPos.z -= 1.5f * Mathf.Sin(rotation * Mathf.PI / 180);
        GameObject obj = Instantiate(bullet, spawnPos, Quaternion.identity) as GameObject;

        Bullet b = obj.GetComponent<Bullet>();

        Quaternion rot = Quaternion.Euler(new Vector3(0, rotation, 0));
        b.heading = rot;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "boundary")
        {
            moveRight = !moveRight;
            transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z - descentAmount);
        }
        if (other.tag == "finishLine")
        {
            GlobalObject.GetComponent<Global>().Lose();
        }
    }

    public void Die()
    {
        GlobalObject.GetComponent<Global>().AlienDie(score);
        AudioSource.PlayClipAtPoint(DeathSound, gameObject.transform.position);
        Destroy(gameObject);
    }
}
