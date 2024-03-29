﻿using Microsoft.AspNetCore.Hosting;
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
    [Route("api/Headphones")]
    [ApiController]

    public class HeadphonesController : Controller
    {
        private readonly IConfiguration _configuration;
        private readonly IWebHostEnvironment _env;

        public HeadphonesController(IConfiguration configuration, IWebHostEnvironment env)
        {
            _configuration = configuration;
            _env = env;
        }

        [HttpGet]
        public JsonResult Get()
        {
            string query = @"
                    select  HeadphonesId, HeadphonesName, HeadphonesBrand,HeadphonesUnit, HeadphonesPrice, HeadphonesDescription, HeadphonesPhoto from dbo. Headphones";
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
        public JsonResult Post(Headphones he)
        {
            string query = @"
                   insert into dbo. Headphones(  HeadphonesName, HeadphonesBrand,HeadphonesUnit, HeadphonesPrice, HeadphonesDescription, HeadphonesPhoto) 
             values(
             '" + he.HeadphonesName + @"'
             ,'" + he.HeadphonesBrand + @"'
             ,'" + he.HeadphonesUnit + @"'
             ,'" + he.HeadphonesPrice + @"'
             ,'" + he.HeadphonesDescription + @"'
             ,'" + he.HeadphonesPhoto + @"'

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
        public JsonResult Put(Headphones he)
        {
            string query = @"
                   update dbo. Headphones set
                    HeadphonesName ='" + he.HeadphonesName + @"'
                   , HeadphonesBrand ='" + he.HeadphonesBrand + @"'
                   , HeadphonesUnit ='" + he.HeadphonesUnit + @"'

                   , HeadphonesPrice ='" + he.HeadphonesPrice + @"'
                   , HeadphonesDescription ='" + he.HeadphonesDescription + @"'
                   , HeadphonesPhoto ='" + he.HeadphonesPhoto + @"'

                   where  HeadphonesId=" + he.HeadphonesId + @"
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
                   delete from dbo. Headphones
                  where  HeadphonesId =" + id + @"
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