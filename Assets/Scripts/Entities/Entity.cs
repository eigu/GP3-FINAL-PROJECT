using System;
using UnityEngine;

public class Entity : MonoBehaviour
{
    #region HP
    [SerializeField, Range(0, 100)] protected int _maxHP;
    [SerializeField] protected int _currentHP;
    protected int _update; public int update; // edit later

public int CurrentHP
    {
        get { return _maxHP; }
        set { _currentHP = update; }
    }
    #endregion

    #region STA
    [SerializeField, Range(0, 100)] protected int _maxSTA;
    [SerializeField] protected int _currentSTA;
    public int CurrentSTA
    {
        get { return _maxSTA; }
        set { _currentSTA = value; }
    }
    #endregion

    #region ATK
    [SerializeField] protected int _maxATK;
    [SerializeField] protected int _currentATK;
    public int CurrentATK
    {
        get { return _maxATK; }
        set { _currentATK = value; }
    }
    #endregion

    #region DEF
    [SerializeField] protected int _maxDEF;
    [SerializeField] protected int _currentDEF;
    public int CurrentDEF
    {
        get { return _maxDEF; }
        set { _currentDEF = value; }
    }
    #endregion

    public int GetDamageFactor()
    {
        //if conditions for equpped
        return CurrentATK;
    }

    public int GetDefenseFactor()
    {
        //if conditions for equpped
        return CurrentDEF;
    }

    private void Awake()
    {
        update = CurrentHP;
    }
    public void TakeDamage(int damage)
    {
        CurrentHP -= damage;
        update = CurrentHP - damage;
       
    }

  

 
}
