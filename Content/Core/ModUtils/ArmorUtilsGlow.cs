using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using ReLogic.Content;
using System;
using System.Collections.Generic;
using Terraria;
using Terraria.DataStructures;
using static Terraria.ModLoader.PlayerDrawLayer;
using Terraria.ModLoader;

namespace WoS.Content.Core.ModUtils
{/*
    public class ArmorUtilsGlow
    {
        public sealed class HeadGlowData
        {
            public Asset<Texture2D> Texture;
            public Func<PlayerDrawSet, Color> ColorFunc;
            public Func<PlayerDrawSet, bool> VisibilityFunc;
        }
        /// <summary>
        /// Регистрирует glow-слой для шлема
        /// </summary>
        /// <param name="headSlot">Equip slot головы</param>
        /// <param name="texture">Glow текстура</param>
        /// <param name="colorFunc">Цвет (обычно Color.White * alpha)</param>
        /// <param name="visibilityFunc">Условие видимости (можно null)</param>
        public static void RegisterHead(
            int headSlot,
            Asset<Texture2D> texture,
            Func<PlayerDrawSet, Color> colorFunc,
            Func<PlayerDrawSet, bool> visibilityFunc = null
        )
        {
            if (Main.dedServ)
                return;

            HeadGlowLayer.RegisterData(headSlot, new HeadGlowData
            {
                Texture = texture,
                ColorFunc = colorFunc,
                VisibilityFunc = visibilityFunc
            });
        }

        public sealed class LegsGlowData
        {
            public Asset<Texture2D> Texture;
            public Func<PlayerDrawSet, Color> ColorFunc;
            public Func<PlayerDrawSet, bool> VisibilityFunc;
        }

        public static void RegisterLegs(
            int legSlot,
            Asset<Texture2D> texture,
            Func<PlayerDrawSet, Color> colorFunc,
            Func<PlayerDrawSet, bool> visibilityFunc = null
        )
        {
            if (Main.dedServ)
                return;

            LegsGlowLayer.RegisterData(legSlot, new LegsGlowData
            {
                Texture = texture,
                ColorFunc = colorFunc,
                VisibilityFunc = visibilityFunc
            });
        }
    }

    public sealed class HeadGlowLayer : PlayerDrawLayer
    {
        private static Dictionary<int, ArmorUtilsGlow.HeadGlowData> HeadGlowDataMap;

        internal static void RegisterData(int headSlot, ArmorUtilsGlow.HeadGlowData data)
        {
            if (!HeadGlowDataMap.ContainsKey(headSlot))
                HeadGlowDataMap.Add(headSlot, data);
        }

        public override void Load()
        {
            HeadGlowDataMap = new Dictionary<int, ArmorUtilsGlow.HeadGlowData>();
        }

        public override void Unload()
        {
            HeadGlowDataMap = null;
        }

        public override Position GetDefaultPosition()
        {
            // Поверх обычной головы
            return new AfterParent(PlayerDrawLayers.Head);
        }

        public override bool GetDefaultVisibility(PlayerDrawSet drawInfo)
        {
            Player p = drawInfo.drawPlayer;
            return !p.dead && !p.invis && p.head != -1 && drawInfo.shadow == 0f;
        }

        protected override void Draw(ref PlayerDrawSet drawInfo)
        {
            Player player = drawInfo.drawPlayer;

            if (!HeadGlowDataMap.TryGetValue(player.head, out var data))
                return;

            if (data.VisibilityFunc != null && !data.VisibilityFunc(drawInfo))
                return;

            Texture2D texture = data.Texture.Value;
            Color color = data.ColorFunc(drawInfo);

            Vector2 drawPos =
                drawInfo.Position
                - Main.screenPosition
                + new Vector2(
                    player.width / 2 - player.bodyFrame.Width / 2,
                    player.height - player.bodyFrame.Height + 4f
                )
                + player.headPosition;

            Vector2 origin = drawInfo.headVect;

            DrawData drawData = new DrawData(
                texture,
                drawPos.Floor() + origin,
                player.bodyFrame,
                color,
                player.headRotation,
                origin,
                1f,
                drawInfo.playerEffect,
                0
            )
            {
                shader = 0 // glow НЕ должен использовать dye
            };

            drawInfo.DrawDataCache.Add(drawData);
        }
    }
    public sealed class LegsGlowLayer : PlayerDrawLayer
    {
        private static Dictionary<int, ArmorUtilsGlow.LegsGlowData> LegsGlowDataMap;

        internal static void RegisterData(int legSlot, ArmorUtilsGlow.LegsGlowData data)
        {
            if (!LegsGlowDataMap.ContainsKey(legSlot))
                LegsGlowDataMap.Add(legSlot, data);
        }

        public override void Load()
        {
            LegsGlowDataMap = new Dictionary<int, ArmorUtilsGlow.LegsGlowData>();
        }

        public override void Unload()
        {
            LegsGlowDataMap = null;
        }

        public override Position GetDefaultPosition()
        {
            // Поверх обычных поножей
            return new AfterParent(PlayerDrawLayers.Leggings);
        }

        public override bool GetDefaultVisibility(PlayerDrawSet drawInfo)
        {
            Player p = drawInfo.drawPlayer;
            return !p.dead && !p.invis && p.legs != -1 && drawInfo.shadow == 0f;
        }

        protected override void Draw(ref PlayerDrawSet drawInfo)
        {
            Player player = drawInfo.drawPlayer;

            if (!LegsGlowDataMap.TryGetValue(player.legs, out var data))
                return;

            if (data.VisibilityFunc != null && !data.VisibilityFunc(drawInfo))
                return;

            Texture2D texture = data.Texture.Value;
            Color color = data.ColorFunc(drawInfo);

            // --- СИДЕНИЕ / РОБЫ ---
            if (drawInfo.isSitting &&
                (DrawLayerHelper.ShouldOverrideLegs_CheckShoes(ref drawInfo) || player.wearsRobe))
            {
                DrawLayerHelper.DrawSittingLegsMethod(
                    ref drawInfo,
                    texture,
                    color,
                    shader: 0 // glow без dye
                );
                return;
            }



            // --- ОБЫЧНАЯ ОТРИСОВКА ---
            Vector2 drawPos =
                drawInfo.Position
                - Main.screenPosition
                + new Vector2(
                    player.width / 2 - player.legFrame.Width / 2,
                    player.height - player.legFrame.Height + 4f
                )
                + player.legPosition;

            Vector2 legsOffset = drawInfo.legsOffset;

            DrawData drawData = new DrawData(
                texture,
                drawPos.Floor() + legsOffset,
                player.legFrame,
                color,
                player.legRotation,
                legsOffset,
                1f,
                drawInfo.playerEffect,
                0
            )
            {
                shader = 0 // glow НЕ использует dye
            };

            drawInfo.DrawDataCache.Add(drawData);
        }
    }*/
}
