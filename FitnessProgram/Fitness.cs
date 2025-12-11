using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using FitnessProgram;

public class Fitness
{
    private readonly List<Member> memberList = new List<Member>(); //liste hvor de forskellige medlemmer bliver tilføjet ind i

    public Fitness()
    {
        // Opretter medlemmer med givende id, navn og køn, bliver nu kun brugt til login
        memberList.Add(new Member(0, "admin", 'M', "Admin"));
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

    public List<Member> GetAllMembers() //Public metode der retunere listen af den gamle liste over medlemmer
    {
        return memberList;
    }

    // Login - Philip
    // Forsøger at logge en bruger ind.
    // Login sker via Medlemmets Fornavn (username) og ID (password).
    // Returnerer Member-objektet hvis successfuldt login, ellers returneres null.
    public Member? Authenticate(string username, string password) // Member? for at håndtere null korrekt
    {
        // Tjekker for tomme eller whitespace-input, før vi fortsætter.
        if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
        {
            return null;
        }

        // Forsøg at konvertere password (ID) til et tal. Hvis det fejler, returneres null.
        if (!int.TryParse(password, out int id))
            return null;

        // Søg efter et medlem, hvor ID matcher, og hvor Fornavnet matcher username.
        // Vi splitter navnet for at få Fornavnet [m.name.Split(' ')[0]].
        var authenticatedMember = memberList.FirstOrDefault(m =>
            m.name.Split(' ')[0].Equals(username, StringComparison.OrdinalIgnoreCase)
            && m.id == id);

        // Returnerer det fundne medlem (eller null).
        return authenticatedMember;
    }

    // REGISTER (opret nyt medlem) - Philip
    public Member Register(string name, char gender, int age)
    {
        // Generer et nyt ID. Tager det højeste eksisterende ID og lægger 1 til.
        int newId = (memberList.Count > 0) ? memberList.Max(m => m.id) + 1 : 1; 
        Member newMember = new Member(newId, name, gender); // Opretter det nye medlem
        memberList.Add(newMember); // Tilføjer den til memberList
        return newMember;
    }

    public List<string> MemberFromFile() //Public metode med liste over medlemmer -- Sidney
    {
        string filePath = @"MemberList.txt"; //Gemmer stien til textfilen
        string[] members = File.ReadAllLines(filePath); //Læser hver linje og laver den til en array af strings
        return new List<string>(members); //Retunere en ny liste når metoden bliver kaldt
    }

    public List<string> ActivityFromFile() //Public metode med en liste over aktiviteter -- Sidney
    {
        string filePath = @"ActivityList.txt"; //Gemmer stien til textfilen
        string[] activities = File.ReadAllLines(filePath); //Læser hver linje og laver den til en array af strings
        return new List<string>(activities); //Retunerer en ny liste når metoden bliver kaldt
    }

    public void SaveMemberToFile(Member member) //Metode der gemmer ny medlem i text filen, tager den nye medlem fra RegisterWindow som input -- Sidney
    {
        string filePath = @"MemberList.txt"; //Gemmer stien til textfilen
        string m = $"ID: {member.id}, Navn: {member.name}, Køn: {member.gender}"; //Opretter ny string med medlemmets infomation
        File.AppendAllText(filePath, Environment.NewLine + m); //Bliver gemt i filen

    }

    public void SaveActivityToFile(string activity) //Metode der gemmer ny aktivitet i text filen, tager string som input -- Sidney
    {
        string filePath = @"ActivityList.txt"; //Gemmer stien til textfilen
        File.AppendAllText(filePath, Environment.NewLine + activity); //Tilføjer den nye aktivitet til textfilen

    }

}