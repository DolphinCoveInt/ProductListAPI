using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProductListAPI.Model;
using Microsoft.EntityFrameworkCore;
using ProductListAPI.Data;


namespace ProductListAPI.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class InventoryController : ControllerBase
    {
        //Create context connection to Database
        private readonly ProductDBContext _ctx;

        //Constructor for Dependancy Injection to connect to DB
        public InventoryController(ProductDBContext context) => _ctx = context;


        //Product EndPoint
        //Get All Product
        [HttpGet]
        public IActionResult GetProduct()
        {
            //Include Models, without specification that is unique to the front-end
            var pItems = _ctx.Products.Include(b => b.Category).Include(b => b.Size).ToList();
            if (pItems == null)
            {
                return BadRequest();
            }

            return Ok(pItems);
        }

        //Get Product by ID
        [HttpGet("{id}")]
        public IActionResult GetProductById(int id)
        {
            //Fetches a record where the Id matches the results of the query
            var pItem = _ctx.Products.Include(b => b.Category).Include(b => b.Size).FirstOrDefault(x => x.Id == id);
            if (pItem == null)
            {
                return NotFound();
            }

            return Ok(pItem);
        }

        //Post All Product
        [HttpPost]
        //[FromBody] tells the program to expects a product that's coming from the body of the request
        public IActionResult CreateProduct([FromBody] Product item)
        {
            _ctx.Products.Add(item);
            _ctx.SaveChanges();

            //Fetches the most recent created record and adds and Id then display the info
            return CreatedAtAction(nameof(GetProductById), new { id = item.Id }, item);
        }

        //Put Changes to a Product based on it's ID
        [HttpPut("{id}")]
        public IActionResult UpdateProduct(int id, [FromBody] Product item)
        {
            if (id != item.Id)
            {
                return BadRequest();
            }

            _ctx.Products.Update(item);
            _ctx.SaveChanges();

            return CreatedAtAction(nameof(GetProductById), new { id = item.Id }, item);
        }


        #region Category Endpoint
        //Category Endpoint
        [HttpGet]
        [Route("Category")]
        public IActionResult GetCategory()
        {
            var pCategory = _ctx.Categories.ToList();
            if (pCategory == null)
            {
                return BadRequest();
            }

            return Ok(pCategory);
        }

        [HttpGet]
        [Route("Category/{Id}")]
        public IActionResult GetCategoryById(int id)
        {
            var pCategory = _ctx.Categories.FirstOrDefault(x => x.Id == id);
            if (pCategory == null)
            {
                return NotFound();
            }

            return Ok(pCategory);
        }

        [HttpPost]
        [Route("Category")]
        public IActionResult CreateCategory([FromBody] Category item)
        {
            _ctx.Categories.Add(item);
            _ctx.SaveChanges();

            return CreatedAtAction(nameof(GetCategoryById), new { id = item.Id }, item);
        }

        [HttpPut("Category/{Id}")]
        public IActionResult UpdateCategory(int id, [FromBody] Category item)
        {
            //var pItem = _ctx.Category.FirstOrDefault(x => x.Id == id);
            if (id != item.Id)
            {
                return BadRequest();
            }

            _ctx.Categories.Update(item);
            _ctx.SaveChanges();

            return CreatedAtAction(nameof(GetCategoryById), new { id = item.Id }, item);
        }
        #endregion


        #region Size Endpoint
        //Size Endpoint
        [HttpGet]
        [Route("Size")]
        public IActionResult GetSize()
        {
            var Size = _ctx.Sizes.ToList();
            if (Size == null)
            {
                return BadRequest();
            }

            return Ok(Size);
        }

        [HttpGet]
        [Route("Size/{id}")]
        public IActionResult GetSizeById(int id)
        {
            var size = _ctx.Sizes.FirstOrDefault(x => x.Id == id);
            if (size == null)
            {
                return NotFound();
            }

            return Ok(size);
        }

        [HttpPost]
        [Route("Size")]
        public IActionResult CreateSize([FromBody] Size item)
        {
            _ctx.Sizes.Add(item);
            _ctx.SaveChanges();

            return CreatedAtAction(nameof(GetSizeById), new { id = item.Id }, item);
        }

        [HttpPut]
        [Route("Size/{id}")]
        public IActionResult UpdateSize(int id, [FromBody] Size item)
        {
            if (id != item.Id)
            {
                return BadRequest();
            }

            _ctx.Sizes.Update(item);
            _ctx.SaveChanges();

            return CreatedAtAction(nameof(GetSizeById), new { id = item.Id }, item);
        }
        #endregion
    }
}
