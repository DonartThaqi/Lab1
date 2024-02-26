using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using OnlineShoppingElectronic.Models;


namespace OnlineShoppingElectronic.Controllers
{
    [Route("api/motherboard")]
    [ApiController]
    public class MotherboardController : Controller
    {
        /*public IActionResult Index()
        {
            return View();
        }*/
        private readonly IConfiguration _configuration;
        private readonly IWebHostEnvironment _env;

        public MotherboardController(IConfiguration configuration, IWebHostEnvironment env)
        {
            _configuration = configuration;
            _env = env;
        }

        [HttpGet]
        public JsonResult Get()
        {
            string query = @"
                    select * from Motherboard";
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
        public JsonResult Post(Motherboard mtb)
        {
            string query = @"
                   insert into Motherboard( MotherboardName, MotherboardBrand,MotherboardUnit, MotherboardPrice, 
                    MotherboardDescription, MotherboardPhoto) 
             values(
             '" + mtb.MotherboardName + @"'
             ,'" + mtb.MotherboardBrand + @"'
             ,'" + mtb.MotherboardUnit + @"'
             ,'" + mtb.MotherboardPrice + @"'
             ,'" + mtb.MotherboardDescription + @"'
             ,'" + mtb.MotherboardPhoto + @"'

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
        public JsonResult Put(Motherboard mtb)
        {
            string query = @"
                   update Motherboard set
                   MotherboardName ='" + mtb.MotherboardName + @"'
                   ,MotherboardBrand ='" + mtb.MotherboardBrand + @"'
                   ,MotherboardUnit ='" + mtb.MotherboardUnit + @"'
                   ,MotherboardPrice ='" + mtb.MotherboardPrice + @"'
                   ,MotherboardDescription ='" + mtb.MotherboardDescription + @"'
                   ,MotherboardPhoto ='" + mtb.MotherboardPhoto + @"'
                   

                   where MotherboardId=" + mtb.MotherboardId + @"
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

                return new JsonResult("foto.png");
            }
        }

        [HttpDelete("{id}")]
        public JsonResult Delete(int id)
        {
            string query = @"
                   delete from Motherboard 
                  where MotherboardId =" + id + @"
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
