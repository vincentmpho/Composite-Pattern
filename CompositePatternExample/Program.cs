using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompositePatternExample
{
    class Program
    {
        static void Main(string[] args)
        {
            Section First = new Section("August 2017 ONT4202 Paper");
            First.Add(new Section("Section A"));
            First.Add(new Section("Section B"));

            First[0].Add(new Question("Question A-1", 10));
            First[0].Add(new Question("Question A-2", 5));
            First[1].Add(new Question("Question B-1", 7));
            First[1].Add(new Question("Question B-2", 19));

            First.Display();

            Console.WriteLine("Paper total:  <{0}>", First.CalculateTotal());

            Console.ReadLine();
        }
    }

    public abstract class QPComponent : IEnumerable<QPComponent>//Component
    {
        public abstract int CalculateTotal();

        public abstract void Display();

        public virtual void Add(QPComponent menuComponent) { }

        public virtual void Remove(QPComponent menuComponent) { }

        public abstract IEnumerator<QPComponent> GetEnumerator();        

        IEnumerator IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }

        public virtual string Name { get; set; }

        public virtual int Total { get; set; }

        public virtual QPComponent this [int Index]
        {
            get
            {
                throw new NotImplementedException();
            }

            set
            {
                throw new NotImplementedException();
            }
        }
    }


    public class Section : QPComponent//Composite
    {
        private List<QPComponent> _children = new List<QPComponent>();

        public Section(string Name)
        {
            this.Name = Name;
            this.Total = 0;
        }

        public override void Add(QPComponent child)
        {
            _children.Add(child);
        }

        public override void Remove(QPComponent child)
        {
            _children.Remove(child);
        }

        public override QPComponent this[int Index]
        {
            get
            {
                return _children[Index];
            }

            set
            {
                _children[Index] = value;
            }
        }

        public override void Display()
        {
            Console.WriteLine("{0} [{1}]", Name, CalculateTotal());

            IEnumerator<QPComponent> Target = this.GetEnumerator();

            while (Target.MoveNext())
            {
                Target.Current.Display();
            }
        }

        public override int CalculateTotal()
        {
            int TempTotal = 0;

            IEnumerator<QPComponent> Target = this.GetEnumerator();

            while (Target.MoveNext())
            {
                TempTotal += Target.Current.CalculateTotal();
            }

            return TempTotal;
        }

        public override IEnumerator<QPComponent> GetEnumerator()
        {
            return _children.GetEnumerator();
        }
    }


    public class Question : QPComponent//Leaf
    {
        public Question(string Name, int Total)
        {
            this.Name = Name;
            this.Total = Total;
        }
        public override void Display()
        {
            Console.WriteLine("{0} ({1})", Name, CalculateTotal());
        }

        public override int CalculateTotal()
        {
            return Total;
        }

        public override IEnumerator<QPComponent> GetEnumerator()
        {
            return null;
        }
    }
}
