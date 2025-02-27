using UnityEngine;
using TMPro;

public class MuffinClicker : MonoBehaviour
{
    //GAME OBJECTS
    [SerializeField]
    private GameManager _gameManager;
    [SerializeField]
    private TextMeshProUGUI _textRewardPrefab;

    [SerializeField]
    private float _minXPos = -150f, _maxXPos = 180f, _minYPos = -15f, _maxYPos = -150f;
    
    public void OnMuffinClicked()
    {
        int addedMuffins = _gameManager.AddMuffins();
        CreateTextRewardPrefab(addedMuffins);
    }

    private void CreateTextRewardPrefab(int addedMuffins)
    {
        TextMeshProUGUI textRewardClone = Instantiate(_textRewardPrefab, transform);
        Vector2 randomVector = MyToolbox.GetRandomVector2(_minXPos, _maxXPos, _minYPos, _maxYPos);
        textRewardClone.transform.localPosition = randomVector;
        _textRewardPrefab.text = $"+{addedMuffins}";
    }
}
