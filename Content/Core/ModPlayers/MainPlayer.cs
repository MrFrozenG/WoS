using Terraria.ID;
using Terraria;
using Terraria.ModLoader;
using WoS.Content.Buffs.Damage;
using WoS.Content.Core.CritSystem;
using WoS.Content.Core.ModSets;
using WoS.Content.Items.Armor.Pearlescent;
using WoS.Content.Core.DamageClasses;
using WoS.Content.Buffs.Weakness;
using WoS.Content.Items.Accessories.Universal;
using WoS.Content.Items.Accessories.Expert;

namespace WoS.Content.Core.ModPlayers
{
    public class MainPlayer : ModPlayer
    {
        /// <summary>
        /// Значение 1% от максимального запаса Здоровья игрока (Максимальное значение Здоровья / 100)
        /// </summary>
        public int PercLife => Player.statLifeMax2 / 100;
        /// <summary>
        /// Значение 1% от максимального запаса Маны игрока (Максимальное значение Маны / 100)
        /// </summary>
        public int PercMana => Player.statManaMax2 / 100;
        /// <summary>Плоский бонус к Support Points от аксессуаров.</summary>
        public int SupportPointsBonus = 0;
        /// <summary>Множитель Support Points (1f = без изменений).</summary>
        public float SupportPointsBonus2 = 1f;
        /// <summary>Плоский бонус к критическому урону от аксессуаров и эффектов.</summary>
        public float BonusCritDamageFlat;
        /// <summary>Множитель критического урона (1.0 = без изменений).</summary>
        public float BonusCritDamageMult = 1f;
        /// <summary>Итоговый бонус крит. урона после всех модификаторов. Не изменять.</summary>
        public float CritDamageBonus => (BonusCritDamageFlat) * BonusCritDamageMult;
        //Weapon
        public int DuneSerpentFangsHits;

        //Cooldowns, Statutes
        public int BloodlustHealCD;

        //Armor Sets
        public bool MeteorSet;
        public bool PearlescentSet;

        //Common Accessories
        public bool HoVHairpin;
        public bool DryadsFlower;
        public bool HunterBoots;
        public bool HunterGloves;

        //Unique Accessories
        public Item RoyalGelType;
        public Item RingOfFlamesType;
        public Item AnkhType;
        public Item WormScarfType;
        public Item HeartOfSinType;
        public float HeartofSinCrit;
        public Item AutoUseRanged;
        public Item AutoUseMelee;
        public Item AutoUseMagic;
        public Item QuickUseSummon;
        public int GetModifiedSupportPoints(int baseValue)
        {
            return (int)((baseValue + SupportPointsBonus) * SupportPointsBonus2);
        }
        public override void ResetEffects()
        {
            ResetAccessories();
            ResetAccessoriesGroups();
            ResetSupport();
            ResetCrit();
            ResetArmorSets();
        }
        public override void PreUpdate()
        {
            UpdateCooldowns();
            UpdateTimers();
        }
        public override void UpdateEquips()
        {
            CheckUniqueAccessories();
            UniqueAccessories();
            CommonAccessories();
            ApplyCritAccessoryBonuses(Main.LocalPlayer);
            GrantEnemiesImmunity(Player);
            CheckArmorSets(Player);
            GrandArmorSetsBonus(Player);
        }
        public override bool? CanAutoReuseItem(Item item)
        {
            if (item == null || item.IsAir)
                return null;

            // Если есть аксессуар для автоатаки соответствующего типа
            if (AutoUseMelee != null && item.CountsAsClass(DamageClass.Melee))
                return true;
            if (AutoUseRanged != null && item.CountsAsClass(DamageClass.Ranged))
            {
                // Специально для HunterGloves
                if (AutoUseRanged.type == ModContent.ItemType<HunterGlove>())
                {
                    if (item.useAmmo == AmmoID.Arrow) 
                        return true;
                    else
                        return item.autoReuse;
                }

                return true;
            }
            if (AutoUseMagic != null && item.CountsAsClass(DamageClass.Magic))
                return true;
            if (QuickUseSummon != null && item.CountsAsClass(DamageClass.Summon))
                return true;

            // Ванильное поведение
            return item.autoReuse;
        }
        int PearlescentWeaknessBonusTime => 20 * GetModifiedSupportPoints(1);
        public override void ModifyHitNPCWithProj(Projectile proj, NPC target, ref NPC.HitModifiers modifiers)
        {
            Player owner = Main.player[proj.owner];

            int bloodlustBuffID = ModContent.BuffType<Bloodlust>();
            if (target.HasBuff(bloodlustBuffID) && BloodlustHealCD <= 0 && proj.type != ProjectileID.VampireKnife)
            {
                int healAmount = (int)(owner.statLifeMax2 * 0.005f);
                if (healAmount > 0)
                {
                    BloodlustHealCD = 180;
                    owner.statLife += healAmount;
                    owner.HealEffect(healAmount, true);
                }
            }
        }
        public override void OnHitNPCWithProj(Projectile proj, NPC target, NPC.HitInfo hit, int damageDone)
        {
            if (PearlescentSet && Main.rand.NextBool(3))
            {
                target.AddBuff(ModContent.BuffType<PearlescentWeakness>(), 90 + PearlescentWeaknessBonusTime);
            }
        }
        public override void OnHitNPC(NPC target, NPC.HitInfo hit, int damageDone)
        {
            if (PearlescentSet && Main.rand.NextBool(3))
            {
                target.AddBuff(ModContent.BuffType<PearlescentWeakness>(), 90 + PearlescentWeaknessBonusTime);
            }
        }
        private void ResetAccessories()
        {
            HoVHairpin = false;
            DryadsFlower = false;
            HunterBoots = false;
            HunterGloves = false;
        }
        private void ResetAccessoriesGroups()
        {
            RoyalGelType = null;
            RingOfFlamesType = null;
            AnkhType = null;
            WormScarfType = null;
            HeartOfSinType = null;
            AutoUseRanged = null;
            AutoUseMelee = null;
            AutoUseMagic = null;
            QuickUseSummon = null;
        }
        private void ResetSupport()
        {
            SupportPointsBonus = 0;
            SupportPointsBonus2 = 1f;
        }
        private void ResetCrit()
        {
            BonusCritDamageFlat = 0;
            BonusCritDamageMult = 1f;
            HeartofSinCrit = 0f;
        }
        private void ResetArmorSets()
        {
            MeteorSet = false;
            PearlescentSet = false;
        }
        private void CheckUniqueAccessories()
        {
            for (int i = 3; i < 10 + Player.extraAccessorySlots; i++)
            {
                Item currentItem = Player.armor[i];
                if (currentItem.type == ItemID.WormScarf)
                {
                    WormScarfType = currentItem;
                }

                if (currentItem.type == ItemID.RoyalGel ||
                    currentItem.type == ModContent.ItemType<PhoenixShield>() ||
                    currentItem.type == ModContent.ItemType<MonsterSlayerShield>() ||
                    currentItem.type == ModContent.ItemType<ObsidianMonsterSlayerShield>() ||
                    currentItem.type == ModContent.ItemType<RoyalShieldCthulhu>())
                {
                    RoyalGelType = currentItem;
                }
                if (currentItem.type == ModContent.ItemType<HeartofSin>())
                {
                    HeartOfSinType = currentItem;
                }

                if (currentItem.type == ItemID.AnkhCharm ||
                    currentItem.type == ItemID.AnkhShield ||
                    currentItem.type == ModContent.ItemType<PhoenixShield>())
                {
                    AnkhType = currentItem;
                }
                if (currentItem.type == ModContent.ItemType<RingofDivineFlames>() ||
                    currentItem.type == ModContent.ItemType<RingofLivingFlames>() ||
                    currentItem.type == ModContent.ItemType<PhoenixShield>())
                {
                    RingOfFlamesType = currentItem;
                }
                if (currentItem.type == ModContent.ItemType<HunterGlove>() )
                {
                    AutoUseRanged = currentItem;
                }
                if (currentItem.type == ModContent.ItemType<HunterGlove>() )
                {
                    QuickUseSummon = currentItem;
                }
                if (currentItem.type == ItemID.FeralClaws ||
                    currentItem.type == ItemID.PowerGlove ||
                    currentItem.type == ItemID.MechanicalGlove ||
                    currentItem.type == ItemID.BerserkerGlove ||
                    currentItem.type == ItemID.FireGauntlet)
                {
                    AutoUseMelee = currentItem;
                }
            }
        }
        private void UniqueAccessories()
        {
            if (AnkhType != null)
            {
                Player.buffImmune[BuffID.Weak] = true;
                Player.buffImmune[BuffID.BrokenArmor] = true;
                Player.buffImmune[BuffID.Bleeding] = true;
                Player.buffImmune[BuffID.Poisoned] = true;
                Player.buffImmune[BuffID.Slow] = true;
                Player.buffImmune[BuffID.Confused] = true;
                Player.buffImmune[BuffID.Silenced] = true;
                Player.buffImmune[BuffID.Cursed] = true;
                Player.buffImmune[BuffID.Darkness] = true;
                Player.buffImmune[BuffID.Stoned] = true;
                Player.buffImmune[BuffID.Chilled] = true;
            }
            if (HeartOfSinType != null)
            {
                Player.statLifeMax2 += PercLife * 35;
                Player.statLifeMax2 += 50;
                Player.GetCritChance(DamageClass.Generic) += 4;
                Player.GetDamage(DamageClass.Generic) += 0.1f;

                HeartofSinCrit = 0f;
                int extraLife = Player.statLifeMax2 - 400;
                if (extraLife > 0)
                {
                    int steps = extraLife / 100;
                    float bonus = steps * 0.05f;

                    if (bonus > 0.30f)
                        bonus = 0.30f;  

                    HeartofSinCrit = bonus;
                    BonusCritDamageFlat += HeartofSinCrit;  
                }
            }
            //Subtypes included
            if (RingOfFlamesType != null)
            {
                if (RingOfFlamesType.type == ModContent.ItemType<RingofLivingFlames>())
                {
                    Player.lavaMax = 420;
                    Player.buffImmune[BuffID.OnFire] = true;
                    Player.buffImmune[BuffID.OnFire3] = true;
                    Player.buffImmune[BuffID.Frostburn] = true;
                    Player.buffImmune[BuffID.Frostburn2] = true;

                    if (!Player.HasBuff(BuffID.Inferno) && Player.statLife <= PercLife * 25)
                        Player.AddBuff(BuffID.Inferno, 120);
                }
                else if (RingOfFlamesType.type == ModContent.ItemType<RingofDivineFlames>())
                {
                    Player.lavaRose = true;
                    Player.buffImmune[BuffID.CursedInferno] = true;
                    Player.buffImmune[BuffID.ShadowFlame] = true;
                    if (Player.statLife <= PercLife * 35)
                        Player.endurance += 0.05f;

                    if (!Player.HasBuff(BuffID.Inferno))
                        Player.AddBuff(BuffID.Inferno, 120);
                }
                else if (RingOfFlamesType.type == ModContent.ItemType<PhoenixShield>())
                {
                    Player.lavaMax = 840;
                    Player.noKnockback = true;
                    Player.fireWalk = true;
                    Player.buffImmune[BuffID.WindPushed] = true;

                    Player.lavaRose = true;
                    Player.buffImmune[BuffID.CursedInferno] = true;
                    Player.buffImmune[BuffID.ShadowFlame] = true;
                    if (!Player.HasBuff(BuffID.Inferno) && Player.statLife <= PercLife * 35)
                        Player.AddBuff(BuffID.Inferno, 120);
                }
            }
        }
        private void CommonAccessories()
        {
            if (HoVHairpin)
            {
                Player.GetDamage(DamageClass.Melee) += 0.02f;
                Player.GetDamage(DamageClass.Ranged) += 0.02f;
            }
            if (DryadsFlower)
            {
                Player.statLifeMax2 += 10;
                SupportPointsBonus += 1;
            }
            if (HunterGloves)
            {
                Player.GetAttackSpeed(DamageClass.Generic) += 0.04f;
            }
            if (HunterBoots)
            {
                Player.moveSpeed += 0.07f;
            }
        }
        private void GrandArmorSetsBonus(Player player)
        {
            if (PearlescentSet)
            {
                SupportPointsBonus += 2;
                player.GetDamage(ModContent.GetInstance<SupportClass>()) += 0.1f;
                player.GetAttackSpeed(ModContent.GetInstance<SupportClass>()) += 0.1f;
            }
        }
        private void CheckArmorSets(Player player)
        {
            static int IT<T>() where T : ModItem => ModContent.ItemType<T>();

            var head = player.head;
            var body = player.body;
            var legs = player.legs;

            bool IsWearingSet(int h, int b, int l)
                => head == h && body == b && legs == l;

            MeteorSet = IsWearingSet(
                ItemID.MeteorHelmet,
                ItemID.MeteorSuit,
                ItemID.MeteorLeggings);

            PearlescentSet = IsWearingSet(
                IT<PearlescentHelmet>(),
                IT<PearlescentChestplate>(),
                IT<PearlescentLeggings>());
        }
        private void GrantEnemiesImmunity(Player pl)
        {
            if (RoyalGelType != null)
            {
                for (int i = 0; i < WoSNPCsSets.Slimes.Length; i++)
                {
                    if (WoSNPCsSets.Slimes[i])
                    {
                        pl.npcTypeNoAggro[i] = true;
                    }
                }
            }
            if (RingOfFlamesType != null)
            {
                for (int i = 0; i < WoSNPCsSets.RingofFlamesEnemies.Length; i++)
                {
                    if (WoSNPCsSets.RingofFlamesEnemies[i])
                    {
                        Player.npcTypeNoAggro[i] = true;
                    }
                }
            }
        }
        private void ApplyCritAccessoryBonuses(Player player)
        {
            foreach (var item in player.armor)
            {
                if (item.ModItem is ICritAccessoryBonus bonus)
                {
                    BonusCritDamageFlat += bonus.GetCritDamageBonus(player);
                }
            }
        }
        private void UpdateCooldowns()
        {
            if (BloodlustHealCD > 0) BloodlustHealCD--;
        }
        private void UpdateTimers()
        {
        }

        public override float UseAnimationMultiplier(Item item)
        {
            return base.UseAnimationMultiplier(item);
        }
    }
}
