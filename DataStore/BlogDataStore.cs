using DataStore.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Linq;

namespace DataStore
{
    public class BlogDataStore
    {
        const string UploadsFolder = "wwwroot\\Uploads";
        const string PostsFolder = "BlogFiles\\Posts";
        const string DraftsFolder = "BlogFiles\\Drafts";

        private static Object thisLock = new object();

        private readonly IFileSystem _fileSystem;

        public BlogDataStore(IFileSystem fileSystem)
        {
            _fileSystem = fileSystem;
            InitStorageFolders();
        }

        public void InitStorageFolders()
        {
            _fileSystem.CreateDirectory(PostsFolder);
            _fileSystem.CreateDirectory(DraftsFolder);
            _fileSystem.CreateDirectory(UploadsFolder);
        }
        private void SetId(BlogPost post)
        {
            if (post.Id == Guid.Empty)
            {
                post.Id = Guid.NewGuid();
            }
        }

        private static XElement GetCommentsRootNode(XDocument doc)
        {
            XElement commentsNode;
            if (doc.Root.Elements("Comments").Any())
            {
                commentsNode = doc.Root.Element("Comments");
            }
            else
            {
                commentsNode = new XElement("Comments");
                doc.Root.Add(commentsNode);
            }
            return commentsNode;
        }

        private XDocument LoadPostXml(string filePath)
        {
            string text = _fileSystem.ReadFileText(filePath);
            var reader = new StringReader(text);
            return XDocument.Load(reader);
        }

        public IEnumerable<XElement> GetCommentRoot(XDocument doc)
        {
            IEnumerable<XElement> commentRoot = doc.Root.Elements("Comments");
            return commentRoot;
        }

        public void AppendCommentInfo(Comment comment, BlogPost Post, XDocument doc)
        {
            XElement commentsNode = GetCommentsRootNode(doc);
            XElement commentNode = new XElement("Comment");
            commentNode.Add(new XElement("AuthorName", comment.AuthorName));
            commentNode.Add(new XElement("PubDate", comment.PubDate.ToString("o")));
            commentNode.Add(new XElement("CommentBody", comment.Body));
            commentNode.Add(new XElement("IsPublic", true));
            commentNode.Add(new XElement("UniqueId", comment.UniqueId));

            commentsNode.Add(commentNode);
        }

        public void IterateComments(IEnumerable<XElement> comments, List<Comment> listAllComments)
        {
            foreach (XElement comment in comments)
            {
                Comment newComment = new Comment
                {
                    AuthorName = comment.Element("AuthorName").Value,
                    Body = comment.Element("CommentBody").Value,
                    PubDate = DateTimeOffset.Parse(comment.Element("PubDate").Value),
                    IsPublic = Convert.ToBoolean(comment.Element("IsPublic").Value),
                    UniqueId = (Guid.Parse(comment.Element("UniqueId").Value)),

                };
                listAllComments.Add(newComment);
            }
        }


    }
}
