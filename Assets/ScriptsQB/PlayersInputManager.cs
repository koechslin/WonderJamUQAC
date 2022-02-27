using UnityEngine;

public class PlayersInputManager : MonoBehaviour
{
    [SerializeField]
    private Player player1;
    [SerializeField]
    private Player player2;
    
    void Update()
    {
        // Player 1
        player1.horizontalInput = Input.GetAxis("P1_Horizontal");
        player1.verticalInput = Input.GetAxis("P1_Vertical");
        player1.isBoostActivated = Input.GetKey(KeyCode.Space);

        // Player 2
        player2.horizontalInput = Input.GetAxis("P2_Horizontal");
        player2.verticalInput = Input.GetAxis("P2_Vertical");
        player2.isBoostActivated = Input.GetKey(KeyCode.Return);
    }
}
