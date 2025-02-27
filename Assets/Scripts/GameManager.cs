using UnityEngine;

public class GameManager : MonoBehaviour
{    
    private int _totalMuffins = 0;
    private int _muffinsPerClick = 1;

    //GAME OBJECTS
    [SerializeField]
    private Header _header;

    //CRITICAL HIT
    [Range(0f, 1f)]
    [SerializeField]
    private float _critChance = 0.01f;
    [SerializeField]
    private int _criticalHitMultiplier = 10;

    void Start()
    {
        _header.UpdateTotalMuffins(0);
    }

    /// <summary>
    /// Adds muffins to the total muffins.
    /// </summary>
    /// <returns>Returns the added muffins.</returns>
    public int AddMuffins()
    {
         int addedMuffins = _muffinsPerClick;

        //try for critical hit
        float randomNum = UnityEngine.Random.Range(0f, 1f);
        //Debug.Log("Random number generated for " + _critChance + " probability = " + randomNum);

        if (randomNum <= _critChance) //doing < because Range is inclusive on upper/lower limits
        {
            addedMuffins *= _criticalHitMultiplier;
            Debug.Log("Critical hit! Going to add " + addedMuffins);
        }

        //add original _muffinsPerClick or critical hit
        _totalMuffins += addedMuffins;
        _header.UpdateTotalMuffins(_totalMuffins);

        return addedMuffins;
    }
}
