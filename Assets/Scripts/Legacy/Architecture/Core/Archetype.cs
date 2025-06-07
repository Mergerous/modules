using System;

namespace Modules.Architecture.Systems
{
    public class Archetype<TItem>
    {
        public readonly TItem item1;
        
        public Archetype(TItem item1)
        {
            this.item1 = item1;
        }

        public void Deconstruct(out TItem item1)
        {
            item1 = this.item1;
        }

        public static implicit operator TItem(Archetype<TItem> d) => d.item1;
    }
    
    
    public class Archetype<TItem1, TItem2> : Archetype<TItem1>
    {
        public readonly TItem2 item2;
        
        public Archetype(TItem1 item1, TItem2 item2) : base(item1)
        {
            this.item2 = item2;
        }
        
        public void Deconstruct(out TItem1 item1, out TItem2 item2)
        {
            Deconstruct(out item1);
            item2 = this.item2;
        }
    }
    
    
    public class Archetype<TItem1, TItem2, TItem3> : Archetype<TItem1, TItem2>
    {
        public readonly TItem3 item3;
        
        public Archetype(TItem1 item1, TItem2 item2, TItem3 item3) : base(item1, item2)
        {
            this.item3 = item3;
        }
        
        public void Deconstruct(out TItem1 item1, out TItem2 item2, out TItem3 item3)
        {
            Deconstruct(out item1, out item2);
            item3 = this.item3;
        }
    }
    
    public class Archetype<TItem1, TItem2, TItem3, TItem4> : Archetype<TItem1, TItem2, TItem3>
    {
        public readonly TItem4 item4;

        public Archetype(TItem1 item1, TItem2 item2, TItem3 item3, TItem4 item4) : base(item1, item2, item3)
        {
            this.item4 = item4;
        }
        
        public void Deconstruct(out TItem1 item1, out TItem2 item2, out TItem3 item3, out TItem4 item4)
        {
            Deconstruct(out item1, out item2, out item3);
            item4 = this.item4;
        }
    }
    
    public class Archetype<TItem1, TItem2, TItem3, TItem4, TItem5> : Archetype<TItem1, TItem2, TItem3, TItem4>
    {
        public readonly TItem5 item5;

        public Archetype(TItem1 item1, TItem2 item2, TItem3 item3, TItem4 item4, TItem5 item5) : base(item1, item2, item3, item4)
        {
            this.item5 = item5;
        }
        
        public void Deconstruct(out TItem1 item1, out TItem2 item2, out TItem3 item3, out TItem4 item4, out TItem5 item5)
        {
            Deconstruct(out item1, out item2, out item3, out item4);
            item5 = this.item5;
        }
    }
}