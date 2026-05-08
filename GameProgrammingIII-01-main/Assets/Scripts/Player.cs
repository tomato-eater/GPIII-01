using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.InputSystem;
using static UnityEngine.Rendering.DebugUI;

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
        //移動入力
        var move = playerInput.actions["Move"].ReadValue<Vector2>();
        //移動している場合、スケールを進行方向に合わせて変える
        if(move != Vector2.zero)
            transform.localScale = new(Mathf.CeilToInt(move.x), 1, 1);
        //移動量を反映
        rb.linearVelocityX = move.x * speed;
             
        //死亡処理
        if (rb.position.y < -8.5f)
        {
            Ccamera.Follow = null;
            Debug.Log("Death");
        }
    }


    void OnJump()
    {
        if (jump)
        {
            rb.linearVelocityY = jumpSpeed;
            jump = false;
        }
    }

    //敵を踏んだ時のジャンプ
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
