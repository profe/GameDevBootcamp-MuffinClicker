using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Algorithm:
/// 1. choose random image for current dessert
/// 2. Float the dessert down
/// 3. Destroy the dessert
/// </summary>
public class FallingDessert : MonoBehaviour
{
    [SerializeField]
    private float _fallSpeed;

    void Update()
    {
        // 2. Float the dessert down
        transform.Translate(0, -1 * _fallSpeed * Time.deltaTime, 0);

        // 3. Destroy the dessert
        if (gameObject.transform.position.y < -1 * Screen.height)
        {
            Destroy(gameObject);
        }
    }
}
