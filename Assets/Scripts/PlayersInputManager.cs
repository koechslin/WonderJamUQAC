using UnityEngine;

public class PlayersInputManager : MonoBehaviour
{
    [SerializeField]
    private SimpleMovement player1Movement;
    [SerializeField]
    private SimpleMovement player2Movement;
    
    void Update()
    {
        // Player 1
        player1Movement.horizontalInput = Input.GetAxis("P1_Horizontal");
        player1Movement.verticalInput = Input.GetAxis("P1_Vertical");

        // Player 2
        player2Movement.horizontalInput = Input.GetAxis("P2_Horizontal");
        player2Movement.verticalInput = Input.GetAxis("P2_Vertical");
    }
}
