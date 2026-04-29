using BaseLib.Abstracts;
using BaseLib.Extensions;
using BaseLib.Utils;
using Godot;
using KatanaZeroMod.Fiftine;
using KatanaZeroMod.Fiftine.DynamicVars;
using KatanaZeroMod.Fiftine.Powers;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Powers;
using MegaCrit.Sts2.Core.Nodes.Combat;
using MegaCrit.Sts2.Core.ValueProps;
using MegaCrit.Sts2.GameInfo.Objects;
using System.Collections.Generic;
using System.Threading.Tasks;
namespace KatanaZeroMod.Fiftine.Cards;

public class KnockUp() : FiftineCard(1,
    CardType.Attack, CardRarity.Ancient,
    TargetType.AllEnemies)
{
    protected override HashSet<CardTag> CanonicalTags => [CardTag.Strike];
    protected override IEnumerable<DynamicVar> CanonicalVars => [
        new DamageVar(16, ValueProp.Move),
        new PowerVar<VulnerablePower>(3),
        new TimeHaltVar(2)
    ];

    protected override IEnumerable<IHoverTip> ExtraHoverTips => [
        HoverTipFactory.FromPower<ChronosPower>(),
        HoverTipFactory.FromPower<VulnerablePower>()
    ];

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        if (DynamicVars.TryGetValue("FIFTINE-TIMEHALT", out DynamicVar fth))
        {
            int timeHaltNeedChronos = fth.IntValue;//时间效果发动所需要的柯罗诺斯层数
            if (Owner.HasPower<ChronosPower>())
            {
                int nowChronos = Owner.Creature.GetPowerAmount<ChronosPower>();
                if (nowChronos >= timeHaltNeedChronos)
                {
                    //给予场上所有敌人易伤效果
                    foreach (var enemy in Owner.Creature.CombatState!.Enemies) 
                    {
                        await CommonActions.Apply<VulnerablePower>(enemy, this, DynamicVars.Power<VulnerablePower>().IntValue);
                    }
                    Owner.Creature.GetPower<ChronosPower>().SetAmount(nowChronos - timeHaltNeedChronos);//扣除相应的柯罗诺斯层数
                }
            }
        }
        await CommonActions.CardAttack(this, cardPlay.Target).Execute(choiceContext);
    }

    protected override void OnUpgrade()
    {
        DynamicVars.Damage.UpgradeValueBy(8m);
        DynamicVars.Power<VulnerablePower>().UpgradeValueBy(2m);
    }
}
