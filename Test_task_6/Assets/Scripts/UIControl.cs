using TMPro;

public class UIControl
{
    private readonly TMP_Text _allCoins;
    private readonly TMP_Text _powerLevel;
    private readonly TMP_Text _speedLevel;
    private readonly TMP_Text _healthLevel;
    private readonly TMP_Text _powerValue;
    private readonly TMP_Text _speedValue;
    private readonly TMP_Text _healthValue;
    private readonly TMP_Text _powerCost;
    private readonly TMP_Text _speedCost;
    private readonly TMP_Text _healthCost;

    public UIControl(TMP_Text allCoins, TMP_Text powerLevel, TMP_Text speedLevel, TMP_Text healthLevel, TMP_Text powerValue, TMP_Text speedValue, TMP_Text healthValue, TMP_Text powerCost, TMP_Text speedCost, TMP_Text healthCost)
    {
        _allCoins = allCoins;
        _powerLevel = powerLevel;
        _speedLevel = speedLevel;
        _healthLevel = healthLevel;
        _powerValue = powerValue;
        _speedValue = speedValue;
        _healthValue = healthValue;
        _powerCost = powerCost;
        _speedCost = speedCost;
        _healthCost = healthCost;
    }

    public void ChangeCoins(int value)
    {
        _allCoins.text = value.ToString();
    }

    public void ChangePower(int level, int value, int cost)
    {
        _powerLevel.text = "Lv " + level;
        _powerValue.text = value.ToString();
        _powerCost.text = cost.ToString();
    }

    public void ChangeSpeed(int level, float value, int cost)
    {
        _speedLevel.text = "Lv " + level;
        _speedValue.text = value.ToString();
        _speedCost.text = cost.ToString();
    }

    public void ChangeHealth(int level, int value, int cost)
    {
        _healthLevel.text = "Lv " + level;
        _healthValue.text = value.ToString();
        _healthCost.text = cost.ToString();
    }
}