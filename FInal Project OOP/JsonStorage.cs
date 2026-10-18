using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace LibraryManagementSystem
{
    public class PhysicalBookDto
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public bool IsBorrowed { get; set; }
        public string ShelfLocation { get; set; }
    }

    public class EBookDto
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public bool IsBorrowed { get; set; }
        public string DownloadUrl { get; set; }
    }

    public class MemberDto
    {
        public int MemberId { get; set; }
        public string Name { get; set; }
    }

    public class LibraryDataDataTransferObject
    {
        public List<PhysicalBookDto> PhysicalBooks { get; set; } = new List<PhysicalBookDto>();
        public List<EBookDto> EBooks { get; set; } = new List<EBookDto>();
        public List<MemberDto> Members { get; set; } = new List<MemberDto>();
    }

    public class JsonStorage
    {
        private readonly string _filePath;

        public JsonStorage(string filePath = "library_data.json")
        {
            _filePath = filePath;
        }

        public void SaveData(LibraryManager manager)
        {
            LibraryDataDataTransferObject data = new LibraryDataDataTransferObject();

            foreach (var item in manager.Items)
            {
                if (item is PhysicalBook pb)
                {
                    data.PhysicalBooks.Add(new PhysicalBookDto
                    {
                        Id = pb.Id,
                        Title = pb.Title,
                        Author = pb.Author,
                        IsBorrowed = pb.IsBorrowed,
                        ShelfLocation = pb.ShelfLocation
                    });
                }
                else if (item is EBook eb)
                {
                    data.EBooks.Add(new EBookDto
                    {
                        Id = eb.Id,
                        Title = eb.Title,
                        Author = eb.Author,
                        IsBorrowed = eb.IsBorrowed,
                        DownloadUrl = eb.DownloadUrl
                    });
                }
            }

            foreach (var member in manager.Members)
            {
                data.Members.Add(new MemberDto
                {
                    MemberId = member.MemberId,
                    Name = member.Name
                });
            }

            string jsonString = JsonSerializer.Serialize(data, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(_filePath, jsonString);
        }

        public LibraryManager LoadData()
        {
            LibraryManager manager = new LibraryManager();

            if (!File.Exists(_filePath))
                return manager;

            string jsonString = File.ReadAllText(_filePath);
            LibraryDataDataTransferObject data = JsonSerializer.Deserialize<LibraryDataDataTransferObject>(jsonString);

            if (data == null)
                return manager;

            foreach (var pbDto in data.PhysicalBooks)
            {
                PhysicalBook pb = new PhysicalBook(pbDto.Id, pbDto.Title, pbDto.Author, pbDto.ShelfLocation);
                if (pbDto.IsBorrowed) pb.MarkAsBorrowed();
                manager.AddItem(pb);
            }

            foreach (var ebDto in data.EBooks)
            {
                EBook eb = new EBook(ebDto.Id, ebDto.Title, ebDto.Author, ebDto.DownloadUrl);
                if (ebDto.IsBorrowed) eb.MarkAsBorrowed();
                manager.AddItem(eb);
            }

            foreach (var mDto in data.Members)
            {
                manager.AddMember(new Member(mDto.MemberId, mDto.Name));
            }

            return manager;
        }
    }
}