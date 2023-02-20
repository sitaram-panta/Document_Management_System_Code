using DMS.Web.Models;
using DMS.Web.viewmodel;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace DMS.Web.Mapper
{
    public static class DocumentMapper
    {
        public static DocumentViewModel ToViewModel(this Document document)
        {
            DocumentViewModel documentvm = new DocumentViewModel
            {
                DocumentId = document.DocumentId,
                Title = document.Title,
                Description = document.Description,
                Author = document.Author,
                Content = document.Content,
                FileName=document.FileName
            };

            return documentvm;

        }   

        public static List<DocumentViewModel> ToViewModel(this List<Document> documents)=>
            documents.Select(x=> x.ToViewModel()).ToList();


        public static Document ToModel(this DocumentViewModel documentViewModel) => new()
        {
            DocumentId = documentViewModel.DocumentId,
            Title = documentViewModel.Title,
            Description = documentViewModel.Description,    
             Author = documentViewModel.Author, 
             Content = documentViewModel.Content,
             FileName= documentViewModel.FileName

        };

        public static List<Document> ToModel(this List<DocumentViewModel> documentViewModels)=>
            documentViewModels.Select(x=> x.ToModel()).ToList();

    }
}
