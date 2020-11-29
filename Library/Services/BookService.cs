using AutoMapper;
using Library.Models;
using Library.Repository;
using Library.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Library.Services
{
    public class BookService : IBookService
    {
        private readonly IBookRepository _bookRepository;
        private readonly IMapper _mapper;

        public BookService(IBookRepository bookRepository, IMapper mapper)
        {
            _bookRepository = bookRepository ?? throw new ArgumentNullException(nameof(bookRepository));
            _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
        }
        public async Task<List<BookViewModel>> GetAllBooksAsync()
        {
            var bookList = await _bookRepository.ListAllBookAsync();
            return _mapper.Map<List<BookViewModel>>(bookList);
        }      
        public async Task AddBookAsync(BookViewModel bookViewModel)
        {
            var book =_mapper.Map<Book>(bookViewModel);
            await _bookRepository.AddBookAsync(book);
        }
        public async Task<BookViewModel> GetByIdAsync(int id)
        {
            var book = await _bookRepository.GetByIdAsync(id);            
            return _mapper.Map<BookViewModel>(book);
        }
        public async Task UpdateBookAsync(BookViewModel bookViewModel)
        {
            var book = _mapper.Map<Book>(bookViewModel);
            await _bookRepository.UpdateBookAsync(book);
        }
    }
}
