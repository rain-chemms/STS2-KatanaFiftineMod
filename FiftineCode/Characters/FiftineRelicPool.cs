using BaseLib.Abstracts;
using Godot;
using KatanaZeroMod.Fiftine.Character;
using System;

namespace KatanaZeroMod.Fiftine.Character;

public partial class FiftineRelicPool : CustomRelicPoolModel
{
	public override string EnergyColorName => Fiftine.CharacterId;
    public override Color LabOutlineColor => Fiftine.Color;
}
