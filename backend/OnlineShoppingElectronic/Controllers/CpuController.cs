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
    [Route("api/Cpu")]
    [ApiController]

    public class CpuController : Controller
    {
        private readonly IConfiguration _configuration;
        private readonly IWebHostEnvironment _env;

        public CpuController(IConfiguration configuration, IWebHostEnvironment env)
        {
            _configuration = configuration;
            _env = env;
        }

        [HttpGet]
        public JsonResult Get()
        {
            string query = @"
                    select CpuId,CpuName,CpuBrand,CpuUnit,CpuPrice,CpuDescription,CpuPhoto from dbo.Cpu";
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
        public JsonResult Post(Cpu cp)
        {
            string query = @"
                   insert into dbo.Cpu(CpuName,CpuBrand,CpuUnit,CpuPrice,CpuDescription,CpuPhoto) 
             values(
             '" + cp.CpuName + @"'
             ,'" + cp.CpuBrand + @"'
             ,'" + cp.CpuUnit + @"'
             ,'" + cp.CpuPrice + @"'
             ,'" + cp.CpuDescription + @"'
             ,'" + cp.CpuPhoto + @"'

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
        public JsonResult Put(Cpu cp)
        {
            string query = @"
                   update dbo.Cpu set
                   CpuName ='" + cp.CpuName + @"'
                   ,CpuBrand ='" + cp.CpuBrand + @"'
                   ,CpuUnit ='" + cp.CpuUnit + @"'

                   ,CpuPrice ='" + cp.CpuPrice + @"'
                   ,CpuDescription ='" + cp.CpuDescription + @"'
                   ,CpuPhoto ='" + cp.CpuPhoto + @"'

                   where CpuId=" + cp.CpuId + @"
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
                   delete from dbo.Cpu
                  where CpuId =" + id + @"
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