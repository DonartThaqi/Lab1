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
    [Route("api/ram")]
    [ApiController]
    public class RamController : Controller
    {
        private readonly IConfiguration _configuration;
        private readonly IWebHostEnvironment _env;

        public RamController(IConfiguration configuration, IWebHostEnvironment env)
        {
            _configuration = configuration;
            _env = env;
        }

        [HttpGet]
        public JsonResult Get()
        {
            string query = @"
                    select * from Ram";
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
        public JsonResult Post(Ram ram)
        {
            string query = @"
                   insert into Ram( RamName, RamBrand,RamUnit, RamPrice, 
                    RamDescription, RamPhoto) 
             values(
             '" + ram.RamName + @"'
             ,'" + ram.RamBrand + @"'
             ,'" + ram.RamUnit + @"'
             ,'" + ram.RamPrice + @"'
             ,'" + ram.RamDescription + @"'
             ,'" + ram.RamPhoto + @"'

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
        public JsonResult Put(Ram ram)
        {
            string query = @"
                   update Ram set
                   RamName ='" + ram.RamName + @"'
                   ,RamBrand ='" + ram.RamBrand + @"'
                   ,RamUnit ='" + ram.RamUnit + @"'
                   ,RamPrice ='" + ram.RamPrice + @"'
                   ,RamDescription ='" + ram.RamDescription + @"'
                   ,RamPhoto ='" + ram.RamPhoto + @"'
                   

                   where RamId=" + ram.RamId + @"
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
                   delete from Ram 
                  where RamId =" + id + @"
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
