using DivEditor.Controls;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Forms.NET.Controls;
using MonoGame.Forms.NET.Services;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;
using static System.Net.Mime.MediaTypeNames;
using ButtonState = Microsoft.Xna.Framework.Input.ButtonState;
using Color = Microsoft.Xna.Framework.Color;
using Mouse = Microsoft.Xna.Framework.Input.Mouse;
using Point = Microsoft.Xna.Framework.Point;
using Rectangle = Microsoft.Xna.Framework.Rectangle;

namespace Editor.Controls
{
    public class MGGraphicalOutput : MonoGameControl
    {
        static List<int[]> ObjectShow = new(); // 0 - мировой номер, 1 - SpriteID, 2 - поведение, 3 - координата Х, 4 - координата Y, 5 - сортировка
        static int WindowWidth, WindowHeight;
        static int vScrollPos = 0;
        static int hScrollPos = 0;
        private Texture2D? vScroll, hScroll, point, test;
        private static List<Sprite>? tileSprite;
        private static Texture2D[,] TilesUpTextures = new Texture2D[Vars.maxVerticalTails, Vars.maxHorizontalTails];
        private static Texture2D[,] TilesDownTextures = new Texture2D[Vars.maxVerticalTails, Vars.maxHorizontalTails];
        private static Texture2D[]? objectTextures;
        private static Texture2D[]? tileTextures;
        private SpriteFont? DrawFont;
        static bool mouseLBState;
        static bool mouseRBState;
        static bool mouseLBOldState;
        static bool mouseRBOldState;
        static Point mouseOldPosition;
        static bool vScrollChange;
        static bool hScrollChange;
        static bool mouseClickHandler = false;
        public static int selectedObjID = -1;
        public static List<int[]> newObject = new();    // 0 - мировой номер, 1 - SpriteID, 2 - поведение, 3 - координата Х, 4 - координата Y, 5 - сортировка, 6 - смещение по Х, 7 - смещение по Y
        public static bool procMovingObject = false;
        public static bool procMovingNewObject = false;
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
                    tileSprite = FileManager.GetSprites(GameData.pathToTileTexturesFolder);
                    if (tileSprite != null)
                    {
                        tileTextures = new Texture2D[tileSprite.Count];
                        for (int i = 0; i < tileSprite.Count; i++)
                        {
                            tileTextures[i] = new Texture2D(graphics, 64, 64);
                            tileTextures[i].SetData(0, new Rectangle(0, 0, 64, 64), tileSprite[i].Color, 0, 64 * 64);
                        }
                    }
                    tileSprite = FileManager.GetSprites(GameData.pathToObjectsTexturesFolder);
                    if (tileSprite != null)
                    {
                        objectTextures = new Texture2D[tileSprite.Count];
                        for (int i = 0; i < tileSprite.Count; i++)
                        {
                            objectTextures[i] = new Texture2D(graphics, tileSprite[i].Wigth, tileSprite[i].Heigth);
                            objectTextures[i].SetData(0, new Rectangle(0, 0, tileSprite[i].Wigth, tileSprite[i].Heigth), tileSprite[i].Color, 0, tileSprite[i].Wigth * tileSprite[i].Heigth);
                        }
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
                    if (mouseLBState &&
                    !mouseClickHandler &&
                    currentMouseState.X > 0 &&
                    currentMouseState.Y > 0 &&
                    currentMouseState.X < WindowWidth &&
                    currentMouseState.Y < WindowHeight &&
                    Stopwatch.GetTimestamp() - timer > 200000)
                    {
                        GameData.TextureMapping(EditForm.selectTextures, currentMouseState.X, currentMouseState.Y, tileBiasX, tileBiasY);
                        mouseClickHandler = true;
                        timer = Stopwatch.GetTimestamp();
                    }
                }
                if (EditForm.selectTollBarPage == 1) // Обработка мыши для вкладки объектов
                {
                    // Выбор объекта
                    if (mouseLBState &&
                        !procMovingObject &&
                        !mouseLBOldState &&
                        !mouseRBState &&
                        !mouseClickHandler &&
                        currentMouseState.X > 0 &&
                        currentMouseState.Y > 0 &&
                        currentMouseState.X < WindowWidth &&
                        currentMouseState.Y < WindowHeight &&
                        Stopwatch.GetTimestamp() - timer > 500000)
                    {
                        for (int i = ObjectShow.Count - 1; i >= 0; i--)
                        {
                            if (CheckCursorInSprite(ObjectShow[i][0], tileBiasX, tileBiasY, currentMouseState.X, currentMouseState.Y))
                            {
                                selectedObjID = ObjectShow[i][0];
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
                        !mouseRBState &&
                        !mouseClickHandler &&
                        currentMouseState.X > 0 &&
                        currentMouseState.Y > 0 &&
                        currentMouseState.X < WindowWidth &&
                        currentMouseState.Y < WindowHeight &&
                        Stopwatch.GetTimestamp() - timer > 2000000)
                    {
                        for (int i = ObjectShow.Count - 1; i >= 0; i--)
                        {
                            if (CheckCursorInSprite(ObjectShow[i][0], tileBiasX, tileBiasY, currentMouseState.X, currentMouseState.Y))
                            {
                                if (selectedObjID == ObjectShow[i][0])
                                {
                                    procMovObjCursOffsetX = currentMouseState.X - ObjectShow[i][3];
                                    procMovObjCursOffsetY = currentMouseState.Y - ObjectShow[i][4];
                                    procMovObjCorInTileX = GameData.objects[ObjectShow[i][0]].AbsolutePixelPosition.X % 64;
                                    procMovObjCorInTileY = GameData.objects[ObjectShow[i][0]].AbsolutePixelPosition.Y % 64;
                                    procMovingObject = true;
                                    ObjectBuffer.ID = ObjectShow[i][0];
                                    ObjectBuffer.SpriteID = GameData.objects[ObjectBuffer.ID].SpriteID;
                                    ObjectBuffer.State = 3;
                                    ObjectBuffer.Sort = 0;
                                    ObjectBuffer.Xpos = currentMouseState.X - procMovObjCursOffsetX;
                                    ObjectBuffer.Ypos = currentMouseState.Y - procMovObjCursOffsetY;
                                    ObjectBuffer.Sort = ObjectDepth(ObjectBuffer.Ypos, ObjectBuffer.Xpos, ObjectBuffer.ID);
                                    ObjectBuffer.ParentTileObjPosX = GameData.objects[ObjectShow[i][0]].TilePosition.X;
                                    ObjectBuffer.ParentTileObjPosY = GameData.objects[ObjectShow[i][0]].TilePosition.Y;
                                    break;
                                }
                            }
                            else
                            {
                                procMovingObject = false;
                            }
                        }
                        mouseClickHandler = true;
                        timer = Stopwatch.GetTimestamp();
                    }
                    // Перемещение объекта
                    if (mouseLBState &&
                        mouseLBOldState &&
                        procMovingObject &&
                        !mouseRBState &&
                        !mouseClickHandler &&
                        currentMouseState.X > 0 &&
                        currentMouseState.Y > 0 &&
                        currentMouseState.X < WindowWidth &&
                        currentMouseState.Y < WindowHeight &&
                        Stopwatch.GetTimestamp() - timer > 20000)
                    {
                        if (EditForm.objectStepOneCell)
                        {
                            ObjectBuffer.Xpos = (currentMouseState.X - procMovObjCursOffsetX) / 64 * 64 + procMovObjCorInTileX;
                            ObjectBuffer.Ypos = (currentMouseState.Y - procMovObjCursOffsetY) / 64 * 64 + procMovObjCorInTileY;
                        }
                        else
                        {
                            ObjectBuffer.Xpos = currentMouseState.X - procMovObjCursOffsetX;
                            ObjectBuffer.Ypos = currentMouseState.Y - procMovObjCursOffsetY;
                        }
                        ObjectBuffer.Sort = ObjectDepth(ObjectBuffer.Ypos, ObjectBuffer.Xpos, ObjectBuffer.ID);
                        timer = Stopwatch.GetTimestamp();
                    }
                    // Вставка объекта после перемещения
                    if (!mouseLBState &&
                        mouseLBOldState &&
                        procMovingObject &&
                        !mouseRBState &&
                        !mouseClickHandler &&
                        currentMouseState.X > 0 &&
                        currentMouseState.Y > 0 &&
                        currentMouseState.X < WindowWidth &&
                        currentMouseState.Y < WindowHeight)
                    {
                        // Удаляем спрятанный объект из MetaTile
                        //for (int i = 0; i < ObjectShow.Count; i++)
                        //{
                        //    if (ObjectShow[i][2] == 0)
                        //    {
                        //        int X = tileBiasX + (ObjectShow[i][3] / Vars.tileSize);
                        //        int Y = tileBiasY + (ObjectShow[i][4] / Vars.tileSize);
                        //        GameData.metaTileArray[Y, X].DelObject(ObjectShow[i][0]);
                        //    }
                        //}

                        int X = ObjectBuffer.ParentTileObjPosX;
                        int Y = ObjectBuffer.ParentTileObjPosY;
                        GameData.metaTileArray[Y, X].DelObject(ObjectBuffer.ID);

                        System.Diagnostics.Debug.WriteLine(ObjectBuffer.Xpos + "  " + ObjectBuffer.Ypos);
                        if (ObjectBuffer.Xpos < 0) ObjectBuffer.Xpos = 0;
                        if (ObjectBuffer.Ypos < 0) ObjectBuffer.Ypos = 0;
                        if (ObjectBuffer.Xpos > (Vars.maxHorizontalTails - 1) * Vars.tileSize) ObjectBuffer.Xpos = (Vars.maxHorizontalTails - 1) * Vars.tileSize;
                        if (ObjectBuffer.Ypos > (Vars.maxVerticalTails - 1) * Vars.tileSize) ObjectBuffer.Ypos = (Vars.maxVerticalTails - 1) * Vars.tileSize;
                        int tileX = tileBiasX + (ObjectBuffer.Xpos / Vars.tileSize);
                        int tileY = tileBiasY + (ObjectBuffer.Ypos / Vars.tileSize);
                        if (tileX >= Vars.maxHorizontalTails) tileX = Vars.maxHorizontalTails - 1;
                        if (tileY >= Vars.maxVerticalTails) tileY = Vars.maxVerticalTails - 1;
                        if (GameData.metaTileArray[tileY, tileX].GetObjectsCount() < Vars.maxObjectsCount)
                        {
                            GameData.metaTileArray[tileY, tileX].AddObject(ObjectBuffer.ID);
                        }
                        GameData.objects[ObjectBuffer.ID].AbsolutePixelPosition = new System.Drawing.Point(ObjectBuffer.Xpos + tileBiasX * Vars.tileSize, ObjectBuffer.Ypos + tileBiasY * Vars.tileSize);
                        GameData.objects[ObjectBuffer.ID].TilePosition = new System.Drawing.Point(tileX, tileY);
                        ObjectBuffer.Clear();
                        selectedObjID = -1;
                        UpdateShowObjects();
                        procMovingObject = false;
                    }
                    // Перемещение нового объекта
                    if (newObject.Count > 0 && 
                        procMovingNewObject &&
                        currentMouseState.X > 0 &&
                        currentMouseState.Y > 0 &&
                        currentMouseState.X < WindowWidth &&
                        currentMouseState.Y < WindowHeight &&
                        !mouseLBState &&
                        Stopwatch.GetTimestamp() - timer > 20000) 
                    {
                        for(int i = 0; i < newObject.Count; i++) 
                        {
                            newObject[i][3] = currentMouseState.X + newObject[i][6];
                            newObject[i][4] = currentMouseState.Y + newObject[i][7];
                            newObject[i][5] = ObjectDepth(newObject[i][4], newObject[i][3], newObject[i][0]);
                        }
                        timer = Stopwatch.GetTimestamp();
                    }
                    // Вставка нового объекта
                    if (newObject.Count > 0 &&
                        procMovingNewObject &&
                        !mouseClickHandler &&
                        currentMouseState.X > 0 &&
                        currentMouseState.Y > 0 &&
                        currentMouseState.X < WindowWidth &&
                        currentMouseState.Y < WindowHeight &&
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
                    if (Keyboard.IsKeyDown(System.Windows.Forms.Keys.Delete) &&
                        selectedObjID >= 0) // Обработка клавиши DEL
                    {
                        int X = GameData.objects[selectedObjID].TilePosition.X;
                        int Y = GameData.objects[selectedObjID].TilePosition.Y;
                        GameData.metaTileArray[Y, X].DelObject(selectedObjID);
                        selectedObjID = -1;
                    }
                    if (Keyboard.IsKeyDown(System.Windows.Forms.Keys.Control) && 
                        Keyboard.IsKeyDown(System.Windows.Forms.Keys.C) &&
                        selectedObjID >= 0 &&
                        !procMovingNewObject &&
                        Stopwatch.GetTimestamp() - timer > 2000000) // Обработка клавиши Ctrl + C
                    {
                        newObject.Add(new int[8] { Objects.getObjectsCount(), GameData.objects[selectedObjID].SpriteID, 1, 0, 0, 0, 0, 0 });
                        //Objects objNew = new(0, 0, 0, 0, 0, 0, 0, 0, 0, 0, new System.Drawing.Point(0,0), 0, GameData.objects[selectedObjID].SpriteID)
                        //{
                        //    Height = 0
                        //};
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
                        procMovingNewObject = true;
                        selectedObjID = -1;
                        Cursor.Hide();
                        timer = Stopwatch.GetTimestamp();
                    }
                    if (Keyboard.IsKeyDown(System.Windows.Forms.Keys.Space) &&
                        selectedObjID >= 0 &&
                        !procMovingNewObject &&
                        Stopwatch.GetTimestamp() - timer > 2000000) // Обработка клавиши Space
                    {
                        ObjectProperties ObjPropForm = new ObjectProperties(new int[] { GameData.objects[selectedObjID].Var_0, 
                            GameData.objects[selectedObjID].Var_1, 
                            GameData.objects[selectedObjID].Var_2, 
                            GameData.objects[selectedObjID].Var_3, 
                            GameData.objects[selectedObjID].Var_4, 
                            GameData.objects[selectedObjID].Var_5, 
                            GameData.objects[selectedObjID].Var_6, 
                            GameData.objects[selectedObjID].Var_7, 
                            GameData.objects[selectedObjID].Var_8, 
                            GameData.objects[selectedObjID].Var_9, 
                            GameData.objects[selectedObjID].Var_10}, selectedObjID);
                        ObjPropForm.Show();
                        timer = Stopwatch.GetTimestamp();
                    }
                }
                if (EditForm.selectTollBarPage == 2) // Обработка мыши для вкладки болванчиков
                {

                }
                if (EditForm.selectTollBarPage == 3) // Обработка мыши для вкладки эффектов
                {
                    // Наложение эффектов
                    if (mouseLBState &&
                    !mouseClickHandler &&
                    currentMouseState.X > 0 &&
                    currentMouseState.Y > 0 &&
                    currentMouseState.X < WindowWidth &&
                    currentMouseState.Y < WindowHeight &&
                    Stopwatch.GetTimestamp() - timer > 200000)
                    {
                        GameData.EffectsMapping(currentMouseState.X, currentMouseState.Y, tileBiasX, tileBiasY, EditForm.effects);
                        mouseClickHandler = true;
                        timer = Stopwatch.GetTimestamp();
                    }
                    // Удаление эффектов ПКМ
                    if (mouseRBState &&
                    !mouseClickHandler &&
                    currentMouseState.X > 0 &&
                    currentMouseState.Y > 0 &&
                    currentMouseState.X < WindowWidth &&
                    currentMouseState.Y < WindowHeight &&
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

                for (int y = 0; y < WindowHeight / Vars.tileSize + 1; y++)
                { // Отображаем текстуры
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
                if (EditForm.selectTollBarPage == 3)
                { // Отображаем эффекты плитки
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
                for (int i = 0; i < ObjectShow.Count; i++)
                { // Отображаем объекты
                    if (ObjectShow[i][2] > 0)
                    { // Если отбъект должен быть отображен
                        int Xcor = ObjectShow[i][3];
                        int Ycor = ObjectShow[i][4];
                        int heigth = GameData.objects[ObjectShow[i][0]].Height;
                        Vector2 objPosition = new(Xcor, Ycor - heigth);
                        if (objectTextures is not null)
                        {
                            if (ObjectShow[i][0] == selectedObjID)
                            { // Если объект выделен
                                Editor.spriteBatch.Draw(objectTextures[ObjectShow[i][1]], objPosition, Color.Yellow);
                            }
                            else
                            {
                                Editor.spriteBatch.Draw(objectTextures[ObjectShow[i][1]], objPosition, Color.White);
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
            ObjectShow.Clear();
            for (int y = -5; y < WindowHeight / Vars.tileSize + 5; y++)
            {
                for (int x = -5; x < WindowWidth / Vars.tileSize + 5; x++)
                {
                    if (y + tileBiasY < Vars.maxVerticalTails && y + tileBiasY >= 0 && x + tileBiasX < Vars.maxHorizontalTails && x + tileBiasX >= 0)
                    {
                        for (int i = 0; i < GameData.metaTileArray[y + tileBiasY, x + tileBiasX].GetObjectsCount(); i++)
                        {
                            int worldNum = GameData.metaTileArray[y + tileBiasY, x + tileBiasX].GetObject(i);
                            int spriteID = GameData.objects[worldNum].SpriteID;
                            int posX = (GameData.objects[worldNum].TilePosition.X - tileBiasX) * Vars.tileSize + GameData.objects[worldNum].PixelPositionInTile.X;
                            int posY = (GameData.objects[worldNum].TilePosition.Y - tileBiasY) * Vars.tileSize + GameData.objects[worldNum].PixelPositionInTile.Y;
                            int sort = (GameData.objects[worldNum].AbsolutePixelPosition.Y + GameData.objectDesc[GameData.objects[worldNum].SpriteID].TouchPoint.Y) * Vars.maxHorizontalTails * Vars.tileSize + GameData.objects[worldNum].AbsolutePixelPosition.X + GameData.objectDesc[GameData.objects[worldNum].SpriteID].TouchPoint.X;
                            int actions = 1;
                            if (ObjectBuffer.ID == worldNum)
                            {
                                actions = 2;
                            }
                            if (procMovingObject && ObjectBuffer.ID == worldNum)
                            {
                                actions = 0; //Объект невидим
                            }
                            ObjectShow.Add(new int[] { worldNum, spriteID, actions, posX, posY, sort });
                        }
                    }
                }
            }
            if (procMovingObject && ObjectBuffer.ID != -1)
            {
                ObjectShow.Add(new int[] { ObjectBuffer.ID , ObjectBuffer.SpriteID, ObjectBuffer.State, ObjectBuffer.Xpos, ObjectBuffer.Ypos, ObjectBuffer.Sort});
            }
            if (procMovingNewObject)
            {
                for( int i = 0; i < newObject.Count; i++)
                {
                    ObjectShow.Add(newObject[i]);
                }
            }
            ObjectShow.Sort((x, y) => x[5].CompareTo(y[5]));
            if (EditForm.ShowSort)
            {
                bool sorted = false;
                int[] temp;
                int b = 0;
                while (sorted != true)
                {
                    b++;
                    sorted = true;
                    for (int a = 0; a < ObjectShow.Count - 1; a++)
                    {
                        int objA = a;
                        int objB = a + 1;
                        int AXcor = (GameData.objects[ObjectShow[objA][0]].TilePosition.X - tileBiasX) * Vars.tileSize + GameData.objects[ObjectShow[objA][0]].PixelPositionInTile.X;
                        int AYcor = (GameData.objects[ObjectShow[objA][0]].TilePosition.Y - tileBiasY) * Vars.tileSize + GameData.objects[ObjectShow[objA][0]].PixelPositionInTile.Y;
                        int BXcor = (GameData.objects[ObjectShow[objB][0]].TilePosition.X - tileBiasX) * Vars.tileSize + GameData.objects[ObjectShow[objB][0]].PixelPositionInTile.X;
                        int BYcor = (GameData.objects[ObjectShow[objB][0]].TilePosition.Y - tileBiasY) * Vars.tileSize + GameData.objects[ObjectShow[objB][0]].PixelPositionInTile.Y;

                        int AXmin = AXcor;
                        int AXmax = AXcor + GameData.objectDesc[GameData.objects[ObjectShow[objA][0]].SpriteID].Width;
                        int AYmin = AYcor;
                        int AYmax = AYcor + GameData.objectDesc[GameData.objects[ObjectShow[objA][0]].SpriteID].Height;
                        int BXmin = BXcor;
                        int BXmax = BXcor + GameData.objectDesc[GameData.objects[ObjectShow[objB][0]].SpriteID].Width;
                        int BYmin = BYcor;
                        int BYmax = BYcor + GameData.objectDesc[GameData.objects[ObjectShow[objB][0]].SpriteID].Height;

                        if (!((BXmin > AXmax) || (BXmax < AXmin) || (BYmin > AYmax) || (BYmax < AYmin))) //Если спрайты пересекаоться
                        {
                            int ASXmin = AXcor + GameData.objectDesc[GameData.objects[ObjectShow[objA][0]].SpriteID].Sp1.X + AYcor + GameData.objectDesc[GameData.objects[ObjectShow[objA][0]].SpriteID].Sp1.Y;
                            int ASXmax = AXcor + GameData.objectDesc[GameData.objects[ObjectShow[objA][0]].SpriteID].Sp3.X + AYcor + GameData.objectDesc[GameData.objects[ObjectShow[objA][0]].SpriteID].Sp3.Y;
                            int ASYmin = AYcor + GameData.objectDesc[GameData.objects[ObjectShow[objA][0]].SpriteID].Sp1.Y;
                            int ASYmax = AYcor + GameData.objectDesc[GameData.objects[ObjectShow[objA][0]].SpriteID].Sp3.Y;
                            int BSXmin = BXcor + GameData.objectDesc[GameData.objects[ObjectShow[objB][0]].SpriteID].Sp1.X + BYcor + GameData.objectDesc[GameData.objects[ObjectShow[objB][0]].SpriteID].Sp1.Y;
                            int BSXmax = BXcor + GameData.objectDesc[GameData.objects[ObjectShow[objB][0]].SpriteID].Sp3.X + BYcor + GameData.objectDesc[GameData.objects[ObjectShow[objB][0]].SpriteID].Sp3.Y;
                            int BSYmin = BYcor + GameData.objectDesc[GameData.objects[ObjectShow[objB][0]].SpriteID].Sp1.Y;
                            int BSYmax = BYcor + GameData.objectDesc[GameData.objects[ObjectShow[objB][0]].SpriteID].Sp3.Y;

                            if (!(BSYmax > ASYmin && BSXmin > ASXmin)) //Если А ниже В
                            {
                                temp = ObjectShow[a];
                                ObjectShow[a] = ObjectShow[a + 1];
                                ObjectShow[a + 1] = temp;
                                sorted = false;
                            }
                        }
                    }
                    if (b > 50) sorted = true;
                }
            }
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
        public static bool CheckCursorInSprite(int obj, int biasX, int biasY, int cursorPosX, int cursorPosY) // Проверка на попадание курсора в спрайт объекта
        {
            int objWidth = GameData.objectDesc[GameData.objects[obj].SpriteID].Width;
            int objHeight = GameData.objectDesc[GameData.objects[obj].SpriteID].Height;
            int objPosX = (GameData.objects[obj].TilePosition.X - biasX) * Vars.tileSize + GameData.objects[obj].PixelPositionInTile.X;
            int objPosY = (GameData.objects[obj].TilePosition.Y - biasY) * Vars.tileSize + GameData.objects[obj].PixelPositionInTile.Y - GameData.objects[obj].Height;
            if (cursorPosX > objPosX &&
                cursorPosX < objPosX + objWidth &&
                cursorPosY > objPosY &&
                cursorPosY < objPosY + objHeight)
            {
                Color[,] colors2D = GetColorArray(obj);
                if (colors2D[cursorPosX - objPosX, cursorPosY - objPosY].A != 0)
                {
                    return true;
                }
                else return false;
            }
            else return false;
        }
        //------------------------------------------------------------------------------------------------------------------------
        public static Color[,] GetColorArray(int obj)
        {
            Texture2D SelectObj;
            int objWidth = GameData.objectDesc[GameData.objects[obj].SpriteID].Width;
            int objHeight = GameData.objectDesc[GameData.objects[obj].SpriteID].Height;
            SelectObj = objectTextures[GameData.objects[obj].SpriteID];
            Color[] ColorArray = new Color[objWidth * objHeight];
            SelectObj.GetData(ColorArray);
            Color[,] colors2D = new Color[objWidth, objHeight];
            for (int r = 0; r < objWidth; r++)
            {
                for (int t = 0; t < objHeight; t++)
                {
                    colors2D[r, t] = ColorArray[r + t * objWidth];
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
        private static int ObjectDepth(int Ypos, int Xpos, int ID) // Число, характеризующее парядок вывода объекта на экран
        {
            //ObjectBuffer.Sort = (ObjectBuffer.Ypos + GameData.objectDesc[GameData.objects[ObjectBuffer.ID].SpriteID].TouchPoint.Y + tileBiasY * Vars.tileSize) * Vars.maxHorizontalTails * Vars.tileSize + ObjectBuffer.Xpos + tileBiasX * Vars.tileSize;
            return (Ypos + GameData.objectDesc[GameData.objects[ID].SpriteID].TouchPoint.Y + tileBiasY * Vars.tileSize) * Vars.maxHorizontalTails * Vars.tileSize + Xpos + tileBiasX * Vars.tileSize;
        }
    }
    public static class ObjectBuffer
    {
        public static int ID, Xpos, Ypos, SpriteID, Sort, State, Xbias, Ybias, ParentTileObjPosX, ParentTileObjPosY;
        public static void Clear()
        {
            ID = -1;
            Xpos = -1;
            Ypos = -1;
            SpriteID = -1;
            Sort = -1;
            State = -1;
            Xbias = -1;
            Ybias = -1;
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