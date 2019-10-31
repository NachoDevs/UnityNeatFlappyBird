using SharpNeat.Phenomes;
using UnityEngine;

public class AIController : UnitController
{
    public float fitness;

    [SerializeField]
    private float m_jumpForce = 1.5f;

    [SerializeField]
    private float m_jumpTime;
    private float m_jumpTimeCounter;

    private float m_initialY;

    private GameManager m_gm;
    private PipeManager m_pm;

    private Rigidbody2D m_rigidbody;

    //public int CurrentPiece, LastPiece;

    private bool m_isRunning;

    IBlackBox box;

    private void Awake()
    {
        m_rigidbody = GetComponent<Rigidbody2D>();

        m_gm = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        m_gm.AddPlayer(gameObject);

        m_pm = m_gm.GetComponent<PipeManager>();

        fitness = 0;

        m_initialY = transform.position.y;

        m_jumpTimeCounter = m_jumpTime;
    }

    private void FixedUpdate()
    {
        if(m_isRunning)
        {
            fitness += Time.deltaTime;

            ISignalArray inputArr = box.InputSignalArray;
            inputArr[0] = transform.position.y;
            inputArr[1] = m_pm.currentPipes[0].transform.position.y;
            inputArr[2] = m_pm.currentPipes[1].transform.position.y;

            box.Activate();

            ISignalArray outputArr = box.OutputSignalArray;

            float jumpAxis = (float) outputArr[0];

            jumpAxis = Mathf.Clamp(jumpAxis, 0, 1);

            if(jumpAxis > .5f)
            {
                if (m_jumpTimeCounter > 0)
                {
                    m_rigidbody.velocity = Vector2.up * m_jumpForce;
                    m_jumpTimeCounter -= Time.deltaTime;
                }
            }
            else
            {
                if(m_jumpTimeCounter <= m_jumpTime)
                {
                    m_jumpTimeCounter += Time.deltaTime * 2;
                }
            }

        }
    }

    public override void Activate(IBlackBox box)
    {
        this.box = box;
        m_isRunning = true;
        fitness = 0;
    }

    public override float GetFitness()
    {
        return Mathf.Max(fitness, 0.0f);
    }

    public override void Stop()
    {
        m_isRunning = false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Die();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<ScoreTrigger>() != null)
        {
            fitness += 5f;
        }
        else
        {
            Die();
        }
    }

    private void Die()
    {
        fitness -= 2.5f;
        Stop();
        int playersLeft = m_gm.RemovePlayer(gameObject);

        //if(playersLeft < 1)
        //{
        //    Stop();
        //}

        //Destroy(gameObject);
        //transform.position = new Vector3(0, m_initialY, 0);
        //m_rigidbody.velocity = Vector2.zero;
    }
}
