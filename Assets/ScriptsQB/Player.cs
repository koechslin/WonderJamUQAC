using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(SpriteRenderer))]
public class Player : MonoBehaviour
{
    [SerializeField] private Collider2D m_collider;
    
    public ParticleSystem particle;

    [Header("Fuel Consumption")]
    public int regularFuelConsumption = 1;
    public float regularFuelReplenishment = 0.5f;
    public float fuelReplenishmentWhenEmpty = 0.1f;

    [SerializeField] private Animator m_animator;
    [SerializeField] private PlayerPerks m_playerPerks;
    
    [Header("Movement Settings")]
    public float movementSpeed;
    public float fuel;
    public float maxFuel;
    public float rotateSpeed;
    private new Rigidbody2D rigidbody;
    private bool EngineAlreadyHeard;

    [Header("Health Settings")]
    public int currentHP = 3;
    public int maxHP = 3;
    public Color regularColor;
    public Color flashColor;
    public float flashDuration;
    public int numberOfFlashes;
    public bool isInvincible;
    private SpriteRenderer mySprite;

    [Header("Particles settings")]
    public Color defaultColor;
    public Color boostColor;

    [Header("Respawn settings")]
    public float respawnDelay;

    [HideInInspector]
    public float horizontalInput;
    [HideInInspector]
    public float verticalInput;
    [HideInInspector]
    public bool isBoostActivated;
    [HideInInspector]
    public Transform lastCheckpoint;

    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        fuel = maxFuel;
        isInvincible = false;
        mySprite = GetComponent<SpriteRenderer>();
        currentHP = maxHP;
        EngineAlreadyHeard = false;
        lastCheckpoint = transform;
    }

    void Update()
    {

        // Si on veut implémenter une limite : get la velocity du RigidBody puis normaliser le vecteur + multiplier par la vitesse max
        if (m_playerPerks.m_inverseController) horizontalInput *= -1.0f;

        if (verticalInput != 0 && !EngineAlreadyHeard)
        {
            FindObjectOfType<AudioManager>().Play("Engine Noise");
            EngineAlreadyHeard = true;
        }

        if (verticalInput == 0 && EngineAlreadyHeard)
        {
            FindObjectOfType<AudioManager>().Stop("Engine Noise");
            EngineAlreadyHeard = false;
        }

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

        rigidbody.AddRelativeForce(Vector3.up * movementSpeed * Time.deltaTime * boostFactor, ForceMode2D.Impulse);

        fuel -= regularFuelConsumption * boostFactor;

        var emitParams = new ParticleSystem.EmitParams
        {
            startColor = defaultColor
        };

        if (useBoost) emitParams.startColor = boostColor;
        
        particle.Emit(emitParams, boostFactor);
    }

    public void TakeDamage()
    {
        currentHP--;
        m_animator.SetTrigger("OnHit");
        StartCoroutine(FlashCoroutine());
        if (currentHP == 0) 
        {
            Die();
            m_animator.SetTrigger("Die");
        }
    }

    void Die()
    {
        if (EngineAlreadyHeard) FindObjectOfType<AudioManager>().Stop("Engine Noise");
        enabled = false;
        m_collider.enabled = false;
        rigidbody.velocity = Vector3.zero;
        rigidbody.angularVelocity = 0f;
        StartCoroutine(RespawnCoroutine());
    }

    public void Respawn(Vector3 respawnPosition)
    {
        currentHP = maxHP;
        transform.position = respawnPosition;
        enabled = true;
        m_collider.enabled = true;
        rigidbody.velocity = Vector3.zero;
        rigidbody.angularVelocity = 0f;
        m_animator.SetTrigger("Respawn");
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

    public void StopPlayer()
    {
        rigidbody.velocity = Vector2.zero;
        this.enabled = false;
    }

    private IEnumerator RespawnCoroutine()
    {
        yield return new WaitForSeconds(respawnDelay);
        Respawn(lastCheckpoint.position);
    }
}
