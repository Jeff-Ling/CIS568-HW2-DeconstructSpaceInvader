using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ship : MonoBehaviour
{
    public float speed = 5f;
    public float rotation = 0f;
    public float lives = 3;
    public float fireCoolDown = 2f;
    private float nextFireTime = 0f;
    public GameObject bullet;
    public GameObject cam;
    public GameObject GlobalObject;
    public AudioClip ShootingSound;

    // Start is called before the first frame update
    void Start()
    {
        //rotation = 270;
    }

    void Update()
    {
        if (Time.time > nextFireTime)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                // Instantiate the Bullet
                Vector3 spawnPos = gameObject.transform.position;
                spawnPos.y += 1.5f;
                //spawnPos.y += 1.5f * Mathf.Cos(rotation * Mathf.PI / 180);
                //spawnPos.z -= 1.5f * Mathf.Sin(rotation * Mathf.PI / 180);
                GameObject obj = Instantiate(bullet, spawnPos, Quaternion.identity) as GameObject;

                Bullet b = obj.GetComponent<Bullet>();

                Quaternion rot = Quaternion.Euler(new Vector3(0, 0, 90));
                b.heading = rot;

                AudioSource.PlayClipAtPoint(ShootingSound, gameObject.transform.position);

                nextFireTime = Time.time + fireCoolDown;
            }
        }

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag == "Alien")
        {
            speed = 2f;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.collider.tag == "Alien")
        {
            speed = 5f;
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
        lives--;
        cam.GetComponent<CameraShake>().shakeDuration = 0.5f;
        GlobalObject.GetComponent<Global>().UpdateLivesTextUI();
    }
}
