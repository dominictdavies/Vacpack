using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Vacpack.Content.Items
{ 
	// This is a basic item template.
	// Please see tModLoader's ExampleMod for every other example:
	// https://github.com/tModLoader/tModLoader/tree/stable/ExampleMod
	public class Vacpack : ModItem
	{
		public override void SetStaticDefaults()
		{
			ItemID.Sets.ItemsThatAllowRepeatedRightClick[Item.type] = true;

			// These are all related to gamepad controls and don't seem to affect anything else
			ItemID.Sets.Yoyo[Item.type] = true; // Used to increase the gamepad range when using Strings.
			ItemID.Sets.GamepadExtraRange[Item.type] = 15; // Increases the gamepad range. Some vanilla values: 4 (Wood), 10 (Valor), 13 (Yelets), 18 (The Eye of Cthulhu), 21 (Terrarian).
			ItemID.Sets.GamepadSmartQuickReach[Item.type] = true; // Unused, but weapons that require aiming on the screen are in this set.
		}

		public void SetShootDefaults()
		{
			// Use Properties
			Item.useTime = 30;
			Item.useAnimation = 30;

			// Sound Property
			Item.UseSound = SoundID.Item11;

			// Gun Properties
			Item.channel = false;
			Item.shoot = ProjectileID.Bullet;
			Item.shootSpeed = 10f;
			Item.useAmmo = AmmoID.Gel;
		}

		public void SetSuckDefaults()
		{
			// Use Properties
			Item.useTime = 25;
			Item.useAnimation = 25;

			// Sound Property
			Item.UseSound = SoundID.Item1;

			// Yoyo Properties
			Item.channel = true;
			Item.shoot = ProjectileID.Amarok;
			Item.shootSpeed = 16f;
			Item.useAmmo = AmmoID.None;
		}

		// The Display Name and Tooltip of this item can be edited in the 'Localization/en-US_Mods.Vacpack.hjson' file.
		public override void SetDefaults()
		{
			// Common Properties
			Item.width = 40;
			Item.height = 40;
			Item.value = Item.buyPrice(silver: 1);
			Item.rare = ItemRarityID.Blue;

			// Use Properties
			Item.useStyle = ItemUseStyleID.Shoot;
			Item.autoReuse = true;

			// Weapon Properties
			Item.DamageType = DamageClass.Ranged;
			Item.damage = 50;
			Item.knockBack = 6f;
			Item.noMelee = true;

			SetShootDefaults();
		}

		public override bool AltFunctionUse(Player player) => true;

		public override bool CanUseItem(Player player)
		{
			if (player.altFunctionUse == 2) { SetSuckDefaults(); } else { SetShootDefaults(); }
			return base.CanUseItem(player);
		}

		public override void AddRecipes()
		{
			Recipe recipe = CreateRecipe();
			recipe.AddIngredient(ItemID.DirtBlock, 10);
			recipe.AddTile(TileID.WorkBenches);
			recipe.Register();
		}
	}
}
