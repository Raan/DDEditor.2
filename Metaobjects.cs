using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DivEditor
{
    public class Object
    {
        public int Id;
        public int posX;
        public int posY;
        public int posZ;
        public Object(int id, int posX, int posY, int posZ)
        {
            this.Id = id;
            this.posX = posX;
            this.posY = posY;
            this.posZ = posZ;
        }
    }
    public class MetaObjects
    {
        public String[] metaGroup = new String[10];
        public String type = "";
        public String group = "";
        public String location = "";
        public String[] walltype = new String[2] { "", "" };
        public List<Object> objects = new();

        public void AddMetaGroup(int num, String grp)
        {
            metaGroup[num] = grp;
        }
        public void AddObject(Object obj)
        {
            objects.Add(obj);
        }
    }
}
