using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Boundary
{
    public float xMin, xMax, zMin, zMax;
}

public class PlayerController : MonoBehaviour
{
    private Rigidbody rigidBody;
    private AudioSource boltSound;

    public float speed;
    public float tilt;
    public Boundary boundary;

    public GameObject shot;
    public GameObject shotSpawn;
    public float fireRate;

    private float nextFire;
    private GameObject shotClone;

    void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
        boltSound = GetComponent<AudioSource>();
    }

    void Update ()
    {
        if (Input.GetButton("Fire1") || (Input.GetKey(KeyCode.Space)) && Time.time > nextFire)
        {
            shot.transform.position = shotSpawn.transform.position;
            shotClone = Instantiate(shot) as GameObject;
            nextFire = Time.time + fireRate;
            boltSound.Play();
        }
    }

    void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);
        rigidBody.velocity = movement * speed;

        Vector3 bounds = new Vector3
            (
            (Mathf.Clamp(rigidBody.position.x, boundary.xMin, boundary.xMax)), 
            0.0f, 
            (Mathf.Clamp(rigidBody.position.z, boundary.zMin, boundary.zMax))
            );

        rigidBody.position = bounds;

        rigidBody.rotation = Quaternion.Euler(0.0f, 0.0f, (rigidBody.velocity.x * -tilt));
    }
}
