using UnityEngine;
using UnityEngine.UI;

public class VolumeController : MonoBehaviour
{
    [SerializeField]
    private Slider _volumeSlider;

    public void OnVolumeChanged()
    {
        Debug.Log("Volume changed!");
        AudioListener.volume = _volumeSlider.value;
    }
}
