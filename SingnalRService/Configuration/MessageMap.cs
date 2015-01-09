using SingnalRService.DomainModel;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SingnalRService.Configuration
{
    public class MessageMap :EntityTypeConfiguration<Message>
    {
        public MessageMap()
        {
            this.HasKey(p => p.Id);
        }
    }
}
