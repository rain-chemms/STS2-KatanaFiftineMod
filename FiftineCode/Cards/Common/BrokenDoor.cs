using BaseLib.Extensions;
using BaseLib.Utils;
using Godot;
using KatanaZeroMod.Fiftine.Cards;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Potions;
using MegaCrit.Sts2.Core.Models.Powers;
using MegaCrit.Sts2.Core.Nodes.Combat;
using MegaCrit.Sts2.Core.ValueProps;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace KatanaZeroMod.Fiftine.Cards;

//破门而出:技能牌,给予自身3点活力,升级后给予自身6点活力
public class BrokenDoor() : FiftineCard(0,
	CardType.Skill, CardRarity.Common,
	TargetType.Self)
{
	protected override HashSet<CardTag> CanonicalTags => [CardTag.None];
	protected override IEnumerable<DynamicVar> CanonicalVars => [
		new PowerVar<VigorPower>(3)
	];

	protected override IEnumerable<IHoverTip> ExtraHoverTips => [
		HoverTipFactory.FromPower<VigorPower>()
	];

	protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
	{
		await CommonActions.Apply<VigorPower>(Owner.Creature, this);
	}

	protected override void OnUpgrade()
	{
		DynamicVars.Power<VigorPower>().UpgradeValueBy(3);
	}
}
