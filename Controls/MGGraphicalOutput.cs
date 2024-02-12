using DivEditor.Controls;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using MonoGame.Forms.NET.Controls;
using System.Diagnostics;
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
        private Texture2D? vScroll, hScroll, point;
        private static Texture2D[,] TilesUpTextures = new Texture2D[Vars.maxVerticalTails, Vars.maxHorizontalTails];
        private static Texture2D[,] TilesDownTextures = new Texture2D[Vars.maxVerticalTails, Vars.maxHorizontalTails];
        private static Texture2D[] ObjectTextures = new Texture2D[Vars.maxObjectsCount];
        private SpriteFont DrawFont;
        static bool mouseLBState;
        static bool mouseRBState;
        static bool mouseLBOldState;
        static bool mouseRBOldState;
        static Point mouseOldPosition;
        static bool vScrollChange;
        static bool hScrollChange;
        static bool mouseClickHandler = false;

        static int[] selectedObject = new int[6];       // 0 - мировой номер, 1 - SpriteID, 2 - поведение, 3 - координата Х, 4 - координата Y, 5 - сортировка
        public static List<int[]> newObject = new();    // 0 - мировой номер, 1 - SpriteID, 2 - поведение, 3 - координата Х, 4 - координата Y, 5 - сортировка, 6 - смещение по Х, 7 - смещение по Y
        public static bool procMovingObject = false;
        public static bool procMovingNewObject = false;
        int procMovObjCursOffsetX = 0;
        int procMovObjCursOffsetY = 0;
        int procMovObjCorInTileX;
        int procMovObjCorInTileY;

        


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
            for (int i = 0; i < Vars.maxObjectsCount; i++)
            {
                ObjectTextures[i] = Editor.Content.Load<Texture2D>("objects/" + i.ToString().PadLeft(6, '0'));
            }
            for (int y = 0; y < Vars.maxVerticalTails; y++)
            {
                for (int x = 0; x < Vars.maxHorizontalTails; x++)
                {
                    TilesDownTextures[y, x] = Editor.Content.Load<Texture2D>("floor/000000");
                }
            }
            System.Diagnostics.Debug.WriteLine("Graphic Profile: " + Editor.GraphicsDevice.GraphicsProfile);

        }
        //------------------------------------------------------------------------------------------------------------------------
        protected override void Update(GameTime gameTime)
        {
            if (GameData.READY)
            {
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
                if (Keyboard.IsKeyDown(System.Windows.Forms.Keys.D)) // Обработка клавиши D
                {
                    tileBiasX++;
                    if (shift) tileBiasX += 5;
                    if (tileBiasX > 511) tileBiasX = 511;
                    updateScrollPosition();
                }
                if (Keyboard.IsKeyDown(System.Windows.Forms.Keys.A)) // Обработка клавиши A
                {
                    tileBiasX--;
                    if (shift) tileBiasX -= 5;
                    if (tileBiasX < 0) tileBiasX = 0;
                    updateScrollPosition();
                }
                if (Keyboard.IsKeyDown(System.Windows.Forms.Keys.S)) // Обработка клавиши S
                {
                    tileBiasY++;
                    if (shift) tileBiasY += 5;
                    if (tileBiasY > 1023) tileBiasY = 1023;
                    updateScrollPosition();
                }
                if (Keyboard.IsKeyDown(System.Windows.Forms.Keys.W)) // Обработка клавиши W
                {
                    tileBiasY--;
                    if (shift) tileBiasY -= 5;
                    if (tileBiasY < 0) tileBiasY = 0;
                    updateScrollPosition();
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
                            if (checkCursorInSprite(ObjectShow[i][0], tileBiasX, tileBiasY, currentMouseState.X, currentMouseState.Y))
                            {
                                selectedObject[0] = ObjectShow[i][0];
                                selectedObject[1] = GameData.objects[selectedObject[0]].SpriteID;
                                selectedObject[2] = 3;
                                selectedObject[5] = 0;
                                break;
                            }
                            else
                            {
                                selectedObject[0] = -1;
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
                            if (checkCursorInSprite(ObjectShow[i][0], tileBiasX, tileBiasY, currentMouseState.X, currentMouseState.Y))
                            {
                                if (selectedObject[0] == ObjectShow[i][0])
                                {
                                    procMovObjCursOffsetX = currentMouseState.X - ObjectShow[i][3];
                                    procMovObjCursOffsetY = currentMouseState.Y - ObjectShow[i][4];
                                    procMovObjCorInTileX = GameData.objects[ObjectShow[i][0]].AbsolutePixelPosition.X % 64;
                                    procMovObjCorInTileY = GameData.objects[ObjectShow[i][0]].AbsolutePixelPosition.Y % 64;
                                    procMovingObject = true;
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
                            selectedObject[3] = (currentMouseState.X - procMovObjCursOffsetX) / 64 * 64 + procMovObjCorInTileX;
                            selectedObject[4] = (currentMouseState.Y - procMovObjCursOffsetY) / 64 * 64 + procMovObjCorInTileY;
                        }
                        else
                        {
                            selectedObject[3] = currentMouseState.X - procMovObjCursOffsetX;
                            selectedObject[4] = currentMouseState.Y - procMovObjCursOffsetY;
                        }
                        selectedObject[5] = (selectedObject[4] + GameData.objectDesc[GameData.objects[selectedObject[0]].SpriteID].TouchPoint.Y + tileBiasY * Vars.tileSize) * Vars.maxHorizontalTails * Vars.tileSize + selectedObject[3] + tileBiasX * Vars.tileSize;
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
                        for (int i = 0; i < ObjectShow.Count; i++)
                        {
                            if (ObjectShow[i][2] == 0)
                            {
                                int X = tileBiasX + (ObjectShow[i][3] / Vars.tileSize);
                                int Y = tileBiasY + (ObjectShow[i][4] / Vars.tileSize);
                                GameData.metaTileArray[Y, X].DelObject(ObjectShow[i][0]);
                            }
                        }
                        if (selectedObject[3] < 0) selectedObject[3] = 0;
                        if (selectedObject[4] < 0) selectedObject[4] = 0;
                        if (selectedObject[3] > (Vars.maxHorizontalTails - 1) * Vars.tileSize) selectedObject[3] = (Vars.maxHorizontalTails - 1) * Vars.tileSize;
                        if (selectedObject[4] > (Vars.maxVerticalTails - 1) * Vars.tileSize) selectedObject[4] = (Vars.maxVerticalTails - 1) * Vars.tileSize;
                        int tileX = tileBiasX + (selectedObject[3] / Vars.tileSize);
                        int tileY = tileBiasY + (selectedObject[4] / Vars.tileSize);
                        if (GameData.metaTileArray[tileY, tileX].GetObjectsCount() < Vars.maxObjectsCount)
                        {
                            GameData.metaTileArray[tileY, tileX].AddObject(selectedObject[0]);
                        }
                        GameData.objects[selectedObject[0]].AbsolutePixelPosition = new System.Drawing.Point(selectedObject[3] + tileBiasX * Vars.tileSize, selectedObject[4] + tileBiasY * Vars.tileSize);
                        GameData.objects[selectedObject[0]].TilePosition = new System.Drawing.Point(tileX, tileY);
                        selectedObject[0] = -1;
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
                            newObject[i][5] = (newObject[i][4] + GameData.objectDesc[GameData.objects[newObject[i][0]].SpriteID].TouchPoint.Y + tileBiasY * Vars.tileSize) * Vars.maxHorizontalTails * Vars.tileSize + newObject[i][3] + tileBiasX * Vars.tileSize;
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
                        procMovingNewObject = false;
                        for (int i = 0; i < newObject.Count; i++)
                        {
                            
                            int tileX = tileBiasX + (newObject[i][3] / Vars.tileSize);
                            int tileY = tileBiasY + (newObject[i][4] / Vars.tileSize);
                            if (GameData.metaTileArray[tileY, tileX].GetObjectsCount() < Vars.maxObjectsCountInTile)
                            {
                                GameData.metaTileArray[tileY, tileX].AddObject(newObject[i][0]);
                                GameData.objects[newObject[i][0]].AbsolutePixelPosition = new System.Drawing.Point(newObject[i][3] + tileBiasX * Vars.tileSize, newObject[i][4] + tileBiasY * Vars.tileSize);
                                GameData.objects[newObject[i][0]].TilePosition = new System.Drawing.Point(tileX, tileY);
                            }
                        }
                        UpdateShowObjects();
                        newObject.Clear();
                        mouseClickHandler = false;
                        Cursor.Show();
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

                for (int y = 0; y < WindowHeight / Vars.tileSize + 1; y++) // Отображаем текстуры
                {
                    for (int x = 0; x < WindowWidth / Vars.tileSize + 1; x++)
                    {
                        if (y + tileBiasY < Vars.maxVerticalTails && x + tileBiasX < Vars.maxHorizontalTails)
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
                                if (GameData.metaTileArray[y + tileBiasY, x + tileBiasX].TileEffects != 0) // Если есть эффект
                                {
                                    if (((GameData.metaTileArray[y + tileBiasY, x + tileBiasX].TileEffects & (1 << Vars.tileEffectByteNumber_UpDiagonal)) == 0) &&
                                    ((GameData.metaTileArray[y + tileBiasY, x + tileBiasX].TileEffects & (1 << Vars.tileEffectByteNumber_DownDiagonal)) == 0)) // Если без диагонали
                                    {
                                        for (int a = 0; a < 64; a++)
                                        {
                                            Editor.spriteBatch.Draw(point, new Rectangle(x * Vars.tileSize, y * Vars.tileSize + a, 1, 1), Color.Gray);
                                            Editor.spriteBatch.Draw(point, new Rectangle(x * Vars.tileSize + a, y * Vars.tileSize, 1, 1), Color.Gray);
                                            Editor.spriteBatch.Draw(point, new Rectangle(x * Vars.tileSize + 64, y * Vars.tileSize + a, 1, 1), Color.Gray);
                                            Editor.spriteBatch.Draw(point, new Rectangle(x * Vars.tileSize + a, y * Vars.tileSize + 64, 1, 1), Color.Gray);
                                        }
                                    }
                                    else if ((GameData.metaTileArray[y + tileBiasY, x + tileBiasX].TileEffects & (1 << Vars.tileEffectByteNumber_UpDiagonal)) != 0) // Если верхняя диагонать
                                    {
                                        for (int a = 0; a < 64; a++)
                                        {
                                            Editor.spriteBatch.Draw(point, new Rectangle(x * Vars.tileSize, y * Vars.tileSize + a, 1, 1), Color.Gray);
                                            Editor.spriteBatch.Draw(point, new Rectangle(x * Vars.tileSize + a, y * Vars.tileSize, 1, 1), Color.Gray);
                                            Editor.spriteBatch.Draw(point, new Rectangle(x * Vars.tileSize + a, y * Vars.tileSize + 64 - a, 1, 1), Color.Gray);
                                        }
                                    }
                                    else // Если нижняя диагонать
                                    {
                                        for (int a = 0; a < 64; a++)
                                        {
                                            Editor.spriteBatch.Draw(point, new Rectangle(x * Vars.tileSize + 64, y * Vars.tileSize + a, 1, 1), Color.Gray);
                                            Editor.spriteBatch.Draw(point, new Rectangle(x * Vars.tileSize + a, y * Vars.tileSize + 64, 1, 1), Color.Gray);
                                            Editor.spriteBatch.Draw(point, new Rectangle(x * Vars.tileSize + a, y * Vars.tileSize + 64 - a, 1, 1), Color.Gray);
                                        }
                                    }
                                }
                                if ((GameData.metaTileArray[y + tileBiasY, x + tileBiasX].TileEffects & (1 << Vars.tileEffectByteNumber_Water)) != 0) // Вода
                                {
                                    Editor.spriteBatch.DrawString(DrawFont, "water", new Vector2(x * Vars.tileSize + 3, y * Vars.tileSize - 3), Color.Aqua);
                                }
                                if ((GameData.metaTileArray[y + tileBiasY, x + tileBiasX].TileEffects & (1 << Vars.tileEffectByteNumber_Indoors)) != 0) // Помещение
                                {
                                    Editor.spriteBatch.DrawString(DrawFont, "indoor", new Vector2(x * Vars.tileSize + 3, y * Vars.tileSize + 12), Color.Coral);
                                }
                                if ((GameData.metaTileArray[y + tileBiasY, x + tileBiasX].TileEffects & (1 << Vars.tileEffectByteNumber_Fog)) != 0) // Туман
                                {
                                    Editor.spriteBatch.DrawString(DrawFont, "fog", new Vector2(x * Vars.tileSize + 3, y * Vars.tileSize + 27), Color.LightSteelBlue);
                                }
                                if ((GameData.metaTileArray[y + tileBiasY, x + tileBiasX].TileEffects & (1 << Vars.tileEffectByteNumber_Object)) != 0) // Квест
                                {
                                    Editor.spriteBatch.DrawString(DrawFont, "object", new Vector2(x * Vars.tileSize + 3, y * Vars.tileSize + 42), Color.LightSteelBlue);
                                }
                            }
                        }
                    }
                }
                for (int i = 0; i < ObjectShow.Count; i++) // Отображаем объекты
                {
                    if (ObjectShow[i][2] > 0) // Если отбъект должен быть отображен
                    {
                        if (ObjectShow[i][0] == selectedObject[0]) // Если объект выделен
                        {
                            Vector2 objPosition = new(ObjectShow[i][3], ObjectShow[i][4] - GameData.objects[ObjectShow[i][0]].Height);
                            Editor.spriteBatch.Draw(ObjectTextures[ObjectShow[i][1]], objPosition, Color.Yellow);
                        }
                        else
                        {
                            Vector2 objPosition = new(ObjectShow[i][3], ObjectShow[i][4] - GameData.objects[ObjectShow[i][0]].Height);
                            Editor.spriteBatch.Draw(ObjectTextures[ObjectShow[i][1]], objPosition, Color.White);
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
        private void UpdateFullTileTexture()
        {
            for (int y = 0; y < Vars.maxVerticalTails; y++)
            {
                for (int x = 0; x < Vars.maxHorizontalTails; x++)
                {
                    TilesDownTextures[y, x] = Editor.Content.Load<Texture2D>("floor/" + GameData.metaTileArray[y, x].DownTileTexture.ToString().PadLeft(6, '0'));
                    if (GameData.metaTileArray[y, x].UpTileTexture < 65535)
                    {
                        TilesUpTextures[y, x] = Editor.Content.Load<Texture2D>("floor/" + GameData.metaTileArray[y, x].UpTileTexture.ToString().PadLeft(6, '0'));
                    }
                }
            }
        }
        //------------------------------------------------------------------------------------------------------------------------
        private void UpdateTileTexture()
        {
            for (int y = tileBiasY; y < tileBiasY + WindowHeight / Vars.tileSize + 5; y++)
            {
                for (int x = tileBiasX; x < tileBiasX + WindowWidth / Vars.tileSize + 5; x++)
                {
                    if (y < Vars.maxVerticalTails && x < Vars.maxHorizontalTails)
                    {
                        TilesDownTextures[y, x] = Editor.Content.Load<Texture2D>("floor/" + GameData.metaTileArray[y, x].DownTileTexture.ToString().PadLeft(6, '0'));
                        if (GameData.metaTileArray[y, x].UpTileTexture < 65535)
                        {
                            TilesUpTextures[y, x] = Editor.Content.Load<Texture2D>("floor/" + GameData.metaTileArray[y, x].UpTileTexture.ToString().PadLeft(6, '0'));
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
        private void UpdateShowObjects()
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
                            if (selectedObject[0] == worldNum)
                            {
                                actions = 2;
                            }
                            if (procMovingObject && selectedObject[0] == worldNum)
                            {
                                actions = 0;
                            }
                            ObjectShow.Add(new int[] { worldNum, spriteID, actions, posX, posY, sort });
                        }
                    }
                }
            }
            if (procMovingObject && selectedObject[0] != -1)
            {
                ObjectShow.Add(selectedObject);
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
        public bool checkCursorInSprite(int obj, int biasX, int biasY, int cursorPosX, int cursorPosY) // Проверка на попадание курсора в спрайт объекта
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
                Color[,] colors2D = getColorArray(obj);
                if (colors2D[cursorPosX - objPosX, cursorPosY - objPosY].A != 0)
                {
                    return true;
                }
                else return false;
            }
            else return false;
        }
        //------------------------------------------------------------------------------------------------------------------------
        public Color[,] getColorArray(int obj)
        {
            Texture2D SelectObj;
            int objWidth = GameData.objectDesc[GameData.objects[obj].SpriteID].Width;
            int objHeight = GameData.objectDesc[GameData.objects[obj].SpriteID].Height;
            int objName = GameData.objects[obj].SpriteID;
            SelectObj = Editor.Content.Load<Texture2D>("objects/" + objName.ToString().PadLeft(6, '0'));
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
        public Texture2D getTexture2D(string path)
        {
            Texture2D t;
            t = Editor.Content.Load<Texture2D>(path);
            return t;
        }
        //------------------------------------------------------------------------------------------------------------------------
        public static void setCursor(int X, int Y)
        {
            tileBiasY = (Vars.maxVerticalTails - 1 - WindowHeight / Vars.tileSize) * vScrollPos / (WindowHeight - 53); // 53 - высота иконки прокрутки
            tileBiasX = (Vars.maxHorizontalTails - 1 - WindowWidth / Vars.tileSize) * hScrollPos / (WindowWidth - 53);
            tileBiasX = X;
            tileBiasY = Y;
            updateScrollPosition();
        }
        public static Point getCursor()
        {
            return new Point(tileBiasX, tileBiasY);
        }
        private static void updateScrollPosition()
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









    }
}
//System.Diagnostics.Debug.WriteLine("Graphic Profile: " + Editor.GraphicsDevice.GraphicsProfile);