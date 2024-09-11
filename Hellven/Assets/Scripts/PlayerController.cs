using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public int money;

    public int maxHP;
    public int currentHP;
    public float moveSpeed;
    public float damping;
    public AudioSource audioSourceSword;
    public AudioSource audioSourceDash;
    public AudioSource audioSourceSwing;
    public bool isMoving;
    public bool isSprinting = false;
    //public Transform attackPoint;
    //public float attackRange = 0.5f;
    //public LayerMask enemyLayers;
    public Transform attackPoint;
    public float attackRange = 0.5f;
    public LayerMask enemyLayers;
    public int attackDamage = 10;
    public float attackDelay = 0.5f;

    public Vector3 velocity;
    public Vector3 acceleration;

    [SerializeField] private ParticleSystem test = default(ParticleSystem);
    private bool isAttacking;

    public Vector3 input;

    private Animator animator;

    public LayerMask solidLayer;
    //public LayerMask battleLayer;

    public float sprintMultiplier = 20f;

    private float stamina;
    public float maxStamina = 100f;
    public float staminaUsageRate = 10f;
    public float staminaRecoveryRate = 5f;

    private bool canSprint = true;

    public Slider HPSlider;        
    public Slider StaminaSlider;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }
    private void Start()
    {
        currentHP = maxHP;
        stamina = maxStamina;
        InitializeSliders();
    }

    void InitializeSliders()
    {
        HPSlider.maxValue = maxHP;
        HPSlider.value = currentHP;
        StaminaSlider.maxValue = maxStamina;
        StaminaSlider.value = stamina;
    }

    void UpdateSliders()
    {
        HPSlider.value = currentHP;
        StaminaSlider.value = stamina; 
    }

    void ParticlePlay()
    {
        Vector3 newpos = new Vector3(transform.position.x, transform.position.y + 1, transform.position.z);
        ParticleSystem newParticleSystem = Instantiate(test);
        // Ustaw pozycję nowego systemu cząsteczek na pozycję tego obiektu
        newParticleSystem.transform.position = newpos;
        newParticleSystem.gameObject.layer = 0;

        var particleRenderer = newParticleSystem.GetComponent<Renderer>();
        particleRenderer.sortingOrder = 0;
        particleRenderer.sortingLayerName = "Player";
        // Uruchom animację cząsteczek
        newParticleSystem.Play();
        Destroy(newParticleSystem.gameObject, newParticleSystem.main.duration);
    }
    private void Update()
    {
        input.x = Input.GetAxisRaw("Horizontal");
        input.y = Input.GetAxisRaw("Vertical");
        input.z = 0;

        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            PlayDash();
        }
        if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            StopDash();
        }

        bool sprintInput = Input.GetKey(KeyCode.LeftShift);
        if (sprintInput && stamina > 0 && canSprint)
        {
            isSprinting = true;
        }
        else
        {
            isSprinting = false;
        }

        float currentSpeed = isSprinting ? moveSpeed * sprintMultiplier : moveSpeed;

        if (input != Vector3.zero)
        {
            // Normalizowanie kierunku ruchu
            Vector3 direction = input.normalized;

            // Pozycja docelowa, na którą zmierza postać
            Vector3 targetPosition = transform.position + direction * currentSpeed * Time.deltaTime;


            RaycastHit2D hit2 = Physics2D.Raycast(transform.position, direction, 2f, solidLayer);

            if (hit2.collider != null)
            {
                // Jeśli Raycast 2D trafia w coś
                UnityEngine.Debug.Log("Wykryto kolizję z: " + hit2.collider.name);
            }


            // Sprawdzanie kolizji
            if (hit2.collider == null)
            {
                // Jeśli nie ma kolizji, kontynuuj ruch
                animator.SetFloat("MoveX", input.x);
                animator.SetFloat("MoveY", input.y);

                acceleration = direction * moveSpeed;
                isMoving = true;

                if (isSprinting)
                {
                    stamina -= staminaUsageRate * Time.deltaTime;
                    if (stamina <= 0)
                    {
                        stamina = 0;
                        canSprint = false;
                    }
                }
                else if (stamina < maxStamina)
                {
                    stamina += staminaRecoveryRate * Time.deltaTime;
                    if (stamina > maxStamina) stamina = maxStamina;

                    if (stamina >= maxStamina * 0.2f)
                    {
                        canSprint = true;
                    }
                }
            }
            else
            {
                // Zatrzymanie ruchu, jeśli Raycast wykryje kolizję
                acceleration = Vector3.zero;
                isMoving = false;
            }
        }
        else
        {
            acceleration = Vector3.zero;
            if (stamina < maxStamina)
            {
                stamina += staminaRecoveryRate * Time.deltaTime;
                if (stamina > maxStamina) stamina = maxStamina;
            }
        }

        if (Input.GetKey(KeyCode.Space))
        {
            StartCoroutine(Attack());
        }
        else
        {
            isAttacking = false;
        }

        velocity.x = (velocity.x + acceleration.x) * damping;
        velocity.y = (velocity.y + acceleration.y) * damping;
        velocity.z = (velocity.z + acceleration.z) * damping;

        if (velocity.x < 0.02f && velocity.x > -0.02f) velocity.x = 0;
        if (velocity.y < 0.02f && velocity.y > -0.02f) velocity.y = 0;
        if (velocity.z < 0.02f && velocity.z > -0.02f) velocity.z = 0;
        if (velocity == Vector3.zero) isMoving = false;

        transform.position += velocity * currentSpeed * Time.deltaTime;

        animator.SetBool("isAttacking", isAttacking);
        animator.SetBool("isMoving", isMoving);

        UpdateSliders();
    }

    //void Attack()
    //{
    //    Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);

    //    // Zadanie obrażeń trafionym przeciwnikom
    //    foreach (Collider2D enemy in hitEnemies)
    //    {
    //        Debug.Log("Trafiono: " + enemy.name);
    //        // Tutaj dodaj kod zadawania obrażeń przeciwnikom
    //    }
    //}

    IEnumerator Attack()
    {
        isAttacking = true;
        {
            yield return new WaitForSeconds(attackDelay);
        }
        isAttacking = false;
    }

    public void TakeDamage(int damage)
    {
        currentHP -= damage;
        if (currentHP <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        UnityEngine.Debug.Log("Player died!");
        // Tutaj można dodać logikę śmierci gracza
    }
    public void DealDamage()
    {
        // Sprawdzenie kolizji z przeciwnikami
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);
        
        if (hitEnemies.Length == 0)
        {
            PlaySwing();
            UnityEngine.Debug.Log("Nie trafiono żadnego przeciwnika.");
        }
        // Zadanie obrażeń trafionym przeciwnikom
        foreach (Collider2D enemy in hitEnemies)
        {
            PlaySword();
            UnityEngine.Debug.Log("Trafiono: " + enemy.name);
            enemy.GetComponent<EnemyController>().TakeDamage(attackDamage);
        }
    }

    void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
            return;

        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }


    IEnumerator Move(Vector3 targetPos)
    {
        isMoving = true;
        while ((targetPos - transform.position).sqrMagnitude > Mathf.Epsilon)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPos, moveSpeed * Time.deltaTime);
            yield return null;
        }
        transform.position = targetPos;

        isMoving = false;

    }

    private bool isWalkable(Vector3 targetPos)
    {
        if (Physics2D.OverlapCircle(targetPos, 0.01f, solidLayer) != null)
        {
            return false;
        }
        return true;

    }
    public void PlaySwing()
    {
        audioSourceSwing.Play();
    }
    public void PlaySword()
    {
        audioSourceSword.Play();
    }
    public void PlayDash()
    {
        audioSourceDash.Play();
    }
    public void StopDash()
    {
        audioSourceDash.Stop();
    }
}