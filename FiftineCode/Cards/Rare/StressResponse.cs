using BaseLib.Extensions;
using BaseLib.Utils;
using Godot;
using KatanaZeroMod.Fiftine;
using KatanaZeroMod.Fiftine.DynamicVars;
using KatanaZeroMod.Fiftine.Powers;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.Entities.Creatures;
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

public class StressResponse() : FiftineCard(1,
    CardType.Skill, CardRarity.Rare,
    TargetType.Self)
{
    public override HashSet<CardKeyword> CanonicalKeywords => new HashSet<CardKeyword> {
        CardKeyword.Exhaust
    };
    protected override HashSet<CardTag> CanonicalTags => [CardTag.Strike];
    protected override IEnumerable<DynamicVar> CanonicalVars => [
        new PowerVar<RegenPower>(5),
        new TimeHaltVar(5)
    ];

    protected override IEnumerable<IHoverTip> ExtraHoverTips => [
        HoverTipFactory.FromPower<ChronosPower>(),
        HoverTipFactory.FromPower<RegenPower>()
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
                    //获得再生效果
                    Owner.Creature.GetPower<ChronosPower>().SetAmount(Owner.Creature.GetPowerAmount<RegenPower>() + DynamicVars.Power<RegenPower>().IntValue);
                    Owner.Creature.GetPower<ChronosPower>().SetAmount(nowChronos - timeHaltNeedChronos);//扣除相应的柯罗诺斯层数
                }
            }
        }
    }

    protected override void OnUpgrade()
    {
        CanonicalKeywords.Remove(CardKeyword.Exhaust);
    }
}
