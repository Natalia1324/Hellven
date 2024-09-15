using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;
using UnityEngine.Rendering.Universal;

public class EnemyController : MonoBehaviour
{
    public int lvl = 1;
    public string enemyName = "Glutek";
    public string enemyTitle = "";

    public int maxHealth;
    public int currentHealth;

    [SerializeField] private ParticleSystem test = default(ParticleSystem);

    public float hitColorDuration = 0.5f;
    public Color hitColor = Color.red;
    private SpriteRenderer spriteRenderer;
    public float separationForce = 10f; 


    public float jumpForce = 0.2f;
    public float knockbackIntencity = 5f;
    public float chargeSpeed = 7f;
    public float randomJumpInterval = 2f;
    public float aggroRange = 5f;
    public float deaggroRange = 20f;

    private GameObject PlayerObject;
    private Transform player;
    private bool isAggroed = false;
    private bool isAggroedLast = false;
    private Rigidbody2D rb;
    private Coroutine jumpCoroutine;

    public GameObject healthBarPrefab;
    private GameObject healthBarInstance = null;
    private Slider healthBarSlider;

    public TMP_Text nameText;
    public bool isMagic = false;

    private Light2D enemyLight = null;

    void Start()
    {
        lvl = EnemyManager.Instance.GetGlobalEnemyLevel();
        maxHealth = 10 * lvl;
        currentHealth = maxHealth;
        spriteRenderer = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        rb.drag = 2f;
        rb.freezeRotation = true; // Prevent the sprite from rotating

        PlayerObject = GameObject.Find("Player");
        player = PlayerObject.transform;

        enemyLight = GetComponentInChildren<Light2D>();  

        float randomValue = UnityEngine.Random.Range(0f, 1f);
        if (randomValue < 0.3f) // 30% chance
        {
            randomColor();
        }
        else if (randomValue < 0.4f) // 10% chance
        {
            isMagic = true;
            enemyTitle = "Magic";
            StartCoroutine(HueRotate());
        }
        else if (randomValue < 0.45f) // 5% chance
        {
            MakeElite();
        }

        jumpCoroutine = StartCoroutine(RandomJumpRoutine());

        healthBarInstance = Instantiate(healthBarPrefab, transform.position + Vector3.up * 1.5f, Quaternion.identity);
        healthBarInstance.transform.SetParent(GameObject.Find("Canvas").transform, false);
        healthBarSlider = healthBarInstance.GetComponentInChildren<Slider>();
        healthBarSlider.maxValue = maxHealth;

        nameText = healthBarInstance.GetComponentInChildren<TMP_Text>();
        nameText.text = $"{enemyName} {enemyTitle} Lvl: {lvl}";

        healthBarInstance.SetActive(false);

        if (enemyLight != null)
        {
            enemyLight.color = spriteRenderer.color;
            enemyLight.intensity = 0.5f; 
        }
    }

    void randomColor()
    {
        Color color = Color.HSVToRGB(UnityEngine.Random.Range(0f, 1f), 1f, 1f);
        spriteRenderer.color = color;

        if (enemyLight != null)
        {
            enemyLight.color = color; 
        }
    }

    void MakeElite()
    {
        enemyTitle = "Elite";
        lvl += 10;
        maxHealth = 10 * lvl;
        currentHealth = maxHealth;
        spriteRenderer.color = Color.yellow;

        if (enemyLight != null)
        {
            enemyLight.color = Color.yellow; 
        }
    }

    private void ApplySeparationForce()
    {
        Collider2D[] nearbyEnemies = Physics2D.OverlapCircleAll(transform.position, 1f);
        foreach (Collider2D collider in nearbyEnemies)
        {
            if (collider != null && collider.gameObject != gameObject)
            {
                Vector2 separationDirection = (transform.position - collider.transform.position).normalized;
                rb.AddForce(separationDirection * separationForce, ForceMode2D.Force);
            }
        }
    }


    void Update()
    {
        AI();
        UpdateHealthBarPosition();
        ApplySeparationForce(); 

    }

    private void AI()
    {
        float distanceToPlayer = Vector2.Distance(transform.position, player.position);

        if (distanceToPlayer < aggroRange)
        {
            isAggroed = true;
            if (enemyLight != null)
            {
                enemyLight.intensity = 5f;  
            }
        }
        else if (distanceToPlayer > deaggroRange)
        {
            isAggroed = false;
            if (enemyLight != null)
            {
                enemyLight.intensity = 0.5f; 
            }
        }

        if (isAggroed)
        {
            if (jumpCoroutine != null)
            {
                StopCoroutine(jumpCoroutine);
                jumpCoroutine = null;
            }

            Vector2 direction = (player.position - transform.position).normalized;
            rb.velocity = direction * chargeSpeed;
        }
        else
        {
            if (isAggroedLast)
            {
                rb.velocity = Vector2.zero;
                if (jumpCoroutine == null)
                {
                    jumpCoroutine = StartCoroutine(RandomJumpRoutine());
                }
            }
        }

        isAggroedLast = isAggroed;
    }

    private IEnumerator RandomJumpRoutine()
    {
        while (true)
        {
            if (!isAggroed)
            {
                Vector2 randomDirection = UnityEngine.Random.insideUnitCircle.normalized;
                rb.AddForce(randomDirection * jumpForce, ForceMode2D.Impulse);

                randomJumpInterval = UnityEngine.Random.Range(0.2f, 5f);
                yield return new WaitForSeconds(randomJumpInterval);
            }
            else
            {
                yield break;
            }
        }
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            Die();
        }
        else
        {
            healthBarInstance.SetActive(true);
            healthBarSlider.value = currentHealth;

            StartCoroutine(FlashEnemyColor());
            KnockbackOnDamage();
        }
    }

    private void KnockbackOnDamage()
    {
        Vector2 knockbackDirection = UnityEngine.Random.insideUnitCircle.normalized;
        rb.AddForce(knockbackDirection * knockbackIntencity, ForceMode2D.Impulse);
    }

    IEnumerator FlashEnemyColor()
    {
        Color originalColor = spriteRenderer.color;
        spriteRenderer.color = hitColor;
        yield return new WaitForSeconds(hitColorDuration);
        spriteRenderer.color = originalColor;

        if (enemyLight != null)
        {
            enemyLight.color = originalColor;  
        }
    }

    private void UpdateHealthBarPosition()
    {
        if (healthBarInstance != null)
        {
            healthBarInstance.transform.position = Camera.main.WorldToScreenPoint(transform.position + Vector3.up * 1.5f);
        }
    }

    void Die()
    {
        ParticleSystem newParticleSystem = Instantiate(test);
        newParticleSystem.transform.position = transform.position;
        newParticleSystem.gameObject.layer = 0;
        var particleRenderer = newParticleSystem.GetComponent<Renderer>();
        particleRenderer.sortingOrder = 1;
        newParticleSystem.Play();
        Destroy(newParticleSystem.gameObject, newParticleSystem.main.duration);

        despawn();
    }

    public void despawn()
    {
        Destroy(healthBarInstance);
        Destroy(gameObject);
    }

    private IEnumerator HueRotate()
    {
        while (isMagic)
        {
            float hue, saturation, value;
            Color.RGBToHSV(spriteRenderer.color, out hue, out saturation, out value);
            hue += 0.01f; 
            if (hue >= 1) hue = 0;
            spriteRenderer.color = Color.HSVToRGB(hue, saturation, value);

            if (enemyLight != null)
            {
                enemyLight.color = spriteRenderer.color;  
            }

            yield return new WaitForSeconds(0.1f); 
        }
    }

    public void SetLevel(int level)
    {
        lvl = level;
        maxHealth = 10 * lvl;
        currentHealth = maxHealth;
    }
}
