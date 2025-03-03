using System;
using System.Threading;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    private int _totalMuffins;
    private int _muffinsPerClick = 1;
    [SerializeField] private int _muffinsPerSecond;
    private float _timer;

    //EVENTS
    public UnityEvent<int> OnTotalMuffinsChanged;
    public UnityEvent<int> OnMuffinsPerSecondChanged;

    //CRITICAL HIT
    [Range(0f, 1f)]
    [SerializeField]
    private float _critChance = 0.01f;
    [SerializeField]
    private int _criticalHitMultiplier = 10;

    //PROPERTIES
    private int TotalMuffins
    {
        get
        {
            return _totalMuffins;
        }
        set
        {
            _totalMuffins = value;
            OnTotalMuffinsChanged.Invoke(_totalMuffins);
        }
    }

    private int MuffinsPerSecond
    {
        get
        {
            return _muffinsPerSecond;
        }
        set
        {
            _muffinsPerSecond = value;
            OnMuffinsPerSecondChanged.Invoke(_muffinsPerSecond);
        }
    }


    //METHODS

    void Start()
    {
        TotalMuffins = 0;
        //init for muffins per second upgrade
        MuffinsPerSecond = 0;
        _timer = 0f;
    }

    void Update()
    {
        //timer for muffins per second upgrade
        _timer += Time.deltaTime;
        if (_timer >= 1.0f)
        {
            _timer -= 1.0f;
            TotalMuffins += MuffinsPerSecond;
        }
    }


    public bool TryMuffinPurchaseUpgrade(int currentCost, int level)
    {
        if (TotalMuffins >= currentCost)
        {
            TotalMuffins -= currentCost;
            level++;
            _muffinsPerClick = 1 + level * 2;
            return true;
        }
        else
        {
            return false;
        }
    }

    public bool TrySugarRushPurchaseUpgrade(int currentCost, int level)
    {
        if (TotalMuffins >= currentCost)
        {
            TotalMuffins -= currentCost;
            level++;
            MuffinsPerSecond++; //or some other calc based on level?
            //_muffinsPerClick = 1 + level * 2;
            return true;
        }
        else
        {
            return false;
        }
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
        TotalMuffins += addedMuffins;

        return addedMuffins;
    }
}
