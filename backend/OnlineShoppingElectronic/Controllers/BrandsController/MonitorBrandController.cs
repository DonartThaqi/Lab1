using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using OnlineShoppingElectronic.Models.Brands;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace OnlineShoppingElectronic.Controllers.BrandsController
{
    [Route("api/MonitorBrand")]
    [ApiController]
    public class MonitorBrandController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly IWebHostEnvironment _env;

        public MonitorBrandController(IConfiguration configuration, IWebHostEnvironment env)
        {
            _configuration = configuration;
            _env = env;
        }

        [HttpGet]
        public JsonResult Get()
        {
            string query = @"
                    select MonitorBrand from dbo.MonitorBrandData";
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
        public JsonResult Post(MonitorBrandData com)
        {
            string query = @"
                   insert into dbo.MonitorBrandData(MonitorBrandId,MonitorBrand) 
             values(
             '" + com.MonitorBrandId + @"',
             '" + com.MonitorBrand + @"'

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
        public JsonResult Put(MonitorBrandData com)
        {
            string query = @"
                   update dbo.MonitorBrandData set
                   MonitorBrand ='" + com.MonitorBrand + @"'
                   
                    
                   where MonitorBrandId=" + com.MonitorBrandId + @"
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
        [HttpDelete("{id}")]
        public JsonResult Delete(int id)
        {
            string query = @"
                   delete from dbo.MonitorBrandData
                  where MonitorBrandId =" + id + @"
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