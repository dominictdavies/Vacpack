using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace Vacpack.Content.Projectiles
{
	public class Tornado : ModProjectile
	{
		// The following code was adapted from ExampleMod's ExampleDrill

		public override void SetStaticDefaults()
		{
			// Prevents jitter when stepping up and down blocks and half blocks
			ProjectileID.Sets.HeldProjDoesNotUsePlayerGfxOffY[Type] = true;
		}

		public override void SetDefaults()
		{
			Projectile.width = 128;
			Projectile.height = 128;
			Projectile.tileCollide = false;
			Projectile.penetrate = -1;
			Projectile.DamageType = DamageClass.Ranged;
			Projectile.ownerHitCheck = true;
			Projectile.hide = true; // Hides the projectile, so it will draw in the player's hand when we set the player's heldProj to this one.
		}

		public void DrawTornado()
		{
			Player player = Main.player[Projectile.owner];

			Projectile.timeLeft = 60;

			// Animation code could go here if the projectile was animated.

			// Plays a sound every 20 ticks. In aiStyle 20, soundDelay is set to 30 ticks.
			if (Projectile.soundDelay <= 0) {
				SoundEngine.PlaySound(SoundID.Item22, Projectile.Center);
				Projectile.soundDelay = 20;
			}

			Vector2 playerCenter = player.RotatedRelativePoint(player.MountedCenter);
			if (Main.myPlayer == Projectile.owner) {
				// This code must only be ran on the client of the projectile owner
				if (Main.mouseRight == true) {
					float holdoutDistance = player.HeldItem.shootSpeed * Projectile.scale;
					// Calculate a normalized vector from player to mouse and multiply by holdoutDistance to determine resulting holdoutOffset
					Vector2 holdoutOffset = holdoutDistance * Vector2.Normalize(Main.MouseWorld - playerCenter);
					if (holdoutOffset.X != Projectile.velocity.X || holdoutOffset.Y != Projectile.velocity.Y) {
						// This will sync the projectile, most importantly, the velocity.
						Projectile.netUpdate = true;
					}

					// Projectile.velocity acts as a holdoutOffset for held projectiles.
					Projectile.velocity = holdoutOffset;
				} else {
					Projectile.Kill();
				}
			}

			if (Projectile.velocity.X > 0f) {
				player.ChangeDir(1);
			} else if (Projectile.velocity.X < 0f) {
				player.ChangeDir(-1);
			}

			Projectile.spriteDirection = Projectile.direction;
			player.ChangeDir(Projectile.direction); // Change the player's direction based on the projectile's own
			player.heldProj = Projectile.whoAmI; // We tell the player that the drill is the held projectile, so it will draw in their hand
			player.SetDummyItemTime(2); // Make sure the player's item time does not change while the projectile is out
			Projectile.Center = playerCenter; // Centers the projectile on the player. Projectile.velocity will be added to this in later Terraria code causing the projectile to be held away from the player at a set distance.
			Projectile.rotation = Projectile.velocity.ToRotation() + MathHelper.PiOver2;
			player.itemRotation = (Projectile.velocity * Projectile.direction).ToRotation();
		}

		public void MoveEnemies()
		{
			foreach (NPC npc in Main.ActiveNPCs) {
				Rectangle currentTornadoHitbox = new((int)(Projectile.Hitbox.X + Projectile.velocity.X), (int)(Projectile.Hitbox.Y + Projectile.velocity.Y), Projectile.width, Projectile.height);
				if (currentTornadoHitbox.Intersects(npc.Hitbox)) {
					npc.velocity -= Projectile.velocity / 256f;
				}
			}
		}

		public override void AI()
		{
			DrawTornado();
			MoveEnemies();
		}
	}
}
