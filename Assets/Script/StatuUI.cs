using UnityEngine;
using UnityEngine.UI;

public class StatuUI : MonoBehaviour {
    [SerializeField]
    private Slider hpBar;
    [SerializeField]
    private Text hpText;
    [SerializeField]
    private Text attackText;
    [SerializeField]
    private Image icon;
    /// <summary>
    /// UIの更新
    /// </summary>
    /// <param name="maxHp"></param>
    /// <param name="nowHp"></param>
    public void HPUIChange(float maxHp, float nowHp)
    {
        BarUpdate(maxHp,nowHp);
        HPTextUpdate(maxHp, nowHp);
    }
    public void AttackPowerUIChange(float attckPower)
    {
        AttackTextUpdate(attckPower);
    }
    public void BarUpdate(float maxHp,float nowHp)
    {
        hpBar.maxValue = maxHp;
        hpBar.value = nowHp;
    }
    public void HPTextUpdate(float maxHp, float nowHp)
    {
        hpText.text = string.Format("{0}/{1}", nowHp, maxHp);
    }
    public void AttackTextUpdate(float attackPower)
    {
        attackText.text = string.Format("{0}",attackPower);
    }
    public void ImageUpdate()
    {
    }
}
