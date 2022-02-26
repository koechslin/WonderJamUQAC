using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(SpriteRenderer))]
public class Player : MonoBehaviour
{
    public ParticleSystem particle;

    [Header("Fuel Consumption")]
    public int regularFuelConsumption = 1;
    public float regularFuelReplenishment = 0.5f;
    public float fuelReplenishmentWhenEmpty = 0.1f;

    [Header("Movement Settings")]
    public int movementSpeed;
    public float fuel;
    public float maxFuel;
    public float rotateSpeed;
    private new Rigidbody2D rigidbody;

    [Header("Health Settings")]
    public int hp = 3;
    public Color regularColor;
    public Color flashColor;
    public float flashDuration;
    public int numberOfFlashes;
    private bool isInvincible;
    private SpriteRenderer mySprite;

    [Header("Particles settings")]
    public Color defaultColor;
    public Color boostColor;

    [HideInInspector]
    public float horizontalInput;
    [HideInInspector]
    public float verticalInput;
    [HideInInspector]
    public bool isBoostActivated;

    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        fuel = maxFuel;
        isInvincible = false;
        mySprite = GetComponent<SpriteRenderer>();
        movementSpeed = 1000;
    }

    void Update()
    {

        // Si on veut implémenter une limite : get la velocity du RigidBody puis normaliser le vecteur + multiplier par la vitesse max

        if (horizontalInput < - 0.01f) transform.Rotate(new Vector3(0, 0, rotateSpeed));
        
        else if (horizontalInput > 0.01f) transform.Rotate(new Vector3(0, 0, -rotateSpeed));

        if (verticalInput > 0.01f)
        {
            if (fuel < regularFuelConsumption)
            {
                fuel += fuelReplenishmentWhenEmpty;
            }

            if (isBoostActivated)
            {
                if (fuel >= regularFuelConsumption * 2)
                {
                    AddForceToShip(true);
                }
                else if (fuel >= regularFuelConsumption && fuel < regularFuelConsumption * 2)
                {
                    AddForceToShip(false);
                }
            }
            else
            {
                if (fuel >= regularFuelConsumption)
                {
                    AddForceToShip(false);
                }
            }
        }
        else
        {
            fuel += regularFuelReplenishment;
            if (fuel > maxFuel) fuel = maxFuel;
        }
    }

    private void AddForceToShip(bool useBoost)
    {
        int boostFactor = useBoost ? 2 : 1;

        rigidbody.AddRelativeForce(Vector3.up * movementSpeed * Time.deltaTime * boostFactor);
        fuel -= regularFuelConsumption * boostFactor;

        var emitParams = new ParticleSystem.EmitParams
        {
            startColor = defaultColor
        };

        if (useBoost) emitParams.startColor = boostColor;
        
        particle.Emit(emitParams, boostFactor);
    }

    void TakeDamage()
    {
        hp--;
        StartCoroutine(FlashCoroutine());
        if (hp == 0) Die();
    }

    void Die()
    {
        gameObject.SetActive(false);
    }

    private IEnumerator FlashCoroutine()
    {
        int currentNbOfFlashes = 0;
        isInvincible = true;
        
        while(currentNbOfFlashes < numberOfFlashes)
        {
            mySprite.color = flashColor;
            yield return new WaitForSeconds(flashDuration);
            mySprite.color = regularColor;
            yield return new WaitForSeconds(flashDuration);
            currentNbOfFlashes++;
        }
        
        isInvincible = false;
    }
}
