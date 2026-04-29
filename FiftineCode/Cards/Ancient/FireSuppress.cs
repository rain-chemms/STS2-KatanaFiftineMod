using BaseLib.Extensions;
using BaseLib.Utils;
using Godot;
using KatanaZeroMod.Fiftine.Powers;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Powers;
using MegaCrit.Sts2.Core.Nodes.Combat;
using MegaCrit.Sts2.Core.ValueProps;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace KatanaZeroMod.Fiftine.Cards;

//火力压制:1伤害14次,升级后1伤害18次,时停X:额外增加X次攻击
public class FireSuppress() : FiftineCard(2,
    CardType.Attack, CardRarity.Ancient,
    TargetType.AnyEnemy)
{
    protected override HashSet<CardTag> CanonicalTags => [CardTag.None];
    protected override IEnumerable<DynamicVar> CanonicalVars => [
        new DamageVar(1, ValueProp.Move),
        new RepeatVar(14)
    ];

    protected override IEnumerable<IHoverTip> ExtraHoverTips => [
        HoverTipFactory.FromPower<ChronosPower>()    
    ];

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        int attackTimes = DynamicVars.Repeat.IntValue;
        //时停X效果编写:
        if (Owner.HasPower<ChronosPower>())
        {
            int nowChronos = Owner.Creature.GetPowerAmount<ChronosPower>();
            if (nowChronos > 0)
            {
                attackTimes += nowChronos;//增加攻击次数
                Owner.Creature.GetPower<ChronosPower>().SetAmount(nowChronos - nowChronos);//扣除相应的柯罗诺斯层数
            }
        }
        await CommonActions.CardAttack(this, cardPlay.Target, attackTimes).Execute(choiceContext);
    }

    protected override void OnUpgrade()
    {
        DynamicVars.Repeat.UpgradeValueBy(4m);
    }
}
