using UnityEngine;

public class ScoreTrigger : MonoBehaviour
{
    [HideInInspector]
    public ScoreManager m_sm;

    private PipeManager m_pm;

    private void Start()
    {
        m_pm = m_sm.GetComponent<PipeManager>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponentInParent<Player>() != null || collision.GetComponentInParent<AIController>() != null)
        {
            m_sm.UpScore(1);
        }
    }
}
