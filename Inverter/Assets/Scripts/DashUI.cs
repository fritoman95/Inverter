using UnityEngine;
using UnityEngine.UI;

public class DashUI : MonoBehaviour
{
    public Image[] dashIcon;
    public Image[] DashTimer;

    private int imageIndex = 3;

    private void Update()
    {
        switch (imageIndex)
        {
            case 3:
                DashTimer[0].gameObject.SetActive(false);
                DashTimer[1].gameObject.SetActive(false);
                DashTimer[2].gameObject.SetActive(false);
                break;

            case 2:
                DashTimer[2].gameObject.SetActive(true);
                DashTimer[1].gameObject.SetActive(false);
                DashTimer[0].gameObject.SetActive(false);
                
                break;

            case 1:
                DashTimer[1].gameObject.SetActive(true);
                DashTimer[2].gameObject.SetActive(false);
                DashTimer[0].gameObject.SetActive(false);
                break;

            case 0:
                DashTimer[0].gameObject.SetActive(true);
                DashTimer[1].gameObject.SetActive(false);
                DashTimer[2].gameObject.SetActive(false);
                break;
        }
    }

    public void IncreaseDash()
    {
        dashIcon[imageIndex].gameObject.SetActive(true);

        imageIndex++;
    }

    public void DecreaseDash()
    {
        imageIndex--;

        dashIcon[imageIndex].gameObject.SetActive(false);
    }


}
