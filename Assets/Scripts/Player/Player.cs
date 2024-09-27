using System;
using Cinemachine;
using TMPro;
using UnityEngine;

public class Player : MonoBehaviour
{
    private Rigidbody rb;

    private float hp;
    public float maxHp = 100;

    public TextMeshProUGUI hpText;

    public float moveSpeed;
    public float jumpForce;
    public Transform groundCheck;
    public LayerMask groundMask;

    private bool isGrounded;
    private float horizontal;
    private float vertical;

    public Transform lookAt;
    
    public event Action DeathEvent;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        HpBar();
        IsOnGround();
    }

    public void SetCameraSettings(GameObject cameraSettings)
    {
        var settings = cameraSettings.GetComponent<CinemachineVirtualCamera>();
        settings.Follow = transform;
        settings.LookAt = lookAt;
    }

    public void FullRestore()
    {
        hp = maxHp;
        HpBar();
    }

    private void IsOnGround()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, 0.1f, groundMask);

        if (!isGrounded)
        {
            rb.AddForce(Vector3.down * 2f, ForceMode.VelocityChange);
        }
    }

    public void GetDamage(float damage)
    {
        hp -= damage;
        HpBar();
        if (hp <= 0)
        {
            Death();
        }
    }

    private void Death()
    {
        hp = 0;
        HpBar();
        gameObject.SetActive(false);
        DeathEvent?.Invoke();
    }

    private void HpBar()
    {
        hpText.text = "HP " + hp;
    }

    void Update()
    {
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");
        isGrounded = Physics.CheckSphere(groundCheck.position, 0.1f, groundMask);

        if (isGrounded && Input.GetButtonDown("Jump"))
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }

        if (transform.position.y < -20f)
        {
            Death();
        }
    }

    void FixedUpdate()
    {
        Vector3 move = transform.right * horizontal + transform.forward * vertical;

        rb.MovePosition(rb.position + move * moveSpeed * Time.fixedDeltaTime);
    }
}