using Editor;
using SharpDX.Direct3D9;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace DivEditor.Controls
{
    internal static class GameData
    {
        public static String pathToDivFolder = "";                          // Путь к корневой папке игры
        public static String pathToEditWorldFolder = "";                    // Путь к сохранению
        public static String pathToTileTexturesFolder = "";                 // Путь к текстурам плиток
        public static String pathToObjectsTexturesFolder = "";              // Путь к текстурам объектов
        public static int worldMapNumber = 0;                               // Номер мира
        public static List<Terrain> terrain = new();                        // Список текстур 
        public static List<MetaObjects> MObjects = new();                   // Список метаобъектов
        public static MetaTile[,] metaTileArray = default!;                 // Список данных World
        public static List<ObjectsDescriptions> objectDesc = new();         // Список описания объектов
        public static List<Objects> objects = new();                        // Список объектов
        public static List<string[]> metaObjHead = new();                   // Группировка объектов для списка
        public static bool READY = false;                                   // Флаг готовности к отображению мира

        public static void Initialize()
        {
            Objects.clearObjectsCount();
            terrain = FileManager.ReadTerrain(Vars.pathTarrainFile);
            objectDesc = FileManager.ReadObjectsInfo(Vars.pathObjectDescFile);
            FileManager.ReadWorldAndObjects(ref metaTileArray, ref objects, pathToEditWorldFolder, worldMapNumber);
            metaObjHead = FileManager.ReadMetaobjectHead(Vars.pathMetaObjHeads);
            FileManager.WriteXMLObjHead(metaObjHead);
            MObjects = FileManager.ReadMetaobject(Vars.pathMetaObjBody);
        }
        public static void TextureMapping(int textures, int MouseStateX, int MouseStateY, int xCor, int yCor)
        {
            int y = MouseStateY / Vars.tileSize + yCor;
            int x = MouseStateX / Vars.tileSize + xCor;
            int quarter = 0;
            if (MouseStateY % Vars.tileSize < Vars.tileSize / 2 && MouseStateX % Vars.tileSize < Vars.tileSize / 2) quarter = 0; // Вычисляем, в какую четверть тайтла наведен курсор
            if (MouseStateY % Vars.tileSize < Vars.tileSize / 2 && MouseStateX % Vars.tileSize > Vars.tileSize / 2) quarter = 1;
            if (MouseStateY % Vars.tileSize > Vars.tileSize / 2 && MouseStateX % Vars.tileSize > Vars.tileSize / 2) quarter = 2;
            if (MouseStateY % Vars.tileSize > Vars.tileSize / 2 && MouseStateX % Vars.tileSize < Vars.tileSize / 2) quarter = 3;
            int[,] tilesDownNew = new int[3, 3];
            int[,] tilesUpNew = new int[3, 3] { { 65535, 65535, 65535 }, { 65535, 65535, 65535 }, { 65535, 65535, 65535 } };
            int[,] tilesEffectNew = new int[3, 3] { { 0, 0, 0 }, { 0, 0, 0 }, { 0, 0, 0 } };
            int[,] tileFilling = new int[3, 3];
            if (textures >= 0 && y > 0 && x > 0 && y < Vars.maxVerticalTails - 2 && x < Vars.maxHorizontalTails - 2) // Проверяем, чтобы изменяемая область текстур не зашла за границы карты и выбрана ли текстура
            {
                if (terrain[textures].Transition != "") // Проверка на тип текстуры (с геометрией или обычная)
                {
                    for (int i = 0; i < 3; i++)
                    {
                        for (int j = 0; j < 3; j++)
                        {
                            tilesDownNew[i, j] = metaTileArray[y + i - 1, x + j - 1].DownTileTexture;
                            for (int k = 0; k < terrain[textures].BaseTileCount; k++)
                            {
                                if (tilesDownNew[i, j] == terrain[textures].getBaseTileDown(k)) tileFilling[i, j] = 1111;
                            }
                            for (int k = 0; k < 16; k++)
                            {
                                for (int t = 0; t < terrain[textures].getTrnsCount(k); t++)
                                {
                                    if (tilesDownNew[i, j] == terrain[textures].getTrnsDown(k, t))
                                    {
                                        tileFilling[i, j] = k switch
                                        {
                                            1 => 1100,
                                            2 => 1000,
                                            3 => 1001,
                                            4 => 1,
                                            5 => 11,
                                            6 => 10,
                                            7 => 110,
                                            8 => 100,
                                            9 => 1101,
                                            10 => 1011,
                                            11 => 111,
                                            12 => 1110,
                                            14 => 101,
                                            15 => 1010,
                                            _ => 0,
                                        };
                                    }
                                }
                            }
                        }
                    }
                    // |_0_|_1_|
                    // |_3_|_2_|
                    if (quarter == 0) tileFilling[1, 1] = (tileFilling[1, 1] / 10) * 10 + 1;
                    if (quarter == 1) tileFilling[1, 1] = (tileFilling[1, 1] / 100) * 100 + tileFilling[1, 1] % 10 + 10;
                    if (quarter == 2) tileFilling[1, 1] = (tileFilling[1, 1] / 1000) * 1000 + tileFilling[1, 1] % 100 + 100;
                    if (quarter == 3) tileFilling[1, 1] = tileFilling[1, 1] % 1000 + 1000;

                    if (tileFilling[1, 1] % 10 == 1) //0
                    {
                        tileFilling[1, 0] = (tileFilling[1, 0] / 100) * 100 + tileFilling[1, 0] % 10 + 10;
                        tileFilling[0, 0] = (tileFilling[0, 0] / 1000) * 1000 + tileFilling[0, 0] % 100 + 100;
                        tileFilling[0, 1] = tileFilling[0, 1] % 1000 + 1000;
                    }
                    if ((tileFilling[1, 1] / 10) % 10 == 1) //1
                    {
                        tileFilling[0, 1] = (tileFilling[0, 1] / 1000) * 1000 + tileFilling[0, 1] % 100 + 100;
                        tileFilling[0, 2] = tileFilling[0, 2] % 1000 + 1000;
                        tileFilling[1, 2] = (tileFilling[1, 2] / 10) * 10 + 1;
                    }
                    if ((tileFilling[1, 1] / 100) % 10 == 1) //2
                    {
                        tileFilling[1, 2] = tileFilling[1, 2] % 1000 + 1000;
                        tileFilling[2, 2] = (tileFilling[2, 2] / 10) * 10 + 1;
                        tileFilling[2, 1] = (tileFilling[2, 1] / 100) * 100 + tileFilling[2, 1] % 10 + 10;
                    }
                    if ((tileFilling[1, 1] / 1000) % 10 == 1) //3
                    {
                        tileFilling[2, 1] = (tileFilling[2, 1] / 10) * 10 + 1;
                        tileFilling[2, 0] = (tileFilling[2, 0] / 100) * 100 + tileFilling[2, 0] % 10 + 10;
                        tileFilling[1, 0] = (tileFilling[1, 0] / 1000) * 1000 + tileFilling[1, 0] % 100 + 100;
                    }
                    for (int i = 0; i < 3; i++)
                    {
                        for (int j = 0; j < 3; j++)
                        {
                            metaTileArray[y + i - 1, x + j - 1].UpTileTexture = 65535;
                            if (tileFilling[i, j] == 1111)
                            {
                                tilesEffectNew[i, j] = terrain[textures].TileEffect;
                                bool a = false;
                                for (int k = 0; k < terrain[textures].BaseTileCount; k++)
                                {
                                    if (tilesDownNew[i, j] == terrain[textures].getBaseTileDown(k)) a = true;
                                }
                                if (!a)
                                {
                                    Random rnd = new();
                                    int r = 0;
                                    if (terrain[textures].BaseTileCount > 0) r = rnd.Next(0, terrain[textures].BaseTileCount - 1);
                                    tilesDownNew[i, j] = terrain[textures].getBaseTileDown(r);
                                    tilesUpNew[i, j] = terrain[textures].getBaseTileUp(r);
                                }
                                else if (EditForm.RandfillingTex)
                                {
                                    Random rnd = new();
                                    int r = 0;
                                    if (terrain[textures].BaseTileCount > 0) r = rnd.Next(0, terrain[textures].BaseTileCount - 1);
                                    tilesDownNew[i, j] = terrain[textures].getBaseTileDown(r);
                                    tilesUpNew[i, j] = terrain[textures].getBaseTileUp(r);
                                }
                            }
                            else
                            {
                                if (tileFilling[i, j] != 0) tilesEffectNew[i, j] = terrain[textures].TileEffect;
                                switch (tileFilling[i, j])
                                {
                                    case 1100: 
                                        {
                                            bool b = false;
                                            for (int q = 0; q < terrain[textures].getTrnsCount(1); q++)
                                            {
                                                if (tilesDownNew[i, j] == terrain[textures].getTrnsDown(1, q))
                                                {
                                                    b = true;
                                                }
                                            }
                                            if (!b || EditForm.RandfillingTex)
                                            {
                                                Random rnd = new();
                                                int r = 0;
                                                if (terrain[textures].getTrnsCount(1) > 0) r = rnd.Next(0, terrain[textures].getTrnsCount(1) - 1);
                                                int a = terrain[textures].getTrnsDown(1, r);
                                                if (a != 0)
                                                {
                                                    tilesDownNew[i, j] = a;
                                                    if (terrain[textures].getTrnsUp(1, r) >= 0)
                                                    {
                                                        tilesUpNew[i, j] = terrain[textures].getTrnsUp(1, r);
                                                    }
                                                }
                                            }
                                            break;
                                        }
                                    case 1000:
                                        {
                                            bool b = false;
                                            for (int q = 0; q < terrain[textures].getTrnsCount(2); q++)
                                            {
                                                if (tilesDownNew[i, j] == terrain[textures].getTrnsDown(2, q))
                                                {
                                                    b = true;
                                                }
                                            }
                                            if (!b || EditForm.RandfillingTex)
                                            {
                                                Random rnd = new();
                                                int r = 0;
                                                if (terrain[textures].getTrnsCount(2) > 0) r = rnd.Next(0, terrain[textures].getTrnsCount(2) - 1);
                                                int a = terrain[textures].getTrnsDown(2, r);
                                                if (a != 0)
                                                {
                                                    tilesDownNew[i, j] = a;
                                                    if (terrain[textures].getTrnsUp(2, r) >= 0)
                                                    {
                                                        tilesUpNew[i, j] = terrain[textures].getTrnsUp(2, r);
                                                    }
                                                }
                                            }
                                            break;
                                        }
                                    case 1001:
                                        {
                                            bool b = false;
                                            for (int q = 0; q < terrain[textures].getTrnsCount(3); q++)
                                            {
                                                if (tilesDownNew[i, j] == terrain[textures].getTrnsDown(3, q))
                                                {
                                                    b = true;
                                                }
                                            }
                                            if (!b || EditForm.RandfillingTex)
                                            {
                                                Random rnd = new();
                                                int r = 0;
                                                if (terrain[textures].getTrnsCount(3) > 0) r = rnd.Next(0, terrain[textures].getTrnsCount(3) - 1);
                                                int a = terrain[textures].getTrnsDown(3, r);
                                                if (a != 0)
                                                {
                                                    tilesDownNew[i, j] = a;
                                                    if (terrain[textures].getTrnsUp(3, r) >= 0)
                                                    {
                                                        tilesUpNew[i, j] = terrain[textures].getTrnsUp(3, r);
                                                    }
                                                }
                                            }
                                            break;
                                        }
                                    case 1:
                                        {
                                            bool b = false;
                                            for (int q = 0; q < terrain[textures].getTrnsCount(4); q++)
                                            {
                                                if (tilesDownNew[i, j] == terrain[textures].getTrnsDown(4, q))
                                                {
                                                    b = true;
                                                }
                                            }
                                            if (!b || EditForm.RandfillingTex)
                                            {
                                                Random rnd = new();
                                                int r = 0;
                                                if (terrain[textures].getTrnsCount(4) > 0) r = rnd.Next(0, terrain[textures].getTrnsCount(4) - 1);
                                                int a = terrain[textures].getTrnsDown(4, r);
                                                if (a != 0)
                                                {
                                                    tilesDownNew[i, j] = a;
                                                    if (terrain[textures].getTrnsUp(4, r) >= 0)
                                                    {
                                                        tilesUpNew[i, j] = terrain[textures].getTrnsUp(4, r);
                                                    }
                                                }
                                            }
                                            break;
                                        }
                                    case 11:
                                        {
                                            bool b = false;
                                            for (int q = 0; q < terrain[textures].getTrnsCount(5); q++)
                                            {
                                                if (tilesDownNew[i, j] == terrain[textures].getTrnsDown(5, q))
                                                {
                                                    b = true;
                                                }
                                            }
                                            if (!b || EditForm.RandfillingTex)
                                            {
                                                Random rnd = new();
                                                int r = 0;
                                                if (terrain[textures].getTrnsCount(5) > 0) r = rnd.Next(0, terrain[textures].getTrnsCount(5) - 1);
                                                int a = terrain[textures].getTrnsDown(5, r);
                                                if (a != 0)
                                                {
                                                    tilesDownNew[i, j] = a;
                                                    if (terrain[textures].getTrnsUp(5, r) >= 0)
                                                    {
                                                        tilesUpNew[i, j] = terrain[textures].getTrnsUp(5, r);
                                                    }
                                                }
                                            }
                                            break;
                                        }
                                    case 10:
                                        {
                                            bool b = false;
                                            for (int q = 0; q < terrain[textures].getTrnsCount(6); q++)
                                            {
                                                if (tilesDownNew[i, j] == terrain[textures].getTrnsDown(6, q))
                                                {
                                                    b = true;
                                                }
                                            }
                                            if (!b || EditForm.RandfillingTex)
                                            {
                                                Random rnd = new();
                                                int r = 0;
                                                if (terrain[textures].getTrnsCount(6) > 0) r = rnd.Next(0, terrain[textures].getTrnsCount(6) - 1);
                                                int a = terrain[textures].getTrnsDown(6, r);
                                                if (a != 0)
                                                {
                                                    tilesDownNew[i, j] = a;
                                                    if (terrain[textures].getTrnsUp(6, r) >= 0)
                                                    {
                                                        tilesUpNew[i, j] = terrain[textures].getTrnsUp(6, r);
                                                    }
                                                }
                                            }
                                            break;
                                        }
                                    case 110:
                                        {
                                            bool b = false;
                                            for (int q = 0; q < terrain[textures].getTrnsCount(7); q++)
                                            {
                                                if (tilesDownNew[i, j] == terrain[textures].getTrnsDown(7, q))
                                                {
                                                    b = true;
                                                }
                                            }
                                            if (!b || EditForm.RandfillingTex)
                                            {
                                                Random rnd = new();
                                                int r = 0;
                                                if (terrain[textures].getTrnsCount(7) > 0) r = rnd.Next(0, terrain[textures].getTrnsCount(7) - 1);
                                                int a = terrain[textures].getTrnsDown(7, r);
                                                if (a != 0)
                                                {
                                                    tilesDownNew[i, j] = a;
                                                    if (terrain[textures].getTrnsUp(7, r) >= 0)
                                                    {
                                                        tilesUpNew[i, j] = terrain[textures].getTrnsUp(7, r);
                                                    }
                                                }
                                            }
                                            break;
                                        }
                                    case 100:
                                        {
                                            bool b = false;
                                            for (int q = 0; q < terrain[textures].getTrnsCount(8); q++)
                                            {
                                                if (tilesDownNew[i, j] == terrain[textures].getTrnsDown(8, q))
                                                {
                                                    b = true;
                                                }
                                            }
                                            if (!b || EditForm.RandfillingTex)
                                            {
                                                Random rnd = new();
                                                int r = 0;
                                                if (terrain[textures].getTrnsCount(8) > 0) r = rnd.Next(0, terrain[textures].getTrnsCount(8) - 1);
                                                int a = terrain[textures].getTrnsDown(8, r);
                                                if (a != 0)
                                                {
                                                    tilesDownNew[i, j] = a;
                                                    if (terrain[textures].getTrnsUp(8, r) >= 0)
                                                    {
                                                        tilesUpNew[i, j] = terrain[textures].getTrnsUp(8, r);
                                                    }
                                                }
                                            }
                                            break;
                                        }
                                    case 1101:
                                        {
                                            bool b = false;
                                            for (int q = 0; q < terrain[textures].getTrnsCount(9); q++)
                                            {
                                                if (tilesDownNew[i, j] == terrain[textures].getTrnsDown(9, q))
                                                {
                                                    b = true;
                                                }
                                            }
                                            if (!b || EditForm.RandfillingTex)
                                            {
                                                Random rnd = new();
                                                int r = 0;
                                                if (terrain[textures].getTrnsCount(9) > 0) r = rnd.Next(0, terrain[textures].getTrnsCount(9) - 1);
                                                int a = terrain[textures].getTrnsDown(9, r);
                                                if (a != 0)
                                                {
                                                    tilesDownNew[i, j] = a;
                                                    if (terrain[textures].getTrnsUp(9, r) >= 0)
                                                    {
                                                        tilesUpNew[i, j] = terrain[textures].getTrnsUp(9, r);
                                                    }
                                                }
                                            }
                                            break;
                                        }
                                    case 1011:
                                        {
                                            bool b = false;
                                            for (int q = 0; q < terrain[textures].getTrnsCount(10); q++)
                                            {
                                                if (tilesDownNew[i, j] == terrain[textures].getTrnsDown(10, q))
                                                {
                                                    b = true;
                                                }
                                            }
                                            if (!b || EditForm.RandfillingTex)
                                            {
                                                Random rnd = new();
                                                int r = 0;
                                                if (terrain[textures].getTrnsCount(10) > 0) r = rnd.Next(0, terrain[textures].getTrnsCount(10) - 1);
                                                int a = terrain[textures].getTrnsDown(10, r);
                                                if (a != 0)
                                                {
                                                    tilesDownNew[i, j] = a;
                                                    if (terrain[textures].getTrnsUp(10, r) >= 0)
                                                    {
                                                        tilesUpNew[i, j] = terrain[textures].getTrnsUp(10, r);
                                                    }
                                                }
                                            }
                                            break;
                                        }
                                    case 111:
                                        {
                                            bool b = false;
                                            for (int q = 0; q < terrain[textures].getTrnsCount(11); q++)
                                            {
                                                if (tilesDownNew[i, j] == terrain[textures].getTrnsDown(11, q))
                                                {
                                                    b = true;
                                                }
                                            }
                                            if (!b || EditForm.RandfillingTex)
                                            {
                                                Random rnd = new();
                                                int r = 0;
                                                if (terrain[textures].getTrnsCount(11) > 0) r = rnd.Next(0, terrain[textures].getTrnsCount(11) - 1);
                                                int a = terrain[textures].getTrnsDown(11, r);
                                                if (a != 0)
                                                {
                                                    tilesDownNew[i, j] = a;
                                                    if (terrain[textures].getTrnsUp(11, r) >= 0)
                                                    {
                                                        tilesUpNew[i, j] = terrain[textures].getTrnsUp(11, r);
                                                    }
                                                }
                                            }
                                            break;
                                        }
                                    case 1110:
                                        {
                                            bool b = false;
                                            for (int q = 0; q < terrain[textures].getTrnsCount(12); q++)
                                            {
                                                if (tilesDownNew[i, j] == terrain[textures].getTrnsDown(12, q))
                                                {
                                                    b = true;
                                                }
                                            }
                                            if (!b || EditForm.RandfillingTex)
                                            {
                                                Random rnd = new();
                                                int r = 0;
                                                if (terrain[textures].getTrnsCount(12) > 0) r = rnd.Next(0, terrain[textures].getTrnsCount(12) - 1);
                                                int a = terrain[textures].getTrnsDown(12, r);
                                                if (a != 0)
                                                {
                                                    tilesDownNew[i, j] = a;
                                                    if (terrain[textures].getTrnsUp(12, r) >= 0)
                                                    {
                                                        tilesUpNew[i, j] = terrain[textures].getTrnsUp(12, r);
                                                    }
                                                }
                                            }
                                            break;
                                        }
                                    case 101:
                                        {
                                            bool b = false;
                                            for (int q = 0; q < terrain[textures].getTrnsCount(13); q++)
                                            {
                                                if (tilesDownNew[i, j] == terrain[textures].getTrnsDown(13, q))
                                                {
                                                    b = true;
                                                }
                                            }
                                            if (!b || EditForm.RandfillingTex)
                                            {
                                                Random rnd = new();
                                                int r = 0;
                                                if (terrain[textures].getTrnsCount(13) > 0) r = rnd.Next(0, terrain[textures].getTrnsCount(13) - 1);
                                                int a = terrain[textures].getTrnsDown(13, r);
                                                if (a != 0)
                                                {
                                                    tilesDownNew[i, j] = a;
                                                    if (terrain[textures].getTrnsUp(13, r) >= 0)
                                                    {
                                                        tilesUpNew[i, j] = terrain[textures].getTrnsUp(13, r);
                                                    }
                                                }
                                            }
                                            break;
                                        }
                                    case 1010:
                                        {
                                            bool b = false;
                                            for (int q = 0; q < terrain[textures].getTrnsCount(14); q++)
                                            {
                                                if (tilesDownNew[i, j] == terrain[textures].getTrnsDown(14, q))
                                                {
                                                    b = true;
                                                }
                                            }
                                            if (!b || EditForm.RandfillingTex)
                                            {
                                                Random rnd = new();
                                                int r = 0;
                                                if (terrain[textures].getTrnsCount(14) > 0) r = rnd.Next(0, terrain[textures].getTrnsCount(14) - 1);
                                                int a = terrain[textures].getTrnsDown(14, r);
                                                if (a != 0)
                                                {
                                                    tilesDownNew[i, j] = a;
                                                    if (terrain[textures].getTrnsUp(14, r) >= 0)
                                                    {
                                                        tilesUpNew[i, j] = terrain[textures].getTrnsUp(14, r);
                                                    }
                                                }
                                            }
                                            break;
                                        }
                                }
                            }
                            metaTileArray[y + i - 1, x + j - 1].DownTileTexture = tilesDownNew[i, j];
                            if (tilesUpNew[i, j] < 65535) metaTileArray[y + i - 1, x + j - 1].UpTileTexture = tilesUpNew[i, j];
                            metaTileArray[y + i - 1, x + j - 1].TileEffects = tilesEffectNew[i, j];
                        }
                    }
                }
                else
                {
                    if (!EditForm.BigfillingTex) // Если кисть 1х1
                    {
                        if (!EditForm.RandfillingTex) // Рандом отключен
                        {
                            bool a = false;
                            for (int k = 0; k < terrain[textures].BaseTileCount; k++)
                            {
                                if (metaTileArray[y, x].DownTileTexture == terrain[textures].getBaseTileDown(k)) a = true;
                            }
                            if (!a)
                            {
                                Random rnd = new();
                                int r = 0;
                                if (terrain[textures].BaseTileCount > 0) r = rnd.Next(0, terrain[textures].BaseTileCount - 1);
                                metaTileArray[y, x].DownTileTexture = terrain[textures].getBaseTileDown(r);
                                metaTileArray[y, x].UpTileTexture = 65535;
                                metaTileArray[y, x].TileEffects = terrain[textures].TileEffect;
                            }
                            else
                            {
                                metaTileArray[y, x].UpTileTexture = 65535;
                                metaTileArray[y, x].TileEffects = terrain[textures].TileEffect;
                            }
                        }
                        else // Рандом включен
                        {
                            Random rnd = new();
                            int r = 0;
                            if (terrain[textures].BaseTileCount > 0) r = rnd.Next(0, terrain[textures].BaseTileCount - 1);
                            metaTileArray[y, x].DownTileTexture = terrain[textures].getBaseTileDown(r);
                            metaTileArray[y, x].UpTileTexture = 65535;
                            metaTileArray[y, x].TileEffects = terrain[textures].TileEffect;
                        }
                    }
                    else // Если кисть 3х3
                    if (!EditForm.RandfillingTex)  // Рандом отключен
                    {
                        for (int i = 0; i < 3; i++)
                        {
                            for (int j = 0; j < 3; j++)
                            {
                                bool a = false;
                                for (int k = 0; k < terrain[textures].BaseTileCount; k++)
                                {
                                    if (metaTileArray[y + i - 1, x + j - 1].DownTileTexture == terrain[textures].getBaseTileDown(k)) a = true;
                                }
                                if (!a)
                                {
                                    Random rnd = new();
                                    int r = 0;
                                    if (terrain[textures].BaseTileCount > 0) r = rnd.Next(0, terrain[textures].BaseTileCount - 1);
                                    metaTileArray[y + i - 1, x + j - 1].DownTileTexture = terrain[textures].getBaseTileDown(r);
                                    metaTileArray[y + i - 1, x + j - 1].UpTileTexture = 65535;
                                    metaTileArray[y + i - 1, x + j - 1].TileEffects = terrain[textures].TileEffect;
                                }
                                else
                                {
                                    metaTileArray[y + i - 1, x + j - 1].UpTileTexture = 65535;
                                    metaTileArray[y + i - 1, x + j - 1].TileEffects = terrain[textures].TileEffect;
                                }
                            }
                        }
                    }
                    else // Рандом включен
                    {
                        for (int i = 0; i < 3; i++)
                        {
                            for (int j = 0; j < 3; j++)
                            {
                                Random rnd = new();
                                int r = 0;
                                if (terrain[textures].BaseTileCount > 0) r = rnd.Next(0, terrain[textures].BaseTileCount - 1);
                                metaTileArray[y + i - 1, x + j - 1].DownTileTexture = terrain[textures].getBaseTileDown(r);
                                metaTileArray[y + i - 1, x + j - 1].UpTileTexture = 65535;
                                metaTileArray[y + i - 1, x + j - 1].TileEffects = terrain[textures].TileEffect;
                            }
                        }
                    }
                }
            }
        }
        public static void EffectsMapping(int MouseStateX, int MouseStateY, int xCor, int yCor, int effect)
        {
            int y = MouseStateY / Vars.tileSize + yCor;
            int x = MouseStateX / Vars.tileSize + xCor;
            if (effect == 0)
            {
                metaTileArray[y, x].TileEffects = 0;
            }
            else
            {
                if (metaTileArray[y, x].TileEffects != effect)
                {
                    if ((MouseStateY % Vars.tileSize) + (MouseStateX % Vars.tileSize) < Vars.tileSize) // Если курсор в верхней диагонали
                    {
                        if ((metaTileArray[y, x].TileEffects & (1 << Vars.tileEffectByteNumber_DownDiagonal)) != 0) // Если уже есть нижняя диагональ
                        {
                            metaTileArray[y, x].TileEffects = effect;
                        }
                        else
                        {
                            metaTileArray[y, x].TileEffects = effect | Vars.tileEffect_UpDiagonal;
                        }
                    }
                    else // Если курсор в нижней диагонали
                    {
                        if ((metaTileArray[y, x].TileEffects & (1 << Vars.tileEffectByteNumber_UpDiagonal)) != 0) // Если уже есть верхняя диагональ
                        {
                            metaTileArray[y, x].TileEffects = effect;
                        }
                        else
                        {
                            metaTileArray[y, x].TileEffects = effect | Vars.tileEffect_DownDiagonal;
                        }
                    }
                }
            }
        }
    }
}
