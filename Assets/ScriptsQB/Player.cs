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

    [SerializeField] private Animator m_animator;
    [SerializeField] private PlayerPerks m_playerPerks;
    
    [Header("Movement Settings")]
    public int movementSpeed;
    public float fuel;
    public float maxFuel;
    public float rotateSpeed;
    private new Rigidbody2D rigidbody;

    [Header("Health Settings")]
    public int currentHP = 3;
    public int maxHP = 3;
    public Color regularColor;
    public Color flashColor;
    public float flashDuration;
    public int numberOfFlashes;
    public bool isInvincible;
    private SpriteRenderer mySprite;

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
        currentHP = maxHP;
    }

    void Update()
    {

        // Si on veut implémenter une limite : get la velocity du RigidBody puis normaliser le vecteur + multiplier par la vitesse max

        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");
        Vector2 direction = new Vector3(horizontalInput, verticalInput, 0f);
        Vector2 checkDirectionNull = new Vector2(0, 0);

        if (m_playerPerks.m_inverseController)
        {
            if (Input.GetKey("left")) transform.Rotate(new Vector3(0, 0, -rotateSpeed));

            else if (Input.GetKey("right")) transform.Rotate(new Vector3(0, 0, rotateSpeed));
        }
        else
        {
            if (Input.GetKey("left")) transform.Rotate(new Vector3(0, 0, rotateSpeed));

            else if (Input.GetKey("right")) transform.Rotate(new Vector3(0, 0, -rotateSpeed));
        }
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
        int boost = useBoost ? 2 : 1;
        
        rigidbody.AddRelativeForce(Vector3.up * movementSpeed * Time.deltaTime * boost);
        fuel -= regularFuelConsumption * boost;
        var emitParams = new ParticleSystem.EmitParams();
        if (boost == 1) emitParams.startColor = Color.magenta;
        else if (boost == 2) emitParams.startColor = Color.red;
        particle.Emit(emitParams, boost);
    }

    public void TakeDamage()
    {
        currentHP--;
        m_animator.SetTrigger("OnHit");
        StartCoroutine(FlashCo());
        if (currentHP == 0) Die();
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
