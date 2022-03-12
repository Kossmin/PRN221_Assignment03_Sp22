using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using PRN221_Assignment03_TranPhamKimSon_SE151317.Models;
using PRN221_Assignment03_TranPhamKimSon_SE151317.Views.Post;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PRN221_Assignment03_TranPhamKimSon_SE151317.SignalRHub
{
    public class NotiHub: Hub
    {
        SignalRAssignmentDB03Context _context = new SignalRAssignmentDB03Context();
        public async Task SendNoti(string type, string createdDate, string updatedDate,string title, string content, string status, string authorId, string categoryId, string postId)
        {
                var post = new Post
                {
                    PostId = int.Parse(postId),
                    CreatedDate = DateTime.Parse(createdDate),
                    UpdatedDate = DateTime.Parse(updatedDate),
                    Title = title,
                    Content = content,
                    PublishStatus = status == "true" ? true : false,
                    AuthorId = int.Parse(authorId),
                    CategoryId = int.Parse(categoryId),
                };
                _context.Add(post);
                await _context.SaveChangesAsync();
                await Clients.All.SendAsync("RecieveNotiAdd", type ,createdDate, updatedDate, title, content, status, authorId, categoryId, post.PostId);
         
        }

        public async Task SendNotiDelete(string postId)
        {
            await Clients.All.SendAsync("RecieveNotiDelete", postId);
        }

        public async Task SendNotiUpdate(string createdDate, string updatedDate, string title, string content, string status, string authorId, string categoryId, string postId)
        {
            await Clients.All.SendAsync("RecieveNotiUpdate", createdDate, updatedDate, title, content, status, authorId, categoryId, postId);
        }
    }
}
