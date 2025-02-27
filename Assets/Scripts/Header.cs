using UnityEngine;
using TMPro;

/// <summary>
/// Updates the Header children UI elements.
/// </summary>
public class Header : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI _totalMuffinsText;
    
    /// <summary>
    /// Updates the total muffins text.
    /// </summary>
    /// <param name="counter">The total muffins</param>
    public void UpdateTotalMuffins(int counter)
    {
        _totalMuffinsText.text = (counter == 1)
                                    ? $"{counter} muffin"
                                    : $"{counter} muffins";
    }

}