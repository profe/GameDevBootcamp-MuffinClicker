using UnityEngine;
using TMPro;

/// <summary>
/// Updates the Header children UI elements.
/// </summary>
public class Header : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI _totalMuffinsText;
    [SerializeField] private TextMeshProUGUI _muffinsPerSecondText;

    /// <summary>
    /// Updates the total muffins text.
    /// </summary>
    /// <param name="counter">The total muffins</param>
    public void UpdateTotalMuffins(int counter)
    {
        _totalMuffinsText.text = GetMuffinsString(counter);
    }

    public void UpdateMuffinsPerSecond(int counter)
    {
        _muffinsPerSecondText.text = GetMuffinsString(counter) + "/second";
    }

    private string GetMuffinsString(int counter)
    {
        return (counter == 1)
                    ? $"{counter} muffin"
                    : $"{counter} muffins";
    }

}