using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LibApp.Models;
using LibApp.ViewModels;
using LibApp.Data;
using Microsoft.EntityFrameworkCore;
using LibApp.Interfaces;

namespace LibApp.Controllers
{
    public class BooksController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IBookRepository _bookRepository;

        public BooksController(ApplicationDbContext context, IBookRepository bookRepository)
        {
            _context = context;
            _bookRepository = bookRepository;
        }

        public IActionResult Index()
        {
            var books = _bookRepository.GetBooks()
                .ToList();

            return View(books);
        }

        public IActionResult Details(int id)
        {
            var book = _context.Books
                .Include(b => b.Genre)
                .SingleOrDefault(b => b.Id == id);

            return View(book);
        }

        public IActionResult Edit(int id)
        {
            var book = _context.Books.SingleOrDefault(b => b.Id == id);
            if (book == null)
            {
                return NotFound();
            }

            var viewModel = new BookFormViewModel
            {
                Book = book,
                Genres = _context.Genre.ToList()
            };

            return View("BookForm", viewModel);
        }

        public IActionResult New()
        {
            var viewModel = new BookFormViewModel
            {
                Genres = _context.Genre.ToList()
            };

            return View("BookForm", viewModel);
        }

        [HttpGet]
        [Route("api/allbooks")]
        public IList<Book> GetBooks()
        {
            return _bookRepository.GetBooks().ToList();
        }

        [HttpPost]
        [Route("api/books/add")]
        public IActionResult AddBook(Book book)
        {
            try
            {
                _bookRepository.UpdateBook(book);
                return Ok(book);
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

        [HttpDelete("{BookId}")]
        [Route("api/books/delete")]
        public IActionResult DeleteBook(int BookId)
        {
            try
            {
                _bookRepository.DeleteBook(BookId);
                return Ok(BookId);
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

        [HttpPut]
        [Route("api/books/upgrade")]
        public IActionResult UpgradeBook(Book book)
        {
            try
            {
                _bookRepository.UpdateBook(book);
                return Ok(book);
            }
            catch (Exception e)
            {
                return BadRequest(e);
            }
        }

    }
}
