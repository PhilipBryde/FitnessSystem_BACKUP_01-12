using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FitnessProgram
{
    public class Fitness
    {
        List<Member> memberList = new List<Member>(); //liste hvor de forskellige medlemmer bliver tilføjet ind i

        public Fitness()
        {

            //Opretter medlemmer med givende id, navn og køn
            memberList.Add(new Member(1, "Mathias", 'M'));
            memberList.Add(new Member(2, "Anders", 'M'));
            memberList.Add(new Member(3, "Sofie", 'F'));
            memberList.Add(new Member(4, "Caroline", 'F'));
            memberList.Add(new Member(5, "Rasmus", 'M'));
            memberList.Add(new Member(6, "Johan", 'M'));
            memberList.Add(new Member(7, "Ida", 'F'));
            memberList.Add(new Member(8, "Emma", 'F'));
            memberList.Add(new Member(9, "Victor", 'M'));
            memberList.Add(new Member(10, "Gertrud", 'F'));
            memberList.Add(new Member(11, "Richard", 'M'));
            memberList.Add(new Member(12, "Lasse", 'M'));
            memberList.Add(new Member(13, "Maya", 'F'));
            memberList.Add(new Member(14, "Victoria", 'F'));
            memberList.Add(new Member(15, "Magnus", 'M'));

        }

        public List<Member> GetAllMembers()
        {
            return memberList;
        }
    }
}
