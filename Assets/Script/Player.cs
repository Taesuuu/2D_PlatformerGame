using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float maxSpeed;


    private Rigidbody2D m_rigidbody2D;

    // Start is called before the first frame update
    void Start()
    {
        m_rigidbody2D = GetComponent<Rigidbody2D>();
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
            Debug.Log("Key");
        }
        if (collision.CompareTag("Trap"))
        {
            Debug.Log("Trap");
        }
        if (collision.CompareTag("EndObject"))
        {
            Debug.Log("EndObject");
        }
    }
}
