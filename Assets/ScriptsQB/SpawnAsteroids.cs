using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnAsteroids : MonoBehaviour
{
    [SerializeField] private GameObject m_asteroid;
    [SerializeField] private float m_time;

    public bool m_canSpawn = true;
    private Coroutine timeBetweenSpawnCoroutine;

    void Update()
    {
        if (m_canSpawn)
        {
            m_canSpawn = false;
            Instantiate(m_asteroid, transform);
            timeBetweenSpawnCoroutine = StartCoroutine(TimeBetweenTwoSpawn());
        }
    }

    private IEnumerator TimeBetweenTwoSpawn()
    {
        yield return new WaitForSeconds(m_time);
        m_canSpawn = true;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Player player = collision.gameObject.GetComponent<Player>();
        if (player != null)
        {
            if (!player.isInvincible)
            {
                FindObjectOfType<AudioManager>().Play("Crash");
                player.TakeDamage();
            }
        }
    }

    public void StopSpawn()
    {
        StopCoroutine(timeBetweenSpawnCoroutine);
        m_canSpawn = false;
    }

    public void BeginSpawn()
    {
        m_canSpawn = true;
    }
}
