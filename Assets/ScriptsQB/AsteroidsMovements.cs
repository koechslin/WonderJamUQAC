using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidsMovements : MonoBehaviour
{
    [SerializeField] private float m_speed;
    [SerializeField] private float m_forceValue;
    [SerializeField] private Rigidbody2D m_rigidbody;
    [SerializeField] private float m_detectionDistance;
    [SerializeField] private float m_timeBetweenTwoChange;
    
    private GameObject[] m_players;
    private GameObject m_nearestPlayer;
    private float m_currentDistanceNearestPlayer;
    private Vector2 m_force;
    private Vector2 m_velocity;

    private bool m_canChange = true;

    private float m_rotationValue;
    private Vector3 m_rotate = Vector3.zero;

    // Start is called before the first frame update
    void Start()
    {
        // Get all players
        m_players = GameObject.FindGameObjectsWithTag("Player");

        FindNearestPlayer();
        Debug.Log(m_nearestPlayer);

        // Initialize movements
        m_force = new Vector2((m_nearestPlayer.transform.position.x - transform.position.x) * m_speed, (m_nearestPlayer.transform.position.y - transform.position.y) * m_speed);
        m_rigidbody.AddForce(m_force);

        // Set up rotation
        m_rotationValue = Random.Range(0.03f, 0.05f);
    }

    // Update is called once per frame
    void Update()
    {
        FindNearestPlayer();
        m_currentDistanceNearestPlayer = GetDistanceWith(m_nearestPlayer);

        // Check if near to a player and his deviation perks
        if ((m_currentDistanceNearestPlayer < m_detectionDistance) && m_canChange)
        {
            if (m_nearestPlayer.GetComponent<PlayerPerks>().m_asteroidDeviation)
            {
                m_canChange = false;
                m_velocity = m_rigidbody.velocity;

                Vector2 newForce = Vector2.Reflect(m_velocity, transform.position.normalized) * m_forceValue;
                m_rigidbody.AddForce(newForce);
            }
        }

        // update the rotation
        transform.Rotate(m_rotate + new Vector3(0, 0, m_rotationValue));
    }

    private void FindNearestPlayer()
    {
        float distance = float.MaxValue;
        foreach (GameObject player in m_players)
        {
            float distanceWithPlayer = GetDistanceWith(player);
            if (distanceWithPlayer < distance)
            {
                distance = distanceWithPlayer;
                m_nearestPlayer = player;
            }
        }
    }

    private float GetDistanceWith(GameObject gb)
    {
        float distance = Mathf.Sqrt(Mathf.Pow((gb.transform.position.x - transform.position.x), 2) + Mathf.Pow((gb.transform.position.y - transform.position.y), 2));
        return distance;
    }

    private IEnumerator ReleaseChangeForce()
    {
        yield return new WaitForSeconds(m_timeBetweenTwoChange);
        m_canChange = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("hit");
            Destroy(gameObject);
        }
    }
}
