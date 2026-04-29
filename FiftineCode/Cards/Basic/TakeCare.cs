using BaseLib.Utils;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.ValueProps;
using System.Collections.Generic;
using System.Threading.Tasks;
using KatanaZeroMod.Fiftine.Powers;
using BaseLib.Extensions;

namespace KatanaZeroMod.Fiftine.Cards;

public class TakeCare() : FiftineCard(1,
    CardType.Skill, CardRarity.Basic,
    TargetType.Self)
{
    protected override HashSet<CardTag> CanonicalTags => [CardTag.None];
    protected override IEnumerable<DynamicVar> CanonicalVars => [new PowerVar<ChronosPower>(2)];

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        await CommonActions.Apply<ChronosPower>(Owner.Creature,this);
    }

    protected override void OnUpgrade()
    {
        DynamicVars.Power<ChronosPower>().UpgradeValueBy(1m);
    }
}
