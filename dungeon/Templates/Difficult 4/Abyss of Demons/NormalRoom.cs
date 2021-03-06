﻿using System;
using LoESoft.Dungeon.utils;
using RotMG.Common.Rasterizer;
using LoESoft.Dungeon.engine;

namespace LoESoft.Dungeon.templates.Difficult_4.Abyss_of_Demons
{
    internal class NormalRoom : Room
    {
        readonly int w;
        readonly int h;

        public NormalRoom(int w, int h)
        {
            this.w = w;
            this.h = h;
        }

        public override RoomType Type { get { return RoomType.Normal; } }

        public override int Width { get { return w; } }

        public override int Height { get { return h; } }

        public override void Rasterize(BitmapRasterizer<DungeonTile> rasterizer, Random rand)
        {
            rasterizer.FillRect(Bounds, new DungeonTile
            {
                TileType = AbyssTemplate.RedSmallChecks
            });


            int numImp = new Range(0, 2).Random(rand);
            int numDemon = new Range(2, 4).Random(rand);
            int numBrute = new Range(1, 4).Random(rand);
            int numSkull = new Range(1, 3).Random(rand);

            var buf = rasterizer.Bitmap;
            var bounds = Bounds;
            while (numImp > 0 || numDemon > 0 || numBrute > 0 || numSkull > 0)
            {
                int x = rand.Next(bounds.X, bounds.MaxX);
                int y = rand.Next(bounds.Y, bounds.MaxY);
                if (buf[x, y].Object != null)
                    continue;

                switch (rand.Next(4))
                {
                    case 0:
                        if (numImp > 0)
                        {
                            buf[x, y].Object = new DungeonObject
                            {
                                ObjectType = AbyssTemplate.AbyssImp
                            };
                            numImp--;
                        }
                        break;
                    case 1:
                        if (numDemon > 0)
                        {
                            buf[x, y].Object = new DungeonObject
                            {
                                ObjectType = AbyssTemplate.AbyssDemon[rand.Next(AbyssTemplate.AbyssDemon.Length)]
                            };
                            numDemon--;
                        }
                        break;
                    case 2:
                        if (numBrute > 0)
                        {
                            buf[x, y].Object = new DungeonObject
                            {
                                ObjectType = AbyssTemplate.AbyssBrute[rand.Next(AbyssTemplate.AbyssBrute.Length)]
                            };
                            numBrute--;
                        }
                        break;
                    case 3:
                        if (numSkull > 0)
                        {
                            buf[x, y].Object = new DungeonObject
                            {
                                ObjectType = AbyssTemplate.AbyssBones
                            };
                            numSkull--;
                        }
                        break;
                }
            }
        }
    }
}