using System;
using System.Threading;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    private int _totalMuffins;
    private int _muffinsPerClick = 1;
    private int _muffinsPerSecond;
    private float _muffinsPerSecondTimer;

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
        _muffinsPerSecondTimer = 0f;
    }

    void Update()
    {
        //timer for muffins per second upgrade
        _muffinsPerSecondTimer += Time.deltaTime;
        if (_muffinsPerSecondTimer >= 1)
        {
            _muffinsPerSecondTimer--;
            TotalMuffins += MuffinsPerSecond;
        }
    }

    public bool TryPurchaseUpgrade(int currentCost, int level, UpgradeType upgradeType)
    {
        if (TotalMuffins >= currentCost)
        {
            //handle general things for all upgrades:
            TotalMuffins -= currentCost;
            level++;

            //handle upgrade specific things:
            switch (upgradeType)
            {
                case UpgradeType.MuffinUpgrade:
                    _muffinsPerClick = 1 + level * 2;
                    break;
                case UpgradeType.SugarRushUpgrade:
                    MuffinsPerSecond = level;
                    break;
                case UpgradeType.FancyMuffinUpgrade:
                    ///TODO: do fancy muffin thing here
                    break;
            }
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
