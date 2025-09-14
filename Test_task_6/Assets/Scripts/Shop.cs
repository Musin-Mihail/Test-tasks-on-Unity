public class Shop
{
    private readonly Coins _coins;
    private readonly Ability _ability;
    private readonly UIControl _uiControl;
    private int _powerCost;
    private int _speedCons;
    private int _healthCost;

    public Shop(UIControl uiControl, Coins coins, Ability ability)
    {
        _powerCost = 11;
        _speedCons = 23;
        _healthCost = 7;
        _uiControl = uiControl;
        _ability = ability;
        _coins = coins;
    }

    public void BuyPower()
    {
        if (_coins.RemovedCoins(_powerCost))
        {
            _powerCost += (int)(_powerCost * 0.1f + 1);
            _ability.AddPower();
            _uiControl.ChangePower(_ability.GetPowerLevel(), _ability.GetPowerValue(), _powerCost);
            _uiControl.ChangeCoins(_coins.GetCoins());
        }
    }

    public void BuySpeed()
    {
        if (_coins.RemovedCoins(_speedCons))
        {
            _speedCons += (int)(_speedCons * 0.1f + 1);
            _ability.AddSpeed();
            _uiControl.ChangeSpeed(_ability.GetSpeedLevel(), _ability.GetSpeedValue(), _speedCons);
            _uiControl.ChangeCoins(_coins.GetCoins());
        }
    }

    public void BuyHealth()
    {
        if (_coins.RemovedCoins(_healthCost))
        {
            _healthCost += (int)(_healthCost * 0.1f + 1);
            _ability.AddHealth();
            _uiControl.ChangeHealth(_ability.GetHealthLevel(), _ability.GetHealthValue(), _healthCost);
            _uiControl.ChangeCoins(_coins.GetCoins());
        }
    }
}