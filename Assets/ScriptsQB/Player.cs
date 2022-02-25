using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    public int movementspeed;
    Rigidbody2D m_Rigidbody;
    [SerializeField] float fuel;
    public float maxFuel;

    // Start is called before the first frame update
    void Start()
    {
        m_Rigidbody = GetComponent<Rigidbody2D>();
        fuel = maxFuel;
    }

    // Update is called once per frame
    void Update()
    {

        // Si on veut implémenter une limite : get la velocity du RigidBody puis normaliser le vecteur + multiplier par la vitesse max

        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        Vector2 direction = new Vector3(horizontalInput, verticalInput, 0f);
        Vector2 checkDirectionNull = new Vector2(0, 0);

        if (!direction.Equals(checkDirectionNull))
        {
            if (fuel < 1)
            {
                fuel += 0.1f;
            }

            if (fuel >= 1)
            {
                m_Rigidbody.AddForce(direction * movementspeed * Time.deltaTime);
                fuel -= 1;
            }
                
        }

        else
        {
            fuel += 0.5f;
            if (fuel > maxFuel) fuel = maxFuel;
        }
            

        
    }
}
