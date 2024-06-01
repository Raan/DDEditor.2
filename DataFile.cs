using DivEditor.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Point = Microsoft.Xna.Framework.Point;

namespace DivEditor
{
    internal class DataFile
    {
        public static void Disassembly(String inpFile)
        {
            List<byte> bufByte = new List<byte>();
            String bufString = "aaaaaaaaaa";
            System.IO.Directory.CreateDirectory(Vars.dirDataFile);
            if (File.Exists(inpFile))
            {
                byte[] fileBytes = File.ReadAllBytes(inpFile);
                long HeadPoint = 0;
                int HeadLength = 106;
                long GlobalVarsPoint = 0;
                int GlobalVarsLength = 0;
                long AlignmentmanagerPoint = 0;
                int AlignmentmanagerLength = 0;
                long AgentVariablesPoint = 0;
                int AgentVariablesLength = 0;
                long AgentClassesPoint = 0;
                int AgentClassesLength = 0;
                long AgentsPoint = 0;
                int AgentsLength = 0;
                long EggsPoint = 0;
                int EggsLength = 0;
                long MonsterGenPoint = 0;
                int MonsterLength = 0;
                long PartyPoint = 0;
                int PartyLength = 0;
                long SkillsPoint = 0;
                int SkillsLength = 0;
                long TimePoint = 0;
                int TimeLength = 0;
                long GameclockPoint = 0;
                int GameclockLength = 0;
                long TrapsPoint = 0;
                int TrapsLength = 0;
                long TimersPoint = 0;
                int TimersLength = 0;
                long CountersPoint = 0;
                int CountersLength = 0;
                long ExplosionsPoint = 0;
                int ExplosionsLength = 0;
                long DoorChestListPoint = 0;
                int DoorChestListLength = 0;
                long DialogLogPoint = 0;
                int DialogLogLength = 0;
                long NoMagicZonesPoint = 0;
                int NoMagicZonesLength = 0;
                long MagicPoint = 0;
                int MagicLength = 0;
                long ProjectilesPoint = 0;
                int ProjectilesLength = 0;
                long PainpointsPoint = 0;
                int PainpointsLength = 0;
                long AnieffectsPoint = 0;
                int AnieffectsLength = 0;
                long OsirisobjectsPoint = 0;
                int OsirisobjectsLength = 0;
                long OsirisnamesPoint = 0;
                int OsirisnamesLength = 0;
                long ЁPlayerInfoPoint = 0;
                int ЁPlayerInfoLength = 0;

                for (long i = 0; i < fileBytes.Length; i++)
                {
                    bufByte.Add(fileBytes[i]);
                    bufString = bufString.Substring(1) + Convert.ToChar(fileBytes[i]);
                    if (bufString == "GlobalVars")
                    {
                        GlobalVarsPoint = i - 9;
                        GlobalVarsLength =
                            fileBytes[GlobalVarsPoint - 4] +
                            fileBytes[GlobalVarsPoint - 3] * 10 +
                            fileBytes[GlobalVarsPoint - 2] * 100 +
                            fileBytes[GlobalVarsPoint - 1] * 1000;
                    }
                    if (bufString == "Alignmentm")
                    {
                        AlignmentmanagerPoint = i - 9;
                        AlignmentmanagerLength =
                            fileBytes[AlignmentmanagerPoint - 4] +
                            fileBytes[AlignmentmanagerPoint - 3] * 10 +
                            fileBytes[AlignmentmanagerPoint - 2] * 100 +
                            fileBytes[AlignmentmanagerPoint - 1] * 1000;
                    }
                    if (bufString == "AgentVaria")
                    {
                        AgentVariablesPoint = i - 9;
                        AgentVariablesLength =
                            fileBytes[AgentVariablesPoint - 4] +
                            fileBytes[AgentVariablesPoint - 3] * 10 +
                            fileBytes[AgentVariablesPoint - 2] * 100 +
                            fileBytes[AgentVariablesPoint - 1] * 1000;
                    }
                    if (bufString == "AgentClass")
                    {
                        AgentClassesPoint = i - 9;
                        AgentClassesLength =
                            fileBytes[AgentClassesPoint - 4] +
                            fileBytes[AgentClassesPoint - 3] * 10 +
                            fileBytes[AgentClassesPoint - 2] * 100 +
                            fileBytes[AgentClassesPoint - 1] * 1000;
                    }
                    if (bufString == "AgentsV0.9")
                    {
                        AgentsPoint = i - 9;
                        AgentsLength =
                            fileBytes[AgentsPoint - 4] +
                            fileBytes[AgentsPoint - 3] * 10 +
                            fileBytes[AgentsPoint - 2] * 100 +
                            fileBytes[AgentsPoint - 1] * 1000;
                    }
                    if (bufString == "EggsV0.935")
                    {
                        EggsPoint = i - 9;
                        EggsLength =
                            fileBytes[EggsPoint - 4] +
                            fileBytes[EggsPoint - 3] * 10 +
                            fileBytes[EggsPoint - 2] * 100 +
                            fileBytes[EggsPoint - 1] * 1000;
                    }
                    if (bufString == "MonsterGen")
                    {
                        MonsterGenPoint = i - 9;
                        MonsterLength =
                            fileBytes[MonsterGenPoint - 4] +
                            fileBytes[MonsterGenPoint - 3] * 10 +
                            fileBytes[MonsterGenPoint - 2] * 100 +
                            fileBytes[MonsterGenPoint - 1] * 1000;
                    }
                    if (bufString == "PartyV0.93")
                    {
                        PartyPoint = i - 9;
                        PartyLength =
                            fileBytes[PartyPoint - 4] +
                            fileBytes[PartyPoint - 3] * 10 +
                            fileBytes[PartyPoint - 2] * 100 +
                            fileBytes[PartyPoint - 1] * 1000;
                    }
                    if (bufString == "SkillsV0.9")
                    {
                        SkillsPoint = i - 9;
                        SkillsLength =
                            fileBytes[SkillsPoint - 4] +
                            fileBytes[SkillsPoint - 3] * 10 +
                            fileBytes[SkillsPoint - 2] * 100 +
                            fileBytes[SkillsPoint - 1] * 1000;
                    }
                    if (bufString == "TimeV0.935")
                    {
                        TimePoint = i - 9;
                        TimeLength =
                            fileBytes[TimePoint - 4] +
                            fileBytes[TimePoint - 3] * 10 +
                            fileBytes[TimePoint - 2] * 100 +
                            fileBytes[TimePoint - 1] * 1000;
                    }
                    if (bufString == "GameclockV")
                    {
                        GameclockPoint = i - 9;
                        GameclockLength =
                            fileBytes[GameclockPoint - 4] +
                            fileBytes[GameclockPoint - 3] * 10 +
                            fileBytes[GameclockPoint - 2] * 100 +
                            fileBytes[GameclockPoint - 1] * 1000;
                    }
                    if (bufString == "TrapsV0.93")
                    {
                        TrapsPoint = i - 9;
                        TrapsLength =
                            fileBytes[TrapsPoint - 4] +
                            fileBytes[TrapsPoint - 3] * 10 +
                            fileBytes[TrapsPoint - 2] * 100 +
                            fileBytes[TrapsPoint - 1] * 1000;
                    }
                    if (bufString == "TimersV0.9")
                    {
                        TimersPoint = i - 9;
                        TimersLength =
                            fileBytes[TimersPoint - 4] +
                            fileBytes[TimersPoint - 3] * 10 +
                            fileBytes[TimersPoint - 2] * 100 +
                            fileBytes[TimersPoint - 1] * 1000;
                    }
                    if (bufString == "CountersV0")
                    {
                        CountersPoint = i - 9;
                        CountersLength =
                            fileBytes[CountersPoint - 4] +
                            fileBytes[CountersPoint - 3] * 10 +
                            fileBytes[CountersPoint - 2] * 100 +
                            fileBytes[CountersPoint - 1] * 1000;
                    }
                    if (bufString == "Explosions")
                    {
                        ExplosionsPoint = i - 9;
                        ExplosionsLength =
                            fileBytes[ExplosionsPoint - 4] +
                            fileBytes[ExplosionsPoint - 3] * 10 +
                            fileBytes[ExplosionsPoint - 2] * 100 +
                            fileBytes[ExplosionsPoint - 1] * 1000;
                    }
                    if (bufString == "DoorChestL")
                    {
                        DoorChestListPoint = i - 9;
                        DoorChestListLength =
                            fileBytes[DoorChestListPoint - 4] +
                            fileBytes[DoorChestListPoint - 3] * 10 +
                            fileBytes[DoorChestListPoint - 2] * 100 +
                            fileBytes[DoorChestListPoint - 1] * 1000;
                    }
                    if (bufString == "DialogLogV")
                    {
                        DialogLogPoint = i - 9;
                        DialogLogLength =
                            fileBytes[DialogLogPoint - 4] +
                            fileBytes[DialogLogPoint - 3] * 10 +
                            fileBytes[DialogLogPoint - 2] * 100 +
                            fileBytes[DialogLogPoint - 1] * 1000;
                    }
                    if (bufString == "NoMagicZon")
                    {
                        NoMagicZonesPoint = i - 9;
                        NoMagicZonesLength =
                            fileBytes[NoMagicZonesPoint - 4] +
                            fileBytes[NoMagicZonesPoint - 3] * 10 +
                            fileBytes[NoMagicZonesPoint - 2] * 100 +
                            fileBytes[NoMagicZonesPoint - 1] * 1000;
                    }
                    if (bufString == "MagicV0.93")
                    {
                        MagicPoint = i - 9;
                        MagicLength =
                            fileBytes[MagicPoint - 4] +
                            fileBytes[MagicPoint - 3] * 10 +
                            fileBytes[MagicPoint - 2] * 100 +
                            fileBytes[MagicPoint - 1] * 1000;
                    }
                    if (bufString == "Projectile")
                    {
                        ProjectilesPoint = i - 9;
                        ProjectilesLength =
                            fileBytes[ProjectilesPoint - 4] +
                            fileBytes[ProjectilesPoint - 3] * 10 +
                            fileBytes[ProjectilesPoint - 2] * 100 +
                            fileBytes[ProjectilesPoint - 1] * 1000;
                    }
                    if (bufString == "Painpoints")
                    {
                        PainpointsPoint = i - 9;
                        PainpointsLength =
                            fileBytes[PainpointsPoint - 4] +
                            fileBytes[PainpointsPoint - 3] * 10 +
                            fileBytes[PainpointsPoint - 2] * 100 +
                            fileBytes[PainpointsPoint - 1] * 1000;
                    }
                    if (bufString == "Anieffects")
                    {
                        AnieffectsPoint = i - 9;
                        AnieffectsLength =
                            fileBytes[AnieffectsPoint - 4] +
                            fileBytes[AnieffectsPoint - 3] * 10 +
                            fileBytes[AnieffectsPoint - 2] * 100 +
                            fileBytes[AnieffectsPoint - 1] * 1000;
                    }
                    if (bufString == "Osirisobje")
                    {
                        OsirisobjectsPoint = i - 9;
                        OsirisobjectsLength =
                            fileBytes[OsirisobjectsPoint - 4] +
                            fileBytes[OsirisobjectsPoint - 3] * 10 +
                            fileBytes[OsirisobjectsPoint - 2] * 100 +
                            fileBytes[OsirisobjectsPoint - 1] * 1000;
                    }
                    if (bufString == "Osirisname")
                    {
                        OsirisnamesPoint = i - 9;
                        OsirisnamesLength =
                            fileBytes[OsirisnamesPoint - 4] +
                            fileBytes[OsirisnamesPoint - 3] * 10 +
                            fileBytes[OsirisnamesPoint - 2] * 100 +
                            fileBytes[OsirisnamesPoint - 1] * 1000;
                    }
                    if (bufString == "PlayerInfo")
                    {
                        ЁPlayerInfoPoint = i - 10;
                        ЁPlayerInfoLength =
                            fileBytes[ЁPlayerInfoPoint - 4] +
                            fileBytes[ЁPlayerInfoPoint - 3] * 10 +
                            fileBytes[ЁPlayerInfoPoint - 2] * 100 +
                            fileBytes[ЁPlayerInfoPoint - 1] * 1000;
                    }
                }
                using (BinaryWriter writer = new BinaryWriter(File.Open(Vars.dirDataFile + "\\" + "Head.000", FileMode.Create)))
                {
                    for (long i = HeadPoint; i < HeadLength; i++)
                    {
                        writer.Write(fileBytes[i]);
                    }
                }
                using (BinaryWriter writer = new BinaryWriter(File.Open(Vars.dirDataFile + "\\" + "00_GlobalVars.000", FileMode.Create)))
                {
                    for (long i = GlobalVarsPoint + GlobalVarsLength; i < AlignmentmanagerPoint - 4; i++)
                    {
                        writer.Write(fileBytes[i]);
                    }
                }
                using (BinaryWriter writer = new BinaryWriter(File.Open(Vars.dirDataFile + "\\" + "01_Alignmentmanager.000", FileMode.Create)))
                {
                    for (long i = AlignmentmanagerPoint + AlignmentmanagerLength; i < AgentVariablesPoint - 4; i++)
                    {
                        writer.Write(fileBytes[i]);
                    }
                }
                using (BinaryWriter writer = new BinaryWriter(File.Open(Vars.dirDataFile + "\\" + "02_AgentVariables.000", FileMode.Create)))
                {
                    for (long i = AgentVariablesPoint + AgentVariablesLength; i < AgentClassesPoint - 4; i++)
                    {
                        writer.Write(fileBytes[i]);
                    }
                }
                using (BinaryWriter writer = new BinaryWriter(File.Open(Vars.dirDataFile + "\\" + "03_AgentClasses.000", FileMode.Create)))
                {
                    for (long i = AgentClassesPoint + AgentClassesLength; i < AgentsPoint - 4; i++)
                    {
                        writer.Write(fileBytes[i]);
                    }
                }
                using (BinaryWriter writer = new BinaryWriter(File.Open(Vars.dirDataFile + "\\" + "04_Agents.000", FileMode.Create)))
                {
                    for (long i = AgentsPoint + AgentsLength; i < EggsPoint - 4; i++)
                    {
                        writer.Write(fileBytes[i]);
                    }
                }
                using (BinaryWriter writer = new BinaryWriter(File.Open(Vars.dirDataFile + "\\" + "05_Eggs.000", FileMode.Create)))
                {
                    for (long i = EggsPoint + EggsLength; i < MonsterGenPoint - 4; i++)
                    {
                        writer.Write(fileBytes[i]);
                    }
                }
                using (BinaryWriter writer = new BinaryWriter(File.Open(Vars.dirDataFile + "\\" + "06_MonsterGen.000", FileMode.Create)))
                {
                    for (long i = MonsterGenPoint + MonsterLength; i < PartyPoint - 4; i++)
                    {
                        writer.Write(fileBytes[i]);
                    }
                }
                using (BinaryWriter writer = new BinaryWriter(File.Open(Vars.dirDataFile + "\\" + "07_Party.000", FileMode.Create)))
                {
                    for (long i = PartyPoint + PartyLength; i < SkillsPoint - 4; i++)
                    {
                        writer.Write(fileBytes[i]);
                    }
                }
                using (BinaryWriter writer = new BinaryWriter(File.Open(Vars.dirDataFile + "\\" + "08_Skills.000", FileMode.Create)))
                {
                    for (long i = SkillsPoint + SkillsLength; i < TimePoint - 4; i++)
                    {
                        writer.Write(fileBytes[i]);
                    }
                }
                using (BinaryWriter writer = new BinaryWriter(File.Open(Vars.dirDataFile + "\\" + "09_Time.000", FileMode.Create)))
                {
                    for (long i = TimePoint + TimeLength; i < GameclockPoint - 4; i++)
                    {
                        writer.Write(fileBytes[i]);
                    }
                }
                using (BinaryWriter writer = new BinaryWriter(File.Open(Vars.dirDataFile + "\\" + "10_Gameclock.000", FileMode.Create)))
                {
                    for (long i = GameclockPoint + GameclockLength; i < TrapsPoint - 4; i++)
                    {
                        writer.Write(fileBytes[i]);
                    }
                }
                using (BinaryWriter writer = new BinaryWriter(File.Open(Vars.dirDataFile + "\\" + "11_Traps.000", FileMode.Create)))
                {
                    for (long i = TrapsPoint + TrapsLength; i < TimersPoint - 4; i++)
                    {
                        writer.Write(fileBytes[i]);
                    }
                }
                using (BinaryWriter writer = new BinaryWriter(File.Open(Vars.dirDataFile + "\\" + "12_Timers.000", FileMode.Create)))
                {
                    for (long i = TimersPoint + TimersLength; i < CountersPoint - 4; i++)
                    {
                        writer.Write(fileBytes[i]);
                    }
                }
                using (BinaryWriter writer = new BinaryWriter(File.Open(Vars.dirDataFile + "\\" + "13_Counters.000", FileMode.Create)))
                {
                    for (long i = CountersPoint + CountersLength; i < ExplosionsPoint - 4; i++)
                    {
                        writer.Write(fileBytes[i]);
                    }
                }
                using (BinaryWriter writer = new BinaryWriter(File.Open(Vars.dirDataFile + "\\" + "14_Explosions.000", FileMode.Create)))
                {
                    for (long i = ExplosionsPoint + ExplosionsLength; i < DoorChestListPoint - 4; i++)
                    {
                        writer.Write(fileBytes[i]);
                    }
                }
                using (BinaryWriter writer = new BinaryWriter(File.Open(Vars.dirDataFile + "\\" + "15_DoorChestList.000", FileMode.Create)))
                {
                    for (long i = DoorChestListPoint + DoorChestListLength; i < DialogLogPoint - 4; i++)
                    {
                        writer.Write(fileBytes[i]);
                    }
                }
                using (BinaryWriter writer = new BinaryWriter(File.Open(Vars.dirDataFile + "\\" + "16_DialogLog.000", FileMode.Create)))
                {
                    for (long i = DialogLogPoint + DialogLogLength; i < NoMagicZonesPoint - 4; i++)
                    {
                        writer.Write(fileBytes[i]);
                    }
                }
                using (BinaryWriter writer = new BinaryWriter(File.Open(Vars.dirDataFile + "\\" + "17_NoMagicZones.000", FileMode.Create)))
                {
                    for (long i = NoMagicZonesPoint + NoMagicZonesLength; i < MagicPoint - 4; i++)
                    {
                        writer.Write(fileBytes[i]);
                    }
                }
                using (BinaryWriter writer = new BinaryWriter(File.Open(Vars.dirDataFile + "\\" + "18_Magic.000", FileMode.Create)))
                {
                    for (long i = MagicPoint + MagicLength; i < ProjectilesPoint - 4; i++)
                    {
                        writer.Write(fileBytes[i]);
                    }
                }
                using (BinaryWriter writer = new BinaryWriter(File.Open(Vars.dirDataFile + "\\" + "19_Projectiles.000", FileMode.Create)))
                {
                    for (long i = ProjectilesPoint + ProjectilesLength; i < PainpointsPoint - 4; i++)
                    {
                        writer.Write(fileBytes[i]);
                    }
                }
                using (BinaryWriter writer = new BinaryWriter(File.Open(Vars.dirDataFile + "\\" + "20_Painpoints.000", FileMode.Create)))
                {
                    for (long i = PainpointsPoint + PainpointsLength; i < AnieffectsPoint - 4; i++)
                    {
                        writer.Write(fileBytes[i]);
                    }
                }
                using (BinaryWriter writer = new BinaryWriter(File.Open(Vars.dirDataFile + "\\" + "21_Anieffects.000", FileMode.Create)))
                {
                    for (long i = AnieffectsPoint + AnieffectsLength; i < OsirisobjectsPoint - 4; i++)
                    {
                        writer.Write(fileBytes[i]);
                    }
                }
                using (BinaryWriter writer = new BinaryWriter(File.Open(Vars.dirDataFile + "\\" + "22_Osirisobjects.000", FileMode.Create)))
                {
                    for (long i = OsirisobjectsPoint + OsirisobjectsLength; i < OsirisnamesPoint - 4; i++)
                    {
                        writer.Write(fileBytes[i]);
                    }
                }
                using (BinaryWriter writer = new BinaryWriter(File.Open(Vars.dirDataFile + "\\" + "23_Osirisnames.000", FileMode.Create)))
                {
                    for (long i = OsirisnamesPoint + OsirisnamesLength; i < ЁPlayerInfoPoint - 4; i++)
                    {
                        writer.Write(fileBytes[i]);
                    }
                }
                using (BinaryWriter writer = new BinaryWriter(File.Open(Vars.dirDataFile + "\\" + "24_ЁPlayerInfo.000", FileMode.Create)))
                {
                    for (long i = ЁPlayerInfoPoint + ЁPlayerInfoLength; i < fileBytes.Length; i++)
                    {
                        writer.Write(fileBytes[i]);
                    }
                }
                System.Diagnostics.Debug.WriteLine("Data file disassembly");
            }
        }
        public static void Assembly(String outFile)
        {
            using BinaryWriter writer = new(File.Open(outFile, FileMode.Create));
            String start;
            byte[] bytes;
            if (!File.Exists(Vars.dirDataFile + "\\" + "Head.txt"))
            {
                byte[] buffer = File.ReadAllBytes(Vars.dirDataFile + "\\" + "Head.000");
                writer.Write(buffer);
            }
            if (!File.Exists(Vars.dirDataFile + "\\" + "00_GlobalVars.txt"))
            {
                start = "GlobalVarsV0.935 25-02-2002";
                bytes = Encoding.ASCII.GetBytes(start);
                writer.Write(start.Length + 1);
                writer.Write(bytes);
                writer.Write((byte)0);
                byte[] buffer = File.ReadAllBytes(Vars.dirDataFile + "\\" + "00_GlobalVars.000");
                writer.Write(buffer);
            }
            else { }
            if (!File.Exists(Vars.dirDataFile + "\\" + "01_Alignmentmanager.txt"))
            {
                start = "AlignmentmanagerV0.935 25-02-2002";
                bytes = Encoding.ASCII.GetBytes(start);
                writer.Write(start.Length + 1);
                writer.Write(bytes);
                writer.Write((byte)0);
                byte[] buffer = File.ReadAllBytes(Vars.dirDataFile + "\\" + "01_Alignmentmanager.000");
                writer.Write(buffer);
            }
            else { }
            if (!File.Exists(Vars.dirDataFile + "\\" + "02_AgentVariables.txt"))
            {
                start = "AgentVariablesV0.935 25-02-2002";
                bytes = Encoding.ASCII.GetBytes(start);
                writer.Write(start.Length + 1);
                writer.Write(bytes);
                writer.Write((byte)0);
                byte[] buffer = File.ReadAllBytes(Vars.dirDataFile + "\\" + "02_AgentVariables.000");
                writer.Write(buffer);
            }
            else { }
            if (!File.Exists(Vars.dirDataFile + "\\" + "03_AgentClasses.txt"))
            {
                start = "AgentClassesV0.935 25-02-2002";
                bytes = Encoding.ASCII.GetBytes(start);
                writer.Write(start.Length + 1);
                writer.Write(bytes);
                writer.Write((byte)0);
                byte[] buffer = File.ReadAllBytes(Vars.dirDataFile + "\\" + "03_AgentClasses.000");
                writer.Write(buffer);
            }
            else { }
            if (!File.Exists(Vars.dirDataFile + "\\" + "04_Agents.txt"))
            {
                start = "AgentsV0.935 25-02-2002";
                bytes = Encoding.ASCII.GetBytes(start);
                writer.Write(start.Length + 1);
                writer.Write(bytes);
                writer.Write((byte)0);
                byte[] buffer = File.ReadAllBytes(Vars.dirDataFile + "\\" + "04_Agents.000");
                writer.Write(buffer);
            }
            else { }
            if (!File.Exists(Vars.dirDataFile + "\\" + "05_Eggs.txt"))
            {
                start = "EggsV0.935 25-02-2002";
                bytes = Encoding.ASCII.GetBytes(start);
                writer.Write(start.Length + 1);
                writer.Write(bytes);
                writer.Write((byte)0);
                byte[] buffer = File.ReadAllBytes(Vars.dirDataFile + "\\" + "05_Eggs.000");
                writer.Write(buffer);
            }
            else { }
            if (!File.Exists(Vars.dirDataFile + "\\" + "06_MonsterGen.txt"))
            {
                start = "MonsterGenV0.935 25-02-2002";
                bytes = Encoding.ASCII.GetBytes(start);
                writer.Write(start.Length + 1);
                writer.Write(bytes);
                writer.Write((byte)0);
                byte[] buffer = File.ReadAllBytes(Vars.dirDataFile + "\\" + "06_MonsterGen.000");
                writer.Write(buffer);
            }
            else { }
            if (!File.Exists(Vars.dirDataFile + "\\" + "07_Party.txt"))
            {
                start = "PartyV0.935 25-02-2002";
                bytes = Encoding.ASCII.GetBytes(start);
                writer.Write(start.Length + 1);
                writer.Write(bytes);
                writer.Write((byte)0);
                byte[] buffer = File.ReadAllBytes(Vars.dirDataFile + "\\" + "07_Party.000");
                writer.Write(buffer);
            }
            else { }
            if (!File.Exists(Vars.dirDataFile + "\\" + "08_Skills.txt"))
            {
                start = "SkillsV0.935 25-02-2002";
                bytes = Encoding.ASCII.GetBytes(start);
                writer.Write(start.Length + 1);
                writer.Write(bytes);
                writer.Write((byte)0);
                byte[] buffer = File.ReadAllBytes(Vars.dirDataFile + "\\" + "08_Skills.000");
                writer.Write(buffer);
            }
            else { }
            if (!File.Exists(Vars.dirDataFile + "\\" + "09_Time.txt"))
            {
                start = "TimeV0.935 25-02-2002";
                bytes = Encoding.ASCII.GetBytes(start);
                writer.Write(start.Length + 1);
                writer.Write(bytes);
                writer.Write((byte)0);
                byte[] buffer = File.ReadAllBytes(Vars.dirDataFile + "\\" + "09_Time.000");
                writer.Write(buffer);
            }
            else { }
            if (!File.Exists(Vars.dirDataFile + "\\" + "10_Gameclock.txt"))
            {
                start = "GameclockV0.935 25-02-2002";
                bytes = Encoding.ASCII.GetBytes(start);
                writer.Write(start.Length + 1);
                writer.Write(bytes);
                writer.Write((byte)0);
                byte[] buffer = File.ReadAllBytes(Vars.dirDataFile + "\\" + "10_Gameclock.000");
                writer.Write(buffer);
            }
            else { }
            if (!File.Exists(Vars.dirDataFile + "\\" + "11_Traps.txt"))
            {
                start = "TrapsV0.935 25-02-2002";
                bytes = Encoding.ASCII.GetBytes(start);
                writer.Write(start.Length + 1);
                writer.Write(bytes);
                writer.Write((byte)0);
                byte[] buffer = File.ReadAllBytes(Vars.dirDataFile + "\\" + "11_Traps.000");
                writer.Write(buffer);
            }
            else { }
            if (!File.Exists(Vars.dirDataFile + "\\" + "12_Timers.txt"))
            {
                start = "TimersV0.935 25-02-2002";
                bytes = Encoding.ASCII.GetBytes(start);
                writer.Write(start.Length + 1);
                writer.Write(bytes);
                writer.Write((byte)0);
                byte[] buffer = File.ReadAllBytes(Vars.dirDataFile + "\\" + "12_Timers.000");
                writer.Write(buffer);
            }
            else { }
            if (!File.Exists(Vars.dirDataFile + "\\" + "13_Counters.txt"))
            {
                start = "CountersV0.935 25-02-2002";
                bytes = Encoding.ASCII.GetBytes(start);
                writer.Write(start.Length + 1);
                writer.Write(bytes);
                writer.Write((byte)0);
                byte[] buffer = File.ReadAllBytes(Vars.dirDataFile + "\\" + "13_Counters.000");
                writer.Write(buffer);
            }
            else { }
            if (!File.Exists(Vars.dirDataFile + "\\" + "14_Explosions.txt"))
            {
                start = "ExplosionsV0.935 25-02-2002";
                bytes = Encoding.ASCII.GetBytes(start);
                writer.Write(start.Length + 1);
                writer.Write(bytes);
                writer.Write((byte)0);
                byte[] buffer = File.ReadAllBytes(Vars.dirDataFile + "\\" + "14_Explosions.000");
                writer.Write(buffer);
            }
            else { }
            if (!File.Exists(Vars.dirDataFile + "\\" + "15_DoorChestList.txt"))
            {
                start = "DoorChestListV0.935 25-02-2002";
                bytes = Encoding.ASCII.GetBytes(start);
                writer.Write(start.Length + 1);
                writer.Write(bytes);
                writer.Write((byte)0);
                byte[] buffer = File.ReadAllBytes(Vars.dirDataFile + "\\" + "15_DoorChestList.000");
                writer.Write(buffer);
            }
            else { }
            if (!File.Exists(Vars.dirDataFile + "\\" + "16_DialogLog.txt"))
            {
                start = "DialogLogV0.935 25-02-2002";
                bytes = Encoding.ASCII.GetBytes(start);
                writer.Write(start.Length + 1);
                writer.Write(bytes);
                writer.Write((byte)0);
                byte[] buffer = File.ReadAllBytes(Vars.dirDataFile + "\\" + "16_DialogLog.000");
                writer.Write(buffer);
            }
            else { }
            if (!File.Exists(Vars.dirDataFile + "\\" + "17_NoMagicZones.txt"))
            {
                start = "NoMagicZonesV0.935 25-02-2002";
                bytes = Encoding.ASCII.GetBytes(start);
                writer.Write(start.Length + 1);
                writer.Write(bytes);
                writer.Write((byte)0);
                byte[] buffer = File.ReadAllBytes(Vars.dirDataFile + "\\" + "17_NoMagicZones.000");
                writer.Write(buffer);
            }
            else { }
            if (!File.Exists(Vars.dirDataFile + "\\" + "18_Magic.txt"))
            {
                start = "MagicV0.935 25-02-2002";
                bytes = Encoding.ASCII.GetBytes(start);
                writer.Write(start.Length + 1);
                writer.Write(bytes);
                writer.Write((byte)0);
                byte[] buffer = File.ReadAllBytes(Vars.dirDataFile + "\\" + "18_Magic.000");
                writer.Write(buffer);
            }
            else { }
            if (!File.Exists(Vars.dirDataFile + "\\" + "19_Projectiles.txt"))
            {
                start = "ProjectilesV0.935 25-02-2002";
                bytes = Encoding.ASCII.GetBytes(start);
                writer.Write(start.Length + 1);
                writer.Write(bytes);
                writer.Write((byte)0);
                byte[] buffer = File.ReadAllBytes(Vars.dirDataFile + "\\" + "19_Projectiles.000");
                writer.Write(buffer);
            }
            else { }
            if (!File.Exists(Vars.dirDataFile + "\\" + "20_Painpoints.txt"))
            {
                start = "PainpointsV0.935 25-02-2002";
                bytes = Encoding.ASCII.GetBytes(start);
                writer.Write(start.Length + 1);
                writer.Write(bytes);
                writer.Write((byte)0);
                byte[] buffer = File.ReadAllBytes(Vars.dirDataFile + "\\" + "20_Painpoints.000");
                writer.Write(buffer);
            }
            else { }
            if (!File.Exists(Vars.dirDataFile + "\\" + "21_Anieffects.txt"))
            {
                start = "AnieffectsV0.935 25-02-2002";
                bytes = Encoding.ASCII.GetBytes(start);
                writer.Write(start.Length + 1);
                writer.Write(bytes);
                writer.Write((byte)0);
                byte[] buffer = File.ReadAllBytes(Vars.dirDataFile + "\\" + "21_Anieffects.000");
                writer.Write(buffer);
            }
            else { }
            if (!File.Exists(Vars.dirDataFile + "\\" + "22_Osirisobjects.txt"))
            {
                start = "OsirisobjectsV0.935 25-02-2002";
                bytes = Encoding.ASCII.GetBytes(start);
                writer.Write(start.Length + 1);
                writer.Write(bytes);
                writer.Write((byte)0);
                byte[] buffer = File.ReadAllBytes(Vars.dirDataFile + "\\" + "22_Osirisobjects.000");
                writer.Write(buffer);
            }
            else { }
            if (!File.Exists(Vars.dirDataFile + "\\" + "23_Osirisnames.txt"))
            {
                start = "OsirisnamesV0.935 25-02-2002";
                bytes = Encoding.ASCII.GetBytes(start);
                writer.Write(start.Length + 1);
                writer.Write(bytes);
                writer.Write((byte)0);
                byte[] buffer = File.ReadAllBytes(Vars.dirDataFile + "\\" + "23_Osirisnames.000");
                writer.Write(buffer);
            }
            else { }
            if (!File.Exists(Vars.dirDataFile + "\\" + "24_ЁPlayerInfo.txt"))
            {
                start = "PlayerInfoV0.935 25-02-2002";
                bytes = Encoding.ASCII.GetBytes(start);
                writer.Write(start.Length + 2);
                writer.Write((byte)168);
                writer.Write(bytes);
                writer.Write((byte)0);
                byte[] buffer = File.ReadAllBytes(Vars.dirDataFile + "\\" + "24_ЁPlayerInfo.000");
                writer.Write(buffer);
            }
            else { }
            System.Diagnostics.Debug.WriteLine("Data file assembly");
        }
        public static List<Eggs> ReadEggs(String inpFile)
        {
            List <Eggs> E = new List<Eggs>();
            if (File.Exists(inpFile))
            {
                byte[] buffer = File.ReadAllBytes(inpFile);
                int objCount = getInt32(buffer[0], buffer[1], buffer[2], buffer[3]);
                for (int i = 0; i < objCount; i++)
                {
                    E.Add(new Eggs(new int[]{
                        getInt32(buffer[04 + i * 92], buffer[05 + i * 92], buffer[06 + i * 92], buffer[07 + i * 92]),
                        getInt32(buffer[08 + i * 92], buffer[09 + i * 92], buffer[10 + i * 92], buffer[11 + i * 92]),
                        getInt32(buffer[12 + i * 92], buffer[13 + i * 92], buffer[14 + i * 92], buffer[15 + i * 92]),
                        getInt32(buffer[16 + i * 92], buffer[17 + i * 92], buffer[18 + i * 92], buffer[19 + i * 92]),
                        getInt32(buffer[20 + i * 92], buffer[21 + i * 92], buffer[22 + i * 92], buffer[23 + i * 92]),
                        getInt32(buffer[24 + i * 92], buffer[25 + i * 92], buffer[26 + i * 92], buffer[27 + i * 92]),
                        getInt32(buffer[28 + i * 92], buffer[29 + i * 92], buffer[30 + i * 92], buffer[31 + i * 92]),
                        getInt32(buffer[32 + i * 92], buffer[33 + i * 92], buffer[34 + i * 92], buffer[35 + i * 92]),
                        getInt32(buffer[36 + i * 92], buffer[37 + i * 92], buffer[38 + i * 92], buffer[39 + i * 92]),
                        getInt32(buffer[40 + i * 92], buffer[41 + i * 92], buffer[42 + i * 92], buffer[43 + i * 92])}));
                }
            }
            else System.Diagnostics.Debug.WriteLine("05_Eggs.000 not found");
            return E;
        }
        public static void WriteEggs(List<Eggs> E, String outFile)
        {
            using (BinaryWriter writer = new BinaryWriter(File.Open(outFile, FileMode.Create)))
            {
                writer.Write(E.Count);
                for (int i = 0; i < E.Count; i++)
                {
                    writer.Write(E[i].pos.X);
                    writer.Write(E[i].pos.Y);
                    writer.Write(E[i].agentClass);
                    writer.Write(E[i].var0);
                    writer.Write(E[i].var1);
                    writer.Write(E[i].var2);
                    writer.Write(E[i].var3);
                    writer.Write(E[i].var4);
                    writer.Write(E[i].type);
                    writer.Write(E[i].map);
                    for (int j = 0; j < 52; j++) writer.Write((byte)0);
                }
            }
        }
        public static List <AgentClasses> ReadAgentClasses(String inpFile)
        {
            List<AgentClasses> AC = new List<AgentClasses>();
            String buff = "";
            int cursor = 0;
            int wordLength;
            int count = 0;
            System.IO.FileInfo file = new System.IO.FileInfo(inpFile);
            long sizeFile = file.Length;
            if (File.Exists(inpFile))
            {
                byte[] buffer = File.ReadAllBytes(inpFile);
                count = getInt32(buffer[0], buffer[1], buffer[2], buffer[3]);
                cursor += 4;
                // Hero
                AC.Add(new AgentClasses());
                for (int j = 0; j < 198 * 2; j++)
                {
                    AC[0].var_0.Add(getInt16(buffer[cursor], buffer[cursor + 1]));
                    cursor += 2;
                }
                wordLength = getInt32(buffer[cursor], buffer[cursor + 1], buffer[cursor + 2], buffer[cursor + 3]);
                cursor += 4;
                for (int j = 0; j < wordLength - 1; j++)
                {
                    buff += Convert.ToChar(buffer[cursor + j]);
                }
                cursor += wordLength;
                AC[0].name = buff;
                buff = "";
                for (int j = 0; j < 70; j++)
                {
                    AC[0].var_1.Add(getInt32(buffer[cursor], buffer[cursor + 1], buffer[cursor + 2], buffer[cursor + 3]));
                    cursor += 4;
                }
                wordLength = getInt32(buffer[cursor], buffer[cursor + 1], buffer[cursor + 2], buffer[cursor + 3]);
                cursor += 4;
                for (int j = 0; j < wordLength - 1; j++)
                {
                    buff += Convert.ToChar(buffer[cursor + j]);
                }
                cursor += wordLength;
                AC[0].attitude = buff;
                buff = "";
                cursor += 4;
                while (getInt16(buffer[cursor], buffer[cursor + 1]) == -1)
                {
                    cursor += 2;
                }
                // Another nps
                for (int i = 1; i < count; i++)
                {
                    AC.Add(new AgentClasses());
                    for (int j = 0; j < 198 * 2; j++)
                    {
                        AC[i].var_0.Add(getInt16(buffer[cursor], buffer[cursor + 1]));
                        cursor += 2;
                    }
                    wordLength = getInt32(buffer[cursor], buffer[cursor + 1], buffer[cursor + 2], buffer[cursor + 3]);
                    cursor += 4;
                    for (int j = 0; j < wordLength - 1; j++)
                    {
                        buff += Convert.ToChar(buffer[cursor + j]);
                    }
                    cursor += wordLength;
                    AC[i].name = buff;
                    buff = "";
                    for (int j = 0; j < 34; j++)
                    {
                        AC[i].var_1.Add(getInt32(buffer[cursor], buffer[cursor + 1], buffer[cursor + 2], buffer[cursor + 3]));
                        cursor += 4;
                    }
                    wordLength = getInt32(buffer[cursor], buffer[cursor + 1], buffer[cursor + 2], buffer[cursor + 3]);
                    cursor += 4;
                    for (int j = 0; j < wordLength - 1; j++)
                    {
                        buff += Convert.ToChar(buffer[cursor + j]);
                    }
                    cursor += wordLength;
                    AC[i].attitude = buff;
                    buff = "";
                    wordLength = getInt32(buffer[cursor], buffer[cursor + 1], buffer[cursor + 2], buffer[cursor + 3]);
                    cursor += 4;
                    for (int j = 0; j < wordLength - 1; j++)
                    {
                        buff += Convert.ToChar(buffer[cursor + j]);
                    }
                    cursor += wordLength;
                    AC[i].type = buff;
                    buff = "";
                    while (cursor < sizeFile && getInt16(buffer[cursor + 2], buffer[cursor + 3]) == 0)
                    {
                        AC[i].var_2.Add(getInt32(buffer[cursor], buffer[cursor + 1], buffer[cursor + 2], buffer[cursor + 3]));
                        cursor += 4;
                    }
                }
            }
            return AC;
        }
        public static void WriteAgentClasses(List<AgentClasses> AC, String outFile)
        {
            using (BinaryWriter writer = new BinaryWriter(File.Open(outFile, FileMode.Create)))
            {
                writer.Write(AC.Count);
                // Hero
                for (int i = 0; i < AC[0].var_0.Count; i++)
                {
                    writer.Write(Convert.ToInt16(AC[0].var_0[i]));
                }
                writer.Write(AC[0].name.Length + 1);
                byte[] bytes = Encoding.ASCII.GetBytes(AC[0].name);
                foreach (byte b in bytes)
                {
                    writer.Write(b);
                }
                writer.Write((byte)0);
                for (int i = 0; i < AC[0].var_1.Count; i++)
                {
                    writer.Write(AC[0].var_1[i]);
                }
                writer.Write(AC[0].attitude.Length + 1);
                bytes = Encoding.ASCII.GetBytes(AC[0].attitude);
                foreach (byte b in bytes)
                {
                    writer.Write(b);
                }
                writer.Write((byte)0);
                writer.Write((int)0);
                for (int i = 0; i < 1140; i++)
                {
                    writer.Write((byte)0xFF);
                }
                // Another nps
                for (int j = 1; j < AC.Count; j++)
                {
                    for (int i = 0; i < AC[j].var_0.Count; i++)
                    {
                        writer.Write(Convert.ToInt16(AC[j].var_0[i]));
                    }
                    writer.Write(AC[j].name.Length + 1);
                    bytes = Encoding.ASCII.GetBytes(AC[j].name);
                    foreach (byte b in bytes)
                    {
                        writer.Write(b);
                    }
                    writer.Write((byte)0);
                    for (int i = 0; i < AC[j].var_1.Count; i++)
                    {
                        writer.Write(AC[j].var_1[i]);
                    }
                    writer.Write(AC[j].attitude.Length + 1);
                    bytes = Encoding.ASCII.GetBytes(AC[j].attitude);
                    foreach (byte b in bytes)
                    {
                        writer.Write(b);
                    }
                    writer.Write((byte)0);
                    writer.Write(AC[j].type.Length + 1);
                    bytes = Encoding.ASCII.GetBytes(AC[j].type);
                    foreach (byte b in bytes)
                    {
                        writer.Write(b);
                    }
                    writer.Write((byte)0);
                    for (int i = 0; i < AC[j].var_2.Count; i++)
                    {
                        writer.Write(AC[j].var_2[i]);
                    }
                }
            }
        }

        static int getInt16(int a, int b)
        {
            return unchecked((short)(b * 256 + a));
        }
        static int getInt32(int a, int b, int c, int d)
        {
            return unchecked((int)(d * 256 * 256 * 256 + c * 256 * 256 + b * 256 + a));
        }
    }
    public class Eggs
    {
        public Point pos;
        public int type, agentClass, map;
        public int var0, var1, var2, var3, var4;
        public Eggs(int[] e)
        {
            this.pos.X = e[0];
            this.pos.Y = e[1];
            this.type = e[8];
            this.var0 = e[3];
            this.var1 = e[4];
            this.var2 = e[5];
            this.var3 = e[6];
            this.var4 = e[7];
            this.agentClass = e[2];
            this.map = e[9];
        }
    }
    public class AgentClasses
    {
        public String name;
        public String attitude;
        public String type;
        public List<int> var_0 = new List<int>();
        public List<int> var_1 = new List<int>();
        public List<int> var_2 = new List<int>();
        public static int LVL_0 = 6;
        public static int LVL_1 = 19;
        public static int attack = 17;
        public static int protection = 18;
        public static int view_radius = 20;
        public static int hearing_radius = 21;
        public static int lightning_resistance = 22;
        public static int poison_resistance = 23;
        public static int fire_resistance = 24;
        public static int spirit_resistance = 25;
        public static int armor = 32;
    }
}

/*
 * создаем классы
 * создаем списки с экземплярами в gamedata
 * создаем функцию загрузчика
 */