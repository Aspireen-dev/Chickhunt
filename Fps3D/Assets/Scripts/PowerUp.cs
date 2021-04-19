using System.Collections;
using UnityEngine;

public class PowerUp : MonoBehaviour
{
    [SerializeField]
    private MeshRenderer sphereRend;
    [SerializeField]
    private SphereCollider sphereCol;
    [SerializeField]
    private GameObject powerUp;

    private float inactiveTime = 10f;

    public GameObject GetPowerUp()
    {
        return powerUp;
    }

    public IEnumerator Disable()
    {
        Hide();
        yield return new WaitForSeconds(inactiveTime);
        Show();
    }

    private void Hide()
    {
        sphereRend.enabled = false;
        sphereCol.enabled = false;
        transform.GetChild(0).gameObject.SetActive(false);
    }

    private void Show()
    {
        sphereRend.enabled = true;
        sphereCol.enabled = true;
        transform.GetChild(0).gameObject.SetActive(true);
    }
}
