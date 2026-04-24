using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    [SerializeField] float speed;

    //Jump
    [SerializeField] float jumpSpeed;
    bool jump;

    PlayerInput playerInput;
    Rigidbody2D rb;

    [SerializeField]
    CinemachineCamera Ccamera;

    


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerInput = GetComponent<PlayerInput>();
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        var move = playerInput.actions["Move"].ReadValue<Vector2>();

        rb.linearVelocityX = move.x * speed;
        if (move.x < 0)
             

        //Jump
        if (jump) {
            if (playerInput.actions["Jump"].WasPressedThisFrame() && jump) {
                rb.linearVelocityY = jumpSpeed;
                jump = false;
            }
        }

        if (rb.position.y < -8.5f)
        {
            Ccamera.Follow = null;
            Debug.Log("Death");
        }
    }

    //“G‚ð“¥‚ñ‚¾Žž‚ÌƒWƒƒƒ“ƒv
    void EnemyJump()
    {
        if (playerInput.actions["Jump"].IsPressed())
        {
            rb.linearVelocityY = jumpSpeed;
        }
        else
        {
            rb.linearVelocityY = jumpSpeed * 0.5f;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        foreach (ContactPoint2D cont in collision.contacts) 
        {
            if (cont.normal.y > 0.5f) 
                {
                if (collision.collider.CompareTag("Enemy"))
                {
                    EnemyJump();
                }
                else
                {
                    jump = true;
                    break;
                }
            }
        }
    }
}
