using DivEditor;
using DivEditor.Controls;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Forms.NET.Controls;
using MonoGame.Forms.NET.Services;
using SharpDX.Direct3D11;
using SharpDX.Direct3D9;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Diagnostics;
using System.Drawing;
using System.IO.Compression;
using System.Windows.Forms;
using static System.Net.Mime.MediaTypeNames;
using ButtonState = Microsoft.Xna.Framework.Input.ButtonState;
using Color = Microsoft.Xna.Framework.Color;
using Mouse = Microsoft.Xna.Framework.Input.Mouse;
using Point = Microsoft.Xna.Framework.Point;
using Rectangle = Microsoft.Xna.Framework.Rectangle;
using Texture2D = Microsoft.Xna.Framework.Graphics.Texture2D;

namespace Editor.Controls
{
    public class MGGraphicalOutput : MonoGameControl
    {
        private static List<ObjectsShow> objectsShow = new();   // Список отображаемых объектов
        static int WindowWidth, WindowHeight;
        static int vScrollPos = 0;
        static int hScrollPos = 0;
        private Texture2D? vScroll, hScroll, point, test;
        private static List<DivEditor.Controls.Sprite>? loadSprite;
        private static Texture2D[,] TilesUpTextures = new Texture2D[Vars.maxVerticalTails, Vars.maxHorizontalTails];
        private static Texture2D[,] TilesDownTextures = new Texture2D[Vars.maxVerticalTails, Vars.maxHorizontalTails];
        private static Texture2D[]? objectTextures;
        private static Texture2D[]? tileTextures;
        private static Texture2D[]? agentTextures;
        private SpriteFont? DrawFont;
        static bool mouseLBState;
        static bool mouseRBState;
        static bool mouseLBOldState;
        static bool mouseRBOldState;
        static bool cursorInWindow;
        static Point mouseOldPosition;
        static bool vScrollChange;
        static bool hScrollChange;
        static bool mouseClickHandler = false;
        public static int selectedObjID = -1;
        public static List<int[]> newObject = new();    // 0 - мировой номер, 1 - SpriteID, 2 - поведение, 3 - координата Х, 4 - координата Y, 5 - сортировка, 6 - смещение по Х, 7 - смещение по Y, 8 - общее смещение по Х, 9 - общее смещение по Y
        public static bool procMovingObject = false;
        public static bool procMovingNewObject = false;
        public static bool procMovingNewEgg = false;
        public static bool procMovingCopyObject = false;
        public static bool procMovingCopyEgg = false;
        int procMovObjCursOffsetX = 0;
        int procMovObjCursOffsetY = 0;
        int procMovObjCorInTileX;
        int procMovObjCorInTileY;

        Microsoft.Xna.Framework.Graphics.GraphicsDevice? graphics;

        static int vScrollDiff = 0;
        static int hScrollDiff = 0;
        public static int tileBiasY, tileBiasX; // Координаты смещения экрана в плитках
        long timer = 0;


        //------------------------------------------------------------------------------------------------------------------------
        protected override void Initialize()
        {
            SetMultiSampleCount(8);
            //Components.Remove(Editor.FPSCounter);
            //Editor.RemoveDefaultComponents();
            vScroll = Editor.Content.Load<Texture2D>("images/vScroll");
            hScroll = Editor.Content.Load<Texture2D>("images/hScroll");
            point = Editor.Content.Load<Texture2D>("images/point");
            DrawFont = Editor.Content.Load<SpriteFont>("text");
            graphics = Editor.GraphicsDevice;
            System.Diagnostics.Debug.WriteLine("Graphic Profile: " + Editor.GraphicsDevice.GraphicsProfile);

        }
        //------------------------------------------------------------------------------------------------------------------------
        protected override void Update(GameTime gameTime)
        {
            if (GameData.READY && EditForm.formIsActive)
            {
                if (tileTextures == null || objectTextures == null) // Загрузка текстур
                {
                    Cursor.Current = Cursors.WaitCursor;
                    loadSprite = FileManager.GetSprites(GameData.pathToTileTexturesFolder); // tile
                    if (loadSprite != null)
                    {
                        tileTextures = new Texture2D[loadSprite.Count];
                        for (int i = 0; i < loadSprite.Count; i++)
                        {
                            tileTextures[i] = new Texture2D(graphics, 64, 64);
                            tileTextures[i].SetData(0, new Rectangle(0, 0, 64, 64), loadSprite[i].Color, 0, 64 * 64);
                        }
                    }
                    loadSprite = FileManager.GetSprites(GameData.pathToObjectsTexturesFolder); // objects
                    if (loadSprite != null)
                    {
                        objectTextures = new Texture2D[loadSprite.Count];
                        for (int i = 0; i < loadSprite.Count; i++)
                        {
                            objectTextures[i] = new Texture2D(graphics, loadSprite[i].Wigth, loadSprite[i].Heigth);
                            objectTextures[i].SetData(0, new Rectangle(0, 0, loadSprite[i].Wigth, loadSprite[i].Heigth), loadSprite[i].Color, 0, loadSprite[i].Wigth * loadSprite[i].Heigth);
                        }
                    }
                    loadSprite?.Clear();
                    // Загружаем текстуры НПС
                    int spriteCount = Directory.GetFiles(Vars.dirAgentClassesImg).Length;
                    agentTextures = new Texture2D[spriteCount];
                    for (int i = 0; i < spriteCount; i++)
                    {
                        agentTextures[i] = Editor.Content.Load<Texture2D>("units\\" + i);
                    }
                    UpdateFullTileTexture();
                    Cursor.Current = Cursors.Default;
                }
                mouseClickHandler = false;
                WindowWidth = Editor.GraphicsDevice.Viewport.Width;
                WindowHeight = Editor.GraphicsDevice.Viewport.Height;
                MouseState currentMouseState = Mouse.GetState();
                //Сохраняем начальные состояния кнопок мыши
                if (currentMouseState.LeftButton == ButtonState.Pressed) mouseLBState = true;
                else mouseLBState = false;
                if (currentMouseState.RightButton == ButtonState.Pressed) mouseRBState = true;
                else mouseRBState = false;
                if (currentMouseState.X > 0 &&
                    currentMouseState.Y > 0 &&
                    currentMouseState.X < WindowWidth &&
                    currentMouseState.Y < WindowHeight)
                {
                    cursorInWindow = true;
                }
                else cursorInWindow = false;

                bool shift = Keyboard.IsKeyDown(System.Windows.Forms.Keys.Shift);
                bool ctrl = Keyboard.IsKeyDown(System.Windows.Forms.Keys.Control);
                bool alt = Keyboard.IsKeyDown(System.Windows.Forms.Keys.Alt);

                UpdateTileTexture();
                UpdateShowObjects();

                if (!mouseClickHandler) // Обработка нажатия на ползунок
                {
                    mouseClickHandler = Scrolls(currentMouseState.X, currentMouseState.Y);
                }

                if (Keyboard.IsKeyDown(System.Windows.Forms.Keys.T)) // Обработка клавиши Test
                {
                    UpdateScrollPosition();
                }

                if (Keyboard.IsKeyDown(System.Windows.Forms.Keys.D)) // Обработка клавиши D
                {
                    tileBiasX++;
                    if (shift) tileBiasX += 5;
                    if (tileBiasX > 511) tileBiasX = 511;
                    UpdateScrollPosition();
                }
                if (Keyboard.IsKeyDown(System.Windows.Forms.Keys.A)) // Обработка клавиши A
                {
                    tileBiasX--;
                    if (shift) tileBiasX -= 5;
                    if (tileBiasX < 0) tileBiasX = 0;
                    UpdateScrollPosition();
                }
                if (Keyboard.IsKeyDown(System.Windows.Forms.Keys.S)) // Обработка клавиши S
                {
                    tileBiasY++;
                    if (shift) tileBiasY += 5;
                    if (tileBiasY > 1023) tileBiasY = 1023;
                    UpdateScrollPosition();
                }
                if (Keyboard.IsKeyDown(System.Windows.Forms.Keys.W)) // Обработка клавиши W
                {
                    tileBiasY--;
                    if (shift) tileBiasY -= 5;
                    if (tileBiasY < 0) tileBiasY = 0;
                    UpdateScrollPosition();
                }
                if (EditForm.selectTollBarPage == 0) // Обработка мыши для вкладки текстур
                {
                    // Наложение текстуры
                    if ((mouseLBState || mouseRBState) &&
                    !mouseClickHandler &&
                    currentMouseState.X > 0 &&
                    currentMouseState.Y > 0 &&
                    currentMouseState.X < WindowWidth &&
                    currentMouseState.Y < WindowHeight &&
                    Stopwatch.GetTimestamp() - timer > 200000)
                    {
                        if (shift)
                        {
                            if (mouseLBState)
                            {
                                GameData.TextureMapping(EditForm.selectTextures, currentMouseState.X, currentMouseState.Y, tileBiasX, tileBiasY, false, true);
                            }
                            if (mouseRBState)
                            {
                                GameData.TextureMapping(EditForm.selectTextures, currentMouseState.X, currentMouseState.Y, tileBiasX, tileBiasY, true, true);
                            }
                        }
                        else
                        {
                            if (mouseLBState)
                            {
                                GameData.TextureMapping(EditForm.selectTextures, currentMouseState.X, currentMouseState.Y, tileBiasX, tileBiasY, false, false);
                            }
                            if (mouseRBState)
                            {
                                GameData.TextureMapping(EditForm.selectTextures, currentMouseState.X, currentMouseState.Y, tileBiasX, tileBiasY, true, false);
                            }
                        }
                        mouseClickHandler = true;
                        timer = Stopwatch.GetTimestamp();
                    }
                }
                if ((EditForm.selectTollBarPage == 1 ||
                    EditForm.selectTollBarPage == 2) &&
                    cursorInWindow) // Обработка мыши для вкладки объектов
                {
                    // Выбор объекта
                    if (mouseLBState &&
                        !procMovingObject &&
                        !mouseLBOldState &&
                        !mouseRBState &&
                        !mouseClickHandler &&
                        Stopwatch.GetTimestamp() - timer > 500000)
                    {
                        for (int i = objectsShow.Count - 1; i >= 0; i--)
                        {
                            if (CheckCursorInSprite(objectsShow[i], tileBiasX, tileBiasY, currentMouseState.X, currentMouseState.Y))
                            {
                                selectedObjID = objectsShow[i].ID;
                                break;
                            }
                            else
                            {
                                selectedObjID = -1;
                            }
                        }
                        procMovingObject = false;
                        mouseClickHandler = true;
                        timer = Stopwatch.GetTimestamp();
                    }
                    // Начало перемещения объекта
                    if (mouseLBState &&
                        mouseLBOldState &&
                        !procMovingObject &&
                        !procMovingCopyObject &&
                        !procMovingNewObject &&
                        !mouseRBState &&
                        !mouseClickHandler &&
                        Stopwatch.GetTimestamp() - timer > 2000000)
                    {
                        for (int i = objectsShow.Count - 1; i >= 0; i--)
                        {
                            if (CheckCursorInSprite(objectsShow[i], tileBiasX, tileBiasY, currentMouseState.X, currentMouseState.Y))
                            {
                                if (selectedObjID == objectsShow[i].ID)
                                {
                                    if (shift) // Копируем объект или болванчик
                                    {
                                        if (objectsShow[i].eggs)
                                        {

                                        }
                                        else
                                        {
                                            Objects objNew = new(GameData.objects[selectedObjID].Var_0,
                                                GameData.objects[selectedObjID].Var_1,
                                                GameData.objects[selectedObjID].Var_2,
                                                GameData.objects[selectedObjID].Var_3,
                                                GameData.objects[selectedObjID].Var_4,
                                                GameData.objects[selectedObjID].Var_5,
                                                GameData.objects[selectedObjID].Var_6,
                                                GameData.objects[selectedObjID].Var_7,
                                                GameData.objects[selectedObjID].Var_8,
                                                GameData.objects[selectedObjID].Var_9,
                                                new System.Drawing.Point(0, 0),
                                                GameData.objects[selectedObjID].Var_10,
                                                GameData.objects[selectedObjID].SpriteID)
                                            {
                                                Height = GameData.objects[selectedObjID].Height
                                            };
                                            GameData.objects.Add(objNew);
                                            procMovObjCursOffsetX = currentMouseState.X - objectsShow[i].pos.X;
                                            procMovObjCursOffsetY = currentMouseState.Y - objectsShow[i].pos.Y;
                                            procMovObjCorInTileX = GameData.objects[objectsShow[i].ID].AbsolutePixelPosition.X % 64;
                                            procMovObjCorInTileY = GameData.objects[objectsShow[i].ID].AbsolutePixelPosition.Y % 64;
                                            ObjectBuffer.type = "object";
                                            ObjectBuffer.ID = GameData.objects.Count - 1;
                                            ObjectBuffer.SpriteID = GameData.objects[ObjectBuffer.ID].SpriteID;
                                            ObjectBuffer.State = 3;
                                            ObjectBuffer.Xpos = currentMouseState.X - procMovObjCursOffsetX;
                                            ObjectBuffer.Ypos = currentMouseState.Y - procMovObjCursOffsetY;
                                            //ObjectBuffer.drawDepth = DrawDepth(ObjectBuffer.Ypos, ObjectBuffer.Xpos, ObjectBuffer.ID);
                                            procMovingCopyObject = true;
                                            selectedObjID = -1;
                                            Cursor.Hide();
                                            break;
                                        }
                                    }
                                    else
                                    {
                                        if (objectsShow[i].eggs)
                                        {
                                            procMovObjCursOffsetX = currentMouseState.X - objectsShow[i].pos.X + tileBiasX * Vars.tileSize;
                                            procMovObjCursOffsetY = currentMouseState.Y - objectsShow[i].pos.Y + tileBiasY * Vars.tileSize;
                                            procMovObjCorInTileX = GameData.objects[objectsShow[i].ID].AbsolutePixelPosition.X % 64;
                                            procMovObjCorInTileY = GameData.objects[objectsShow[i].ID].AbsolutePixelPosition.Y % 64;
                                            procMovingObject = true;
                                            ObjectBuffer.type = "egg";
                                            ObjectBuffer.ID = objectsShow[i].ID;
                                            ObjectBuffer.SpriteID = objectsShow[i].SpriteID;
                                            ObjectBuffer.State = 3;
                                            ObjectBuffer.Xpos = currentMouseState.X - procMovObjCursOffsetX;
                                            ObjectBuffer.Ypos = currentMouseState.Y - procMovObjCursOffsetY;
                                            //ObjectBuffer.drawDepth = (ulong)ObjectBuffer.Ypos * Vars.maxHorizontalTails * Vars.tileSize + (ulong)ObjectBuffer.Xpos;
                                            Cursor.Hide();
                                            break;
                                        }
                                        else
                                        {
                                            procMovObjCursOffsetX = currentMouseState.X - objectsShow[i].pos.X;
                                            procMovObjCursOffsetY = currentMouseState.Y - objectsShow[i].pos.Y;
                                            procMovObjCorInTileX = GameData.objects[objectsShow[i].ID].AbsolutePixelPosition.X % 64;
                                            procMovObjCorInTileY = GameData.objects[objectsShow[i].ID].AbsolutePixelPosition.Y % 64;
                                            procMovingObject = true;
                                            ObjectBuffer.type = "object";
                                            ObjectBuffer.ID = objectsShow[i].ID;
                                            ObjectBuffer.SpriteID = GameData.objects[ObjectBuffer.ID].SpriteID;
                                            ObjectBuffer.State = 3;
                                            ObjectBuffer.Xpos = currentMouseState.X - procMovObjCursOffsetX;
                                            ObjectBuffer.Ypos = currentMouseState.Y - procMovObjCursOffsetY;
                                            //ObjectBuffer.drawDepth = DrawDepth(ObjectBuffer.Ypos, ObjectBuffer.Xpos, ObjectBuffer.ID);
                                            ObjectBuffer.ParentTileObjPosX = GameData.objects[objectsShow[i].ID].TilePosition.X;
                                            ObjectBuffer.ParentTileObjPosY = GameData.objects[objectsShow[i].ID].TilePosition.Y;
                                            Cursor.Hide();
                                            break;
                                        }
                                        
                                    }
                                }
                            }
                            else
                            {
                                procMovingObject = false;
                                procMovingCopyObject = false;
                            }
                        }
                        mouseClickHandler = true;
                        timer = Stopwatch.GetTimestamp();
                    }
                    // Перемещение объекта
                    if (mouseLBState &&
                        mouseLBOldState &&
                        (procMovingObject ||
                        procMovingCopyObject) &&
                        !mouseClickHandler &&
                        Stopwatch.GetTimestamp() - timer > 20000)
                    {
                        if (ObjectBuffer.type == "object")
                        {
                            if (mouseRBState)
                            {
                                ObjectBuffer.Xpos = (currentMouseState.X - procMovObjCursOffsetX) / 64 * 64 + procMovObjCorInTileX;
                                ObjectBuffer.Ypos = (currentMouseState.Y - procMovObjCursOffsetY) / 64 * 64 + procMovObjCorInTileY;
                                if (ObjectBuffer.Xpos + tileBiasX * Vars.tileSize < 0) ObjectBuffer.Xpos = -tileBiasX * Vars.tileSize;
                                if (ObjectBuffer.Ypos + tileBiasY * Vars.tileSize < 0) ObjectBuffer.Ypos = -tileBiasY * Vars.tileSize;
                            }
                            else
                            {
                                ObjectBuffer.Xpos = currentMouseState.X - procMovObjCursOffsetX;
                                ObjectBuffer.Ypos = currentMouseState.Y - procMovObjCursOffsetY;
                                if (ObjectBuffer.Xpos + tileBiasX * Vars.tileSize < 0) ObjectBuffer.Xpos = -tileBiasX * Vars.tileSize;
                                if (ObjectBuffer.Ypos + tileBiasY * Vars.tileSize < 0) ObjectBuffer.Ypos = -tileBiasY * Vars.tileSize;
                            }
                            //ObjectBuffer.drawDepth = DrawDepth(ObjectBuffer.Ypos, ObjectBuffer.Xpos, ObjectBuffer.ID);
                            //WriteLine(ObjectBuffer.drawDepth.ToString());
                        }
                        if (ObjectBuffer.type == "egg")
                        {
                            ObjectBuffer.Xpos = currentMouseState.X - procMovObjCursOffsetX + tileBiasX * Vars.tileSize;
                            ObjectBuffer.Ypos = currentMouseState.Y - procMovObjCursOffsetY + tileBiasY * Vars.tileSize;
                            if (ObjectBuffer.Xpos + tileBiasX * Vars.tileSize < 0) ObjectBuffer.Xpos = -tileBiasX * Vars.tileSize;
                            if (ObjectBuffer.Ypos + tileBiasY * Vars.tileSize < 0) ObjectBuffer.Ypos = -tileBiasY * Vars.tileSize;
                            //ObjectBuffer.drawDepth = (ulong)ObjectBuffer.Ypos * Vars.maxHorizontalTails * Vars.tileSize + (ulong)ObjectBuffer.Xpos;
                            //WriteLine(ObjectBuffer.drawDepth.ToString());
                        }
                        timer = Stopwatch.GetTimestamp();
                    }
                    // Вставка объекта после перемещения
                    if (!mouseLBState &&
                        mouseLBOldState &&
                        (procMovingObject ||
                        procMovingCopyObject) &&
                        !mouseClickHandler)
                    {
                        if (procMovingCopyObject)
                        {
                            if (ObjectBuffer.type == "object")
                            {
                                int AbsolutePixelPositionX = ObjectBuffer.Xpos + tileBiasX * Vars.tileSize;
                                int AbsolutePixelPositionY = ObjectBuffer.Ypos + tileBiasY * Vars.tileSize;
                                int tileX = AbsolutePixelPositionX / Vars.tileSize;
                                int tileY = AbsolutePixelPositionY / Vars.tileSize;
                                if (tileX >= Vars.maxHorizontalTails) tileX = Vars.maxHorizontalTails - 1;
                                if (tileY >= Vars.maxVerticalTails) tileY = Vars.maxVerticalTails - 1;
                                if (GameData.metaTileArray[tileY, tileX].GetObjectsCount() < Vars.maxObjectsCount)
                                {
                                    GameData.metaTileArray[tileY, tileX].AddObject(ObjectBuffer.ID);
                                }
                                GameData.objects[ObjectBuffer.ID].AbsolutePixelPosition = new System.Drawing.Point(AbsolutePixelPositionX, AbsolutePixelPositionY);
                                GameData.objects[ObjectBuffer.ID].TilePosition = new System.Drawing.Point(tileX, tileY);
                            }
                            if (ObjectBuffer.type == "egg")
                            {
                                GameData.Eggs[ObjectBuffer.ID].pos.X = ObjectBuffer.Xpos;
                                GameData.Eggs[ObjectBuffer.ID].pos.Y = ObjectBuffer.Ypos;
                            }
                        }
                        else
                        {
                            if (ObjectBuffer.type == "object")
                            {
                                int X = ObjectBuffer.ParentTileObjPosX;
                                int Y = ObjectBuffer.ParentTileObjPosY;
                                GameData.metaTileArray[Y, X].DelObject(ObjectBuffer.ID);
                                int AbsolutePixelPositionX = ObjectBuffer.Xpos + tileBiasX * Vars.tileSize;
                                int AbsolutePixelPositionY = ObjectBuffer.Ypos + tileBiasY * Vars.tileSize;
                                int tileX = AbsolutePixelPositionX / Vars.tileSize;
                                int tileY = AbsolutePixelPositionY / Vars.tileSize;
                                if (tileX >= Vars.maxHorizontalTails) tileX = Vars.maxHorizontalTails - 1;
                                if (tileY >= Vars.maxVerticalTails) tileY = Vars.maxVerticalTails - 1;
                                if (GameData.metaTileArray[tileY, tileX].GetObjectsCount() < Vars.maxObjectsCount)
                                {
                                    GameData.metaTileArray[tileY, tileX].AddObject(ObjectBuffer.ID);
                                }
                                GameData.objects[ObjectBuffer.ID].AbsolutePixelPosition = new System.Drawing.Point(AbsolutePixelPositionX, AbsolutePixelPositionY);
                                GameData.objects[ObjectBuffer.ID].TilePosition = new System.Drawing.Point(tileX, tileY);
                            }
                            if (ObjectBuffer.type == "egg")
                            {
                                GameData.Eggs[ObjectBuffer.ID].pos.X = ObjectBuffer.Xpos;
                                GameData.Eggs[ObjectBuffer.ID].pos.Y = ObjectBuffer.Ypos;
                            }
                        }
                        Cursor.Show();
                        ObjectBuffer.Clear();
                        selectedObjID = -1;
                        UpdateShowObjects();
                        procMovingObject = false;
                        procMovingCopyObject = false;
                    }
                    // Перемещение нового болванчика
                    if (procMovingNewEgg &&
                        !mouseLBState &&
                        Stopwatch.GetTimestamp() - timer > 20000)
                    {
                        GameData.Eggs[^1].pos.X = currentMouseState.X - procMovObjCursOffsetX + tileBiasX * Vars.tileSize;
                        GameData.Eggs[^1].pos.Y = currentMouseState.Y - procMovObjCursOffsetY + tileBiasY * Vars.tileSize;
                        timer = Stopwatch.GetTimestamp();
                    }
                    // Вставка нового болванчика
                    if (procMovingNewEgg &&
                        !mouseClickHandler &&
                        mouseLBState &&
                        Stopwatch.GetTimestamp() - timer > 20000)
                    {
                        procMovingNewEgg = false;
                        ObjectBuffer.Clear();
                        Cursor.Show();
                    }
                    // Перемещение нового объекта
                    if (newObject.Count > 0 && 
                        procMovingNewObject &&
                        !mouseLBState &&
                        Stopwatch.GetTimestamp() - timer > 20000) 
                    {
                        for(int i = 0; i < newObject.Count; i++) 
                        {
                            if (mouseRBState)
                            {
                                newObject[i][3] = currentMouseState.X / Vars.tileSize * Vars.tileSize + newObject[i][6] + newObject[i][8];
                                newObject[i][4] = currentMouseState.Y / Vars.tileSize * Vars.tileSize + newObject[i][7] + newObject[i][9];
                                newObject[i][5] = (int)DrawDepth(newObject[i][4], newObject[i][3], newObject[i][0]);
                            }
                            else
                            {
                                newObject[i][3] = currentMouseState.X + newObject[i][6] + newObject[i][8];
                                newObject[i][4] = currentMouseState.Y + newObject[i][7] + newObject[i][9];
                                newObject[i][5] = (int)DrawDepth(newObject[i][4], newObject[i][3], newObject[i][0]);
                            }
                        }
                        timer = Stopwatch.GetTimestamp();
                    }
                    // Вставка нового объекта
                    if (newObject.Count > 0 &&
                        procMovingNewObject &&
                        !mouseClickHandler &&
                        mouseLBState &&
                        Stopwatch.GetTimestamp() - timer > 20000)
                    {
                        for (int i = 0; i < newObject.Count; i++)
                        {
                            int tileX = tileBiasX + (newObject[i][3] / Vars.tileSize);
                            int tileY = tileBiasY + (newObject[i][4] / Vars.tileSize);
                            if (GameData.metaTileArray[tileY, tileX].GetObjectsCount() < Vars.maxObjectsCountInTile)
                            {
                                GameData.metaTileArray[tileY, tileX].AddObject(newObject[i][0]);
                                GameData.objects[newObject[i][0]].AbsolutePixelPosition = new System.Drawing.Point(newObject[i][3] + tileBiasX * Vars.tileSize, newObject[i][4] + tileBiasY * Vars.tileSize);
                                GameData.objects[newObject[i][0]].TilePosition = new System.Drawing.Point(tileX, tileY);
                                procMovingNewObject = false;
                            }
                        }
                        UpdateShowObjects();
                        newObject.Clear();
                        mouseClickHandler = false;
                        Cursor.Show();
                        timer = Stopwatch.GetTimestamp();
                    }
                    // Удаление объекта или болванчика
                    if (Keyboard.IsKeyDown(System.Windows.Forms.Keys.Delete) &&
                        selectedObjID >= 0) 
                    {
                        for (int i = 0; i < objectsShow.Count; i++) 
                        { 
                            if (objectsShow[i].ID == selectedObjID)
                            {
                                if (objectsShow[i].eggs)
                                {
                                    GameData.Eggs.RemoveAt(objectsShow[i].ID);
                                    selectedObjID = -1;
                                }
                                else
                                {
                                    int X = GameData.objects[selectedObjID].TilePosition.X;
                                    int Y = GameData.objects[selectedObjID].TilePosition.Y;
                                    GameData.metaTileArray[Y, X].DelObject(selectedObjID);
                                    selectedObjID = -1;
                                }
                            }
                        }
                    }
                    // Обработка клавиши Ctrl + C
                    if (Keyboard.IsKeyDown(System.Windows.Forms.Keys.Control) && 
                        Keyboard.IsKeyDown(System.Windows.Forms.Keys.C) &&
                        selectedObjID >= 0 &&
                        !procMovingNewObject &&
                        Stopwatch.GetTimestamp() - timer > 2000000) 
                    {
                        
                        timer = Stopwatch.GetTimestamp();
                    }
                    // Обработка клавиши Space
                    if (Keyboard.IsKeyDown(System.Windows.Forms.Keys.Space) &&
                        selectedObjID >= 0 &&
                        !procMovingNewObject &&
                        Stopwatch.GetTimestamp() - timer > 2000000) 
                    {
                        
                    }
                }
                if (EditForm.selectTollBarPage == 2 && cursorInWindow) // Обработка мыши для вкладки болванчиков
                {

                }
                if (EditForm.selectTollBarPage == 3 && cursorInWindow) // Обработка мыши для вкладки эффектов
                {
                    // Наложение эффектов
                    if (mouseLBState &&
                    !mouseClickHandler &&
                    Stopwatch.GetTimestamp() - timer > 200000)
                    {
                        GameData.EffectsMapping(currentMouseState.X, currentMouseState.Y, tileBiasX, tileBiasY, EditForm.effects);
                        mouseClickHandler = true;
                        timer = Stopwatch.GetTimestamp();
                    }
                    // Удаление эффектов ПКМ
                    if (mouseRBState &&
                    !mouseClickHandler &&
                    Stopwatch.GetTimestamp() - timer > 200000)
                    {
                        GameData.EffectsMapping(currentMouseState.X, currentMouseState.Y, tileBiasX, tileBiasY, 0);
                        mouseClickHandler = true;
                        timer = Stopwatch.GetTimestamp();
                    }
                }
                // Сохраняем последние состояния кнопок мыши
                if (currentMouseState.LeftButton == ButtonState.Pressed) mouseLBOldState = true;
                else mouseLBOldState = false;
                if (currentMouseState.RightButton == ButtonState.Pressed) mouseRBOldState = true;
                else mouseRBOldState = false;
                mouseOldPosition.X = currentMouseState.X;
                mouseOldPosition.Y = currentMouseState.Y;
            }
        }
        //------------------------------------------------------------------------------------------------------------------------
        protected override void Draw()
        {
            if (GameData.READY)
            {
                Editor.BeginAntialising();
                Editor.spriteBatch.Begin();

                for (int y = 0; y < WindowHeight / Vars.tileSize + 1; y++) // Отображаем текстуры
                {
                    for (int x = 0; x < WindowWidth / Vars.tileSize + 1; x++)
                    {
                        if (y + tileBiasY < Vars.maxVerticalTails && x + tileBiasX < Vars.maxHorizontalTails && TilesDownTextures[y + tileBiasY, x + tileBiasX] != null)
                        {
                            Editor.spriteBatch.Draw(TilesDownTextures[y + tileBiasY, x + tileBiasX], new Vector2(x * Vars.tileSize, y * Vars.tileSize), Color.White);
                            if (TilesUpTextures[y + tileBiasY, x + tileBiasX] != null)
                            {
                                Editor.spriteBatch.Draw(TilesUpTextures[y + tileBiasY, x + tileBiasX], new Vector2(x * Vars.tileSize, y * Vars.tileSize), Color.White);
                            }
                        }
                    }
                }
                if (EditForm.selectTollBarPage == 3) // Отображаем эффекты плитки
                {
                    for (int y = 0; y < WindowHeight / Vars.tileSize + 1; y++)
                    {
                        for (int x = 0; x < WindowWidth / Vars.tileSize + 1; x++)
                        {
                            if (y + tileBiasY < Vars.maxVerticalTails && x + tileBiasX < Vars.maxHorizontalTails)
                            {
                                if (GameData.metaTileArray[y + tileBiasY, x + tileBiasX].TileEffects != 0)
                                { // Если есть эффект
                                    if (((GameData.metaTileArray[y + tileBiasY, x + tileBiasX].TileEffects & (1 << Vars.tileEffectByteNumber_UpDiagonal)) == 0) &&
                                    ((GameData.metaTileArray[y + tileBiasY, x + tileBiasX].TileEffects & (1 << Vars.tileEffectByteNumber_DownDiagonal)) == 0))
                                    { // Если без диагонали
                                        for (int a = 0; a < 64; a++)
                                        {
                                            Editor.spriteBatch.Draw(point, new Rectangle(x * Vars.tileSize, y * Vars.tileSize + a, 1, 1), Color.Gray);
                                            Editor.spriteBatch.Draw(point, new Rectangle(x * Vars.tileSize + a, y * Vars.tileSize, 1, 1), Color.Gray);
                                            Editor.spriteBatch.Draw(point, new Rectangle(x * Vars.tileSize + 64, y * Vars.tileSize + a, 1, 1), Color.Gray);
                                            Editor.spriteBatch.Draw(point, new Rectangle(x * Vars.tileSize + a, y * Vars.tileSize + 64, 1, 1), Color.Gray);
                                        }
                                    }
                                    else if ((GameData.metaTileArray[y + tileBiasY, x + tileBiasX].TileEffects & (1 << Vars.tileEffectByteNumber_UpDiagonal)) != 0)
                                    { // Если верхняя диагонать
                                        for (int a = 0; a < 64; a++)
                                        {
                                            Editor.spriteBatch.Draw(point, new Rectangle(x * Vars.tileSize, y * Vars.tileSize + a, 1, 1), Color.Gray);
                                            Editor.spriteBatch.Draw(point, new Rectangle(x * Vars.tileSize + a, y * Vars.tileSize, 1, 1), Color.Gray);
                                            Editor.spriteBatch.Draw(point, new Rectangle(x * Vars.tileSize + a, y * Vars.tileSize + 64 - a, 1, 1), Color.Gray);
                                        }
                                    }
                                    else
                                    { // Если нижняя диагонать
                                        for (int a = 0; a < 64; a++)
                                        {
                                            Editor.spriteBatch.Draw(point, new Rectangle(x * Vars.tileSize + 64, y * Vars.tileSize + a, 1, 1), Color.Gray);
                                            Editor.spriteBatch.Draw(point, new Rectangle(x * Vars.tileSize + a, y * Vars.tileSize + 64, 1, 1), Color.Gray);
                                            Editor.spriteBatch.Draw(point, new Rectangle(x * Vars.tileSize + a, y * Vars.tileSize + 64 - a, 1, 1), Color.Gray);
                                        }
                                    }
                                }
                                if ((GameData.metaTileArray[y + tileBiasY, x + tileBiasX].TileEffects & (1 << Vars.tileEffectByteNumber_Water)) != 0)
                                { // Вода
                                    Editor.spriteBatch.DrawString(DrawFont, "water", new Vector2(x * Vars.tileSize + 3, y * Vars.tileSize - 3), Color.Aqua);
                                }
                                if ((GameData.metaTileArray[y + tileBiasY, x + tileBiasX].TileEffects & (1 << Vars.tileEffectByteNumber_Indoors)) != 0)
                                { // Помещение
                                    Editor.spriteBatch.DrawString(DrawFont, "indoor", new Vector2(x * Vars.tileSize + 3, y * Vars.tileSize + 12), Color.Coral);
                                }
                                if ((GameData.metaTileArray[y + tileBiasY, x + tileBiasX].TileEffects & (1 << Vars.tileEffectByteNumber_Fog)) != 0)
                                { // Туман
                                    Editor.spriteBatch.DrawString(DrawFont, "fog", new Vector2(x * Vars.tileSize + 3, y * Vars.tileSize + 27), Color.LightSteelBlue);
                                }
                                if ((GameData.metaTileArray[y + tileBiasY, x + tileBiasX].TileEffects & (1 << Vars.tileEffectByteNumber_Object)) != 0)
                                { // Объект
                                    Editor.spriteBatch.DrawString(DrawFont, "object", new Vector2(x * Vars.tileSize + 3, y * Vars.tileSize + 42), Color.LightSteelBlue);
                                }
                            }
                        }
                    }
                }
                for (int i = 0; i < objectsShow.Count; i++) // Отображаем объекты
                {
                    if (objectsShow[i].eggs) // Если отбъект болванчик
                    {
                        if (objectsShow[i].state > 0)
                        {
                            int X = objectsShow[i].pos.X - tileBiasX * Vars.tileSize;
                            int Y = objectsShow[i].pos.Y - tileBiasY * Vars.tileSize;
                            Texture2D? egg = agentTextures[objectsShow[i].AgentClass];
                            int IMGPosX = X - egg.Width / 2;
                            int IMGPosY = Y - egg.Height;
                            if (objectsShow[i].state == 4)
                            {
                                if (GameData.AC[objectsShow[i].AgentClass].attitude == "Evil npc" ||
                                    GameData.AC[objectsShow[i].AgentClass].attitude == "Orc" ||
                                    GameData.AC[objectsShow[i].AgentClass].attitude == "Troll" ||
                                    GameData.AC[objectsShow[i].AgentClass].attitude == "Wasp" ||
                                    GameData.AC[objectsShow[i].AgentClass].attitude == "Corpse" ||
                                    GameData.AC[objectsShow[i].AgentClass].attitude == "Evil Assassin" ||
                                    GameData.AC[objectsShow[i].AgentClass].attitude == "Imp" ||
                                    GameData.AC[objectsShow[i].AgentClass].attitude == "Dragon")
                                {
                                    Editor.spriteBatch.Draw(egg, new Vector2(IMGPosX, IMGPosY), Color.Red);
                                }
                                else
                                {
                                    if (GameData.AC[objectsShow[i].AgentClass].attitude == "Good npc" ||
                                        GameData.AC[objectsShow[i].AgentClass].attitude == "Rabbit")
                                    {
                                        Editor.spriteBatch.Draw(egg, new Vector2(IMGPosX, IMGPosY), Color.GreenYellow);
                                    }
                                    else
                                    {
                                        Editor.spriteBatch.Draw(egg, new Vector2(IMGPosX, IMGPosY), Color.Yellow);
                                    }
                                }
                                String message = objectsShow[i].AgentClass + " " + GameData.AC[objectsShow[i].AgentClass].name;
                                Vector2 Vr = new Vector2(X - (Editor.Font.MeasureString(message).X / 2), Y);
                                Editor.spriteBatch.DrawString(Editor.Font, message, Vr, Color.White);

                                message = GameData.AC[objectsShow[i].AgentClass].type;
                                Vr = new Vector2(X - (Editor.Font.MeasureString(message).X / 2), Y + 16);
                                Editor.spriteBatch.DrawString(Editor.Font, message, Vr, Color.White);

                                message = GameData.AC[objectsShow[i].AgentClass].attitude;
                                Vr = new Vector2(X - (Editor.Font.MeasureString(message).X / 2), Y + 32);
                                Editor.spriteBatch.DrawString(Editor.Font, message, Vr, Color.White);
                            }
                            else
                            {
                                if (objectsShow[i].ID == selectedObjID)
                                {
                                    Editor.spriteBatch.Draw(egg, new Vector2(IMGPosX, IMGPosY), Color.Yellow);
                                    String message = objectsShow[i].AgentClass + " " + GameData.AC[objectsShow[i].AgentClass].name;
                                    Vector2 Vr = new Vector2(X - (Editor.Font.MeasureString(message).X / 2), Y);
                                    Editor.spriteBatch.DrawString(Editor.Font, message, Vr, Color.White);

                                    message = GameData.AC[objectsShow[i].AgentClass].type;
                                    Vr = new Vector2(X - (Editor.Font.MeasureString(message).X / 2), Y + 16);
                                    Editor.spriteBatch.DrawString(Editor.Font, message, Vr, Color.White);

                                    message = GameData.AC[objectsShow[i].AgentClass].attitude;
                                    Vr = new Vector2(X - (Editor.Font.MeasureString(message).X / 2), Y + 32);
                                    Editor.spriteBatch.DrawString(Editor.Font, message, Vr, Color.White);
                                }
                                else
                                {
                                    Editor.spriteBatch.Draw(egg, new Vector2(IMGPosX, IMGPosY), Color.White);
                                }  
                            }
                        }
                    }
                    else
                    {
                        if (objectsShow[i].state > 0)
                        { // Если отбъект должен быть отображен
                            int Xcor = objectsShow[i].pos.X;
                            int Ycor = objectsShow[i].pos.Y;
                            int heigth = GameData.objects[objectsShow[i].ID].Height;
                            Vector2 objPosition = new(Xcor, Ycor - heigth);
                            if (objectTextures is not null)
                            {
                                if (objectsShow[i].ID == selectedObjID)
                                { // Если объект выделен
                                    Editor.spriteBatch.Draw(objectTextures[objectsShow[i].SpriteID], objPosition, Color.Yellow);
                                    Editor.spriteBatch.DrawString(Editor.Font, GameData.objects[objectsShow[i].ID].PixelPositionInTile.X + " " + GameData.objects[objectsShow[i].ID].PixelPositionInTile.Y, new Vector2(50,50), Color.White);
                                }
                                else
                                {
                                    Editor.spriteBatch.Draw(objectTextures[objectsShow[i].SpriteID], objPosition, Color.White);
                                }
                            }
                        }
                    }
                }
                // Отображаем полосы прокрутки
                Editor.spriteBatch.Draw(vScroll, new Vector2(Editor.GraphicsDevice.Viewport.Width - 28, vScrollPos), Color.White);
                Editor.spriteBatch.Draw(hScroll, new Vector2(hScrollPos, Editor.GraphicsDevice.Viewport.Height - 28), Color.White);

                Editor.spriteBatch.End();
                Editor.EndAntialising();
            }
        }
        //------------------------------------------------------------------------------------------------------------------------
        public static void UpdateFullTileTexture()
        {
            for (int y = 0; y < Vars.maxVerticalTails; y++)
            {
                for (int x = 0; x < Vars.maxHorizontalTails; x++)
                {
                    if (GameData.metaTileArray[y, x].DownTileTexture < 0xffff)
                    {
                        TilesDownTextures[y, x] = tileTextures[GameData.metaTileArray[y, x].DownTileTexture];
                    }
                    if (GameData.metaTileArray[y, x].UpTileTexture < 0xffff)
                    {
                        TilesUpTextures[y, x] = tileTextures[GameData.metaTileArray[y, x].UpTileTexture];
                    }
                }
            }
        }
        //------------------------------------------------------------------------------------------------------------------------
        private static void UpdateTileTexture()
        {
            for (int y = tileBiasY; y < tileBiasY + WindowHeight / Vars.tileSize + 5; y++)
            {
                for (int x = tileBiasX; x < tileBiasX + WindowWidth / Vars.tileSize + 5; x++)
                {
                    if (y < Vars.maxVerticalTails && x < Vars.maxHorizontalTails)
                    {
                        if (GameData.metaTileArray[y, x].DownTileTexture < 0xffff)
                        {
                            TilesDownTextures[y, x] = tileTextures[GameData.metaTileArray[y, x].DownTileTexture];
                        }
                        else
                        {
                            if (TilesDownTextures[y, x] != null) TilesDownTextures[y, x].Dispose();
                        }
                        
                        if (GameData.metaTileArray[y, x].UpTileTexture < 0xffff)
                        {
                            TilesUpTextures[y, x] = tileTextures[GameData.metaTileArray[y, x].UpTileTexture];
                        }
                        else
                        {
                            if (TilesUpTextures[y, x] != null) TilesUpTextures[y, x].Dispose();
                        }
                    }
                }
            }
        }
        //------------------------------------------------------------------------------------------------------------------------
        private static void UpdateShowObjects()
        {
            objectsShow.Clear();
            // Objects
            int state = 1;
            for (int y = -5; y < WindowHeight / Vars.tileSize + 5; y++)
            {
                for (int x = -5; x < WindowWidth / Vars.tileSize + 5; x++)
                {
                    if (y + tileBiasY < Vars.maxVerticalTails && y + tileBiasY >= 0 && x + tileBiasX < Vars.maxHorizontalTails && x + tileBiasX >= 0)
                    {
                        for (int i = 0; i < GameData.metaTileArray[y + tileBiasY, x + tileBiasX].GetObjectsCount(); i++)
                        {
                            int worldNum = GameData.metaTileArray[y + tileBiasY, x + tileBiasX].GetObject(i);
                            int posX = (GameData.objects[worldNum].TilePosition.X - tileBiasX) * Vars.tileSize + GameData.objects[worldNum].PixelPositionInTile.X;
                            int posY = (GameData.objects[worldNum].TilePosition.Y - tileBiasY) * Vars.tileSize + GameData.objects[worldNum].PixelPositionInTile.Y;
                            state = 1;
                            if (ObjectBuffer.ID == worldNum)
                            {
                                state = 2;
                            }
                            if (procMovingObject && ObjectBuffer.ID == worldNum)
                            {
                                state = 0; //Объект невидим
                            }
                            objectsShow.Add(new ObjectsShow(worldNum, state, new Point(posX, posY)));
                        }
                    }
                }
            }
            if (procMovingNewObject)
            {
                for( int i = 0; i < newObject.Count; i++)
                {
                    objectsShow.Add(new ObjectsShow(newObject[i][0], newObject[i][2], new Point(newObject[i][3], newObject[i][4])));
                }
            }
            // Eggs
            int Xmin = tileBiasX * Vars.tileSize;
            int Ymin = tileBiasY * Vars.tileSize;
            int Xmax = tileBiasX * Vars.tileSize + WindowWidth;
            int Ymax = tileBiasY * Vars.tileSize + WindowHeight;
            for (int i = 0; i < GameData.Eggs.Count; i++)
            {
                if (GameData.Eggs[i].pos.X > Xmin &&
                    GameData.Eggs[i].pos.X < Xmax &&
                    GameData.Eggs[i].pos.Y > Ymin &&
                    GameData.Eggs[i].pos.Y < Ymax &&
                    GameData.worldMapNumber == GameData.Eggs[i].map)
                {
                    state = 1;
                    if (procMovingObject && ObjectBuffer.ID == i)
                    {
                        state = 0;
                    }
                    objectsShow.Add(new ObjectsShow(i, state, GameData.Eggs[i].pos, true));
                    if (Keyboard.IsKeyDown(System.Windows.Forms.Keys.Control))
                    {
                        objectsShow[^1].drawDepth = 9999999999999999999; // Увеличиваем глубину прорисовки для вывода болванчиков на передний план
                        objectsShow[^1].state = 4;
                    }
                }
            }
            if ((procMovingObject || procMovingCopyObject) && ObjectBuffer.ID != -1 && ObjectBuffer.type == "object")
            {
                objectsShow.Add(new ObjectsShow(ObjectBuffer.ID, ObjectBuffer.State, new Point(ObjectBuffer.Xpos, ObjectBuffer.Ypos), DrawDepth(ObjectBuffer.Ypos, ObjectBuffer.Xpos, ObjectBuffer.ID)));
            }
            if (procMovingObject && ObjectBuffer.ID != -1 && ObjectBuffer.type == "egg")
            {
                objectsShow.Add(new ObjectsShow(ObjectBuffer.ID, ObjectBuffer.State, new Point(ObjectBuffer.Xpos, ObjectBuffer.Ypos), true));
            }
            objectsShow.Sort((x, y) => x.drawDepth.CompareTo(y.drawDepth));
        }
        //------------------------------------------------------------------------------------------------------------------------
        private bool Scrolls(int mousX, int mousY) //Обработчик полосы прокрутки
        {
            // Размер бегунка 28х53
            bool mouseClickHandler = false;
            if (mouseLBState && !vScrollChange)
            {
                if (mousX > Editor.GraphicsDevice.Viewport.Width - 28 && mousY > vScrollPos && mousY < vScrollPos + 53)
                {
                    vScrollChange = true;
                    vScrollDiff = mousY - vScrollPos;
                    mouseClickHandler = true;
                }
            }
            if (mouseLBOldState && vScrollChange)
            {
                vScrollPos = mousY - vScrollDiff;
                if (vScrollPos < 0) vScrollPos = 0;
                if (vScrollPos > Editor.GraphicsDevice.Viewport.Height - 53) vScrollPos = Editor.GraphicsDevice.Viewport.Height - 53;
                mouseClickHandler = true;
            }
            else vScrollChange = false;

            if (mouseLBState && !hScrollChange)
            {
                if (mousY > Editor.GraphicsDevice.Viewport.Height - 28 && mousX > hScrollPos && mousX < hScrollPos + 53)
                {
                    hScrollChange = true;
                    hScrollDiff = mousX - hScrollPos;
                    mouseClickHandler = true;
                }
            }
            if (mouseLBOldState && hScrollChange)
            {
                hScrollPos = mousX - hScrollDiff;
                if (hScrollPos < 0) hScrollPos = 0;
                if (hScrollPos > Editor.GraphicsDevice.Viewport.Width - 53) hScrollPos = Editor.GraphicsDevice.Viewport.Width - 53;
                mouseClickHandler = true;
            }
            else hScrollChange = false;
            if (mouseClickHandler)
            {
                tileBiasY = (Vars.maxVerticalTails - 1 - WindowHeight / Vars.tileSize) * vScrollPos / (WindowHeight - 53); // 53 - высота иконки прокрутки
                tileBiasX = (Vars.maxHorizontalTails - 1 - WindowWidth / Vars.tileSize) * hScrollPos / (WindowWidth - 53);
                if (hScrollPos > WindowWidth - 53) hScrollPos = WindowWidth - 53;
                if (vScrollPos > WindowHeight - 53) vScrollPos = WindowHeight - 53;
                EditForm.timer.Start();
            }
            return mouseClickHandler;
        }
        //------------------------------------------------------------------------------------------------------------------------
        public static bool CheckCursorInSprite(ObjectsShow obj, int biasX, int biasY, int cursorPosX, int cursorPosY) // Проверка на попадание курсора в спрайт объекта
        {
            if (obj.eggs && agentTextures is not null)
            {
                int objWidth = agentTextures[obj.AgentClass].Width;
                int objHeight = agentTextures[obj.AgentClass].Height;
                //int objPosX = (GameData.objects[obj.ID].TilePosition.X - biasX) * Vars.tileSize + GameData.objects[obj.ID].PixelPositionInTile.X;
                //int objPosY = (GameData.objects[obj.ID].TilePosition.Y - biasY) * Vars.tileSize + GameData.objects[obj.ID].PixelPositionInTile.Y - GameData.objects[obj.ID].Height;
                int X = obj.pos.X - tileBiasX * Vars.tileSize;
                int Y = obj.pos.Y - tileBiasY * Vars.tileSize;
                int objPosX = X - agentTextures[obj.AgentClass].Width / 2;
                int objPosY = Y - agentTextures[obj.AgentClass].Height;
                if (cursorPosX > objPosX &&
                    cursorPosX < objPosX + objWidth &&
                    cursorPosY > objPosY &&
                    cursorPosY < objPosY + objHeight)
                {
                    Color[,] colors2D = GetColorArray(agentTextures[obj.AgentClass]);
                    if (colors2D[cursorPosX - objPosX, cursorPosY - objPosY].A != 0)
                    {
                        return true;
                    }
                    else return false;
                }
                else return false;
            }
            else
            {
                int objWidth = GameData.objectDesc[obj.SpriteID].Width;
                int objHeight = GameData.objectDesc[obj.SpriteID].Height;
                int objPosX = (GameData.objects[obj.ID].TilePosition.X - biasX) * Vars.tileSize + GameData.objects[obj.ID].PixelPositionInTile.X;
                int objPosY = (GameData.objects[obj.ID].TilePosition.Y - biasY) * Vars.tileSize + GameData.objects[obj.ID].PixelPositionInTile.Y - GameData.objects[obj.ID].Height;
                if (cursorPosX > objPosX &&
                    cursorPosX < objPosX + objWidth &&
                    cursorPosY > objPosY &&
                    cursorPosY < objPosY + objHeight)
                {
                    Color[,] colors2D = GetColorArray(objectTextures[obj.SpriteID]);
                    if (colors2D[cursorPosX - objPosX, cursorPosY - objPosY].A != 0)
                    {
                        return true;
                    }
                    else return false;
                }
                else return false;
            }
            
        }
        //------------------------------------------------------------------------------------------------------------------------
        public static Color[,] GetColorArray(Texture2D SelectObj)
        {
            Color[] ColorArray = new Color[SelectObj.Width * SelectObj.Height];
            SelectObj.GetData(ColorArray);
            Color[,] colors2D = new Color[SelectObj.Width, SelectObj.Height];
            for (int r = 0; r < SelectObj.Width; r++)
            {
                for (int t = 0; t < SelectObj.Height; t++)
                {
                    colors2D[r, t] = ColorArray[r + t * SelectObj.Width];
                }
            }
            return colors2D;
        }
        //------------------------------------------------------------------------------------------------------------------------
        public static Texture2D GetTileTexture(int num)
        {
            return tileTextures[num];
        }
        public static Texture2D GetObjectTexture(int num)
        {
            return objectTextures[num];
        }
        public static void SetCursor(int X, int Y)
        {
            tileBiasY = (Vars.maxVerticalTails - 1 - WindowHeight / Vars.tileSize) * vScrollPos / (WindowHeight - 53); // 53 - высота иконки прокрутки
            tileBiasX = (Vars.maxHorizontalTails - 1 - WindowWidth / Vars.tileSize) * hScrollPos / (WindowWidth - 53);
            tileBiasX = X;
            tileBiasY = Y;
            UpdateScrollPosition();
        }
        public static Point GetCursor()
        {
            return new Point(tileBiasX, tileBiasY);
        }
        private static void UpdateScrollPosition()
        {
            if (tileBiasX < 0) tileBiasX = 0;
            if (tileBiasX > Vars.maxHorizontalTails - 1 - WindowWidth / Vars.tileSize) tileBiasX = Vars.maxHorizontalTails - WindowWidth / Vars.tileSize;
            if (tileBiasY < 0) tileBiasY = 0;
            if (tileBiasY > Vars.maxVerticalTails - 1 - WindowHeight / Vars.tileSize) tileBiasY = Vars.maxVerticalTails - WindowHeight / Vars.tileSize;
            hScrollPos = tileBiasX * (WindowWidth - 53) / (Vars.maxHorizontalTails - 1 - WindowWidth / Vars.tileSize);
            vScrollPos = tileBiasY * (WindowHeight - 53) / (Vars.maxVerticalTails - 1 - WindowHeight / Vars.tileSize);
            hScrollPos += 1;
            vScrollPos += 1;
            EditForm.timer.Start();
        }
        private static ulong DrawDepth(int Ypos, int Xpos, int ID) // Число, характеризующее парядок вывода объекта на экран
        {
            //ObjectBuffer.drawDepth = (ObjectBuffer.Ypos + GameData.objectDesc[GameData.objects[ObjectBuffer.ID].SpriteID].TouchPoint.Y + tileBiasY * Vars.tileSize) * Vars.maxHorizontalTails * Vars.tileSize + ObjectBuffer.Xpos + tileBiasX * Vars.tileSize;
            return ((ulong)Ypos + (ulong)GameData.objectDesc[GameData.objects[ID].SpriteID].TouchPoint.Y + (ulong)tileBiasY * Vars.tileSize) * Vars.maxHorizontalTails * Vars.tileSize + (ulong)Xpos + (ulong)tileBiasX * Vars.tileSize;
        }
        private static void WriteLine(String? line)
        {
            if (line is not null) System.Diagnostics.Debug.WriteLine(line);
        }
    }
    public static class ObjectBuffer
    {
        public static int ID, Xpos, Ypos, SpriteID, State, Xbias, Ybias, ParentTileObjPosX, ParentTileObjPosY;
        public static ulong drawDepth;
        public static string type = "";
        public static void Clear()
        {
            ID = -1;
            Xpos = -1;
            Ypos = -1;
            SpriteID = -1;
            drawDepth = 0;
            State = -1;
            Xbias = -1;
            Ybias = -1;
        }
    }
    public class ObjectsShow
    {
        public bool eggs;
        public int ID, SpriteID, state, AgentClass;
        public ulong drawDepth;
        public Point pos, touchPoint;
        public ObjectsShow(int worldID, int state, Point position)
        {
            this.ID = worldID;
            this.SpriteID = GameData.objects[worldID].SpriteID;
            this.drawDepth = ((ulong)GameData.objects[worldID].AbsolutePixelPosition.Y +
                (ulong)GameData.objectDesc[GameData.objects[worldID].SpriteID].TouchPoint.Y) * Vars.maxHorizontalTails * Vars.tileSize +
                (ulong)GameData.objects[worldID].AbsolutePixelPosition.X +
                (ulong)GameData.objectDesc[GameData.objects[worldID].SpriteID].TouchPoint.X;
            this.state = state;
            this.pos = position;
            this.eggs = false;
        }
        public ObjectsShow(int worldID, int state, Point position, ulong drawDepth)
        {
            this.ID = worldID;
            this.SpriteID = GameData.objects[worldID].SpriteID;
            this.drawDepth = drawDepth;
            this.state = state;
            this.pos = position;
            this.eggs = false;
        }
        public ObjectsShow(int ID, int state, Point position, bool eggs)
        {
            this.ID = ID;
            this.state = state;
            this.pos = position;
            this.eggs = eggs;
            this.AgentClass = GameData.Eggs[ID].agentClass;
            this.drawDepth = (ulong)position.Y * Vars.maxHorizontalTails * Vars.tileSize + (ulong)position.X;
        }
    }


}
//System.Diagnostics.Debug.WriteLine("Graphic Profile: " + Editor.GraphicsDevice.GraphicsProfile);

/*
 * добавить удаление объекта +
 * добавить копирование объекта +
 * добавить изменение свойств объекта
 * 
 */