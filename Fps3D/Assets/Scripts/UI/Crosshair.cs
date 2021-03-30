using UnityEngine;

public class Crosshair : MonoBehaviour
{
    private Animator crosshairAnim;

    void Start()
    {
        crosshairAnim = GetComponent<Animator>();
    }

    public void Aim()
    {
        crosshairAnim.SetBool("isAiming", true);
    }

    public void Shoot()
    {
        crosshairAnim.SetBool("isAiming", false);
    }
}
