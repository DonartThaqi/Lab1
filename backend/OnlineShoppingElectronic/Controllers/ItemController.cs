using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using OnlineShoppingElectronic.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineShoppingElectronic.Controllers
{
    [Route("api/Item")]
    [ApiController]
    public class ItemController : Controller
    {
        private readonly IConfiguration _configuration;
        private readonly IWebHostEnvironment _env;

        public ItemController(IConfiguration configuration, IWebHostEnvironment env)
        {
            _configuration = configuration;
            _env = env;
        }

        [HttpGet]
        public JsonResult Get()
        {
            string query = @"
                    select ItemId,ItemForId,ItemTag,ItemName,ItemBrand,ItemUnit,ItemPrice,ItemDescription,ItemPhoto from dbo.Item";
            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("ProduktetCon");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader); ;

                    myReader.Close();
                    myCon.Close();
                }
            }

            return new JsonResult(table);
        }
        [HttpPost]
        public JsonResult Post(Item Ite)
        {
            string query = @"
                   insert into dbo.Item(ItemForId,ItemTag,ItemName,ItemBrand,ItemUnit,ItemPrice,ItemDescription,ItemPhoto)
             values(
             '" + Ite.ItemForId + @"'
             ,'" + Ite.ItemTag + @"'
             ,'" + Ite.ItemName + @"'
             ,'" + Ite.ItemBrand + @"'
             ,'" + Ite.ItemUnit + @"'
             ,'" + Ite.ItemPrice + @"'
             ,'" + Ite.ItemDescription + @"'
             ,'" + Ite.ItemPhoto + @"'

                                                    ) ";
            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("ProduktetCon");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader); ;

                    myReader.Close();
                    myCon.Close();

                }
            }

            return new JsonResult("U shtua me sukses");
        }
        [HttpPut]
        public JsonResult Put(Item It)
        {
            string query = @"
                   update dbo.Item set
                   ,ItemTag='" + It.ItemForId + @"'
                    ItemTag='" + It.ItemTag + @"'
                   ,ItemName ='" + It.ItemName + @"'
                   ,ItemBrand ='" + It.ItemBrand + @"'
                   ,ItemUnit ='" + It.ItemUnit + @"'
                   ,ItemPrice ='" + It.ItemPrice + @"'
                   ,ItemDescription ='" + It.ItemDescription + @"'
                   ,ItemPhoto ='" + It.ItemPhoto + @"'

                   where ItemId=" + It.ItemId + @"
                   ";
            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("ProduktetCon");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader); ;

                    myReader.Close();
                    myCon.Close();
                }
            }

            return new JsonResult("Kjo pjes eshte bere UPDATE me sukses");
        }
        [Route("SaveFile")]
        [HttpPost]
        public JsonResult SaveFile()
        {
            try
            {
                var httpRequest = Request.Form;
                var postedFile = httpRequest.Files[0];
                string filename = postedFile.FileName;
                var physicalPath = _env.ContentRootPath + "/Photos/" + filename;

                using (var stream = new FileStream(physicalPath, FileMode.Create))
                {
                    postedFile.CopyTo(stream);
                }

                return new JsonResult(filename);
            }
            catch (Exception)
            {

                return new JsonResult("fotoLenovo.png");
            }
        }
        [HttpDelete("{id}")]
        public JsonResult Delete(int id)
        {
            string query = @"
                   delete from dbo.Item
                  where ItemId =" + id + @"
                   ";
            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("ProduktetCon");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader); ;

                    myReader.Close();
                    myCon.Close();
                }
            }

            return new JsonResult("Kjo pjes eshte bere Delete me sukses");
        }
    }
}
