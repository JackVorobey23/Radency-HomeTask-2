using AutoMapper;
using HomeTask2.DatabaseContext;
using HomeTask2.DataModel;
using HomeTask2.Dto;
using HomeTask2.Interfaces;
using HomeTask2.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;
namespace HomeTask2.Controllers
{
    [ApiController]
    [Route("api")]
    public class BookController : ControllerBase
    {
        private readonly IRepository<Book> _bookRepository;

        private readonly IMapper _mapper;

        private readonly IConfiguration _config;

        public BookController(IRepository<Book> repository, IMapper mapper, IConfiguration configuration)
        {
            _bookRepository = repository;
            _mapper = mapper;
            _config = configuration;
        }


        [HttpGet("books")]
        public async Task<ActionResult> GetAllBooks([FromQuery(Name = "order")] string order)
        {
            IEnumerable<Book> books = await _bookRepository.GetAll();

            switch (order.ToLower())
            {
                case "author":
                    books = books.OrderBy(b => b.Author);
                    break;

                case "title":
                    books = books.OrderBy(b => b.Title);
                    break;
                default:
                    return NotFound();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            return Ok(books.Select(_mapper.Map<Book, BookDto>));
        }


        [HttpGet("books/{id:int}")]
        public async Task<ActionResult> GetBookById(int id)
        {
            Book? book = await _bookRepository.FindById(id);

            if(book is not null)
                return Ok(_mapper.Map<Book, BookWithReviewsDto>(book));

            return NotFound();
        }


        [HttpGet("books/recommended")]
        public async Task<ActionResult> GetTopTenBooks([FromQuery(Name = "genre")] string genre)
        {
            IEnumerable<Book> books = await _bookRepository.GetAll();
            var getRating = (Book b) =>
            {
                if (b.Ratings is not null)
                    return b.Ratings.Sum(r => r.Score) / b.Ratings.Count;

                else
                    return 1;
            };
            return Ok(books
                .Where(b => b.Genre.ToLower() == genre.ToLower() && b.Reviews.Count >= 10)
                .OrderBy(b => getRating(b))
                .Take(10).Select(_mapper.Map<Book, BookDto>));
        }


        [HttpDelete("books/{id:int}")]
        public async Task<ActionResult> RemoveBookById(int id, [FromQuery(Name = "key")] string secret)
        {
            if (_config.GetValue<string>("SecretKey") != secret) 
                return BadRequest("incorrect key ");

            Book? book = await _bookRepository.FindById(id);

            if (book is not null)
                return Ok(_bookRepository.Remove(book));

            return NotFound();
        }


        [HttpPost("books/save")]
        public async Task<ActionResult> SaveNewBook([FromBody] BookSaveDto bookSaveDto)
        {
            Book book = _mapper.Map<BookSaveDto, Book>(bookSaveDto);

            if (!bookSaveDto.Id.HasValue)
            {
                var added = await _bookRepository.Add(book);

                return Ok(_mapper.Map<Book, IdDto>(added));
            }
            else
            {
                try
                {
                    return Ok(await _bookRepository.Update(book));
                }
                catch (DbUpdateConcurrencyException)
                {
                    return NotFound();
                }
                catch (Exception ex)
                {
                    return BadRequest(ex.Message);
                }
            }
        }


        [HttpPut("books/{id:int}/review")]
        public async Task<ActionResult> SaveReview(int id, [FromBody] SaveReviewDto saveReviewDto)
        {
            Review review = _mapper.Map<Review>(saveReviewDto);

            var book = await _bookRepository.FindById(id);

            book.Reviews.Add(review);

            await _bookRepository.Update(book);

            return Ok(_mapper.Map<Review,IdDto>(review));
        }


        [HttpPut("books/{id:int}/rate")]
        public async Task RateBook(int id, [FromQuery] RateDto dto)
        {
            var book = await _bookRepository.FindById(id);

            book.Ratings.Add( _mapper.Map<Rating>(dto));

            await _bookRepository.Update(book);
        }
    }
}
