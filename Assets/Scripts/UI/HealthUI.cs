using UnityEngine;
using UnityEngine.UI;

public class HealthUI : MonoBehaviour
{
    [SerializeField] private Sprite _imageSprite;
    [SerializeField] private float _scale = 2f;
    [SerializeField] private float _distance = 15f;

    private HealthComponents _healthComponents;

    private void Start()
    {
        GameObject Player = transform.parent.GetComponent<PlayerUI>().GetPlayer();
        _healthComponents = Player.GetComponent<HealthComponents>();
    }

    private void SetSettingsElement(RectTransform recTransform)
    {
        recTransform.anchorMin = Vector2.up;
        recTransform.anchorMax = Vector2.up;
        recTransform.pivot = Vector2.up;
        recTransform.localScale = Vector3.one;
        recTransform.sizeDelta = _imageSprite.textureRect.size * _scale;
    }

    private void CreateElement(int index)
    {
        GameObject NewObj = new GameObject("Life_" + index);

        Image image = NewObj.AddComponent<Image>();
        image.sprite = _imageSprite;

        RectTransform recTransform = NewObj.GetComponent<RectTransform>();
        recTransform.SetParent(transform);
        SetSettingsElement(recTransform);
        recTransform.localPosition = new Vector3((recTransform.rect.width + _distance) * index, 0, 0);
    }

    private void Update()
    {
        //if the number of hp is less than elements, remove the extra ones
        for (int i = _healthComponents.GetHealth(); i < transform.childCount; i++)
        {
            Destroy(transform.GetChild(i).gameObject);
        }
        
        //if the number of hp is larger than elements, add missing
        if (transform.childCount < _healthComponents.GetHealth())
        {
            for (int i = transform.childCount; transform.childCount != _healthComponents.GetHealth(); i++)
            {
                CreateElement(i);
            }
        }
    }
}