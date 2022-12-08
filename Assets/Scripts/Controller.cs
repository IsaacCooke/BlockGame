using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Controller : MonoBehaviour
{
    [Tooltip("Rigidbody component attached to the player")]
    public Rigidbody rb;
    public int health = 100;
    public GameObject deadMenu;
    public Slider healthBar;
    public Text percentageText;

    private float movementX;
    private float movementY;
    private float gravity = -9.81f;
    private float speedX = 10;
    private float speedY = 0.0003f;
    private float speedZ = 5;
    private int jumpTime = 0;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        Physics.gravity = new Vector3(0, -100, 0);
        gameObject.tag = "Player";
        InitHeathBar();
    }

    IEnumerator InitHeathBar()
    {
        healthBar.value = health;
        float value = 0.0f;
        healthBar.maxValue = health;

        while(value > 0)
        {
            yield return new WaitForSeconds(1.0f);
        }
    }
    void Update()
    {
        
    }
    void OnCollisionEnter(Collision collision)
    {
        // SceneManager.LoadScene(1);
    }

    // Update is called once per frame
    void OnMove(InputValue movementValue)
    {
        Vector2 movementVector = movementValue.Get<Vector2>();

        movementX = movementVector.x;
        movementY = movementVector.y;
    }

    void OnJump()
    {
        jumpTime = 25;
    }

    void CalculateMovement(Vector3 jumpVelocity)
    {
        Vector3 movementVelocity = new Vector3(movementX * speedX, jumpVelocity.y, speedZ);
        rb.velocity = movementVelocity;
    }
    void FixedUpdate()
    {
        Vector3 movementVelocity = new Vector3();
        if(jumpTime > 0)
        {
            if(jumpTime >= 10) speedY = 0.0002f;
            else speedY = 0.0003f;
            movementVelocity.y += Mathf.Sqrt(speedY * -3.0f * gravity);
            movementVelocity.y *= 100;
            jumpTime -= 1;
        }
        else
        {
            movementVelocity.y = 0;
        }
        CalculateMovement(movementVelocity);
        if(health <= 0)
        {
            deadMenu.SetActive(true);
            Time.timeScale = 0;
        }
        Debug.Log("Player health is " + health);
        healthBar.value = health;
        // percentageText.text = healthBar.value.ToString();
        if(healthBar.value == health)
        {
            Debug.Log("Health is now " + health);
        }
    }
}