using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessProgram
{
    public class Member
    {
        public List<Member> memberList = new List<Member>(); //liste hvor de forskellige medlemmer bliver tilføjet ind i
        
        //Opretter forskellige variabler til vores medlemmer;de er alle tomme og kan tilgåes og ændres senere hen (ved brug af get; set,)
        public int id { get; set; }
        public string name { get; set; }
        public char gender { get; set; }
        //public string password;

        public Member(int id, string name, char gender) //Constructor som gør det muligt at give vores medlem's variabler nye værdier hver gang vi laver et nyt objekt med dem
        {
            this.id = id;
            this.name = name;
            this.gender = gender;
        }
    }
}
