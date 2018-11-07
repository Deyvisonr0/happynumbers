using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.VisualBasic.CompilerServices;

namespace HappyNumbers.Domain.Entities
{
    public class Number
    {
        public Number(int value, int start, int count)
        {
            Value = value;
            LuckyList = Enumerable.Range(start, count).ToList();
            Validations = new List<string>();
            Validate();
        }

        private List<int> LuckyList { get; }
        private List<string> Validations { get; }
        public int Value { get; set; }
        public bool Valid => !Validations.Any();
        public bool Happy()
        {
            if (!Valid)
                return false;

            int i = 0, actualSum = 0;
            string value = Value.ToString();

            while (i < 100 && actualSum != 1)
            {
                actualSum = 0;
                foreach (var t in value)
                {
                    var digit = int.Parse(t.ToString());
                    actualSum += digit * digit;
                    value = actualSum.ToString();
                }

                i++;
            }

            return actualSum == 1;
        }
        public bool Lucky()
        {
            if (!Valid || Value % 2 == 0)
                return false;

            int position = 0, number = 0;

            LuckyList.RemoveAll(x => x % 2 == 0);

            while (position < LuckyList.Count && number < LuckyList.Count)
            {
                number = LuckyList[position];
                position++;

                if ( number == 1 )
                    continue;

                var removeList = new List<int>();

                for (var index = number; index < LuckyList.Count; index += number)
                    removeList.Add(LuckyList[index - 1]);

                LuckyList.RemoveAll(x => removeList.Contains(x));
            }

            return LuckyList.Contains(Value);
        }
        public void Validate()
        {
            if(Value > LuckyList.Last())
                Validations.Add("Invalid Lucky List or Number");
        }
    }
}