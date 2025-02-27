using UnityEngine;
using TMPro;

/// <summary>
/// Algorithm:
/// 1. Float the text upwards
/// 2. Fade the text
/// 3. Destroy the text
/// </summary>
public class FloatingText : MonoBehaviour
{
    private float _timer;
    private Color _startColor; //used to create color consistent

    [SerializeField]
    private TextMeshProUGUI _text;
    [SerializeField]
    private float _moveSpeed;

    void Start()
    {
        //set start color to keep it consistent for all prefab clones
        _startColor = _text.color;
    }

    void Update()
    {
        //update timer to track time over several frames
        _timer += Time.deltaTime;

        //1. float text upwards
        transform.Translate(0, _moveSpeed * Time.deltaTime, 0);

        //2. fade the text (change color)
        _text.color = Color.Lerp(_startColor, Color.clear, _timer);

        //3. destroy text (when it disappears)
        //if(_timer >= 1) //will destroy after 1 second
        if(_text.color.a <= 0)
        {
            Destroy(gameObject);
            //Debug.Log("Game Object destroyed!");
        }
    }
}