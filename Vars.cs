using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DivEditor.Controls
{
    internal static class Vars
    {
        public const String pathConfigFile = @"Editor\config.ini";
        public const String pathTarrainFile = @"Editor\editor_tiles.dat";
        public const String pathObjectDescFile = @"Editor\editor_objects_descriptions.dat";
        public const String pathMetaObjHeads = @"Editor\editor_metaobjects_head.dat";
        public const String pathMetaObjBody = @"Editor\editor_metaobjects_body.dat";
        public const String xmlMetObjHead = @"Editor\editor_MOHead.xml";
        public const String dirDataFile = @"Editor\DataFile";
        public const String pathAgentClassesName = @"Editor\editor_NPCClass_name.dat";
        public const String dirAgentClassesImg = @"Content\units";
        public const int maxObjectsCount = 11264;
        public const int maxObjectsCountInTile = 14;
        public const int maxVerticalTails = 1024;
        public const int maxHorizontalTails = 512;
        public const int tileSize = 64;
        public const int maxObjectHeight = 255;
        public const int saveWorldImageScale = 4; // Масштаб сохраняемого изображения мира (2 и более)
        public const int texturesMaximumValue = 65535; // Текстура не может занимать более 2х байт памяти
        public const int tileEffectsValueMax = 127; // Максимальная величина значения эффекта плитки
        public static readonly int[] tileEffects = new int[7] { 
            0x00000001, // 
            0x00000010, // Вода
            0x00000100, // Помещение
            0x00001000, // Туман
            0x00010000, // Верхняя диагональ
            0x00100000, // Нижняя диагональ
            0x01000000, // Квест
        };
        public const int tileEffectByteNumber_Water = 1;
        public const int tileEffectByteNumber_Indoors = 2;
        public const int tileEffectByteNumber_Fog = 3;
        public const int tileEffectByteNumber_UpDiagonal = 4;
        public const int tileEffectByteNumber_DownDiagonal = 5;
        public const int tileEffectByteNumber_Object = 6;

        public const int tileEffect_Water = 2;
        public const int tileEffect_Indoors = 4;
        public const int tileEffect_Fog = 8;
        public const int tileEffect_UpDiagonal = 16;
        public const int tileEffect_DownDiagonal = 32;
        public const int tileEffect_Object = 64;
    }
}
