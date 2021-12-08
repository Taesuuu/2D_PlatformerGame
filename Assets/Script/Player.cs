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
    GameObject temp;

    private Rigidbody2D m_rigidbody2D;
    private Animator m_animator;

    // Start is called before the first frame update
    void Start()
    {
        m_rigidbody2D = GetComponent<Rigidbody2D>();
        m_animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (Input.GetButtonDown("Jump"))
        {
            m_rigidbody2D.AddForce(Vector2.up * jumpPower , ForceMode2D.Impulse);
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
            Invoke("activefalse", 1.0f);
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
            Debug.Log("트랩");
            toucthTrap.Invoke();
        }
        
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        timer += Time.deltaTime;
        if(timer > 1.0f)
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
