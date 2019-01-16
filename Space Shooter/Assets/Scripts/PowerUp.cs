using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour {

    public GameObject pickupEffect;
    

    public float multiplier = 1.5f;

	void OnTriggerEnter (Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Pickup(other);
        }
    }

    void Pickup(Collider player)
    {
        Instantiate(pickupEffect, transform.position, transform.rotation);

        PlayerController largerShot = player.GetComponent<PlayerController>();
        largerShot.fireRate *= multiplier;

        Destroy(gameObject);
    }
}
