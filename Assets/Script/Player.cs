using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Player : MonoBehaviour
{
    [Header("-----Events-----")]
    public UnityEvent toucthTrap;
    public UnityEvent stageClear;
    public UnityEvent getKeyObj;
    public UnityEvent gethpobj;
    public UnityEvent boxtouch;

    [Header("-----Public-----")]
    public float maxSpeed;
    public float jumpPower;
    public float timer;
    public bool isItem;
    GameObject temp;

    int jumpCount;

    private SpriteRenderer m_spriteRenderer;
    private Rigidbody2D m_rigidbody2D;
    private Animator m_animator;

    // Start is called before the first frame update
    void Start()
    {
        jumpCount = 2;
        m_rigidbody2D = GetComponent<Rigidbody2D>();
        m_animator = GetComponent<Animator>();
        m_spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if (Input.GetButtonDown("Jump") && !m_animator.GetBool("isDoubleJump"))
        {
            m_rigidbody2D.AddForce(Vector2.up * jumpPower , ForceMode2D.Impulse);
            //Debug.Log(m_rigidbody2D.velocity.y);
            jumpCount--;
            m_animator.SetBool("isJump", true);
            if(m_animator.GetBool("isJump") && jumpCount == 0)
            {
                m_animator.SetBool("isDoubleJump", true);
                m_rigidbody2D.AddForce(Vector2.right * jumpPower * m_rigidbody2D.velocity.x, ForceMode2D.Impulse);
            }
        }

        //방향전환
        if (Input.GetButtonDown("Horizontal"))
        {
            m_spriteRenderer.flipX = Input.GetAxisRaw("Horizontal") == -1;
        }

        if(Mathf.Abs(m_rigidbody2D.velocity.x) < 0.3f)
        {
            m_animator.SetBool("isRun", false);
        }
        else
        {
            m_animator.SetBool("isRun", true);
        }

    }
    // Update is called once per frame
    void FixedUpdate()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");

        m_rigidbody2D.AddForce(Vector2.right * horizontal, ForceMode2D.Impulse);

        if(m_rigidbody2D.velocity.x > maxSpeed)
        {
            m_rigidbody2D.velocity = new Vector2(maxSpeed, m_rigidbody2D.velocity.y);
        }
        else if (m_rigidbody2D.velocity.x < maxSpeed * (-1))
        {
            m_rigidbody2D.velocity = new Vector2(maxSpeed * (-1), m_rigidbody2D.velocity.y);
        }

        if(m_rigidbody2D.velocity.y < 0)
        {
            Debug.DrawRay(m_rigidbody2D.position, Vector3.down, new Color(1, 0, 0));

            RaycastHit2D hit = Physics2D.Raycast(m_rigidbody2D.position, Vector3.down, 1f, LayerMask.GetMask("Platform"));

            if (hit.collider != null)
            {
                if (hit.distance < 1.2f)
                {
                    m_animator.SetBool("isJump", false);
                    m_animator.SetBool("isDoubleJump", false);
                    jumpCount = 2;
                }
            }
        }
      


    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Key"))
        {
            collision.gameObject.SetActive(false);
            getKeyObj.Invoke();
        }
        if (collision.CompareTag("HP"))
        {
            collision.gameObject.SetActive(false);
            gethpobj.Invoke();
        }
        
        if (collision.CompareTag("EndObject"))
        {
            stageClear.Invoke();
        }

        if (collision.CompareTag("RandomBox"))
        {
            temp = collision.gameObject;
            temp.GetComponent<BoxCollider2D>().enabled = false;
            //Invoke("activefalse", 1.0f);
            boxtouch.Invoke();
        }
    }

    public void activefalse()
    {
        temp.SetActive(false);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Trap")
        {
            if (isItem)
            {
                //m_animator.SetBool("isItem", false);
                m_animator.SetTrigger("retrunPlayer");
                //transform.localScale = new Vector3(0.85f, 1f, 1f);
                isItem = false;
                // 무적타임 추가 해야할 듯
            }
            else
            {
                Debug.Log("트랩");
                toucthTrap.Invoke();
            }
            
        }
        if(collision.gameObject.tag == "Item")
        {
            collision.gameObject.SetActive(false);
            if (!isItem)
            {
                //m_animator.SetBool("isItem", true);
                m_animator.SetTrigger("getItem");
                transform.localScale = new Vector3(1.2f, 1.5f, 1.5f);
                isItem = true;
            }
        }
        
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        timer += Time.deltaTime;
        if(timer > 0.6f)
        {
            if (collision.CompareTag("ladder"))
            {
                m_rigidbody2D.velocity = Vector2.zero;
                m_rigidbody2D.AddForce(Vector2.up * jumpPower, ForceMode2D.Impulse);
            }
        }
      
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        timer = 0;
    }
}
