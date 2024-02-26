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
    [Route("api/pccase")]
    [ApiController]
    public class PcCaseController : Controller
    {
        /* public IActionResult Index()
          {
              return View();
          }*/
        private readonly IConfiguration _configuration;
        private readonly IWebHostEnvironment _env;

        public PcCaseController(IConfiguration configuration, IWebHostEnvironment env)
        {
            _configuration = configuration;
            _env = env;
        }

        [HttpGet]
        public JsonResult Get()
        {
            string query = @"
                    select * from PcCase";
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
        public JsonResult Post(PcCase pcc)
        {
            string query = @"
                   insert into PcCase(PcCaseName, PcCaseBrand,PcCaseUnit, PcCasePrice, 
                    PcCaseDescription, PcCasePhoto) 
             values(
             '" + pcc.PcCaseName + @"'
             ,'" + pcc.PcCaseBrand + @"'
             ,'" + pcc.PcCaseUnit + @"'
             ,'" + pcc.PcCasePrice + @"'
             ,'" + pcc.PcCaseDescription + @"'
             ,'" + pcc.PcCasePhoto + @"'

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
        public JsonResult Put(PcCase pcc)
        {
            string query = @"
                   update PcCase set
                   PcCaseName ='" + pcc.PcCaseName + @"'
                   ,PcCaseBrand ='" + pcc.PcCaseBrand + @"'
                   ,PcCaseUnit ='" + pcc.PcCaseUnit + @"'
                   ,PcCasePrice ='" + pcc.PcCasePrice + @"'
                   ,PcCaseDescription ='" + pcc.PcCaseDescription + @"'
                   ,PcCasePhoto ='" + pcc.PcCasePhoto + @"'
                   

                   where PcCaseId=" + pcc.PcCaseId + @"
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
                   delete from PcCase 
                  where PcCaseId =" + id + @"
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
