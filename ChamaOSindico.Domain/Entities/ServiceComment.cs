using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChamaOSindico.Domain.Entities
{
    public class ServiceComment : BaseEntity
    {
        public string CondominalServiceId { get; set; }
        public string Comment { get; set; }
        public string CommentByUserId { get; set; }
        public string CommentByUserName { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
