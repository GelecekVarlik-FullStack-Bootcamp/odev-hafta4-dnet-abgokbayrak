using System.Collections.Generic;

namespace BMS.Entity.Statics
{
    public class AuthorityTypes
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public static List<AuthorityTypes> GetAuthorityTypes()
        {
            List<AuthorityTypes> list = new List<AuthorityTypes>
            {
                new AuthorityTypes {Id = 1, Name = "Yönetici"},
                new AuthorityTypes {Id = 2, Name = "Admin"},
                new AuthorityTypes {Id = 3, Name = "Personel"}
            };
            return list;
        }
    }
}
