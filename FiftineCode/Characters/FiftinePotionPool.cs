
using BaseLib.Abstracts;
using BaseLib.Patches.UI;
using Godot;
using KatanaZeroMod.Fiftine.Character;
using KatanaZeroMod.Fiftine.Extensions;
using MegaCrit.Sts2.Core.Models.PotionPools;
using System;

namespace KatanaZeroMod.Fiftine.Character;

public partial class FiftinePotionPool : CustomPotionPoolModel
{

    public override bool IsShared => false;
    public override string? BigEnergyIconPath => "Charui/big_energy.png".ImagePath();
    public override string? TextEnergyIconPath => "Charui/text_energy.png".ImagePath();

    public FiftinePotionPool() : base()
    {}
}