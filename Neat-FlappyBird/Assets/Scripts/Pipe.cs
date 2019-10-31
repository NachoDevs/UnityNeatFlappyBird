using UnityEngine;

public class Pipe : MonoBehaviour
{
    public float pipeSpeed = 1.0f;

    private void Update()
    {
        Vector3 newPos = new Vector3(transform.position.x - pipeSpeed * Time.deltaTime, transform.position.y, transform.position.z);
        transform.position = newPos;

        if (transform.position.x <= -3)
        {
            Destroy(gameObject);
        }
    }
}
