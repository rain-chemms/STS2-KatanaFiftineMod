using BaseLib.Abstracts;
using Godot;
using KatanaZeroMod.Fiftine.Extensions;
using MegaCrit.Sts2.Core.Assets;

namespace KatanaZeroMod.Fiftine.Character;

public class FiftineCardPool : CustomCardPoolModel
{
	public override string Title => Fiftine.CharacterId; //This is not a display name.

	public override string BigEnergyIconPath => "Charui/big_energy.png".ImagePath();
	public override string TextEnergyIconPath => "Charui/text_energy.png".ImagePath();

	/* These HSV values will determine the color of your card back.
	They are applied as a shader onto an already colored image,
	so it may take some experimentation to find a color you like.
	Generally they should be values between 0 and 1. */
	public override float H => 0.6f;
	public override float S => 0.5f;
	public override float V => 1.2f;
	public override Color ShaderColor => new Color("0000FF");
	//Alternatively, leave these values at 1 and provide a custom frame image.
	/*public override Texture2D CustomFrame(CustomCardModel card)
	{
		//This will attempt to load Oddmelt/images/cards/frame.png
		return PreloadManager.Cache.GetTexture2D("cards/frame.png".ImagePath());
	}*/

	//Color of small card icons
	public override Color DeckEntryCardColor => new("FFBB33");
	public override Color EnergyOutlineColor => new("651565");

	public override bool IsColorless => false;
}
