using UnityEngine;
using UnityEngine.UI;

public class HealthUI : MonoBehaviour
{
    [SerializeField]
    private Player player;
    [SerializeField]
    private GameObject healthParent;
    [SerializeField]
    private GameObject healthIcon;

    private Sprite playerSprite;

    void Start()
    {
        playerSprite = player.GetComponent<SpriteRenderer>().sprite;

        for (int i = 0; i < player.currentHP; ++i)
        {
            GameObject newHealthIcon = GameObject.Instantiate(healthIcon);
            newHealthIcon.GetComponent<Image>().sprite = playerSprite;

            newHealthIcon.transform.SetParent(healthParent.transform, false);
        }

        player.onHealthChange += OnHealthChange;
    }

    private void OnHealthChange()
    {
        foreach (Transform t in healthParent.transform)
        {
            Destroy(t.gameObject);
        }

        for (int i = 0; i < player.currentHP; ++i)
        {
            GameObject newHealthIcon = GameObject.Instantiate(healthIcon);
            newHealthIcon.GetComponent<Image>().sprite = playerSprite;

            newHealthIcon.transform.SetParent(healthParent.transform, false);
        }
    }
}
