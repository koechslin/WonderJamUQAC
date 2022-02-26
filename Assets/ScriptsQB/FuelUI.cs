using UnityEngine;
using UnityEngine.UI;

public class FuelUI : MonoBehaviour
{
    [SerializeField]
    private Player player;
    [SerializeField]
    private Image fillImage;

    private void Start()
    {
        fillImage.color = player.defaultColor;
    }

    private void Update()
    {
        fillImage.fillAmount = player.fuel / player.maxFuel;
    }
}
