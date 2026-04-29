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

public class PeriodOfTreatment() : FiftineCard(2,
    CardType.Power, CardRarity.Rare,
    TargetType.Self)
{
    protected override HashSet<CardTag> CanonicalTags => [CardTag.Strike];
    protected override IEnumerable<DynamicVar> CanonicalVars => [
        new PowerVar<ChronosPower>(2),
        new PowerVar<TreatmentPower>(2),
    ];

    protected override IEnumerable<IHoverTip> ExtraHoverTips => [
        HoverTipFactory.FromPower<ChronosPower>(),
        HoverTipFactory.FromPower<TreatmentPower>()
    ];

    protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
    {
        await CommonActions.Apply<ChronosPower>(Owner.Creature, this);
        await CommonActions.Apply<TreatmentPower>(Owner.Creature, this);
    }

    protected override void OnUpgrade()
    {
        DynamicVars.Power<ChronosPower>().UpgradeValueBy(1m);
        DynamicVars.Power<TreatmentPower>().UpgradeValueBy(1m);
    }
}
