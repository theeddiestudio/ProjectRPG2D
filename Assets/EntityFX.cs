using System.Collections;
using UnityEngine;

public class EntityFX : MonoBehaviour
{
    private SpriteRenderer sr;

    [Header("Flash FX")]
    [SerializeField] private Material[] hitMaterial;
    [SerializeField] private float flashTime = 0.06f;
    private Material originalMaterial;

    private void Start()
    {
        sr = GetComponentInChildren<SpriteRenderer>();
        originalMaterial = sr.material;
    }

    private IEnumerator FlashFX()
    {
        sr.material = hitMaterial[0];

        yield return new WaitForSeconds(flashTime);

        sr.material = originalMaterial;

        yield return new WaitForSeconds(flashTime);

        sr.material = hitMaterial[1];

        yield return new WaitForSeconds(flashTime);

        sr.material = originalMaterial;
    }
}
