using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DivEditor
{
    internal class Metaobjects
    {
        /*
           startdef metaobject WALL small_wall_i_05
           group stone_wall
           walltype normal outside
           object 1087 0 0 0
           enddef metaobject

           placement on 96
           object 811 front 0 0 on 0
           object 7119 back 2 2 on 0
           object 322 right X center 85 on 0
           
        */
        public String metaobject = "";
        public String group = "";
        public String location = "";
        public String type = "";
        public String walltype = "";
        public int placement;
        public int[] size = new int[2];
        //public List<int>[] Object = new List<int>[4];
        public List<int[]> Object = new List<int[]>();
        static int count = 0;
        public Metaobjects()
        {
            count++;
        }
        public void setMet(String met)
        {
            this.metaobject = met;
        }
        public void setGroup(String grp)
        {
            this.group = grp;
        }
        public void setLocation(String loc)
        {
            this.location = loc;
        }
        public void setPlacement(int plac)
        {
            this.placement = plac;
        }
        public void setType(String type)
        {
            this.type = type;
        }
        public void setWalltype(String wtype)
        {
            this.walltype = wtype;
        }
        public void setSize(int x, int y)
        {
            this.size[0] = x;
            this.size[1] = y;
        }
        public void addObject(int[] obj)
        {
            this.Object.Add(obj);
        }
        public static int TotalCount()
        {
            return count;
        }
    }
}
