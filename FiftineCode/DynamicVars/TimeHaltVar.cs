using BaseLib.Extensions;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Localization.DynamicVars;

namespace KatanaZeroMod.Fiftine.DynamicVars;

public class TimeHaltVar : DynamicVar
{
    // 在描述中用作占位符的键，推荐添加前缀避免撞车
    public const string Key = "FIFTINE-TIMEHALT";
    // 本地化键，这里设置为大写的Key，也就是"FIFTINE-TIMEHALT"
    public static readonly string LocKey = Key.ToUpperInvariant();

    public TimeHaltVar(decimal baseValue) : base(Key, baseValue)
    {
        this.WithTooltip(LocKey);
    }
}