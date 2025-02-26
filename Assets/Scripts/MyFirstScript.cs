using UnityEngine;
using TMPro;

public class MyFirstScript : MonoBehaviour
{
    //COUNTER
    private int _counter = 0;
    private int _addedMuffins = 0; //is this necessary as instance var? seems fine as local to OnMuffinClicked();
    [SerializeField]
    private int _muffinsPerClick = 1;

    //ANIMATION
    [SerializeField]
    private float _minSpinSpeed = 0f;
    [SerializeField]
    private float _maxSpinSpeed = 360f;
    [SerializeField]
    private float[] _spinSpeeds;
    [SerializeField]
    private float _pulseSpeed = 1.0f;
    [SerializeField]
    private float _pulseAmplitude = 1.0f;
    [SerializeField]
    private float _pulseOffset = 1.0f;

    [SerializeField]
    private float _xMinRandomMuffin = -150f;
    [SerializeField]
    private float _xMaxRandomMuffin = 180f;
    [SerializeField]
    private float _yMinRandomMuffin = -15f;
    [SerializeField]
    private float _yMaxRandomMuffin = -150f;
    
    //CRITICAL HIT
    [Range(0f, 1f)]
    [SerializeField]
    private float _critChance = 0.01f;
    [SerializeField]
    private int _criticalHitMultiplier = 10;

    //GAME OBJECTS
    [SerializeField]
    private TextMeshProUGUI _totalMuffinsText;
    [SerializeField]
    private Transform[] _spinLights;

    [SerializeField]
    private TextMeshProUGUI _textRewardPrefab;

    private void UpdateTotalMuffins()
    {
        _totalMuffinsText.text = _counter.ToString() + " muffin";
        //make plural if > 1
        if(_counter > 1)
        {
            _totalMuffinsText.text += "s";
        }
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        UpdateTotalMuffins();

        //Init spin speeds method
        _spinSpeeds = new float[_spinLights.Length];
        for(int i = 0; i < _spinSpeeds.Length; i++)
        {
            _spinSpeeds[i] = UnityEngine.Random.Range(_minSpinSpeed, _maxSpinSpeed);
        }

        Debug.Log("Hello, World!");
    }

    // Update is called once per frame
    void Update()
    {
        /* original, non array implementation
        //ROTATE
        Vector3 rotation = new Vector3();
        //rotation.z = 1; //basically creates value of 1 (but as a Z rotation vector)
        rotation.z = _spinSpeed * Time.deltaTime; //now rotating based on time, not frames!
        //actually perform rotation (that's why you can reuse value above)
        _spinLights[0].Rotate(rotation);
        _spinLights[1].Rotate(rotation);
        */

        /*  original, non array implementation
        //PULSE
        float pulse = Mathf.Sin(Time.time * _pulseSpeed) * _pulseAmplitude + _pulseOffset;
        Vector3 pulseScale = new Vector3(pulse, pulse, pulse); //requirement is only x and y, but no overloaded one? doesnt matter since 2D game anyway
        _spinLights[0].localScale = pulseScale;
        _spinLights[1].localScale = pulseScale;
        */

        //array implementation of ROTATE and PULSE
        float pulse = Mathf.Sin(Time.time * _pulseSpeed) * _pulseAmplitude + _pulseOffset;
        Vector3 pulseScale = new Vector3(pulse, pulse, pulse);

        for(int i = 0; i < _spinLights.Length; i++) {
            //ROTATE
            Vector3 rotation = new Vector3(0f, 0f, _spinSpeeds[i] * Time.deltaTime);
            _spinLights[i].Rotate(rotation);

            //PULSE
            _spinLights[i].localScale = pulseScale;
        }

        //Extra Challenge:
        //Debug.Log("Time.time = " + Time.time); //number of milliseconds since launch?
        //Debug.Log("Mathf.Sin(Time.time) = " + Mathf.Sin(Time.time)); //creates sin wave: -1 to +1, starts at 0->1
    }

    public void OnMuffinClicked()
    {
        //ANIMATION
        Vector2 randomVector = MyToolbox.GetRandomVector2(_xMinRandomMuffin, _xMaxRandomMuffin, _yMinRandomMuffin, _yMaxRandomMuffin);
        TextMeshProUGUI textRewardClone = Instantiate(_textRewardPrefab, transform); //using this MuffinContent GameObject with MyFirstScript component attached, so transform comes from that
        textRewardClone.transform.localPosition = randomVector;



        _addedMuffins = _muffinsPerClick;

        //try for critical hit
        float randomNum = UnityEngine.Random.Range(0f, 1f);
        Debug.Log("Random number generated for " + _critChance + " probability = " + randomNum);

        if (randomNum <= _critChance) //doing < because Range is inclusive on upper/lower limits
        {
            _addedMuffins *= _criticalHitMultiplier;
            Debug.Log("Critical hit! Going to add " + _addedMuffins);
        }

        //add original _muffinsPerClick or critical hit
        _counter += _addedMuffins;
        UpdateTotalMuffins();

        //floating + fading text update
        _textRewardPrefab.text = "+" + _addedMuffins;

        Debug.Log("Button Clicked! Counter now at " + _counter);
    }

}
