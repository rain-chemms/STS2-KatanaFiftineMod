using BaseLib.Extensions;
using BaseLib.Utils;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models.Powers;
using MegaCrit.Sts2.Core.ValueProps;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace KatanaZeroMod.Fiftine.Cards;

public class FlashDash() : FiftineCard(1,
	CardType.Attack, CardRarity.Common,
	TargetType.AllEnemies)
{
	protected override HashSet<CardTag> CanonicalTags => [CardTag.None];
	protected override IEnumerable<DynamicVar> CanonicalVars => [
		new DamageVar(8, ValueProp.Move),
	];

	protected override IEnumerable<IHoverTip> ExtraHoverTips => [
	];

	protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
	{
		await CommonActions.CardAttack(this, cardPlay.Target).Execute(choiceContext);
	}

	protected override void OnUpgrade()
	{
		DynamicVars.Damage.UpgradeValueBy(3m);
	}
}
