using BaseLib.Extensions;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Powers;
using System.Collections.Generic;
using System.Threading.Tasks;
using KatanaZeroMod.Fiftine.DynamicVars;
using KatanaZeroMod.Fiftine.Powers;

namespace KatanaZeroMod.Fiftine.Cards;

public class FiftineBulletTime() : FiftineCard(0,
    CardType.Skill, CardRarity.Token,
    TargetType.Self)
{
    protected override HashSet<CardTag> CanonicalTags => [CardTag.None];
    protected override IEnumerable<DynamicVar> CanonicalVars => [
        new TimeHaltVar(3)
    ];

    public override HashSet<CardKeyword> CanonicalKeywords => new HashSet<CardKeyword> {
        CardKeyword.Retain,
        CardKeyword.Innate
    };

    protected override IEnumerable<IHoverTip> ExtraHoverTips => [
        HoverTipFactory.FromPower<ChronosPower>()
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
                    //动画播放
                    await CreatureCmd.TriggerAnim(base.Owner.Creature, "Cast", base.Owner.Character.CastAnimDelay);
                    //降费
                    foreach (CardModel card in PileType.Hand.GetPile(base.Owner).Cards)
                    {
                        if (!card.EnergyCost.CostsX)
                        {
                            card.SetToFreeThisTurn();
                        }
                    }
                    //无法抽牌
                    await PowerCmd.Apply<NoDrawPower>(base.Owner.Creature, 1m, base.Owner.Creature, this);
                    Owner.Creature.GetPower<ChronosPower>().SetAmount(nowChronos - timeHaltNeedChronos);//扣除相应的柯罗诺斯层数
                }
            }
        }
    }

    protected override PileType GetResultPileType()
    {
        PileType resultPileType = base.GetResultPileType();
        if (resultPileType != PileType.Discard)
        {
            return resultPileType;
        }
        return PileType.Hand;
    }

    protected override void OnUpgrade()
    {
        //降低柯诺罗斯的使用量
        if(DynamicVars.TryGetValue("FIFTINE-TIMEHALT", out DynamicVar fth))
        {
            fth.UpgradeValueBy(-1m);
        }
    }
}
