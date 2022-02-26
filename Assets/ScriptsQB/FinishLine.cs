using UnityEngine;

public class FinishLine : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("P1"))
        {
            Debug.Log("P1 wins !");
            Time.timeScale = 0f;
        }
        else if (col.CompareTag("P2"))
        {
            Debug.Log("P2 wins !");
            Time.timeScale = 0f;
        }
    }
}
