using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [Header("Fuel Consumption")]
    public int regularFuelConsumption = 1;
    public float regularFuelReplenishment = 0.5f;
    public float fuelReplenishmentWhenEmpty = 0.1f;

    [Header("Movement Settings")]
    public int movementspeed;
    Rigidbody2D m_Rigidbody;
    public float fuel;
    public float maxFuel;
    public float rotateSpeed;

    [Header("Health Settings")]
    public int hp = 3;
    public Color regularColor;
    public Color flashColor;
    public float flashDuration;
    public int numberOfFlashes;
    private bool isInvincible;
    private SpriteRenderer mySprite;

    // Start is called before the first frame update
    void Start()
    {
        m_Rigidbody = GetComponent<Rigidbody2D>();
        fuel = maxFuel;
        isInvincible = false;
        mySprite = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {

        // Si on veut impl�menter une limite : get la velocity du RigidBody puis normaliser le vecteur + multiplier par la vitesse max

        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        if (Input.GetKey("left")) transform.Rotate(new Vector3(0, 0, rotateSpeed));

        else if (Input.GetKey("right")) transform.Rotate(new Vector3(0, 0, -rotateSpeed));

        if (Input.GetKey("up"))
        {
            if (fuel < regularFuelConsumption)
            {
                fuel += fuelReplenishmentWhenEmpty;
            }

            if (Input.GetKey("space"))
            {
                if (fuel >= regularFuelConsumption * 2)
                {
                    AddForceToShip("boost");
                }
                else if (fuel >= regularFuelConsumption && fuel < regularFuelConsumption*2)
                {
                    AddForceToShip("notBoost");
                }
            }
            else if (!Input.GetKey("space"))
            {
                if (fuel >= regularFuelConsumption)
                {
                    AddForceToShip("notBoost");
                }
            }
        }
        else
        {
            fuel += regularFuelReplenishment;
            if (fuel > maxFuel) fuel = maxFuel;
        }
    }

    private void AddForceToShip(string boostOrNotBoost)
    {
        int boost;
        if (boostOrNotBoost == "boost")
        {
            boost = 2;
        }
        else
        {
            boost = 1;
        }
        m_Rigidbody.AddRelativeForce(Vector3.up * movementspeed * Time.deltaTime * boost);
        fuel -= regularFuelConsumption * boost;
    }

    void TakeDamage()
    {
        hp--;
        StartCoroutine(FlashCo());
        if (hp == 0) Die();
    }

    void Die()
    {
        gameObject.SetActive(false);
    }

    private IEnumerator FlashCo()
    {
        int temp = 0;
        isInvincible = true;
        while(temp < numberOfFlashes)
        {
            mySprite.color = flashColor;
            yield return new WaitForSeconds(flashDuration);
            mySprite.color = regularColor;
            yield return new WaitForSeconds(flashDuration);
            temp++;
        }
        isInvincible = false;
    }
}
