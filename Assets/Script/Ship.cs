using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ship : MonoBehaviour
{
    public float speed = 5f;
    public float rotation = 270;
    public float lives = 3;
    public GameObject bullet;
    public GameObject GlobalObject;

    // Start is called before the first frame update
    void Start()
    {
        rotation = 270;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
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

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        transform.Translate(Vector3.right * horizontalInput * speed * Time.deltaTime);
    }

    public void Die()
    {
        // -- lives connect with global
    }
}
