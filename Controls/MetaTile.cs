using Point = Microsoft.Xna.Framework.Point;

namespace DivEditor.Controls
{
    public class MetaTile
    {
        private int upTileTexture;                  // Верхняя текстура плитки
        private int downTileTexture;                // Нижняя текстура плитки
        private int tileEffects = 0;                // Эффект плитки (туман, влага, помещение и т.д.)
        private int unknownVar_1;                   // Пока не ясно что это за параметр
        private int unknownVar_2;                   // Пока не ясно что это за параметр
        private List<int> objects = new(14);        // Список мировых номеров объектов на плитке (не более 14)
        public int UpTileTexture
        {
            get
            {
                return upTileTexture;
            }
            set
            {
                if (value >= 0 && value <= Vars.texturesMaximumValue)
                {
                    upTileTexture = value;
                }
                else
                {
                    System.Diagnostics.Debug.WriteLine(value + " вне допустимого диапазона upTileTexture");
                }
            }
        }
        public int DownTileTexture
        {
            get
            {
                return downTileTexture;
            }
            set
            {
                if (value >= 0 && value <= Vars.texturesMaximumValue)
                {
                    downTileTexture = value;
                }
                else
                {
                    System.Diagnostics.Debug.WriteLine(value + " вне допустимого диапазона downTileTexture");
                }
            }
        }
        public int TileEffects
        {
            get
            {
                return tileEffects;
            }
            set
            {
                //if (tileEffectsValue.Contains(value))
                if (value <= Vars.tileEffectsValueMax)
                {
                    tileEffects = value;
                }
                else
                {
                    System.Diagnostics.Debug.WriteLine(value + " вне допустимого диапазона tileEffects");
                }
            }
        }
        public int UnknownVar_1
        {
            get
            {
                return unknownVar_1;
            }
            set
            {
                unknownVar_1 = value;
            }
        }
        public int UnknownVar_2
        {
            get
            {
                return unknownVar_2;
            }
            set
            {
                unknownVar_2 = value;
            }
        }
        public MetaTile(int downT, int upT, int eff, int unknVar_1, int unknVar_2) 
        {
            if (upT >= 0 && upT <= Vars.texturesMaximumValue)
            {
                this.upTileTexture = upT;
            }
            else
            {
                System.Diagnostics.Debug.WriteLine(upT + " вне допустимого диапазона upTileTexture");
                this.upTileTexture = 0;
            }
            if (downT >= 0 && downT <= Vars.texturesMaximumValue)
            {
                this.downTileTexture = downT;
            }
            else
            {
                System.Diagnostics.Debug.WriteLine(downT + " вне допустимого диапазона downTileTexture");
                this.downTileTexture = 0;
            }
            if (eff <= Vars.tileEffectsValueMax)
            {
                this.tileEffects = eff;
            }
            else
            {
                System.Diagnostics.Debug.WriteLine(eff + " вне допустимого диапазона tileEffects");
                this.tileEffects = 0;
            }
            this.unknownVar_1 = unknVar_1;
            this.unknownVar_2 = unknVar_2;
        }
        public void AddObject (int obj)
        {
            if (this.objects.Count < 14)
            {
                this.objects.Add(obj);
            }
            else
            {
                System.Diagnostics.Debug.WriteLine("превышено максимальное количество объектов");
            }
        }
        public int GetObject(int num)
        {
            if (num >= 0 && num < this.objects.Count)
            {
                return this.objects[num];
            }
            else
            {
                System.Diagnostics.Debug.WriteLine(num + " больше числа объектов");
                return 0;
            }
        }
        public void DelObject(int objWorldNum)
        {
            for (int i = 0; i < this.objects.Count; i++)
            {
                if (this.objects[i] == objWorldNum)
                {
                    this.objects.RemoveAt(i);
                    System.Diagnostics.Debug.WriteLine("Object " + objWorldNum + " deleted");
                    break;
                }
            }
        }
        public int GetObjectsCount()
        {
            return this.objects.Count;
        }
    }
}