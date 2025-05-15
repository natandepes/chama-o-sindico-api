using ChamaOSindico.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChamaOSindico.Application.DTOs
{
    public class ServiceCommentDTO
    {
        public string? CondominalServiceId { get; set; }
        public string? Comment { get; set; }
        public string? CommentByUserId { get; set; }
        public string? CommentByUserName { get; set; }
        public DateTime CreatedAt { get; set; }

        public ServiceComment Transform()
        {
            return new ServiceComment()
            {
                CondominalServiceId = CondominalServiceId,
                Comment = Comment,
                CommentByUserId = CommentByUserId,
                CommentByUserName = CommentByUserName,
                CreatedAt = CreatedAt
            };
        }

        public static ServiceCommentDTO TransformBack(ServiceComment serviceComment)
        {
            return new ServiceCommentDTO()
            {
                CondominalServiceId = serviceComment.CondominalServiceId,
                Comment = serviceComment.Comment,
                CommentByUserId = serviceComment.CommentByUserId,
                CommentByUserName = serviceComment.CommentByUserName,
                CreatedAt = serviceComment.CreatedAt
            };
        }
    }
}
