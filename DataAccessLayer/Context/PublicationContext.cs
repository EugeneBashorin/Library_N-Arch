using Entities.Entities;

namespace DataAccessLayer.Context
{
    public class PublicationContext //: IDisposable
    {
        public Book Books { get; set; }
        public Magazine Magazines { get; set; }
        public Newspaper Newspapers { get; set; }
        public string ConnectionString { get; set; }
        public PublicationContext(string connectionString)
        {
            ConnectionString = connectionString;
        }
    }
}