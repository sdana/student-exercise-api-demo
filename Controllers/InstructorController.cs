﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System.Data;
using System.Data.SqlClient;
using Dapper;
using StudentExercises.Models;
using Microsoft.AspNetCore.Http;

namespace StudentExercises.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InstructorController : ControllerBase
    {
        private readonly IConfiguration _config;

        public InstructorController(IConfiguration config)
        {
            _config = config;
        }

        public IDbConnection Connection
        {
            get
            {
                return new SqlConnection(_config.GetConnectionString("DefaultConnection"));
            }
        }

        // GET api/instructor
        [HttpGet]
        public async Task<IActionResult> Get(string firstName, string lastName)
        {
            using (IDbConnection conn = Connection)
            {
                string sql = "SELECT * FROM Instructor WHERE 1=1";

                if (firstName != null)
                {
                    sql += $" AND FirstName='{firstName}'";
                }
                else if (lastName != null)
                {
                    sql += $" AND LastName='{lastName}'";
                }

                var allInstructors = await conn.QueryAsync<Instructor>(sql);
                return Ok(allInstructors);
            }

        }

        //// GET api/values/5
        //[HttpGet("{id}", Name = "GetExercise")]
        //public async Task<IActionResult> Get([FromRoute] int id)
        //{
        //    using (IDbConnection conn = Connection)
        //    {
        //        string sql = $"SELECT * FROM Exercise WHERE Id = {id}";

        //        var theSingleExercise = (await conn.QueryAsync<Exercise>(sql)).Single();
        //        return Ok(theSingleExercise);
        //    }
        //}

        //// POST api/values
        //[HttpPost]
        //public async Task<IActionResult> Post([FromBody] Exercise exercise)
        //{
        //    string sql = $@"INSERT INTO Exercise
        //    (Name, Language)
        //    VALUES
        //    ('{exercise.Name}', '{exercise.Language}');
        //    select MAX(Id) from Exercise";

        //    using (IDbConnection conn = Connection)
        //    {
        //        var newExerciseId = (await conn.QueryAsync<int>(sql)).Single();
        //        exercise.Id = newExerciseId;
        //        return CreatedAtRoute("GetExercise", new { id = newExerciseId }, exercise);
        //    }

        //}

        //// PUT api/values/5
        //[HttpPut("{id}")]
        //public async Task<IActionResult> Put([FromRoute] int id, [FromBody] Exercise exercise)
        //{
        //    string sql = $@"
        //    UPDATE Exercise
        //    SET Name = '{exercise.Name}',
        //        Language = '{exercise.Language}'
        //    WHERE Id = {id}";

        //    try
        //    {
        //        using (IDbConnection conn = Connection)
        //        {
        //            int rowsAffected = await conn.ExecuteAsync(sql);
        //            if (rowsAffected > 0)
        //            {
        //                return new StatusCodeResult(StatusCodes.Status204NoContent);
        //            }
        //            throw new Exception("No rows affected");
        //        }
        //    }
        //    catch (Exception)
        //    {
        //        if (!ExerciseExists(id))
        //        {
        //            return NotFound();
        //        }
        //        else
        //        {
        //            throw;
        //        }
        //    }
        //}

        //// DELETE api/values/5
        //[HttpDelete("{id}")]
        //public async Task<IActionResult> Delete([FromRoute] int id)
        //{
        //    string sql = $@"DELETE FROM Exercise WHERE Id = {id}";

        //    using (IDbConnection conn = Connection)
        //    {
        //        int rowsAffected = await conn.ExecuteAsync(sql);
        //        if (rowsAffected > 0)
        //        {
        //            return new StatusCodeResult(StatusCodes.Status204NoContent);
        //        }
        //        throw new Exception("No rows affected");
        //    }

        //}

        //private bool ExerciseExists(int id)
        //{
        //    string sql = $"SELECT Id, Name, Language FROM Exercise WHERE Id = {id}";
        //    using (IDbConnection conn = Connection)
        //    {
        //        return conn.Query<Exercise>(sql).Count() > 0;
        //    }
        //}
    }
}
