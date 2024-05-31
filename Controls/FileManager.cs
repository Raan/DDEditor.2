using SharpDX.Direct3D9;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Editor.Controls;
using Editor;
using System.IO.Compression;
using System.Runtime.ConstrainedExecution;
using static System.Windows.Forms.LinkLabel;
using System.Xml.Linq;
using static System.Net.Mime.MediaTypeNames;
using System.IO;
using Lzo64;
using System.ComponentModel;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Graphics.PackedVector;
using System.Windows.Forms;
using System.Reflection;
using SharpDX.DirectWrite;

namespace DivEditor.Controls
{
    internal static class FileManager
    {
        //------------------------------------------------------------------------------------------------------------------------
        public static bool ReadConfig(string inpFile)
        {
            if (File.Exists(inpFile))
            {
                string[] lines = File.ReadLines(inpFile).ToArray();
                if (lines.Length == 8)
                {
                    GameData.pathToDivFolder = lines[3];
                    GameData.pathToEditWorldFolder = lines[5];
                    GameData.worldMapNumber = int.Parse(lines[7]);
                    GameData.pathToTileTexturesFolder = GameData.pathToDivFolder + "\\static\\imagelists\\CPackedi.2c";
                    GameData.pathToObjectsTexturesFolder = GameData.pathToDivFolder + "\\static\\imagelists\\CPackedi.0c";
                    return true;
                }
                else
                {
                    File.Delete(inpFile);
                    return false;
                }
            }
            else
            {
                bool fileRead = false;
                while (!fileRead)
                {
                    OpenFileDialog openFileDialog1 = new()
                    {
                        InitialDirectory = @"C:\",
                        Title = "Browse Files",
                        CheckFileExists = true,
                        CheckPathExists = true,
                        DefaultExt = "data",
                        Filter = "div.exe (*.exe)|*.exe",
                        FilterIndex = 2,
                        RestoreDirectory = true,
                        ReadOnlyChecked = true,
                        ShowReadOnly = true
                    };
                    if (openFileDialog1.ShowDialog() == DialogResult.OK)
                    {
                        String path = openFileDialog1.FileName;
                        if (path.Remove(0, path.Length - 7) == "div.exe")
                        {
                            GameData.pathToDivFolder = path.Remove(path.Length - 8, 8);
                            fileRead = true;
                        }
                    }
                }
                fileRead = false;
                while (!fileRead)
                {
                    OpenFileDialog openFileDialog1 = new()
                    {
                        InitialDirectory = @"C:\",
                        Title = "Browse Files",
                        CheckFileExists = true,
                        CheckPathExists = true,
                        DefaultExt = "data",
                        Filter = "world files (*.x)|*.x*",
                        FilterIndex = 2,
                        RestoreDirectory = true,
                        ReadOnlyChecked = true,
                        ShowReadOnly = true
                    };
                    if (openFileDialog1.ShowDialog() == DialogResult.OK)
                    {
                        String path = openFileDialog1.FileName;
                        //F:\SteamLibrary\steamapps\common\divine_divinity\savegames\test_3\world.x0
                        if (path.Remove(0, path.Length - 8) == "world.x0" ||
                            path.Remove(0, path.Length - 8) == "world.x1" ||
                            path.Remove(0, path.Length - 8) == "world.x2" ||
                            path.Remove(0, path.Length - 8) == "world.x3" ||
                            path.Remove(0, path.Length - 8) == "world.x4")
                        {
                            GameData.pathToEditWorldFolder = path.Remove(path.Length - 9, 9);
                            GameData.worldMapNumber = int.Parse(path.Remove(0, path.Length - 1));
                            GameData.pathToTileTexturesFolder = GameData.pathToDivFolder + "\\static\\imagelists\\CPackedi.2c";
                            GameData.pathToObjectsTexturesFolder = GameData.pathToDivFolder + "\\static\\imagelists\\CPackedi.0c";
                            fileRead = true;
                        }
                    }
                }
                WriteConfig();
                return true;
            }
        }
        //------------------------------------------------------------------------------------------------------------------------
        public static void WriteConfig()
        {
            String[] lines = new string[8];
            lines[0] = "//            line order is important, do not edit this file!";
            lines[1] = "";
            lines[2] = "// Game directory";
            lines[3] = GameData.pathToDivFolder;
            lines[4] = "// Savegame or main direct";
            lines[5] = GameData.pathToEditWorldFolder;
            lines[6] = "// World";
            lines[7] = GameData.worldMapNumber.ToString();
            File.WriteAllLines("Editor\\config.ini", lines);
        }
        //------------------------------------------------------------------------------------------------------------------------
        public static List<ObjectsDescriptions> ReadObjectsInfo(string inpFile)
        {
            List<ObjectsDescriptions> objDesc = new();
            using StreamReader reader = new(inpFile);
            string line;
            string[] words;
            while ((line = reader.ReadLine()) != null)
            {
                words = line.Split(new char[] { '|' });
                objDesc.Add(new ObjectsDescriptions(words[0],                                                          // SpriteID
                                                    Convert.ToInt32(words[2]),                                         // Height
                                                    Convert.ToInt32(words[1]),                                         // Width
                                                    new Point(Convert.ToInt32(words[3]), Convert.ToInt32(words[4])),   // touchPoint
                                                    Convert.ToInt32(words[5]),                                         // var_0
                                                    Convert.ToInt32(words[6]),                                         // var_1
                                                    Convert.ToInt32(words[7])));                                       // var_2
            }
            return objDesc;
        }
        //------------------------------------------------------------------------------------------------------------------------
        public static void ReadWorldAndObjects(ref MetaTile[,] MTA, ref List<Objects> OBJ, string inpDir, int ext)
        {
            int lineLength = 28; // Длинна строки одного объекта в байтах
            long objCount = 0;
            if (File.Exists(inpDir + "\\objects.x" + ext)) // Узнаем количество объектов в файле objects.x
            {
                System.IO.FileInfo file = new(inpDir + "\\objects.x" + ext);
                objCount = file.Length / lineLength;
                System.Diagnostics.Debug.WriteLine("objCount in Objects file " + objCount);
            }
            else System.Diagnostics.Debug.WriteLine(inpDir + "\\objects.x" + ext + " не найден");
            List<Objects> objсt = new();
            if (File.Exists(inpDir + "\\objects.x" + ext))
            {
                using BinaryReader obj = new(File.Open(inpDir + "\\objects.x" + ext, FileMode.Open));
                for (long i = 0; i < objCount; i++)
                {
                    objсt.Add(new Objects(obj.ReadUInt16(),                                  // var_0
                                           obj.ReadUInt16(),                                 // var_1
                                           obj.ReadUInt16(),                                 // var_2
                                           obj.ReadUInt16(),                                 // var_3
                                           obj.ReadUInt16(),                                 // var_4
                                           obj.ReadUInt16(),                                 // var_5
                                           obj.ReadUInt16(),                                 // var_6
                                           obj.ReadUInt16(),                                 // var_7
                                           obj.ReadUInt16(),                                 // var_8
                                           obj.ReadUInt16(),                                 // var_9
                                           new Point(obj.ReadUInt16(), obj.ReadUInt16()),    // PixelPos
                                           obj.ReadUInt16(),                                 // var_10
                                           obj.ReadUInt16()));                               // SpriteID
                }
            }
            else System.Diagnostics.Debug.WriteLine(inpDir + "\\objects.x" + ext + " не найден");
            MetaTile[,] metaArray = new MetaTile[Vars.maxVerticalTails, Vars.maxHorizontalTails];
            int buf1;
            int upt, downt, num, eff, var1, var2;
            int objCount_2 = 0;
            int a = 0;
            if (File.Exists(inpDir + "/world.x" + ext))
            {
                using BinaryReader world = new(File.Open(inpDir + "\\world.x" + ext, FileMode.Open));
                byte[] checksumAll = world.ReadBytes(4096);
                for (int y = 0; y < Vars.maxVerticalTails; y++)
                {
                    byte[] checksum = world.ReadBytes(1024);
                    for (int x = 0; x < Vars.maxHorizontalTails; x++)
                    {
                        downt = world.ReadUInt16();     //Нижние текстуры
                        upt = world.ReadUInt16();       //Верхние текстуры
                        buf1 = world.ReadUInt16();
                        num = world.ReadByte();         //Количество объектов
                        eff = world.ReadUInt16();       //Эффекты плитки
                        buf1 = world.ReadByte();
                        var1 = world.ReadUInt16();      //var1
                        var2 = world.ReadUInt16();      //var2
                        buf1 = world.ReadUInt16();
                        metaArray[y, x] = new MetaTile(downt, upt, eff, var1, var2);
                        objCount_2 += num;
                        for (int i = 0; i < num; i++)
                        {
                            byte[] objectByte = world.ReadBytes(8);
                            int objNum = objectByte[1] / 16 + objectByte[2] * 16 + objectByte[3] * 4096;    // Мировой номер объекта
                            objсt[objNum].Height = objectByte[4];                                           // Высота объекта
                            objсt[objNum].TilePosition = new Point(x, y);                                   // Положение на сетке плиток
                            objсt[objNum].Effect = objectByte[7];                                           // Еффект объекта
                            metaArray[y, x].AddObject(objNum);
                            if (objNum > a) a = objNum;
                        }
                    }
                }
                System.Diagnostics.Debug.WriteLine("objCount in World file " + objCount_2);
                System.Diagnostics.Debug.WriteLine("max object world number in World file " + a);
            }
            else System.Diagnostics.Debug.WriteLine(inpDir + "\\world.x" + ext + " не найден");
            OBJ = objсt;
            MTA = metaArray;
        }
        //------------------------------------------------------------------------------------------------------------------------
        public static void SaveWorldAndObjects(MetaTile[,] MTA, List<Objects> OBJ, string outDir, int ext, int count)
        {
            using (BinaryWriter writer = new BinaryWriter(File.Open(outDir + "\\objects.x" + ext, FileMode.Create)))
            {
                for (int i = 0; i < count; i++)
                {
                    if (OBJ[i].Exists) // Если объект существует
                    {
                        writer.Write(Convert.ToUInt16(OBJ[i].Var_0));
                        writer.Write(Convert.ToUInt16(OBJ[i].Var_1));
                        writer.Write(Convert.ToUInt16(OBJ[i].Var_2));
                        writer.Write(Convert.ToUInt16(OBJ[i].Var_3));
                        writer.Write(Convert.ToUInt16(OBJ[i].Var_4));
                        writer.Write(Convert.ToUInt16(OBJ[i].Var_5));
                        writer.Write(Convert.ToUInt16(OBJ[i].Var_6));
                        writer.Write(Convert.ToUInt16(OBJ[i].Var_7));
                        writer.Write(Convert.ToUInt16(OBJ[i].Var_8));
                        writer.Write(Convert.ToUInt16(OBJ[i].Var_9));
                        writer.Write(Convert.ToUInt16(OBJ[i].AbsolutePixelPosition.X));
                        writer.Write(Convert.ToUInt16(OBJ[i].AbsolutePixelPosition.Y));
                        writer.Write(Convert.ToUInt16(OBJ[i].Var_10));
                        writer.Write(Convert.ToUInt16(OBJ[i].SpriteID));
                    }
                    else // Если объект удален
                    {
                        for (int l = 0; l < 14; l++)
                        {
                            writer.Write(Convert.ToUInt16(65535));
                        }
                    }
                }
            }
            using (BinaryWriter writer = new BinaryWriter(File.Open(outDir + "\\world.x" + ext, FileMode.Create)))
            {
                int bufStart = 4096;
                writer.Write(4096);
                for (int i = 0; i < Vars.maxVerticalTails - 1; i++)
                {
                    for (int j = 0; j < Vars.maxHorizontalTails; j++)
                    {
                        bufStart += 18 + MTA[i,j].GetObjectsCount() * 8;
                    }
                    writer.Write(bufStart);
                }
                ushort bufStartSmall = 0;
                for (int i = 0; i < Vars.maxVerticalTails; i++)
                {
                    writer.Write(Convert.ToUInt16(0));
                    for (int j = 0; j < Vars.maxHorizontalTails - 1; j++)
                    {
                        bufStartSmall += Convert.ToUInt16(16 + MTA[i, j].GetObjectsCount() * 8);
                        writer.Write(bufStartSmall);
                    }
                    bufStartSmall = 0;
                    for (int j = 0; j < Vars.maxHorizontalTails; j++)
                    {
                        writer.Write(Convert.ToUInt16(MTA[i, j].DownTileTexture));
                        writer.Write(Convert.ToUInt16(MTA[i, j].UpTileTexture));
                        writer.Write(Convert.ToUInt16(0));
                        writer.Write(Convert.ToByte(MTA[i, j].GetObjectsCount()));
                        writer.Write(Convert.ToUInt16(MTA[i, j].TileEffects));
                        writer.Write((byte)0);
                        writer.Write(Convert.ToUInt16(MTA[i, j].UnknownVar_1));
                        writer.Write(Convert.ToUInt16(MTA[i, j].UnknownVar_2));
                        writer.Write(Convert.ToUInt16(0));
                        for (int k = 0; k < MTA[i, j].GetObjectsCount(); k++)
                        {
                            int XY = OBJ[MTA[i, j].GetObject(k)].PixelPositionInTile.Y * 64 + OBJ[MTA[i, j].GetObject(k)].PixelPositionInTile.X;
                            int Y = XY / 256;
                            int X = XY % 256;
                            int c = MTA[i, j].GetObject(k) / 4096;
                            int b = (MTA[i, j].GetObject(k) - c * 4096) / 16;
                            int a = (MTA[i, j].GetObject(k) - c * 4096) - b * 16;
                            a = a * 16 + Y;
                            writer.Write((byte)X);
                            writer.Write((byte)a);
                            writer.Write((byte)b);
                            writer.Write((byte)c);
                            writer.Write((byte)OBJ[MTA[i, j].GetObject(k)].Height);
                            writer.Write((byte)(OBJ[MTA[i, j].GetObject(k)].SpriteID % 64 * 4));
                            writer.Write((byte)(OBJ[MTA[i, j].GetObject(k)].SpriteID / 64));
                            writer.Write((byte)OBJ[MTA[i, j].GetObject(k)].Effect);
                        }
                    }
                }
                writer.Write(103);
            }
        }
        //------------------------------------------------------------------------------------------------------------------------
        public static List<Terrain> ReadTerrain(string inpFile) // Читаем информацию о текстурах
        {
            List<Terrain> ter = new();
            using StreamReader reader = new(inpFile);
            string line;
            bool terrain = true;
            int terCount = -1;
            string[] words;
            while ((line = reader.ReadLine()) != null && terrain)
            {
                words = line.Split(new char[] { ' ' });
                if (words[0] == "endsection" && words[1] == "terrain") terrain = false;
                if (words[0] == "startdef" && words[1] == "terrain")
                {
                    terCount++;
                    ter.Add(new Terrain());
                    ter[terCount].TerrName = words[2];
                }
                if (words[0] == "transition") ter[terCount].Transition =words[1];
                if (words[0] == "system") ter[terCount].System = int.Parse(words[1]);
                if (words[0] == "tileEffect") ter[terCount].TileEffect = byte.Parse(words[1]);
                if (words[0] == "tile" && words[1] == "base")
                {
                    if (int.Parse(words[5]) > -1)
                    {
                        ter[terCount].addBaseTile(int.Parse(words[3]), int.Parse(words[5]));
                    }
                    else ter[terCount].addBaseTile(int.Parse(words[3]), -1);
                }
                if (words[0] == "tile" && words[1] == "transition")
                {
                    if (int.Parse(words[7]) > -1)
                    {
                        ter[terCount].addTrns(int.Parse(words[3]), int.Parse(words[5]), int.Parse(words[7]));
                    }
                    else ter[terCount].addTrns(int.Parse(words[3]), int.Parse(words[5]), -1);
                }
            }
            return ter;
        }
        //------------------------------------------------------------------------------------------------------------------------
        public static List<MetaObjects> ReadMetaobject(string inpFile)
        {
            List<MetaObjects> MO = new();
            string line;
            string[] words;
            List<int> num = new List<int>();
            List<string[]> nam = new List<string[]>();
            using StreamReader reader = new(inpFile);
            while ((line = reader.ReadLine()) != null)
            {
                words = line.Split(new char[] { ' ' });
                if (words[0] == "startdef" && words[1] == "metaobject")
                {
                    MO.Add(new MetaObjects());
                    MO[^1].type = words[2];
                }
                if (words[0] == "group") MO[^1].group = words[1];
                if (words[0] == "location") MO[^1].location = words[1];
                if (words[0] == "walltype")
                {
                    if (words.Count() > 2)
                    {
                        MO[^1].walltype[0] = words[1];
                        MO[^1].walltype[1] = words[2];
                    }
                    else
                    {
                        MO[^1].walltype[0] = words[1];
                        MO[^1].walltype[1] = "";
                    }
                }
                if (words[0] == "object")
                {
                    MO[^1].AddObject(new Object(
                        int.Parse(words[1]),
                        int.Parse(words[2]),
                        int.Parse(words[3]),
                        int.Parse(words[4])));
                }
            }
            return MO;
        }
        //------------------------------------------------------------------------------------------------------------------------
        public static List<string[]> ReadMetaobjectHead(string inpFile)
        {
            using StreamReader reader = new(inpFile);
            string line;
            string[] words;
            List<string[]> nam = new List<string[]>();
            while ((line = reader.ReadLine()) != null)
            {
                words = line.Split(new char[] { '	' });
                string[] buf = new string[11];
                for (int i = 0; i < 11; i++)
                {
                    if (i < words.Length)
                    {
                        buf[i] = "" + words[i];
                    }
                    else buf[i] = "";
                }
                nam.Add(buf);
            }
            return nam;
        }
        //------------------------------------------------------------------------------------------------------------------------
        public static void WriteXMLObjHead(List<string[]> line)
        {
            List<string> xml = new();
            xml.Add("<?xml version=\"1.0\" encoding=\"utf-8\" ?>");
            xml.Add("<empty_node>");
            List<int> buf = new();
            for (int i = 0; i < line.Count; i++)
            {
                for (int j = 10; j > 0; j--)
                {
                    if (line[i][j].Length > 0)
                    {
                        for (int k = buf.Count - 1; k >= 0; k--)
                        {
                            if (buf[k] >= j)
                            {
                                xml.Add("</node>");
                                for (int t = 0; t < buf[k]; t++)
                                {
                                    xml[^1] = "\t" + xml[^1];
                                }
                                buf.RemoveAt(k);
                            }
                        }
                        xml.Add("<node name = \"" + line[i][j] + "\">");
                        buf.Add(j);
                        for (int t = 0; t < j; t++)
                        {
                            xml[^1] = "\t" + xml[^1];
                        }
                        break;
                    }
                }
            }
            for (int k = buf.Count - 1; k >= 0; k--)
            {
                xml.Add("</node>");
            }
            xml.Add("</empty_node>");
            String[] xml_2 = new string[xml.Count];
            for (int i = 0; i < xml.Count; i++)
            {
                xml_2[i] = xml[i];
            }
            if (File.Exists(Vars.xmlMetObjHead))
            {
                File.Delete(Vars.xmlMetObjHead);
            }
            File.WriteAllLines(Vars.xmlMetObjHead, xml_2);
        }
        //------------------------------------------------------------------------------------------------------------------------
        public static bool BeoyndDivinityFile(string inpDir)
        {
            long objectsLength = 0;
            long tileLength = 0;
            if (File.Exists(inpDir + "\\static\\imagelists\\CPackedb.0c")) // Проверяем размер файла со спрайтами объектов (79364096 - DD, 164421632 - BD)
            {
                System.IO.FileInfo file = new(inpDir + "\\static\\imagelists\\CPackedb.0c");
                objectsLength = file.Length;
            }
            if (File.Exists(inpDir + "\\static\\imagelists\\CPackedb.2c")) // Проверяем размер файла со спрайтами плитки (17149952 - DD, 44376064 - BD)
            {
                System.IO.FileInfo file = new(inpDir + "\\static\\imagelists\\CPackedb.2c");
                tileLength = file.Length;
            }
            if (objectsLength > 80000000 && tileLength > 20000000) return true;
            else return false;
        }
        //------------------------------------------------------------------------------------------------------------------------
        public static List<Sprite>? GetSprites(string path)
        {
            string inpDirI = path;
            string inpDirB = inpDirI.Remove(inpDirI.Length - 4, 1).Insert(inpDirI.Length - 4, "b");
            System.Diagnostics.Debug.WriteLine(inpDirI);
            System.Diagnostics.Debug.WriteLine(inpDirB);
            List<Sprite> spriteList = new List<Sprite>();
            List<Int64[]> H = new();
            int lineLength = 56;
            long lineCount = 0;
            if (File.Exists(inpDirI))
            {
                System.IO.FileInfo IFile = new(inpDirI);
                lineCount = IFile.Length / lineLength;
                using BinaryReader ISprite = new(File.Open(inpDirI, FileMode.Open));
                {
                    for (long i = 0; i < lineCount; i++)
                    {
                        H.Add(new Int64[14] { ISprite.ReadUInt32(),
                                    ISprite.ReadUInt32(),
                                    ISprite.ReadUInt32() ,
                                    ISprite.ReadUInt32() ,
                                    ISprite.ReadUInt32() ,
                                    ISprite.ReadUInt32() ,
                                    ISprite.ReadUInt32() ,
                                    ISprite.ReadUInt32() ,
                                    ISprite.ReadUInt32() ,
                                    ISprite.ReadUInt32() ,
                                    ISprite.ReadUInt32() ,
                                    ISprite.ReadUInt32() ,
                                    ISprite.ReadUInt32() ,
                                    ISprite.ReadUInt32()});
                    }
                }
            }
            else
            {
                System.Diagnostics.Debug.WriteLine(inpDirI + " не найден");
                return null;
            }

            if (File.Exists(inpDirB))
            {
                System.IO.FileInfo BFile = new(inpDirB);
                lineCount = BFile.Length;
                byte[] file = new byte[lineCount];
                using BinaryReader BSprite = new(File.Open(inpDirB, FileMode.Open));
                {
                    file = BSprite.ReadBytes((int)lineCount);
                }
                Int64 statrPoint;
                Int64 endPoint;
                Int64 decompressArraySize;
                Int64 imgSizeX;
                Int64 imgSizeY;
                for (int i = 0; i < H.Count; i++)
                {
                    statrPoint = H[i][0];
                    if (i == H.Count - 1)
                    {
                        endPoint = file.Length;
                    }
                    else
                    {
                        endPoint = H[i + 1][0];
                    }
                    imgSizeX = H[i][1];
                    imgSizeY = H[i][2];
                    decompressArraySize = file[statrPoint] + (file[statrPoint + 1] << 8) + (file[statrPoint + 2] << 16) + (file[statrPoint + 3] << 24);

                    byte[] compressData = new byte[endPoint - statrPoint - 0];

                    for (Int64 j = statrPoint + 4; j < endPoint; j++)
                    {
                        compressData[j - statrPoint - 4] = file[j];
                    }

                    compressData[endPoint - statrPoint - 4] = file[statrPoint + 0];
                    compressData[endPoint - statrPoint - 3] = file[statrPoint + 1];
                    compressData[endPoint - statrPoint - 2] = file[statrPoint + 2];
                    compressData[endPoint - statrPoint - 1] = file[statrPoint + 3];

                    byte[] decompressData = new byte[decompressArraySize];

                    decompressData = EditForm.compressor.Decompress(compressData);

                    int count = 0;
                    if (H[i][3] == 0) // Если ттекстура без прозрачности
                    {
                        Microsoft.Xna.Framework.Color[] colors = new Microsoft.Xna.Framework.Color[(int)imgSizeX * (int)imgSizeY];
                        int[] d = new int[(int)imgSizeX * (int)imgSizeY];
                        int a = 0;
                        for (int y = 0; y < imgSizeY; y++)
                        {
                            for (int x = 0; x < imgSizeX; x++)
                            {
                                colors[a++] = getPixelColor(decompressData[count++] + (decompressData[count++] << 8));
                            }
                        }
                        spriteList.Add(new Sprite((int)imgSizeY, (int)imgSizeX, colors));
                    }
                    else // Если ттекстура с прозрачностю
                    {
                        int len = decompressData[count++] + (decompressData[count++] << 8) + (decompressData[count++] << 16) + (decompressData[count++] << 24);
                        int datBiasBig = decompressData[count++] + (decompressData[count++] << 8) + (decompressData[count++] << 16) + (decompressData[count++] << 24);
                        int width = decompressData[count++] + (decompressData[count++] << 8);
                        int heigth = decompressData[count++] + (decompressData[count++] << 8);
                        Microsoft.Xna.Framework.Color[] colors = new Microsoft.Xna.Framework.Color[heigth * width];
                        for(int h = 0; h < heigth * width; h++)
                        {
                            colors[h] = new Microsoft.Xna.Framework.Color(0, 0, 0, 0);
                        }
                        for (int y = 0; y < heigth; y++)
                        {
                            int numberOfLines = decompressData[count++] + (decompressData[count++] << 8);
                            if (numberOfLines != 0)
                            {
                                int datBiasSmall = decompressData[count++] + (decompressData[count++] << 8) + (decompressData[count++] << 16) + (decompressData[count++] << 24);
                                int picCnt = 0;
                                for (int l = 0; l < numberOfLines; l++)
                                {
                                    int pixelBias = decompressData[count++] + (decompressData[count++] << 8);
                                    int pixelCount = decompressData[count++] + (decompressData[count++] << 8);
                                    for (int p = 0; p < pixelCount; p++)
                                    {
                                        colors[y * width + p + pixelBias] = getPixelColor(decompressData[datBiasBig + datBiasSmall + picCnt++] + (decompressData[datBiasBig + datBiasSmall + picCnt++] << 8));
                                    }
                                }
                                count += 2;
                            }
                            else
                            {
                                count += 6;
                            }
                        }
                        spriteList.Add(new Sprite(heigth, width, colors));
                    }
                }
            }
            else
            {
                System.Diagnostics.Debug.WriteLine(inpDirB + " не найден");
                return null;
            }
            return spriteList;
        }
        //------------------------------------------------------------------------------------------------------------------------
        private static Microsoft.Xna.Framework.Color getPixelColor(int color)
        {
            int red, green, blue, alpha;

            color &= 0xFFFF;
            blue = (((color >> 0) & 0x1f) << 3) | 0x7;
            green = (((color >> 5) & 0x3f) << 2) | 0x3;
            red = (((color >> 11) & 0x1f) << 3) | 0x7;
            alpha = 0xFF;

            if (red < 0)
                red = 0;
            else if (red > 0xFF)
                red = 0xFF;

            if (green < 0)
                green = 0;
            else if (green > 0xFF)
                green = 0xFF;

            if (blue < 0)
                blue = 0;
            else if (blue > 0xFF)
                blue = 0xFF;

            return new Microsoft.Xna.Framework.Color(red, green, blue, alpha);
        }
        //------------------------------------------------------------------------------------------------------------------------
        public static string[] ReadAgentClassesName(string inpFile)
        {
            string[] lines = File.ReadLines(inpFile).ToArray();
            return lines;
        }
            //------------------------------------------------------------------------------------------------------------------------
            //------------------------------------------------------------------------------------------------------------------------
            //------------------------------------------------------------------------------------------------------------------------
            //------------------------------------------------------------------------------------------------------------------------
            //------------------------------------------------------------------------------------------------------------------------
    }
}
