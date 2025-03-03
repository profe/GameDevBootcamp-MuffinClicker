using UnityEngine;
using TMPro;

public class UpgradeButton : MonoBehaviour
{
    private int _level;

    [SerializeField] private float _costPowerScale = 1.5f;

    //GAME OBJECTS
    [SerializeField] private TextMeshProUGUI _levelText;
    [SerializeField] private TextMeshProUGUI _costText;
    [SerializeField] private GameManager _gameManager;

    public void Start()
    {
        UpdateUI();
    }

    private int CurrentCost
    {
        get
        {
            return 5 + Mathf.RoundToInt(Mathf.Pow(_level, _costPowerScale)); //steep/narrow parabola shifted/starting at 5
        }
    }

    public void TotalMuffinsChanged(int totalMuffins)
    {
        bool canAfford = totalMuffins >= CurrentCost;
        _costText.color = canAfford ? Color.green : Color.red;
    }

    public void OnMuffinUpgradeClicked()
    {
        bool purchasedUpgrade = _gameManager.TryMuffinPurchaseUpgrade(CurrentCost, _level);
        if (purchasedUpgrade)
        {
            _level++;
            UpdateUI();
        }
    }

    public void OnSugarRushUpgradeClicked()
    {
        bool purchasedUpgrade = _gameManager.TrySugarRushPurchaseUpgrade(CurrentCost, _level);
        if (purchasedUpgrade)
        {
            _level++;
            UpdateUI();
        }
    }

    private void UpdateUI()
    {
        _levelText.text = _level.ToString();
        _costText.text = CurrentCost.ToString();
    }
}
