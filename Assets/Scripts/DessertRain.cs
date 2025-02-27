using UnityEngine;
using UnityEngine.UI;

public class DessertRain : MonoBehaviour
{

    private float _timer;       //holds current time (resets every time a new dessert drawn)
    private float _threshold;   //random threshold until next one is drawn

    [SerializeField]
    private int _maxThreshold;
    
    [SerializeField]
    private FallingDessert _fallingDessert;

    [SerializeField]
    private Texture2D[] _candies;


    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //setup timer and random threshold
        ResetTimer();
    }

    private void ResetTimer()
    {
        _timer = 0;
        _threshold = Random.Range(0, _maxThreshold);
    }

    // Update is called once per frame
    void Update()
    {
        //update time
        _timer += Time.deltaTime;

        //generate new dessert if reached threshold
        if(_timer >= _threshold) {
            //generate random dessert location
            FallingDessert fallingDesertClone = Instantiate(_fallingDessert, transform);
            Vector2 randomVector = MyToolbox.GetRandomVector2(-1 * Screen.width, Screen.width, Screen.height, Screen.height);
            fallingDesertClone.transform.localPosition = randomVector;

            //get random image
            Texture2D randomDessert = _candies[Random.Range(0, _candies.Length)];//max is exclusive! 

            //set random image
            fallingDesertClone.GetComponent<Image>().sprite = Sprite.Create(randomDessert, new Rect(0, 0, randomDessert.width, randomDessert.height), Vector2.zero);

            //reset timer to drawn next dessert
            ResetTimer();
        }
        //else just let update run again (more time pass)
    }
}
