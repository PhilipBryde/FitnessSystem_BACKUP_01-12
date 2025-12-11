using System;
using System.Collections.Generic;
using System.Linq;
using FitnessProgram;

// --- BRUGES IKKE LÆNGERE ---
/*namespace FitnessProgram
{
    // Login-klassen står for at verificere brugere (Medlemmer og Administratorer).
    public class Login
    {
        private List<Member> _members; // Felt til at holde systemets liste af medlemmer.

        public Login(List<Member> members) // Constructor modtager listen af medlemmer.
        {
            // Kaster en fejl hvis listen er null ved oprettelse, for at sikre stabilitet.
            _members = members ?? throw new ArgumentNullException(nameof(members), "Medlemsliste må ikke være null ved initialisering.");
        }

        // Forsøger at logge en bruger ind.
        // Login sker via Medlemmets Fornavn (username) og ID (password).
        // Returnerer Member-objektet hvis successfuldt login, ellers returneres null.
        public Member? Authenticate(string username, string password)
        {
            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password)) // Tjekker for tomme eller whitespace-input.
            {
                return null;
            }

            // 1. Forsøg at konvertere password (som skal være ID) til et tal (int).
            if (!int.TryParse(password, out int id))
            {
                // Returnerer null, hvis password ikke er et gyldigt ID-nummer.
                return null;
            }

            // 2. Søg efter det første medlem (FirstOrDefault), der opfylder begge betingelser:
            // - Fornavn (første ord i m.name) matcher det indtastede brugernavn.
            // - Medlemmets ID matcher det konverterede password (id).
            var authenticatedMember = _members.FirstOrDefault(m =>
                m.name.Split(' ')[0].Equals(username, StringComparison.OrdinalIgnoreCase)
                && m.id == id);

            return authenticatedMember; // Returnerer det fundne medlem (eller null, hvis ingen match).
        }

    }
}*/