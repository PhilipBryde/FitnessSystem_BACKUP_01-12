using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace FitnessProgram
{
    // TIL SIDNEY OG MIG SELV(PHILIP) VI SKAL SELF MATCHE DE METODER VI KALDER I MEMBERS KLASSEN
    // OG I MaindWindow.xaml det her er bare hvordna login kan virke og se ud(måske)


    /// <summary>
    /// Login-klassen står for at verificere brugere.
    /// Brugeren logger ind med:
    ///   - Fornavn som brugernavn
    ///   - ID som password
    /// </summary>
    public class Login
    {
        // Dette felt (_members) holder en liste af alle medlemmer i systemet.
        // Underscore "_" betyder: privat internt felt

        private List<Member> _members;

        // Constructor modtager listen af medlemmer
        public Login(List<Member> members)
        {
            _members = members;
        }

        /// <summary>
        /// Nu skal vi logge en bruger ind.
        /// username = fornavn
        /// password = medlems-ID
        /// Returnerer Member-objektet hvis successfuldt login, ellers null.
        /// </summary>
        
        public Member? Authenticate(string username, string password)
        {
            // Først laver vi password om til et tal (ID)
            if (!int.TryParse(password, out int id))
            {
                return null;
            }

            // Nu finder det første medlem hvor både:
            // Fornavnet matcher username
            // ID Matcher password

            return _members.FirstOrDefault(m =>
               m.name.Split(' ')[0].Equals(username, StringComparison.OrdinalIgnoreCase)
               && m.id == id
            );
        }

    }

}