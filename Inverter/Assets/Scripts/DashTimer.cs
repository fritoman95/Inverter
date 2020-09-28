using UnityEngine.UI;
using UnityEngine;

public class DashTimer : MonoBehaviour
{
    [SerializeField]
    private PlayerDash dashtimer;

    private float amount;

    private Image image;

    private void Start()
    {
        image = GetComponent<Image>();
    }

    private void Update()
    {
        image.fillAmount = amount;

        amount = (dashtimer.currentTimer / dashtimer.maxTimer);
    }
}
