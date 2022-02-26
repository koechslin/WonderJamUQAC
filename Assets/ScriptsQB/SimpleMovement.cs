using UnityEngine;

public class SimpleMovement : MonoBehaviour
{
    public float horizontalInput;
    public float verticalInput;
    public float scaleFactor;
    
    void Update()
    {
        transform.Translate(scaleFactor * horizontalInput, scaleFactor * verticalInput, 0f);
    }
}
