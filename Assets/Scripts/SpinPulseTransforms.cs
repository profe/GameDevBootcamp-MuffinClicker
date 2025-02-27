using UnityEngine;

public class SpinPulseTransforms : MonoBehaviour
{
    //GAME OBJECTS
    [SerializeField]
    private Transform[] _spinLights;

    //ANIMATION
    [SerializeField]
    private float[] _spinSpeeds;

    [SerializeField]
    private float _minSpinSpeed = 0f, _maxSpinSpeed = 360f;
    [SerializeField]
    private float _pulseSpeed = 1.0f, _pulseAmplitude = 1.0f, _pulseOffset = 1.0f;

    void Start()
    {
        //Init spin speeds method
        _spinSpeeds = new float[_spinLights.Length];
        for(int i = 0; i < _spinSpeeds.Length; i++)
        {
            _spinSpeeds[i] = UnityEngine.Random.Range(_minSpinSpeed, _maxSpinSpeed);
        }
    }

    void Update()
    {
        float pulse = Mathf.Sin(Time.time * _pulseSpeed) * _pulseAmplitude + _pulseOffset;
        Vector3 pulseScale = new Vector3(pulse, pulse, pulse);

        for(int i = 0; i < _spinLights.Length; i++) {
            //ROTATE
            Vector3 rotation = new Vector3(0f, 0f, _spinSpeeds[i] * Time.deltaTime);
            _spinLights[i].Rotate(rotation);

            //PULSE
            _spinLights[i].localScale = pulseScale;
        }
        
    }
}
