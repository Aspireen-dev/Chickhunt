using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class TextColor : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField]
    private Color baseColor;
    [SerializeField]
    private Color clickColor;


    private TextMeshProUGUI text;

    void Start()
    {
        text = GetComponent<TextMeshProUGUI>();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        text.fontMaterial.SetFloat(ShaderUtilities.ID_FaceDilate, 0.1f);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        text.fontMaterial.SetFloat(ShaderUtilities.ID_FaceDilate, 0f);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        text.faceColor = clickColor;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        text.faceColor = baseColor;
    }


}
