using System.Collections;
using UnityEngine;

public class Bow : MonoBehaviour
{
    public GameObject arrowPrefab;
    private GameObject arrowInstance;

    private Animator bowAnim;
    private Animator arrowAnim;

    private SkinnedMeshRenderer bowRend;

    private GameObject powerUp = null;

    // Start is called before the first frame update
    void Start()
    {
        bowAnim = GetComponent<Animator>();
        bowRend = GetComponent<SkinnedMeshRenderer>();
        SpawnArrow();
    }

    public void Aim()
    {
        bowAnim.SetBool("isAiming", true);
        arrowAnim.SetBool("isAiming", true);
    }

    public void Shoot()
    {
        if (bowRend.GetBlendShapeWeight(0) > 0.01f)
        {
            bowAnim.SetBool("isAiming", false);
            arrowAnim.SetBool("isAiming", false);

            arrowInstance.transform.parent = null;
            float pulledForce = bowRend.GetBlendShapeWeight(0);
            arrowInstance.GetComponent<Arrow>().Shoot(pulledForce);
            arrowInstance = null;

            StartCoroutine(ReloadArrow());
        }
    }

    IEnumerator ReloadArrow()
    {
        yield return new WaitForSeconds(1f);
        SpawnArrow();
    }

    void SpawnArrow()
    {
        arrowInstance = Instantiate(arrowPrefab, transform);
        arrowAnim = arrowInstance.GetComponent<Animator>();
        if (powerUp)
        {
            SetPowerUp(powerUp);
        }
    }

    public void SetPowerUp(GameObject newPowerUp)
    {
        powerUp = newPowerUp;
        if (arrowInstance)
        {
            arrowInstance.GetComponent<Arrow>().SetPowerUp(newPowerUp);
        }
    }
}
