using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(CircleCollider2D))]
public class npc : MonoBehaviour
{
    public GameObject dialoguePanel;
    public Text dialogueText;
    public string[] dialogue;
    private int index;

    public GameObject countButton;
    public float wordSpeed;
    public bool playerIsClose;

    // Movement related variables
    public float moveSpeed = 2f;        // Speed of the NPC
    public float changeDirectionTime = 3f; // Time between direction changes
    private Vector2 movementDirection;
    private Rigidbody2D rb;
    private float timeSinceLastDirectionChange = 0f;

    // Movement area boundaries
    private Vector2 minBound = new Vector2(-200, -200);
    private Vector2 maxBound = new Vector2(200, 200);

    void Start()
    {
        // Get the Rigidbody2D component
        rb = GetComponent<Rigidbody2D>();

        // Start moving in a random direction
        SetRandomDirection();
    }

    //void Update()
    //{
    //    if(Input.GetKeyDown(KeyCode.P) && playerIsClose)
    //    {
    //        Debug.Log("Jest P");
    //        if (dialoguePanel.activeInHierarchy)
    //        {
    //            zeroText();
    //        }
    //        else
    //        {
    //            dialoguePanel.SetActive(true);
    //            StartCoroutine(Typing());
    //        }
    //    }

    //    if(dialogueText.text == dialogue[index])
    //    {
    //        countButton.SetActive(true);
    //    }
    //}
    void Update()
    {
        // Handle player interaction for dialogue
        HandleDialogueInput();

        // Handle NPC movement if the player is not close
        if (!playerIsClose)
        {
            HandleMovement();
        }
    }

    void HandleMovement()
    {
        // Change direction after a certain time interval
        timeSinceLastDirectionChange += Time.deltaTime;
        if (timeSinceLastDirectionChange >= changeDirectionTime)
        {
            SetRandomDirection();
            timeSinceLastDirectionChange = 0f;
        }

        // Move the NPC
        rb.velocity = movementDirection * moveSpeed;

        // Keep the NPC within bounds
        Vector2 currentPosition = rb.position;
        currentPosition.x = Mathf.Clamp(currentPosition.x, minBound.x, maxBound.x);
        currentPosition.y = Mathf.Clamp(currentPosition.y, minBound.y, maxBound.y);
        rb.position = currentPosition;
    }

    void SetRandomDirection()
    {
        // Generowanie losowego kierunku
        float randomX = Random.Range(-1f, 1f);
        float randomY = Random.Range(-1f, 1f);
        movementDirection = new Vector2(randomX, randomY).normalized;
    }
  

    void HandleDialogueInput()
    {
        if (Input.GetKeyDown(KeyCode.P) && playerIsClose)
        {
            Debug.Log("Pressed P");
            if (dialoguePanel.activeInHierarchy)
            {
                zeroText();
            }
            else
            {
                dialoguePanel.SetActive(true);
                StartCoroutine(Typing());
            }
        }

        if (dialogueText.text == dialogue[index])
        {
            countButton.SetActive(true);
        }
    }

    public void zeroText()
    {
        dialogueText.text = "";
        index = 0;
        dialoguePanel.SetActive(false);
    }

    IEnumerator Typing()
    {
        foreach(char letter in dialogue[index].ToCharArray())
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(wordSpeed);
        }
    }

    public void NextLine()
    {

        countButton.SetActive(false);


        if(index < dialogue.Length - 1)
        {
            index++;
            dialogueText.text = " ";
            StartCoroutine(Typing());
        }
        else
        {
            zeroText();

        }
    }

    //private void OnTriggerEnter2D(Collider2D other)
    //{
    //    if (other.CompareTag("Player"))
    //    {
    //        playerIsClose = true;
    //    }
    //}

    private void OnTriggerEnter2D(Collider2D otherCollider)
    {
        if (otherCollider.CompareTag("Player"))
        {
            playerIsClose = true;
            rb.velocity = Vector2.zero; // Zatrzymanie ruchu, gdy gracz jest blisko
        }
    }

    private void OnTriggerExit2D(Collider2D otherCollider)
    {
        if (otherCollider.CompareTag("Player"))
        {
            playerIsClose = false;
            zeroText();
        }
    }
}
