using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class Parallax : MonoBehaviour
{
    [SerializeField]
    private GameObject cam;
    [SerializeField]
    private float parallaxEffect;
    
    private float startPosX;
    private float startPosY;

    private void Start()
    {
        startPosX = transform.position.x;
        startPosY = transform.position.y;
    }

    private void FixedUpdate()
    {
        float distanceX = cam.transform.position.x * parallaxEffect;
        float distanceY = cam.transform.position.y * parallaxEffect;

        transform.position = new Vector3(startPosX + distanceX, startPosY + distanceY, transform.position.z);
    }
}
