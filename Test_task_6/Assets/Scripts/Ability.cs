public class Ability
{
    private int _powerLevel;
    private int _speedLevel;
    private int _healthLevel;
    private int _power;
    private float _speed;
    private int _health;

    public Ability()
    {
        _powerLevel = 1;
        _speedLevel = 1;
        _healthLevel = 1;
        _power = 10;
        _speed = 1.0f;
        _health = 110;
    }

    public void AddPower()
    {
        _powerLevel++;
        _power = 10 + _powerLevel * 2;
    }

    public void AddSpeed()
    {
        _speedLevel++;
        _speed = 1.0f + _speedLevel * 0.05f;
    }

    public void AddHealth()
    {
        _healthLevel++;
        _health = 110 + _healthLevel * 10;
    }

    public int GetPowerLevel()
    {
        return _powerLevel;
    }

    public int GetSpeedLevel()
    {
        return _speedLevel;
    }

    public int GetHealthLevel()
    {
        return _healthLevel;
    }

    public int GetPowerValue()
    {
        return _power;
    }

    public float GetSpeedValue()
    {
        return _speed;
    }

    public int GetHealthValue()
    {
        return _health;
    }
}