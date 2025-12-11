using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FitnessProgram;


namespace FitnessProgram 
{
    public class Member //Sidney & Philip
    {        
        //Opretter forskellige variabler til vores medlemmer, de er alle tomme og kan tilgåes og ændres senere hen når der laves en medlem (ved brug af get; set,)
        public int id { get; set; }
        public string name { get; set; }
        public char gender { get; set; }
        public string role { get; set; } // Admin eller user - Philip 

        public Member(int id, string name, char gender, string role = "User") //Constructor som gør det muligt at give vores medlem's properties nye værdier hver gang vi opretter en ny medlem
        {
            this.id = id;
            this.name = name;
            this.gender = gender;
            this.role = role; //admin eller user - Philip
        }
    }
}
