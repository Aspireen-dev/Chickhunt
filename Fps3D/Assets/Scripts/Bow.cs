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
    private int nbArrows = 10;
    private int nbArrowsWithPowerUp = 0;

    // Accessor for the player to update the UI
    public int NbArrows
    {
        get
        {
            return nbArrows;
        }
    }

    public void AddArrows(int value)
    {
        if (value > 0)
        {
            nbArrows += value;
            SpawnArrow();
        }
    }

    private bool arrowSlotted = false;
    
    // Accessor for the player to know if the player can aim
    public bool ArrowSlotted
    {
        get
        {
            return arrowSlotted;
        }
    }

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
        // If the bow has been bent even a little bit
        if (bowRend.GetBlendShapeWeight(0) > 0.01f)
        {
            bowAnim.SetBool("isAiming", false);
            arrowAnim.SetBool("isAiming", false);

            arrowInstance.transform.parent = null;
            float pulledForce = bowRend.GetBlendShapeWeight(0);
            arrowInstance.GetComponent<Arrow>().Shoot(pulledForce);
            arrowInstance = null;

            arrowSlotted = false;
            nbArrows--;
            if (nbArrowsWithPowerUp > 0)
            {
                nbArrowsWithPowerUp--;
            }

            StartCoroutine(ReloadArrow());
        }
    }

    IEnumerator ReloadArrow()
    {
        yield return new WaitForSeconds(0.5f);
        SpawnArrow();
    }

    void SpawnArrow()
    {
        if (!arrowInstance && nbArrows > 0)
        {
            arrowInstance = Instantiate(arrowPrefab, transform);
            arrowAnim = arrowInstance.GetComponent<Animator>();
            if (nbArrowsWithPowerUp > 0 && powerUp)
            {
                SetPowerUp(powerUp);
            }
            arrowSlotted = true;
        }
    }

    public void SetPowerUp(GameObject newPowerUp, bool powerUpCollected = false)
    {
        if (powerUpCollected)
        {
            nbArrowsWithPowerUp = 3;
        }
        powerUp = newPowerUp;
        if (arrowInstance)
        {
            arrowInstance.GetComponent<Arrow>().SetPowerUp(newPowerUp);
        }
    }
}
