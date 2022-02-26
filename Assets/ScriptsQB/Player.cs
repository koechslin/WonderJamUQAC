using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    [SerializeField] private Animator m_animator;
    [SerializeField] private PlayerPerks m_playerPerks;
    
    [Header("Movement Settings")]
    public int movementspeed;
    Rigidbody2D m_Rigidbody;
    [SerializeField] float fuel;
    public float maxFuel;
    public float rotateSpeed;

    [Header("Health Settings")]
    public int currentHP = 3;
    public int maxHP = 3;
    public Color regularColor;
    public Color flashColor;
    public float flashDuration;
    public int numberOfFlashes;
    public bool isInvincible;
    private SpriteRenderer mySprite;

    // Start is called before the first frame update
    void Start()
    {
        m_Rigidbody = GetComponent<Rigidbody2D>();
        fuel = maxFuel;
        isInvincible = false;
        mySprite = GetComponent<SpriteRenderer>();
        currentHP = maxHP;
    }

    // Update is called once per frame
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

        if (Input.GetKey("up"))
        {
            if (fuel < 1)
            {
                fuel += 0.1f;
            }

            if (fuel >= 1)
            {
                m_Rigidbody.AddRelativeForce(Vector3.up * movementspeed * Time.deltaTime);
                fuel--;
            }

        }
        else
        {
            fuel += 0.5f;
            if (fuel > maxFuel) fuel = maxFuel;
        }
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
