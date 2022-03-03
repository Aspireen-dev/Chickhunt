using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowSpot : MonoBehaviour
{
    [SerializeField]
    private MeshRenderer sphereRend;
    [SerializeField]
    private SphereCollider sphereCol;

    private int nbArrows = 10;
    private float inactiveTime = 10f;

    public int GetNbArrows()
    {
        return nbArrows;
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
