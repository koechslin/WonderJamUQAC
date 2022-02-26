using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class Parallax : MonoBehaviour
{
    [SerializeField]
    private GameObject cam;
    [SerializeField]
    private float parallaxEffect;
    
    private float length;
    private float startPos;

    private void Start()
    {
        startPos = transform.position.x;
        length = GetComponent<SpriteRenderer>().bounds.size.x;
    }

    private void FixedUpdate()
    {
        float distance = cam.transform.position.x * parallaxEffect;

        transform.position = new Vector3(startPos + distance, transform.position.y, transform.position.z);
    }
}
