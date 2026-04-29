using BaseLib.Abstracts;
using BaseLib.Utils;
using Godot;
using KatanaZeroMod.Fiftine.Cards;
using KatanaZeroMod.Fiftine.Extensions;
using KatanaZeroMod.Fiftine.Relics;
using MegaCrit.Sts2.Core.Entities.Characters;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.CardPools;
using MegaCrit.Sts2.Core.Models.Cards;
using MegaCrit.Sts2.Core.Models.PotionPools;
using MegaCrit.Sts2.Core.Models.RelicPools;
using MegaCrit.Sts2.Core.Models.Relics;
using System;
using System.Collections.Generic;
using System.Reflection.Metadata;
using static KatanaZeroMod.Fiftine.Character.Fiftine;

namespace KatanaZeroMod.Fiftine.Character;

public class Fiftine : PlaceholderCharacterModel
{
	public const string CharacterId = "Fiftine";
    // 能量图标轮廓颜色
    public override Color EnergyLabelOutlineColor => new(255/255, 170/255, 43/255);
    //角色音效
    //public override string CharacterSelectSfx => "res://Fiftine/Sfx/fiftine_select.ogg";
    public override string CustomAttackSfx => "res://Fiftine/Sfx/AttackSfx.tres";
    //public override string CustomCastSfx => "res://Fiftine/Sfx/fiftine_cast.wav";
    //public override string CustomDeathSfx => "res://Fiftine/Sfx/fiftine_die.tscn";
    //过渡音效
    public override string CharacterTransitionSfx => "event:/sfx/ui/wipe_ironclad";
    //角色选择背景

    public override string CustomCharacterSelectBg => "res://Fiftine/Scenes/Char_Select/char_select_bg_fiftine.tscn";//背景场景,要自己做

	public override string PlaceholderID => "necrobinder";

	public static readonly Color Color = new Color("FCFF3D");

	public override Color NameColor => Color;
	public override CharacterGender Gender => CharacterGender.Masculine;//Feminine
	public override int StartingHp => 45;

	public override IEnumerable<CardModel> StartingDeck => [
		ModelDb.Card<FiftineAttack>(),
		ModelDb.Card<FiftineAttack>(),
		ModelDb.Card<FiftineAttack>(),
		ModelDb.Card<FiftineAttack>(),
		ModelDb.Card<FiftineBlock>(),
		ModelDb.Card<FiftineBlock>(),
		ModelDb.Card<FiftineBlock>(),
		ModelDb.Card<FiftineBlock>(),
		ModelDb.Card<AbruptAttack>(),
		ModelDb.Card<TakeCare>()
		//ModelDb.Card<FiftineBulletTime>()
    ];

	public override IReadOnlyList<RelicModel> StartingRelics => [ModelDb.Relic<FiftineKatana>()];

	public override CardPoolModel CardPool => ModelDb.CardPool<FiftineCardPool>();
	public override RelicPoolModel RelicPool => ModelDb.RelicPool<FiftineRelicPool>();
	public override PotionPoolModel PotionPool => ModelDb.PotionPool<FiftinePotionPool/*SharedPotionPool*/>();

	/*  PlaceholderCharacterModel will utilize placeholder basegame assets for most of your character assets until you
		override all the other methods that define those assets.
		These are just some of the simplest assets, given some placeholders to differentiate your character with.
		You don't have to, but you're suggested to rename these images. */
	public override string CustomVisualPath => "res://Fiftine/Scenes/FiftineVisual.tscn";//角色视图路径
	public override string CustomIconTexturePath => "character_icon_fiftine.png".CharacterUiPath();
	public override string CustomCharacterSelectIconPath => "char_select_char_name.png".CharacterUiPath();
    public override string CustomCharacterSelectLockedIconPath => "char_select_char_name_locked.png".CharacterUiPath();
	public override string CustomMapMarkerPath => "map_marker_char_name.png".CharacterUiPath();
    public override string CustomEnergyCounterPath => "res://Fiftine/Scenes/FiftineEnergyCounter.tscn";    
}
