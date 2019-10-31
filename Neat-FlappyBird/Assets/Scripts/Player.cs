using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    private float m_jumpForce = 1.5f;

    [SerializeField]
    private float m_jumpTime;
    private float m_jumpTimeCounter;

    private GameManager m_gm;

    private Rigidbody2D m_rigidbody;

    private void Awake()
    {
        m_rigidbody = GetComponent<Rigidbody2D>();

        m_gm = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        m_gm.AddPlayer(gameObject);
        m_gm.isPlayerGame = true;
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            m_jumpTimeCounter = m_jumpTime;
            m_rigidbody.velocity = Vector2.up * m_jumpForce;
        }

        if(Input.GetKey(KeyCode.Space))
        {
            if(m_jumpTimeCounter > 0)
            {
                m_rigidbody.velocity = Vector2.up * m_jumpForce;
                m_jumpTimeCounter -= Time.deltaTime;
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Die();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.GetComponent<ScoreTrigger>() == null)
        {
            Die();
        }
    }

    private void Die()
    {
        Destroy(gameObject);

        int playersLeft = m_gm.RemovePlayer(gameObject);

        if (playersLeft < 1)
        {
            // GO back to the menu
            m_gm.LoadScene(0);
        }
    }
}
