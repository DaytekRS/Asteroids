using UnityEngine;
using UnityEngine.UI;


public class HealthUI : MonoBehaviour
{
    [SerializeField] private HealthComponents _healthComponents;
    [SerializeField] private Sprite _imageSprite;
    [SerializeField] private float _scale = 2f;
    [SerializeField] private float _distance = 15f;

    private void CreateElement(int index)
    {
        GameObject NewObj = new GameObject("Life_" + index);
        
        Image image = NewObj.AddComponent<Image>();
        image.sprite = _imageSprite;
        
        RectTransform recTransform = NewObj.GetComponent<RectTransform>();
        recTransform.SetParent(transform);
        recTransform.anchorMin = Vector2.up;
        recTransform.anchorMax = Vector2.up;
        recTransform.pivot = Vector2.up;
        recTransform.localScale = Vector3.one;
        recTransform.sizeDelta = _imageSprite.textureRect.size * _scale;
        recTransform.localPosition = new Vector3((recTransform.rect.width + _distance) * index, 0, 0);
    }


    private void Update()
    {
        for (int i = _healthComponents.GetHealth(); i < transform.childCount; i++)
        {
            Destroy(transform.GetChild(i).gameObject);
        }

        if (transform.childCount < _healthComponents.GetHealth())
        {
            for (int i = transform.childCount; transform.childCount != _healthComponents.GetHealth(); i++)
            {
                CreateElement(i);
            }
        }
    }
}