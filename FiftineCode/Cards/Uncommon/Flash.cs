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

public class Flash() : FiftineCard(1,
    CardType.Skill, CardRarity.Basic,
    TargetType.AnyEnemy)
{
    protected override HashSet<CardTag> CanonicalTags => [CardTag.None];
    protected override IEnumerable<DynamicVar> CanonicalVars => [
        new BlockVar(4, ValueProp.Move),
        new CardsVar(2),
        new TimeHaltVar(3)
    ];

    protected override IEnumerable<IHoverTip> ExtraHoverTips => [
        HoverTipFactory.FromPower<ChronosPower>()
    ];

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        await CommonActions.CardBlock(this,cardPlay);
        if (DynamicVars.TryGetValue("FIFTINE-TIMEHALT", out DynamicVar fth))
        {
            int timeHaltNeedChronos = fth.IntValue;//时间效果发动所需要的柯罗诺斯层数
            if (Owner.HasPower<ChronosPower>())
            {
                int nowChronos = Owner.Creature.GetPowerAmount<ChronosPower>();
                if (nowChronos >= timeHaltNeedChronos)
                {
                    await CommonActions.Draw(this, choiceContext);//抽牌
                    Owner.Creature.GetPower<ChronosPower>().SetAmount(nowChronos - timeHaltNeedChronos);//扣除相应的柯罗诺斯层数
                }
            }
        }
    }

    protected override void OnUpgrade()
    {
        DynamicVars.Block.UpgradeValueBy(3m);
        DynamicVars.Cards.UpgradeValueBy(1m);
    }
}
