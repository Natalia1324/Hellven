using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using TMPro;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public int maxHP;
    public int currentHP;
    public float moveSpeed;
    public float damping;

    public bool isMoving;
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


    private bool isAttacking;

    private Vector3 input;

    private Animator animator;

    public LayerMask solidLayer;
    //public LayerMask battleLayer;

    public TextMeshProUGUI healthText;
    private void Awake()
    {
        animator = GetComponent<Animator>();
        
    }
    private void Start()
    {
        //currentHP = maxHP;
        UpdateHealthUI();
    }
    void UpdateHealthUI()
    {
        Debug.Log("HP is: " +  currentHP);
        healthText.text = "HP: " + currentHP + "/" + maxHP;
    }
    private void Update()
    {
        if (!isMoving)
        {
            input.x = Input.GetAxisRaw("Horizontal");
            input.y = Input.GetAxisRaw("Vertical");
            input.z = 0;

            //Debug.Log(input.x + " " + input.y);


            if (input != Vector3.zero)
            {
                animator.SetFloat("MoveX", input.x);
                animator.SetFloat("MoveY", input.y);

                acceleration = input * moveSpeed;

                isMoving = true;
            }
            else
            {
                acceleration = Vector3.zero;
            }
        }
        else
        {
            acceleration = Vector3.zero;
        }

        if (Input.GetKey(KeyCode.Space))
        {
            StartCoroutine(Attack());
        }
        else isAttacking = false;

        //velocity = velocity * acceleration * Time.deltaTime * damping;
        velocity.x = ( velocity.x + acceleration.x ) * damping;
        velocity.y = ( velocity.y + acceleration.y ) * damping;
        velocity.z = ( velocity.z + acceleration.z ) * damping;

        if ( velocity.x < 0.02f && velocity.x > -0.02f ) velocity.x = 0;
        if ( velocity.y < 0.02f && velocity.y > -0.02f ) velocity.y = 0;
        if ( velocity.z < 0.02f && velocity.z > -0.02f ) velocity.z = 0;
        if ( velocity == Vector3.zero ) isMoving = false;


        var targetPos = transform.position;
        targetPos += velocity;
        
        transform.position = targetPos * moveSpeed * Time.deltaTime;

        // if (isWalkable(targetPos)) StartCoroutine(Move(targetPos));

        //Debug.Log("Attack is: "+ isAttacking);

        animator.SetBool("isAttacking", isAttacking);
        animator.SetBool("isMoving", isMoving);
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

        // Włączenie animacji ataku
        //animator.SetTrigger("Attack");

        // Opóźnienie przed zadaniem obrażeń
        yield return new WaitForSeconds(attackDelay);

        // Sprawdzenie kolizji z przeciwnikami
        //Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);

        //// Zadanie obrażeń trafionym przeciwnikom
        //foreach (Collider2D enemy in hitEnemies)
        //{
        //    Debug.Log("Trafiono: " + enemy.name);
        //    enemy.GetComponent<EnemyController>().TakeDamage(attackDamage);
        //}

        isAttacking = false;
    }

    public void TakeDamage(int damage)
    {
        currentHP -= damage;
        if (currentHP <= 0)
        {
            Die();
        }
        UpdateHealthUI();
    }

    void Die()
    {
        Debug.Log("Player died!");
        // Tutaj można dodać logikę śmierci gracza
    }
    public void DealDamage()
    {
        // Sprawdzenie kolizji z przeciwnikami
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);

        // Zadanie obrażeń trafionym przeciwnikom
        foreach (Collider2D enemy in hitEnemies)
        {
            Debug.Log("Trafiono: " + enemy.name);
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
}