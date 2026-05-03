using BaseLib.Extensions;
using BaseLib.Utils;
using KatanaZeroMod.Fiftine.Powers;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.ValueProps;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace KatanaZeroMod.Fiftine.Cards;

public class OverDose() : FiftineCard(2,
    CardType.Skill, CardRarity.Rare,
    TargetType.Self)
{
    public override HashSet<CardKeyword> CanonicalKeywords => new HashSet<CardKeyword> {
        CardKeyword.Exhaust
    };

    protected override HashSet<CardTag> CanonicalTags => [CardTag.None];
    protected override IEnumerable<DynamicVar> CanonicalVars => [
            new PowerVar<ChronosPower>(12)
    ];
    
    protected override IEnumerable<IHoverTip> ExtraHoverTips => [
        HoverTipFactory.FromPower<ChronosPower>()
    ];

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        await CommonActions.Apply<ChronosPower>(Owner.Creature, this);
    }

    protected override void OnUpgrade()
    {
        DynamicVars.Power<ChronosPower>().UpgradeValueBy(6m);
    }
}
