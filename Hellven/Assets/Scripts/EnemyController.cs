using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class EnemyController : MonoBehaviour
{
    public int maxHealth = 20;
    private int currentHealth;
    [SerializeField] private ParticleSystem test = default(ParticleSystem);
    public float hitColorDuration = 0.5f; // Czas trwania zmiany koloru po trafieniu
    public Color hitColor = Color.red; // Kolor trafienia

    private SpriteRenderer spriteRenderer;
    void Start()
    {
        currentHealth = maxHealth;
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            Die();

            if (GameEventsManager.instance != null)
            {
                if (GameEventsManager.instance.miscEvents != null)
                {
                    GameEventsManager.instance.miscEvents.Killmonster();
                }
                else
                {
                    Debug.LogError("miscEvents jest null.");
                }
            }
            else
            {
                Debug.LogError("GameEventsManager.instance jest null.");
            }
        }
        else
        {
            StartCoroutine(FlashEnemyColor());
        }
    }

    IEnumerator FlashEnemyColor()
    {
        // Zapisz pierwotny kolor
        Color originalColor = spriteRenderer.color;

        // Zmień kolor na trafienie
        spriteRenderer.color = hitColor;

        // Poczekaj przez hitColorDuration sekund
        yield return new WaitForSeconds(hitColorDuration);

        // Przywróć pierwotny kolor
        spriteRenderer.color = originalColor;
    }
    void Die()
    {
        
        //Debug.Log("Enemy died!");
        ParticleSystem newParticleSystem = Instantiate(test);
        // Ustaw pozycję nowego systemu cząsteczek na pozycję tego obiektu
        newParticleSystem.transform.position = transform.position;
        newParticleSystem.gameObject.layer = 0;
        var particleRenderer = newParticleSystem.GetComponent<Renderer>();
        particleRenderer.sortingOrder = 1;
        // Uruchom animację cząsteczek
        newParticleSystem.Play();
        // Zniszcz nowo utworzony system cząsteczek po zakończeniu animacji
        Destroy(newParticleSystem.gameObject, newParticleSystem.main.duration);
        Destroy(gameObject);
    }
}
