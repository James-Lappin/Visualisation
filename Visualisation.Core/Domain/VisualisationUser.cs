using System;

namespace Visualisation.Core.Domain
{
    public class VisualisationUser
    {
        public int Id { get; set; }
        public string Email { get; set; }

        public static VisualisationUser GetByEmailAndPassword(string email, string password)
        {
            throw new NotImplementedException();
        }

        public static VisualisationUser Create(string email, string password)
        {
            throw new NotImplementedException();
        }
    }
}