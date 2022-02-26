using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidHit : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Player player = collision.gameObject.GetComponent<Player>();
        GameObject shipTouched = collision.gameObject;
        if (player != null)
        {
            if (!player.isInvincible)
            {
                Vector3 playerPosition = shipTouched.transform.position;
                Vector3 asteroidPosition = gameObject.transform.position;
                Vector3 reboundForce = playerPosition - asteroidPosition;
                shipTouched.GetComponent<Rigidbody2D>().AddRelativeForce(reboundForce * Time.deltaTime * 50, ForceMode2D.Impulse);

                FindObjectOfType<AudioManager>().Play("Crash");
                player.TakeDamage();
            }
        }
    }
}
