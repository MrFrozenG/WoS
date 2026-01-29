using Terraria.GameContent.Personalities;
using Terraria.ID;
using Terraria;
using Terraria.ModLoader;
using Terraria.GameContent.Bestiary;
using Terraria.Localization;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Terraria.DataStructures;
using System.Collections.Generic;
using System;
using WoS.Content.Items.Weapons.Ranged.Bows;
using Terraria.Utilities;
using WoS.Content.Core.ModWorld;
using WoS.Content.Items.Accessories.Universal;
using WoS.Content.Items.Weapons.Ranged.Throw;
using WoS.Content.Items.Weapons.Magic.YoYo;
using WoS.Content.Core.ModUtils.ShopConditions;
using WoS.Content.Core.ModPlayers;

namespace WoS.Content.NPCs.Town
{
    [AutoloadHead]
    public class HunterTrader : ModNPC
    {
        public override void SetStaticDefaults()
        {
            Main.npcFrameCount[Type] = 22;
            NPCID.Sets.AttackFrameCount[Type] = 1;
            NPCID.Sets.DangerDetectRange[Type] = 16 * 32; // The amount of pixels away from the center of the npc that it tries to attack enemies.
            NPCID.Sets.AttackType[NPC.type] = 1;
            NPCID.Sets.AttackTime[Type] = 40; 
            NPCID.Sets.AttackAverageChance[Type] = 1;

            NPCID.Sets.NPCBestiaryDrawModifiers drawModifiers = new NPCID.Sets.NPCBestiaryDrawModifiers()
            {
                Velocity = 0.25f,
                Direction = 1 
            };

            NPCID.Sets.NPCBestiaryDrawOffset.Add(Type, drawModifiers);
            NPC.Happiness
                .SetBiomeAffection<JungleBiome>(AffectionLevel.Love)
                .SetBiomeAffection<SnowBiome>(AffectionLevel.Love)
                .SetBiomeAffection<ForestBiome>(AffectionLevel.Love)
                .SetBiomeAffection<UndergroundBiome>(AffectionLevel.Dislike)
                .SetBiomeAffection<DesertBiome>(AffectionLevel.Dislike)

                .SetNPCAffection(NPCID.BestiaryGirl, AffectionLevel.Love)
                .SetNPCAffection(NPCID.ArmsDealer, AffectionLevel.Like)
                .SetNPCAffection(NPCID.Clothier, AffectionLevel.Like)
                .SetNPCAffection(NPCID.Demolitionist, AffectionLevel.Dislike);
        }
        //        private static Profiles.StackedNPCProfile NPCProfile;
        //        public override ITownNPCProfile TownNPCProfile()
        //        {
        //            return NPCProfile;
        //        }

        public const string HunterShop = "HunterShop";
        public const string HunterShopAdditional = "HunterShopAdditional";
        private static int SilverС = 100;
        private static int GoldС = 10_000;
        private static int PreHardmodeAccessories = 12_500;
        private static int PreHardmodeWeapons = 55_500;
        private static int BaseEnemiesLoot = 100;
        private static int RareEnemiesLoot = 1_500;
        private static int HMRareEnemiesLoot = 5_050;
        public override void AddShops()
        {
            var npcShop = new NPCShop(Type, HunterShop)
            .Add(new Item(ItemID.FlareGun) { shopCustomPrice = GoldС })
            .Add(new Item(ModContent.ItemType<WildBeast>()) { shopCustomPrice = PreHardmodeWeapons })
            .Add(new Item(ModContent.ItemType<HuntingKnife>()) { shopCustomPrice = PreHardmodeWeapons })
            .Add(new Item(ModContent.ItemType<HunterBoots>()) { shopCustomPrice = PreHardmodeAccessories })
            .Add(new Item(ModContent.ItemType<HunterGlove>()) { shopCustomPrice = PreHardmodeAccessories })
            .Add(new Item(ModContent.ItemType<SparksYoYo>()));
            /* ADD LATER
            .Add(new Item(ModContent.ItemType<WildBoar>()) { shopCustomPrice = GoldС + SilverС*50 });
            .Add(new Item(ModContent.ItemType<LionTail>()) { shopCustomPrice = GoldС + SilverС*50 });
            .Add(new Item(ModContent.ItemType<WildBeast>()) { shopCustomPrice = GoldС + SilverС*50 });

            */
            npcShop.Register();

            var npcShopA = new NPCShop(Type, HunterShopAdditional)
                 .Add(new Item(ItemID.WoodenArrow) { shopCustomPrice = 8 })
                 .Add(new Item(ItemID.FlamingArrow) { shopCustomPrice = 12 }, Condition.DownedEyeOfCthulhu)
                 .Add(new Item(ItemID.FrostburnArrow) { shopCustomPrice = 12 }, Condition.DownedEyeOfCthulhu, Condition.InSnow)
                 .Add(new Item(ItemID.UnholyArrow) { shopCustomPrice = 50 }, Condition.DownedEowOrBoc, Condition.NightOrEclipse)
                 .Add(new Item(ItemID.JestersArrow) { shopCustomPrice = 100 }, Condition.DownedSkeletron, Condition.NightOrEclipse)
                 .Add(new Item(ItemID.JestersArrow) { shopCustomPrice = 400 }, Condition.Hardmode)
                 .Add(new Item(ItemID.Flare) { shopCustomPrice = 7 })
                 .Add(new Item(ItemID.BlueFlare) { shopCustomPrice = 7 })
                 .Add(new Item(ItemID.PoisonDart) { shopCustomPrice = 5 }, Condition.Hardmode)
                 .Add(new Item(ItemID.CrystalDart) { shopCustomPrice = 100 }, Condition.InHallow, Condition.Hardmode)
                 .Add(new Item(ItemID.CursedDart) { shopCustomPrice = 100 }, Condition.CorruptWorld, Condition.Hardmode)

                 .Add(new Item(ItemID.StyngerBolt) { shopCustomPrice = 150 }, Condition.Hardmode, PlayerHasItemCondition.HasStynger)
                 .Add(new Item(ItemID.Nail) { shopCustomPrice = 150 }, Condition.Hardmode, PlayerHasItemCondition.HasNailWeapon)
                 .Add(new Item(ItemID.Stake) { shopCustomPrice = 150 }, Condition.Hardmode, PlayerHasItemCondition.HasStakeLauncher)
                 .Add(new Item(ItemID.ExplosiveJackOLantern) { shopCustomPrice = 150 }, Condition.Hardmode, PlayerHasItemCondition.HasJackLauncher)
                 .Add(new Item(ItemID.CandyCorn) { shopCustomPrice = 150 }, Condition.Hardmode, PlayerHasItemCondition.HasCandyCornRifle);
            npcShopA.Register();
        }

        public override bool CanGoToStatue(bool toKingStatue) => true;
        public override void OnGoToStatue(bool toKingStatue)
        {
            if (Main.netMode == NetmodeID.Server)
            {
                ModPacket packet = Mod.GetPacket();
                packet.Write((byte)WoS.MessageType.NPCTeleportToStatue);
                packet.Write((byte)NPC.whoAmI);
                packet.Send();
            }
            else
            {
                StatueTeleport();
            }
        }

        public void StatueTeleport()
        {
            for (int i = 0; i < 30; i++)
            {
                Vector2 position = Main.rand.NextVector2Square(-20, 21);
                if (Math.Abs(position.X) > Math.Abs(position.Y))
                {
                    position.X = Math.Sign(position.X) * 20;
                }
                else
                {
                    position.Y = Math.Sign(position.Y) * 20;
                }

                Dust.NewDustPerfect(NPC.Center + position, DustID.Cloud, Vector2.Zero).noGravity = true;
            }
        }
        public override void TownNPCAttackStrength(ref int damage, ref float knockback)
        {
            damage = NPC.damage;
            knockback = 6f;
        }
        public override void TownNPCAttackCooldown(ref int cooldown, ref int randExtraCooldown)
        {
            cooldown = 25;
            randExtraCooldown = 25;
        }
        public override void TownNPCAttackProjSpeed(ref float multiplier, ref float gravityCorrection, ref float randomOffset)
        {
            multiplier = 16.5f;
        }
        public override void TownNPCAttackProj(ref int projType, ref int attackDelay)
        {
            attackDelay = 10;
            if (!Main.hardMode)
            {
                projType = ProjectileID.WoodenArrowFriendly;
            }
            if (Main.hardMode)
            {
                projType = ProjectileID.CrystalBullet;
            }
        }

        public override void DrawTownAttackGun(ref Texture2D item, ref Rectangle itemFrame, ref float scale, ref int horizontalHoldoutOffset)
        {
            scale = 1f;
            if (!Main.hardMode)
            {
                int WildBeast = ModContent.ItemType<WildBeast>();
                Main.GetItemDrawFrame(WildBeast, out item, out itemFrame);
                horizontalHoldoutOffset = (int)Main.DrawPlayerItemPos(1f, WildBeast).X - 12;
                scale = 0.85f;
            }
            if (Main.hardMode)
            {
                int MiniShark = ItemID.Minishark;
                Main.GetItemDrawFrame(MiniShark, out item, out itemFrame);
                horizontalHoldoutOffset = (int)Main.DrawPlayerItemPos(1f, MiniShark).X - 12;
            }
        }

        public override bool PreDraw(SpriteBatch spriteBatch, Vector2 screenPos, Color drawColor)
        {
            if (NPCID.Sets.NPCBestiaryDrawOffset.TryGetValue(Type, out NPCID.Sets.NPCBestiaryDrawModifiers drawModifiers))
            {
                drawModifiers.Direction -= 1;

                NPCID.Sets.NPCBestiaryDrawOffset.Remove(Type);
                NPCID.Sets.NPCBestiaryDrawOffset.Add(Type, drawModifiers);
            }

            return true;
        }
        public override void OnSpawn(IEntitySource source)
        {
            if (source is EntitySource_SpawnNPC)
            {
                World_NPCs.unlockedHunter = true;
            }
        }

        public override bool CanTownNPCSpawn(int numTownNPCs)
        {
            if (World_NPCs.unlockedHunter)
            {
                return true;
            }
            { 
                for (int k = 0; k < 255; k++)
                {
                    Player player = Main.player[k];
                    if (!player.active && numTownNPCs > 3 && NPC.downedBoss1)
                    {
                        return true;
                    }
                }

                return false;
            }
        }

        public override void SetDefaults()
        {
            NPC.townNPC = true;
            NPC.friendly = true; 
            NPC.width = 18;
            NPC.height = 40;
            NPC.aiStyle = 7;
            NPC.damage = 15;
            NPC.defense = 10;
            NPC.lifeMax = 250;
            NPC.HitSound = SoundID.NPCHit1;
            NPC.DeathSound = SoundID.NPCDeath1;
            NPC.knockBackResist = 0.35f;

            AnimationType = NPCID.Guide;
        }

        public override void SetChatButtons(ref string button, ref string button2)
        { 
            button = Language.GetTextValue("Mods.WoS.NPCs.HunterTrader.Shop.ShopBasic");
            button2 = Language.GetTextValue("Mods.WoS.NPCs.HunterTrader.Shop.ShopAdditional");
        }
        public override void OnChatButtonClicked(bool firstButton, ref string shop)
        {
            if (firstButton)
            {
                shop = HunterShop;
            }
            else
            {
                shop = HunterShopAdditional;
            }
        }
        public override void SetBestiary(BestiaryDatabase database, BestiaryEntry bestiaryEntry)
        {
            bestiaryEntry.Info.AddRange(new IBestiaryInfoElement[] {
				BestiaryDatabaseNPCsPopulator.CommonTags.SpawnConditions.Biomes.Surface,
                new FlavorTextBestiaryInfoElement("Mods.WoS.NPCs.HunterTrader.Bestiary.First"),
                new FlavorTextBestiaryInfoElement("Mods.WoS.NPCs.HunterTrader.Bestiary.Second")
            });
        }
        public override void FindFrame(int frameHeight)
        {
            NPC.spriteDirection = NPC.direction;
        }
        public override List<string> SetNPCNameList()
        {
            string Tourin = Language.GetTextValue("Mods.WoS.NPCs.HunterTrader.Names.Tourin");
            string Thorne = Language.GetTextValue("Mods.WoS.NPCs.HunterTrader.Names.Thorne");
            string Edward = Language.GetTextValue("Mods.WoS.NPCs.HunterTrader.Names.Edward");
            string Yanokage = Language.GetTextValue("Mods.WoS.NPCs.HunterTrader.Names.Yanokage");
            string Rowan = Language.GetTextValue("Mods.WoS.NPCs.HunterTrader.Names.Rowan");
            string Eldric = Language.GetTextValue("Mods.WoS.NPCs.HunterTrader.Names.Eldric");
            string Kazuki = Language.GetTextValue("Mods.WoS.NPCs.HunterTrader.Names.Kazuki");
            string Steve = Language.GetTextValue("Mods.WoS.NPCs.HunterTrader.Names.Steve");
            string Johnny = Language.GetTextValue("Mods.WoS.NPCs.HunterTrader.Names.Johnny");
            string Peter = Language.GetTextValue("Mods.WoS.NPCs.HunterTrader.Names.Peter");
            string Jack = Language.GetTextValue("Mods.WoS.NPCs.HunterTrader.Names.Jack");
            string Hue = Language.GetTextValue("Mods.WoS.NPCs.HunterTrader.Names.Hue");
            string Jullian = Language.GetTextValue("Mods.WoS.NPCs.HunterTrader.Names.Jullian");
            string Gerald = Language.GetTextValue("Mods.WoS.NPCs.HunterTrader.Names.Gerald");
//            string Haris = Language.GetTextValue("Mods.WoS.NPCs.Hunter.Names.Haris");
            //References
            string HI3_Kalpas = Language.GetTextValue("Mods.WoS.NPCs.HunterTrader.Names.Kalpas");
            string HS_Astel = Language.GetTextValue("Mods.WoS.NPCs.HunterTrader.Names.Astel");
            string DMC_Dante = Language.GetTextValue("Mods.WoS.NPCs.HunterTrader.Names.Dante");
            string STRQ_QrowBranwen = Language.GetTextValue("Mods.WoS.NPCs.HunterTrader.Names.QrowBranwen");
            string ZZZ_Hugo = Language.GetTextValue("Mods.WoS.NPCs.HunterTrader.Names.Hugo");
            string ZZZ_Lycaon = Language.GetTextValue("Mods.WoS.NPCs.HunterTrader.Names.Lycaon");
            string HSR_HuntAeon = Language.GetTextValue("Mods.WoS.NPCs.HunterTrader.Names.Lan");
            string HSR_AbundanceAeon = Language.GetTextValue("Mods.WoS.NPCs.HunterTrader.Names.Yaoshi");
            string HSR_Blade = Language.GetTextValue("Mods.WoS.NPCs.HunterTrader.Names.Blade");
            return new List<string>() {
                Tourin,
                Thorne,
                Edward, 
                Yanokage,
                Rowan, 
                Eldric,
                Kazuki,
                Steve,
                Johnny,
                Peter,
                Jack, 
                Hue,
                Jullian,
                Gerald,
                HS_Astel,
                HI3_Kalpas,
                DMC_Dante,
                STRQ_QrowBranwen,
                ZZZ_Hugo,
                ZZZ_Lycaon,
                HSR_AbundanceAeon,
                HSR_Blade,
                HSR_HuntAeon
            };
        }

        public override string GetChat()
        {
            WeightedRandom<string> chat = new WeightedRandom<string>();
            string ArmsDealerWeapons = Language.GetTextValue("Mods.WoS.NPCs.HunterTrader.Chat.ArmsDealer.AboutWeapons");
            string ArmsDealerHunt = Language.GetTextValue("Mods.WoS.NPCs.HunterTrader.Chat.ArmsDealer.AboutHunt");
            string ArmsDealerGuns = Language.GetTextValue("Mods.WoS.NPCs.HunterTrader.Chat.ArmsDealer.AboutGuns");
            string ArmsDealerNurse = Language.GetTextValue("Mods.WoS.NPCs.HunterTrader.Chat.ArmsDealer.AboutNurse");
            string ArmsDealerAmmunition = Language.GetTextValue("Mods.WoS.NPCs.HunterTrader.Chat.ArmsDealer.AboutAmmunition");
            string ArmsDealerDryad = Language.GetTextValue("Mods.WoS.NPCs.HunterTrader.Chat.ArmsDealer.AboutDryad");

            string ZoolistAnimals = Language.GetTextValue("Mods.WoS.NPCs.HunterTrader.Chat.Zoolist.AboutAnimals");
            string ZoolistAboutHer = Language.GetTextValue("Mods.WoS.NPCs.HunterTrader.Chat.Zoolist.AboutZoologist");
            string ZoolistZoologistBrother = Language.GetTextValue("Mods.WoS.NPCs.HunterTrader.Chat.Zoolist.AboutZoologistBrother");
            string ZoolistBloodmoon = Language.GetTextValue("Mods.WoS.NPCs.HunterTrader.Chat.Zoolist.AboutBloodmoon");
            string ZoolistSympathy = Language.GetTextValue("Mods.WoS.NPCs.HunterTrader.Chat.Zoolist.AboutSympathy");
            string ZoolistForest = Language.GetTextValue("Mods.WoS.NPCs.HunterTrader.Chat.Zoolist.AboutForest");
            string ZoolistCurse = Language.GetTextValue("Mods.WoS.NPCs.HunterTrader.Chat.Zoolist.AboutCurse");
            string ZoolistTail = Language.GetTextValue("Mods.WoS.NPCs.HunterTrader.Chat.Zoolist.AboutTail");
            string ZoolistTime = Language.GetTextValue("Mods.WoS.NPCs.HunterTrader.Chat.Zoolist.AboutTimeTogether");
            string ZoolistLearning = Language.GetTextValue("Mods.WoS.NPCs.HunterTrader.Chat.Zoolist.LearningNew");

            string ClothierClothes = Language.GetTextValue("Mods.WoS.NPCs.HunterTrader.Chat.Clothier.AboutClothes");
            string ClothierDungeon = Language.GetTextValue("Mods.WoS.NPCs.HunterTrader.Chat.Clothier.AboutDungeon");
            string ClothierMaterials = Language.GetTextValue("Mods.WoS.NPCs.HunterTrader.Chat.Clothier.AboutMaterials");
            string ClothierMonsters = Language.GetTextValue("Mods.WoS.NPCs.HunterTrader.Chat.Clothier.AboutMonsters");
            string ClothierBloodmoon = Language.GetTextValue("Mods.WoS.NPCs.HunterTrader.Chat.Clothier.AboutBloodmoon");
            string ClothierPartyClothes = Language.GetTextValue("Mods.WoS.NPCs.HunterTrader.Chat.Clothier.AboutPartyClothes");

            string MechanicDungeon = Language.GetTextValue("Mods.WoS.NPCs.HunterTrader.Chat.Mechanic.AboutDungeon");
            string MechanicTraps = Language.GetTextValue("Mods.WoS.NPCs.HunterTrader.Chat.Mechanic.AboutTraps");
            string MechanicMedical = Language.GetTextValue("Mods.WoS.NPCs.HunterTrader.Chat.Mechanic.AboutMedicalTroubles");
            string MechanicWorkshop = Language.GetTextValue("Mods.WoS.NPCs.HunterTrader.Chat.Mechanic.AboutExplodedWorkshop");
            string MechanicFear = Language.GetTextValue("Mods.WoS.NPCs.HunterTrader.Chat.Mechanic.AboutFear");
            string MechanicNoise = Language.GetTextValue("Mods.WoS.NPCs.HunterTrader.Chat.Mechanic.AboutNoises");
            string MechanicTools = Language.GetTextValue("Mods.WoS.NPCs.HunterTrader.Chat.Mechanic.AboutTools");
            string MechanicToolbox = Language.GetTextValue("Mods.WoS.NPCs.HunterTrader.Chat.Mechanic.AboutToolbox");
            string MechanicBombs = Language.GetTextValue("Mods.WoS.NPCs.HunterTrader.Chat.Mechanic.AboutBombs");
            string MechanicGoblin = Language.GetTextValue("Mods.WoS.NPCs.HunterTrader.Chat.Mechanic.AboutGoblinTinkerer");
            string MechanicPrivateTime = Language.GetTextValue("Mods.WoS.NPCs.HunterTrader.Chat.Mechanic.AboutPrivateTime");

            string GoblinTraps = Language.GetTextValue("Mods.WoS.NPCs.HunterTrader.Chat.GoblinTinkerer.AboutTraps");
            string GoblinNoise = Language.GetTextValue("Mods.WoS.NPCs.HunterTrader.Chat.GoblinTinkerer.AboutNoise");
            string GoblinFriendship = Language.GetTextValue("Mods.WoS.NPCs.HunterTrader.Chat.GoblinTinkerer.AboutFriendship");
            string GoblinInventions = Language.GetTextValue("Mods.WoS.NPCs.HunterTrader.Chat.GoblinTinkerer.AboutInventions");
            string GoblinGadgets = Language.GetTextValue("Mods.WoS.NPCs.HunterTrader.Chat.GoblinTinkerer.AboutGadgets");
            string GoblinRepair = Language.GetTextValue("Mods.WoS.NPCs.HunterTrader.Chat.GoblinTinkerer.AboutRepair");
            string GoblinTracking = Language.GetTextValue("Mods.WoS.NPCs.HunterTrader.Chat.GoblinTinkerer.AboutTracking");

            string DemolitionistBlastFishing = Language.GetTextValue("Mods.WoS.NPCs.HunterTrader.Chat.Demolitionist.AboutBlastFishing");
            string DemolitionistNoises = Language.GetTextValue("Mods.WoS.NPCs.HunterTrader.Chat.Demolitionist.AboutNoises");
            string DemolitionistBombs = Language.GetTextValue("Mods.WoS.NPCs.HunterTrader.Chat.Demolitionist.AboutBombs");
            string DemolitionistExplosions = Language.GetTextValue("Mods.WoS.NPCs.HunterTrader.Chat.Demolitionist.AboutExplosions");
            string DemolitionistAngler = Language.GetTextValue("Mods.WoS.NPCs.HunterTrader.Chat.Demolitionist.AboutAngler");
            string DemolitionistNight = Language.GetTextValue("Mods.WoS.NPCs.HunterTrader.Chat.Demolitionist.AboutNight");

            string WitchDoctorWildBeasts = Language.GetTextValue("Mods.WoS.NPCs.HunterTrader.Chat.WitchDoctor.AboutWildBeasts");
            string WitchDoctorPoisons = Language.GetTextValue("Mods.WoS.NPCs.HunterTrader.Chat.WitchDoctor.AboutPoisons");
            string WitchDoctorJungles = Language.GetTextValue("Mods.WoS.NPCs.HunterTrader.Chat.WitchDoctor.AboutJungles");

            string DyeTraderPlants = Language.GetTextValue("Mods.WoS.NPCs.HunterTrader.Chat.DyeTrader.AboutDyes");

            string AnglerBoy = Language.GetTextValue("Mods.WoS.NPCs.HunterTrader.Chat.Angler.AboutAngler");
            string AnglerFish = Language.GetTextValue("Mods.WoS.NPCs.HunterTrader.Chat.Angler.AboutFish");
            string AnglerSeaCreatures = Language.GetTextValue("Mods.WoS.NPCs.HunterTrader.Chat.Angler.AboutSeaCreatures");
            string AnglerOcean = Language.GetTextValue("Mods.WoS.NPCs.HunterTrader.Chat.Angler.AboutOcean");
            string AnglerFishing = Language.GetTextValue("Mods.WoS.NPCs.HunterTrader.Chat.Angler.AboutFishing");

            //General talk
            string AboutDungeon = Language.GetTextValue("Mods.WoS.NPCs.HunterTrader.Chat.AboutDungeon");
            string AboutHunt = Language.GetTextValue("Mods.WoS.NPCs.HunterTrader.Chat.AboutHunt");
            string AboutBows = Language.GetTextValue("Mods.WoS.NPCs.HunterTrader.Chat.AboutBows");
            string AboutGuns = Language.GetTextValue("Mods.WoS.NPCs.HunterTrader.Chat.AboutGuns");
            string AboutTraps = Language.GetTextValue("Mods.WoS.NPCs.HunterTrader.Chat.AboutTraps");
            string AboutWeather = Language.GetTextValue("Mods.WoS.NPCs.HunterTrader.Chat.AboutWeather");
            string AboutFood = Language.GetTextValue("Mods.WoS.NPCs.HunterTrader.Chat.AboutFood");
            string AboutLoot = Language.GetTextValue("Mods.WoS.NPCs.HunterTrader.Chat.AboutLoot");
            string AboutHunterHonor = Language.GetTextValue("Mods.WoS.NPCs.HunterTrader.Chat.AboutHunterHonor");
            string AboutBeasts = Language.GetTextValue("Mods.WoS.NPCs.HunterTrader.Chat.AboutBeasts");
            string AboutNight = Language.GetTextValue("Mods.WoS.NPCs.HunterTrader.Chat.AboutNight");
            string WeatherRaining = Language.GetTextValue("Mods.WoS.NPCs.HunterTrader.Chat.WeatherRaining");
            string WeatherSlimes = Language.GetTextValue("Mods.WoS.NPCs.HunterTrader.Chat.WeatherSlimes");
            string AboutSlimes = Language.GetTextValue("Mods.WoS.NPCs.HunterTrader.Chat.AboutSlimes");
            string AboutZombies = Language.GetTextValue("Mods.WoS.NPCs.HunterTrader.Chat.AboutZombies");
            string AboutEyes = Language.GetTextValue("Mods.WoS.NPCs.HunterTrader.Chat.AboutEyes");
            string AboutEyeofCthulhu = Language.GetTextValue("Mods.WoS.NPCs.HunterTrader.Chat.AboutEyeofCthulhu");
            string AboutSkeletons = Language.GetTextValue("Mods.WoS.NPCs.HunterTrader.Chat.AboutSkeletons");
            string AboutHornets = Language.GetTextValue("Mods.WoS.NPCs.HunterTrader.Chat.AboutHornets");
            string AboutBloodmoon = Language.GetTextValue("Mods.WoS.NPCs.HunterTrader.Chat.AboutBloodmoon");

            int ArmsDealer = NPC.FindFirstNPC(NPCID.ArmsDealer);
            int Merchant = NPC.FindFirstNPC(NPCID.Merchant);
            int Nurse = NPC.FindFirstNPC(NPCID.Nurse);
            int Clothier = NPC.FindFirstNPC(NPCID.Clothier);
            int Zoologist = NPC.FindFirstNPC(NPCID.BestiaryGirl);
            int Mechainic = NPC.FindFirstNPC(NPCID.Mechanic);
            int Golfer = NPC.FindFirstNPC(NPCID.Golfer);
            int Dryad = NPC.FindFirstNPC(NPCID.Dryad);
            int Angler = NPC.FindFirstNPC(NPCID.Angler);
            int Goblin = NPC.FindFirstNPC(NPCID.GoblinTinkerer);
            int Demolitionist = NPC.FindFirstNPC(NPCID.Demolitionist);
            int WitchDoctor = NPC.FindFirstNPC(NPCID.WitchDoctor);
            int DyeTrader = NPC.FindFirstNPC(NPCID.DyeTrader);
            if (ArmsDealer >= 0)
            {
                chat.Add(string.Format(ArmsDealerWeapons, Main.npc[ArmsDealer].GivenName));
                chat.Add(string.Format(ArmsDealerHunt, Main.npc[ArmsDealer].GivenName));
                chat.Add(string.Format(ArmsDealerGuns, Main.npc[ArmsDealer].GivenName));
                chat.Add(string.Format(ArmsDealerAmmunition, Main.npc[ArmsDealer].GivenName));
                if (Dryad >= 0)
                {
                    chat.Add(string.Format(ArmsDealerDryad,
                        Main.npc[ArmsDealer].GivenName,
                        Main.npc[Dryad].GivenName));
                }
                if (Nurse >= 0)
                {
                    chat.Add(string.Format(ArmsDealerNurse,
                        Main.npc[ArmsDealer].GivenName,
                        Main.npc[Nurse].GivenName));
                }
            }
            if (Zoologist >= 0)
            {
                chat.Add(string.Format(ZoolistAnimals, Main.npc[Zoologist].GivenName));
                chat.Add(string.Format(ZoolistAboutHer, Main.npc[Zoologist].GivenName));
                chat.Add(string.Format(ZoolistSympathy, Main.npc[Zoologist].GivenName), 0.5);
                chat.Add(string.Format(ZoolistForest, Main.npc[Zoologist].GivenName));
                chat.Add(string.Format(ZoolistCurse, Main.npc[Zoologist].GivenName));

                chat.Add(string.Format(ZoolistTail, Main.npc[Zoologist].GivenName), 0.5);
                chat.Add(string.Format(ZoolistTime, Main.npc[Zoologist].GivenName), 0.5);
                chat.Add(string.Format(ZoolistLearning, Main.npc[Zoologist].GivenName), 0.5);
                if (Main.bloodMoon)
                {
                    chat.Add(string.Format(ZoolistBloodmoon, Main.npc[Zoologist].GivenName));
                }
                if (Golfer >= 0)
                {
                    chat.Add(string.Format(ZoolistZoologistBrother, Main.npc[Zoologist].GivenName, Main.npc[Golfer].GivenName));
                }
            }
            if (Clothier >= 0)
            {
                chat.Add(string.Format(ClothierClothes, Main.npc[Clothier].GivenName));
                chat.Add(string.Format(ClothierMaterials, Main.npc[Clothier].GivenName));
                chat.Add(string.Format(ClothierMonsters, Main.npc[Clothier].GivenName));
                chat.Add(string.Format(ClothierDungeon, Main.npc[Clothier].GivenName));
                if (Main.bloodMoon)
                {
                    chat.Add(string.Format(ClothierBloodmoon, Main.npc[Clothier].GivenName));
                }
                if (Terraria.GameContent.Events.BirthdayParty.PartyIsUp)
                {
                    chat.Add(string.Format(ClothierPartyClothes, Main.npc[Clothier].GivenName));
                }
            }
            if (Mechainic >= 0)
            {
                chat.Add(string.Format(MechanicDungeon, Main.npc[Mechainic].GivenName));
                chat.Add(string.Format(MechanicTraps, Main.npc[Mechainic].GivenName));
                chat.Add(string.Format(MechanicMedical, Main.npc[Mechainic].GivenName));
                chat.Add(string.Format(MechanicWorkshop, Main.npc[Mechainic].GivenName));
                chat.Add(string.Format(MechanicFear, Main.npc[Mechainic].GivenName));
                chat.Add(string.Format(MechanicNoise, Main.npc[Mechainic].GivenName));
                chat.Add(string.Format(MechanicTools, Main.npc[Mechainic].GivenName));
                chat.Add(string.Format(MechanicToolbox, Main.npc[Mechainic].GivenName));
                chat.Add(string.Format(MechanicBombs, Main.npc[Mechainic].GivenName));
                if (Goblin >= 0)
                {
                    chat.Add(string.Format(MechanicGoblin, Main.npc[Mechainic].GivenName, Main.npc[Goblin].GivenName));
                    chat.Add(string.Format(MechanicPrivateTime, Main.npc[Mechainic].GivenName, Main.npc[Goblin].GivenName));
                }
            }
            if (Goblin >= 0)
            {
                chat.Add(string.Format(GoblinTraps, Main.npc[Goblin].GivenName));
                chat.Add(string.Format(GoblinNoise, Main.npc[Goblin].GivenName));
                chat.Add(string.Format(GoblinFriendship, Main.npc[Goblin].GivenName));
                chat.Add(string.Format(GoblinInventions, Main.npc[Goblin].GivenName));
                chat.Add(string.Format(GoblinGadgets, Main.npc[Goblin].GivenName));
                chat.Add(string.Format(GoblinRepair, Main.npc[Goblin].GivenName));
                chat.Add(string.Format(GoblinTracking, Main.npc[Goblin].GivenName));
            }
            if (Demolitionist >= 0)
            {
                float sqrDistance = Vector2.DistanceSquared(Main.npc[Demolitionist].Center, NPC.Center);
                float range = 16f * 60f;
                chat.Add(Language.GetTextValue(DemolitionistNoises));
                chat.Add(Language.GetTextValue(DemolitionistBombs));
                chat.Add(Language.GetTextValue(DemolitionistExplosions));
                chat.Add(string.Format(DemolitionistBlastFishing, Main.npc[Demolitionist].GivenName));
                if (sqrDistance < range * range)
                {
                    chat.Add(string.Format(DemolitionistNight, Main.npc[Demolitionist].GivenName));
                    if (Angler >= 0)
                    {
                        float sqrDistanceAngler = Vector2.DistanceSquared(Main.npc[Angler].Center, NPC.Center);
                        if (sqrDistanceAngler < range * range)
                        {
                            chat.Add(string.Format(DemolitionistAngler, Main.npc[Demolitionist].GivenName, Main.npc[Angler].GivenName));

                        }
                    }
                }
            }
            if (Angler >= 0)
            {
                chat.Add(string.Format(AnglerBoy, Main.npc[Angler].GivenName));
                chat.Add(string.Format(AnglerFish, Main.npc[Angler].GivenName));
                chat.Add(string.Format(AnglerSeaCreatures, Main.npc[Angler].GivenName));
                chat.Add(string.Format(AnglerOcean, Main.npc[Angler].GivenName));
                chat.Add(Language.GetTextValue(AnglerFishing));
            }
            if (WitchDoctor >= 0)
            {
                chat.Add(string.Format(WitchDoctorJungles, Main.npc[WitchDoctor].GivenName));
                chat.Add(string.Format(WitchDoctorPoisons, Main.npc[WitchDoctor].GivenName));
                chat.Add(string.Format(WitchDoctorWildBeasts, Main.npc[WitchDoctor].GivenName));
            }
            if (DyeTrader >= 0)
            {
                chat.Add(string.Format(DyeTraderPlants, Main.npc[DyeTrader].GivenName));
            }

            chat.Add(Language.GetTextValue(AboutHunt));
                chat.Add(Language.GetTextValue(AboutBows));
                chat.Add(Language.GetTextValue(AboutGuns));
                chat.Add(Language.GetTextValue(AboutTraps));
                chat.Add(Language.GetTextValue(AboutWeather));
                chat.Add(Language.GetTextValue(AboutFood));
                chat.Add(Language.GetTextValue(AboutLoot));
                chat.Add(Language.GetTextValue(AboutHunterHonor));
                chat.Add(Language.GetTextValue(AboutBeasts));
                chat.Add(Language.GetTextValue(AboutNight));
                chat.Add(Language.GetTextValue(AboutSlimes));
                chat.Add(Language.GetTextValue(AboutZombies));
                chat.Add(Language.GetTextValue(AboutEyes));
                chat.Add(Language.GetTextValue(AboutSkeletons));
                chat.Add(Language.GetTextValue(AboutHornets));
                chat.Add(Language.GetTextValue(AboutBloodmoon));
                if (Main.slimeRain)
                {
                    chat.Add(Language.GetTextValue(WeatherSlimes), 0.8);
                }
                if (Main.raining)
                {
                    chat.Add(Language.GetTextValue(WeatherRaining), 0.8);
                }
                if (NPC.downedBoss1)
                {
                    chat.Add(Language.GetTextValue(AboutEyeofCthulhu), 0.8);
                }
                if (!NPC.downedBoss3)
                {
                    chat.Add(Language.GetTextValue(AboutDungeon), 0.8);
                }
            return chat; 
        }
    }
}
/*
// These are things that the NPC has a chance of telling you when you talk to it.
chat.Add("Come here! Buy it! The best products!");
chat.Add("Angel statues? I'm afraid you're too late, I'm not buying them anymore.");
chat.Add("Is it legal? huh? Are there laws here?");
chat.Add("don't ask me where I got all this.");
chat.Add("Carved on stones, Buried deep in the earth, Long submerged beneath waterfalls...Oh, I didn't see you. Did you want something?");
chat.Add("Hmm? Some old man in an old castle who also turned into a huge skeleton? Interesting.");
chat.Add("I don't sell junk. I sell useful things!", 1.0);
chat.Add("If you see a girl with a bow surprisingly similar to cat ears, and her name is Blake - give her my regards.", 0.1);
*/