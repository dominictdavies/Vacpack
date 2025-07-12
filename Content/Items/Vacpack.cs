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
		// The Display Name and Tooltip of this item can be edited in the 'Localization/en-US_Mods.Vacpack.hjson' file.
		public override void SetDefaults()
		{
			// Common Properties
			Item.width = 40;
			Item.height = 40;
			Item.value = Item.buyPrice(silver: 1);
			Item.rare = ItemRarityID.Blue;

			// Use Properties
			Item.useTime = 2;
			Item.useAnimation = 2;
			Item.useStyle = ItemUseStyleID.Shoot;
			Item.autoReuse = true;

			// Sound Property
			Item.UseSound = SoundID.Item1;

			// Weapon Properties
			Item.DamageType = DamageClass.Ranged;
			Item.damage = 50;
			Item.knockBack = 6f;
			Item.noMelee = true;

			// Gun Properties
			Item.shoot = ProjectileID.Bullet;
			Item.shootSpeed = 10f;
			Item.useAmmo = AmmoID.Gel;
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
