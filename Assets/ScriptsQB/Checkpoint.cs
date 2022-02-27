using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    [SerializeField]
    private Transform respawnPoint;

    private void OnTriggerEnter2D(Collider2D col)
    {
        Player player = col.GetComponent<Player>();

        if (player == null) return;

        player.lastCheckpoint = respawnPoint.position;
    }
}
